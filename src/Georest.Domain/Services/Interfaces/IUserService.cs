using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Georest.Domain.Models;

namespace Georest.Domain.Services.Interfaces
{
    public interface IUserService
    {
        List<User> FetchAll();
        User GetById(int id);
        User AddUser(User user);
        User UpdateUser(User user);
        bool DeleteUser(int id);
    }
}
