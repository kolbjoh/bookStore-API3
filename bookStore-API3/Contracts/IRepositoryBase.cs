using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookStore_API3.Contracts
{
    /// <summary>
    ///  tasks that will be used on all interfaces, inhertitated from this one "RepositoryBase"
    /// </summary>

    public interface IRepositoryBase<T> where T: class
    {
        Task<IList<T>> FindAll();
        Task<T> FindById(int id);
        Task<bool> Create(T entity);
        Task<bool> isExists(int id);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Save();

    }
}
