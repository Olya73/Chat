using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IBotService
    {
        Task<Bot[]> GetAllWithTypeAsync();
    }
}
