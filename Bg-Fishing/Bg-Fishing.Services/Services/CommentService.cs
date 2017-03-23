using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using Bg_Fishing.Data;
using Bg_Fishing.DTOs.CommentDTOs;
using Bg_Fishing.Models;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;

namespace Bg_Fishing.Services
{
    public class CommentService : ICommentService
    {
        private IDatabaseContext dbContext;

        public CommentService(IDatabaseContext dbContext)
        {
            Validator.ValidateForNull(dbContext, paramName: "dbContext");
        }

        public Comment FindById(string id)
        {
            var comment = this.dbContext.Comments.Find(id);

            return comment;
        }

        public IEnumerable<CommentDTO> GetAllByLakeName(string lakeName)
        {
            var allComments = this.dbContext.Comments.Include(c => c.Comments)
                                                        .Where(c => c.LakeName == lakeName)
                                                        .Select(l => new CommentDTO
                                                        {
                                                            LakeName = l.LakeName,
                                                            Username = l.Username,
                                                            PostedDate = l.PostedDate,
                                                            Comments = l.Comments.Select(x => new InnerCommentDTO
                                                            {
                                                                Username = x.Username,
                                                                Content = x.Content
                                                            })
                                                        });

            return allComments;
        }

        public IEnumerable<CommentDTO> GetAllByUsername(string username)
        {
            var allComments = this.dbContext.Comments.Include(c => c.Comments)
                                                        .Where(c => c.LakeName == username)
                                                        .Select(l => new CommentDTO
                                                        {
                                                            LakeName = l.LakeName,
                                                            Username = l.Username,
                                                            PostedDate = l.PostedDate
                                                        });

            return allComments;
        }

        public int Save()
        {
            return this.dbContext.Save();
        }
    }
}
