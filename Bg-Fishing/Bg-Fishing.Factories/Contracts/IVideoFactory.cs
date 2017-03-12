using System;

using Bg_Fishing.Models.Galleries;

namespace Bg_Fishing.Factories.Contracts
{
    public interface IVideoFactory
    {
        Video CreateVideo(string title, string url, DateTime postedOn);
    }
}
