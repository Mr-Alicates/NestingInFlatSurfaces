using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Interfaces;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;

namespace Core.Persistence
{
    public class RavenDBPersistenceService : IPersistenceService
    {
        private static Dictionary<string, IDocumentStore> DocumentStores = new Dictionary<string, IDocumentStore>();
        private static Dictionary<string, bool> IsDocumentStoreInitialized = new Dictionary<string, bool>();
        private static object lockObject = new object();

        private static IDocumentStore GetDocumentStore<T>()
        {
            string name = typeof (T).Name;

            if (DocumentStores.ContainsKey(name))
            {
                return DocumentStores[name];
            }
            
            lock (lockObject)
            {

                if (DocumentStores.ContainsKey(name))
                {
                    return DocumentStores[name];
                }


                IDocumentStore documentStore = new EmbeddableDocumentStore
                {
                    DataDirectory = "Persistence\\" + name
                };

                IsDocumentStoreInitialized.Add(name, false);
                DocumentStores.Add(name, documentStore);

                Task.Run(() =>
                {
                    try
                    {
                        documentStore.Initialize();
                        IsDocumentStoreInitialized[name] = true;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error initializing persistence for type: " + typeof(T).Name + " ", ex);
                    }
                });

                return documentStore;
            }

        }

        private static void AwaitToBeInitialized<T>()
        {
            string name = typeof (T).Name;

            if (IsDocumentStoreInitialized.ContainsKey(name))
            {
                while (!IsDocumentStoreInitialized[name])
                {
                    //Await

                    Thread.Sleep(100);
                }

            }
        }


        private static async Task AwaitToBeInitializedAsync<T>()
        {
            string name = typeof(T).Name;

            if (IsDocumentStoreInitialized.ContainsKey(name))
            {
                await Task.Run(() =>
                {

                    while (!IsDocumentStoreInitialized[name])
                    {
                        //Await

                        Thread.Sleep(100);
                    }
                });
            }
        }


        public async void PreInitPersistence<T>() where T : IPersistible
        {
            await Task.Run(() =>
            {
                GetDocumentStore<T>();
                AwaitToBeInitialized<T>();
            });
        }


        public T AddOrUpdate<T>(string key, T entity) where T : IPersistible
        {
            IDocumentStore database = GetDocumentStore<T>();
            AwaitToBeInitialized<T>();

            using (IDocumentSession session = database.OpenSession())
            {
                session.Store(entity, entity.Id);
                session.SaveChanges();
            }

            return entity;
        }

        public T Lookup<T>(string key) where T : IPersistible
        {
            IDocumentStore database = GetDocumentStore<T>();
            AwaitToBeInitialized<T>();
            
            T result;

            using (IDocumentSession session = database.OpenSession())
            {
                result = session.Load<T>(key);
            }

            return result;
        }

        public bool Delete<T>(string key) where T : IPersistible
        {
            IDocumentStore database = GetDocumentStore<T>();
            AwaitToBeInitialized<T>();
            
            using (IDocumentSession session = database.OpenSession())
            {
                T entity = session.Load<T>(key);
                if (entity == null)
                {
                    return false;
                }
                session.Delete<T>(entity);
                session.SaveChanges();
            }

            return true;
        }

        public int Count<T>() where T : IPersistible
        {
            IDocumentStore database = GetDocumentStore<T>();
            AwaitToBeInitialized<T>();
            
            int result = 0;

            using (IDocumentSession session = database.OpenSession())
            {
                result = session.Query<T>().Count();
            }
            return result;
        }

        public List<T> Query<T>(int pageNumber, int itemsPerPage) where T : IPersistible
        {
            IDocumentStore database = GetDocumentStore<T>();
            AwaitToBeInitialized<T>();
            
            List<T> result;

            int skip = 0;

            if (pageNumber > 0)
            {
                skip = (pageNumber - 1) * itemsPerPage;
            }

            using (IDocumentSession session = database.OpenSession())
            {
                result = session.Query<T>().Skip(skip).Take(itemsPerPage).ToList();
            }

            return result;
        }

        public async Task<List<T>> QueryAsync<T>(int pageNumber, int itemsPerPage) where T : IPersistible
        {
            IDocumentStore database = GetDocumentStore<T>();
            await AwaitToBeInitializedAsync<T>();

            List<T> result;

            int skip = 0;

            if (pageNumber > 0)
            {
                skip = (pageNumber - 1) * itemsPerPage;
            }

            using (IDocumentSession session = database.OpenSession())
            {
                result = session.Query<T>().Skip(skip).Take(itemsPerPage).ToList();
            }

            return result;
        }
    }
}
