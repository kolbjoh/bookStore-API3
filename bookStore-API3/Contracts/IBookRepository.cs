﻿using bookStore_API3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookStore_API3.Contracts
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
    }
}
