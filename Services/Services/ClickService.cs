using Data;
using Entity;
using Services.DTO;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class ClickService : IClickService
    {
        private ReadLaterDataContext _ReadLaterDataContext;

        public ClickService(ReadLaterDataContext readLaterDataContext)
        {
            _ReadLaterDataContext = readLaterDataContext;
        }

        public Clicks AddClick(string url, string userId)
        {
            var addedClick = new Clicks();

            if (url != null)
            {
                addedClick.Url = url;
                addedClick.CreateDate = DateTime.Now;
            }

            if (userId != null)
            {
                addedClick.UserID = userId;
            }

            if (addedClick.Url != null)
            {
                _ReadLaterDataContext.Clicks.Add(addedClick);
                _ReadLaterDataContext.SaveChanges();
            }

            return addedClick;
        }

        public Task<List<DTO.ClickDTO>> GetMostPopularClicks()
        {
            var allClicks = _ReadLaterDataContext.Clicks.ToList().GroupBy(x => x.Url)
                .Select(a => new ClickDTO()
                {
                    url = a.First().Url,
                    TotalClicks = a.Count(),
                }).ToList();

            return Task.FromResult(allClicks);
        }

        public Task<List<DTO.ClickDTO>> GetMostPopularClicksToday()
        {
            var allClicks = _ReadLaterDataContext.Clicks.ToList()
                .Where(z => z.CreateDate.ToShortDateString() == DateTime.Now.ToShortDateString())
                .GroupBy(x => x.Url)
                .Select(a => new ClickDTO()
                {
                    url = a.First().Url,
                    TotalClicks = a.Count(),
                }).ToList();

            return Task.FromResult(allClicks);
        }
    }
}