using System.Collections.Generic;

using Bg_Fishing.Models.Comments;
using Bg_Fishing.Services.Models;

namespace Bg_Fishing.Services.Contracts
{
    public interface ICommentService
    {
        Comment FindById(string id);

        IEnumerable<CommentModel> GetAllByLakeName(string lakeName);

        IEnumerable<CommentModel> GetAllByUsername(string username);

        int Save();
    }
}
