using System;
using System.Collections.Generic;

using Bg_Fishing.Models.Comments;

namespace Bg_Fishing.Models.Contracts
{
    public interface IComment
    {
        string LakeName { get; }

        string Username { get; }

        string Content { get; }

        DateTime PostedDate { get; }

        ICollection<InnerComment> Comments { get; }
    }
}
