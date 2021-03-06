﻿using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Storage.Interface
{
    public interface IUserRepository
    {
        Task<User> GetAsync(int id);
    }
}
