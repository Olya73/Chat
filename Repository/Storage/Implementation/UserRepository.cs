using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Storage.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Storage.Implementation
{
    public class UserRepository: IUserRepository
    {
        private readonly ChatNpgSQLContext _context;

        public UserRepository(ChatNpgSQLContext context)
        {
            _context = context;
        }

        public Task<User> GetAsync(int id)
        {
            return _context.Users.SingleOrDefaultAsync(u => u.Id == id);
        }
    }
}
