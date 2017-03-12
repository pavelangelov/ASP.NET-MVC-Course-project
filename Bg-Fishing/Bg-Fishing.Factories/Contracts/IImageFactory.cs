using System;

using Bg_Fishing.Models.Galleries;

namespace Bg_Fishing.Factories.Contracts
{
    public interface IImageFactory
    {
        Image CreateImage();

        Image CreateImage(string imageUrl, DateTime date);

        Image CreateImage(string imageUrl, DateTime date, string info);
    }
}
