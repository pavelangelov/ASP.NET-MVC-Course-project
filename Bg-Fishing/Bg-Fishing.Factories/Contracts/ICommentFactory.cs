using System;

using Bg_Fishing.Models.Comments;

namespace Bg_Fishing.Factories.Contracts
{
    public interface ICommentFactory
    {
        Comment CreateComment();

        Comment CreateComment(string lakeName, string username, string content, DateTime postedDate);
    }
}
