using Entity;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IClickService
    {
        Clicks AddClick(string url, string userId);
        Task<List<DTO.ClickDTO>> GetMostPopularClicks();
        Task<List<DTO.ClickDTO>> GetMostPopularClicksToday();
    }
}