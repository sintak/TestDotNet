using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiFrame.Models;

namespace WebApiFrame.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();

        User GetById(int id);
    }
}
