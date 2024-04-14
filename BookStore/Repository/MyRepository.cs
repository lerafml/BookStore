using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BookStore.Repository
{
    public class MyRepository : IRepository
    {
        string connectionString = "Server=LAPTOP-M4U16T3P;Database=BookStore;Trusted_Connection=True;";

        public List<BOOK> GetBooks()
        {
            List<BOOK> result = new List<BOOK>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM BOOKS", conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        long id = reader.GetInt64(0);
                        string name = reader.GetString(1);
                        string caption = reader.GetString(2);
                        string author = reader.GetString(3);
                        int count = reader.GetInt32(4);
                        result.Add(new BOOK
                        {
                            ID = id,
                            NAME = name,
                            CAPTION = caption,
                            AUTHOR = author,
                            COUNT = count
                        });
                    }
                }
            }
            return result;
        }

        public void AddBook(BOOK item)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string expression = string.Format("INSERT INTO BOOKS (NAME,CAPTION,AUTHOR,COUNT) VALUES('{0}','{1}','{2}',{3})", item.NAME, item.CAPTION, item.AUTHOR, item.COUNT);
                conn.Open();
                SqlCommand command = new SqlCommand(expression, conn);
                command.ExecuteNonQuery();
            }
        }
        public void EditBook(BOOK item)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string expression = string.Format("UPDATE BOOKS SET NAME = '{0}',CAPTION = '{1}',AUTHOR = '{2}',COUNT = '{3}' WHERE ID = {4}", item.NAME, item.CAPTION, item.AUTHOR, item.COUNT, item.ID);
                conn.Open();
                SqlCommand command = new SqlCommand(expression, conn);
                command.ExecuteNonQuery();
            }
        }
        public void DeleteBook(BOOK item)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string expression = string.Format("DELETE FROM BOOKS WHERE ID = {0}", item.ID);
                conn.Open();
                SqlCommand command = new SqlCommand(expression, conn);
                command.ExecuteNonQuery();
            }
        }

        public List<USER> GetUsers()
        {
            List<USER> result = new List<USER>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM USERS", conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        long id = reader.GetInt64(0);
                        string email = reader.GetValue(1).GetType() == typeof(DBNull) ? null : reader.GetString(1);
                        string fio = reader.GetString(2);
                        string login = reader.GetString(3);
                        string password = reader.GetString(4);
                        result.Add(new USER
                        {
                            ID = id,
                            EMAIL = email,
                            FIO = fio,
                            LOGIN = login,
                            PASSWORD = password,
                        });
                    }
                }
            }
            return result;
        }
        public void AddUser(USER item)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string expression = string.Format("INSERT INTO USERS (EMAIL,FIO,LOGIN,PASSWORD) VALUES('{0}','{1}','{2}',{3})", item.EMAIL, item.FIO, item.LOGIN, item.PASSWORD);
                conn.Open();
                SqlCommand command = new SqlCommand(expression, conn);
                command.ExecuteNonQuery();
            }
        }
        public virtual void EditUser(USER item)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string expression = string.Format("UPDATE USERS SET EMAIL = '{0}',FIO = '{1}',LOGIN = '{2}',PASSWORD = '{3}' WHERE ID = {4}", item.EMAIL, item.FIO, item.LOGIN, item.PASSWORD, item.ID);
                conn.Open();
                SqlCommand command = new SqlCommand(expression, conn);
                command.ExecuteNonQuery();
            }
        }
        public virtual void DeleteUser(USER item)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string expression = string.Format("DELETE FROM USERS WHERE ID = {0}", item.ID);
                conn.Open();
                SqlCommand command = new SqlCommand(expression, conn);
                command.ExecuteNonQuery();
            }
        }
        
        public bool HasTheBook(string login, long id)
        {
            int count = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string expression = string.Format("SELECT COUNT(*) FROM USERSBOOKS WHERE USERID = (SELECT u.ID FROM USERS AS u WHERE u.LOGIN = '{0}') AND BOOKID = {1}", login, id);
                conn.Open();
                SqlCommand command = new SqlCommand(expression, conn);
                count = (int)command.ExecuteScalar();
            }

            return count > 0;
        }

        public void TakeABook(string login, long id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string expression = string.Format("INSERT INTO USERSBOOKS (USERID,BOOKID) VALUES((SELECT u.ID FROM USERS AS u WHERE u.LOGIN = '{0}'),{1});UPDATE BOOKS SET COUNT = COUNT - 1 WHERE ID = {1}", login, id);
                conn.Open();
                SqlCommand command = new SqlCommand(expression, conn);
                command.ExecuteNonQuery();
            }
        }

        public void ReturnABook(string login, long id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string expression = string.Format("DELETE FROM USERSBOOKS WHERE USERID = (SELECT u.ID FROM USERS AS u WHERE u.LOGIN = '{0}') AND BOOKID = {1};UPDATE BOOKS SET COUNT = COUNT + 1 WHERE ID = {1}", login, id);
                conn.Open();
                SqlCommand command = new SqlCommand(expression, conn);
                command.ExecuteNonQuery();
            }
        }
    }
}