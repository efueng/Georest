using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Georest.Domain.Models;
using Georest.Domain.Services.Interfaces;

namespace Georest.Domain.Services
{
    public class UserService : IUserService
    {
        private ApplicationDbContext DbContext { get; }

        public UserService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public List<User> FetchAll()
        {
            return DbContext.Users.ToList();
        }

        public User GetById(int id)
        {
            return DbContext.Users.Find(id);
        }

        public User AddUser(User user)
        {
            DbContext.Users.Add(user);
            DbContext.SaveChanges();
            return user;
        }

        public User UpdateUser(User user)
        {
            DbContext.Users.Update(user);
            DbContext.SaveChanges();
            return user;
        }

        public bool DeleteUser(int id)
        {
            User foundUser = DbContext.Users.Find(id);

            if (foundUser != null)
            {
                DbContext.Users.Remove(foundUser);
                DbContext.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
