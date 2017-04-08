using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using Bg_Fishing.Data;
using Bg_Fishing.Models.Comments;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Services.Models;
using Bg_Fishing.Utils;

namespace Bg_Fishing.Services
{
    public class CommentService : ICommentService
    {
        private IDatabaseContext dbContext;

        public CommentService(IDatabaseContext dbContext)
        {
            Validator.ValidateForNull(dbContext, paramName: "dbContext");

            this.dbContext = dbContext;
        }

        public Comment FindById(string id)
        {
            var comment = this.dbContext.Comments.Find(id);

            return comment;
        }

        public IEnumerable<CommentModel> GetAllByLakeName(string lakeName)
        {
            var allComments = this.dbContext.Comments.Include(c => c.Comments)
                                                        .Where(c => c.LakeName == lakeName)
                                                        .Select(CommentModel.Cast);

            return allComments;
        }

        public IEnumerable<CommentModel> GetAllByUsername(string username)
        {
            var allComments = this.dbContext.Comments.Include(c => c.Comments)
                                                        .Where(c => c.Username == username)
                                                        .Select(CommentModel.Cast);

            return allComments;
        }

        public int Save()
        {
            return this.dbContext.Save();
        }
    }
}
