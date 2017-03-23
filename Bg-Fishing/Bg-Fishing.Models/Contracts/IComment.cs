using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg_Fishing.Models.Contracts
{
    public interface IComment
    {
        string LakeName { get; }

        string Username { get; }

        string Content { get; }

        DateTime PostedDate { get; }

        ICollection<Comment> Comments { get; }
    }
}
