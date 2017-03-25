using System;
using System.Collections.Generic;

namespace Bg_Fishing.Models.Contracts
{
    public interface INews
    {
        string Title { get; }

        string Content { get; }

        DateTime PostedOn { get; }

        ICollection<Comment> Comments { get; }
    }
}
