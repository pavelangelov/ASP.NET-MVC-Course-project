namespace Bg_Fishing.Utils
{
    public static class Constants
    {
        #region Common

        public const int AgeMinValue = 6;
        public const int AgeMaxValue = 125;

        public const int NameMinLength = 2;
        public const int NameMaxLength = 35;

        public const int PasswordMinLength = 6;
        public const int PasswordMaxLength = 100;

        public const int InfoMaxLEngth = 3000;

        #endregion

        #region ResourceKeys

        public const string RequiredPropertyResourceKey = "PropertyValueRequired";
        public const string StringLengthResourceKey = "StringLengthErrorMessage";

        #endregion

        #region Location

        public const int LocationNameMaxLength = 50;

        #endregion

        #region Comment

        public const int CommentContentMaxLength = 250;

        #endregion

        #region Images

        public const int ImageMaxSize = 3 * 1024 * 1000;

        #endregion

        #region Moderator.FishController

        public const string FishImagesFolder = "/Images/Fish/";
        public const string FishImagesServerFolder = "~/Images/Fish/";

        #endregion

        #region Moderator.NewsController

        public const string NewsImagesFolder = "/Images/News/";
        public const string NewsImagesServerFolder = "~/Images/News/";

        #endregion

        #region ImageController

        public const string ImageGalleriesBaseFolder = "/Images/Galleries";
        public const string ImageGalleriesBaseServerFolder = "~/Images/Galleries";

        #endregion

        public const string NewsDefaultImage = "";

        public const int ShowedNewsCount = 5;
        public const int ShowedComments = 5;
    }
}
