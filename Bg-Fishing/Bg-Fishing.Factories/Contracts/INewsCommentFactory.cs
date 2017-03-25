using System;

using Bg_Fishing.Models;

namespace Bg_Fishing.Factories.Contracts
{
    public interface INewsCommentFactory
    {
        NewsComment CreateNewsComment();

        NewsComment CreateNewsComment(string content, string username, DateTime postedOn);
    }
}
