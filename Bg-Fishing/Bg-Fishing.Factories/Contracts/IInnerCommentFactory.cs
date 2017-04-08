using System;

using Bg_Fishing.Models.Comments;

namespace Bg_Fishing.Factories.Contracts
{
    public interface IInnerCommentFactory
    {
        InnerComment CreateInnerComment();

        InnerComment CreateInnerComment(string content, string username, DateTime postedDate);
    }
}
