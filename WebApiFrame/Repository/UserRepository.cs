using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiFrame.Models;

namespace WebApiFrame.Repository
{
    public class UserRepository : IUserRepository
    {
        private IList<User> _list = new List<User>()
        {
            new User() { Id = 1, Name = "name:1", Sex = "Male" },
            new User(){ Id = 2, Name = "name:2", Sex = "Female" },
            new User(){ Id = 3, Name = "name:3", Sex = "Male" },
        };

        public IEnumerable<User> GetAll() {
            return _list;
        }

        public User GetById(int id) {
            return _list.FirstOrDefault(i => i.Id == id);
        }
    }
}
