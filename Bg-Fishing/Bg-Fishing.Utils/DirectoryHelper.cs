using System.IO;

using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.Utils
{
    public class DirectoryHelper : IDirectoryHelper
    {
        public void CreateIfNotExist(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
