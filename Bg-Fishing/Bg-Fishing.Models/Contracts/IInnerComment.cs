using System;

namespace Bg_Fishing.Models.Contracts
{
    public interface IInnerComment
    {
        string Content { get; }

        DateTime PostedDate { get; }

        string Username { get; }
    }
}
