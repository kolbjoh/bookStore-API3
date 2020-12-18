using bookStore_API3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookStore_API3.Contracts
{
    /// <summary>
    /// tasks beeing used spesifically for Author, inclduing tasks frome IRepositoryBase, Servicese inherit from the contract
    /// </summary>

    public interface IAuthorRepository : IRepositoryBase<Author>
    {
    }
}
