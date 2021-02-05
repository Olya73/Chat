using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Storage.Interface
{
    public interface IRepository<T, TId>
    {
        Task<bool> DeleteAsync(TId id);
        Task<T> GetAsync(TId id);
        Task<IEnumerable<T>> GetAllAsync();
        void Add(T obj);
        T Update(T obj);
    }
}
