using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace DataAccess.Repository
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> Get();
        public Task<User> Get(int id);
        public Task<User> Create(User user);
        public Task<int> Update(User user);
        public Task<int> Delete(int id);
    }
}
