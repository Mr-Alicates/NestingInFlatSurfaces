using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence
{
    public class InMemoryPersistenceService : IPersistenceService
    {
        private Dictionary<string, IPersistible> _persistibles = new Dictionary<string, IPersistible>();

        public T AddOrUpdate<T>(string key, T entity) where T : IPersistible
        {
            if (string.IsNullOrEmpty(key))
            {
                key = Guid.NewGuid().ToString();
            }

            if (_persistibles.ContainsKey(key))
            {
                _persistibles[key] = entity;
            }
            else
            {
                _persistibles.Add(key, entity);
            }

            return entity;
        }

        public int Count<T>() where T : IPersistible
        {
            return _persistibles.Count();
        }

        public bool Delete<T>(string key) where T : IPersistible
        {
            if (_persistibles.ContainsKey(key))
            {
                _persistibles.Remove(key);
                return true;
            }
            else
            {
                return false;
            }
        }

        public T Lookup<T>(string key) where T : IPersistible
        {
            if (_persistibles.ContainsKey(key))
            {
                return (T) _persistibles[key];
            }
            else
            {
                return default(T);
            }
        }

        public void PreInitPersistence<T>() where T : IPersistible
        {

        }

        public List<T> Query<T>(int pageNumber, int itemsPerPage) where T : IPersistible
        {
            return _persistibles.Values
                .OfType<T>()
                .Skip((pageNumber - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToList();
        }

        public Task<List<T>> QueryAsync<T>(int pageNumber, int itemsPerPage) where T : IPersistible
        {
            return Task.Run(() => Query<T>(pageNumber, itemsPerPage));
        }
    }
}
