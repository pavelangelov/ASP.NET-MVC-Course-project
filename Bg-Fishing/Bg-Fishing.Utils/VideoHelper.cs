namespace Bg_Fishing.Utils
{
    public static class VideoHelper
    {
        public static string FixUrl(string url)
        {
            var fixedUrl = url.Replace("watch?v=", "embed/");

            return fixedUrl;
        }
    }
}
