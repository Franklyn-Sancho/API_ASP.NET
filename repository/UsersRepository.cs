using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyApi.Data;
using MyApi.Models;

namespace MyApi.Repository
{

    public interface IUserRepository
    {
        IEnumerable<Users> GetAllUsers();
        Users GetUserByEmail(string email);
        void CreateUser(Users users);
    }

    public class UserRepository : IUserRepository
    {
        private readonly MyApiContext _context;

        public UserRepository(MyApiContext context)
        {
            _context = context;
        }

        public IEnumerable<Users> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public Users GetUserByEmail(string email)
        {
             return _context.Users.FirstOrDefault(u => u.Email == email);
        }




        public void CreateUser(Users users)
        {
            _context.Users.Add(users);
            _context.SaveChanges();
        }
    }
}