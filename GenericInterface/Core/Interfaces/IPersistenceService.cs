using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;

namespace Core.Interfaces
{
    public interface IPersistenceService
    {
        void PreInitPersistence<T>() where T : IPersistible;

        T AddOrUpdate<T>(string key, T entity) where T : IPersistible;

        T Lookup<T>(string key) where T : IPersistible;

        bool Delete<T>(string key) where T : IPersistible;

        int Count<T>() where T : IPersistible;

        List<T> Query<T>(int pageNumber, int itemsPerPage) where T : IPersistible;

        Task<List<T>> QueryAsync<T>(int pageNumber, int itemsPerPage) where T : IPersistible;
    }
}
