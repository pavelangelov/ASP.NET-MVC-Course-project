using System.Collections.Generic;

using Bg_Fishing.DTOs.CommentDTOs;
using Bg_Fishing.Models;

namespace Bg_Fishing.Services.Contracts
{
    public interface ICommentService
    {
        Comment FindById(string id);

        IEnumerable<CommentDTO> GetAllByLakeName(string lakeName);

        IEnumerable<CommentDTO> GetAllByUsername(string username);

        int Save();
    }
}
