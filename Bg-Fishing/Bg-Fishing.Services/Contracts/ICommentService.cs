using System.Collections.Generic;

using Bg_Fishing.Models.Comments;
using Bg_Fishing.Services.Models;

namespace Bg_Fishing.Services.Contracts
{
    public interface ICommentService
    {
        Comment FindById(string id);

        IEnumerable<CommentModel> GetCommentsByLakeName(string lakeName, int skip, int take);

        IEnumerable<CommentModel> GetAllByUsername(string username);

        int GetCommentsCount(string lakeName);

        int Save();
    }
}
