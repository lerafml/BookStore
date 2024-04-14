using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public interface IRepository
    {
        List<BOOK> GetBooks();
        void AddBook(BOOK item);
        void EditBook(BOOK item);
        void DeleteBook(BOOK item);

        List<USER> GetUsers();
        void AddUser(USER item);
        void EditUser(USER item);
        void DeleteUser(USER item);

        bool HasTheBook(string login, long id);
        void TakeABook(string login, long id);
        void ReturnABook(string login, long id);
    }
}
