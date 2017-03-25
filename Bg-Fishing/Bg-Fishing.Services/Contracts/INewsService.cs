using System.Collections.Generic;

using Bg_Fishing.DTOs;
using Bg_Fishing.Models;

namespace Bg_Fishing.Services.Contracts
{
    public interface INewsService
    {
        void Add(News news);

        News FindById(string id);

        IEnumerable<NewsDTO> GetNews(int skip, int take);

        int GetNewsCount();

        int Save();
    }
}
