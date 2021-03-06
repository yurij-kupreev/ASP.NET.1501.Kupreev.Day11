﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookService
{
    public interface IBookRepository
    {
        IEnumerable<Book> LoadBooks();
        void SaveBooks(IEnumerable<Book> books);
    } 

}
