using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week5HW.Core.Entities;
using Week5HW.Core.Enums;
using Week5HW.Core.Interfaces;
using Week5HW.Infrastructure.Data;

namespace Week5HW.Infrastructure.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {

        private readonly DbSet<User> _users;
        public UserRepository(AppDbContext dbContext, Func<CacheTech, ICacheService> cacheService) :
            base(dbContext, cacheService)
        {
            _users = dbContext.Set<User>();
        }
    }


}
