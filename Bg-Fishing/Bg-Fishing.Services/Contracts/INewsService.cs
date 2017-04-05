using System.Collections.Generic;

using Bg_Fishing.Models;
using Bg_Fishing.Services.Models;

namespace Bg_Fishing.Services.Contracts
{
    public interface INewsService
    {
        void Add(News news);

        News FindById(string id);

        IEnumerable<NewsModel> GetNews(int skip, int take);

        int GetNewsCount();

        int Save();
    }
}
