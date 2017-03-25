using System;

using Bg_Fishing.Models;

namespace Bg_Fishing.Factories.Contracts
{
    public interface INewsFactory
    {
        News CreateNews();

        News CreateNews(string title, string content, string imageUrl, DateTime postedOn);
    }
}
