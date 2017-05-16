namespace Bg_Fishing.Utils
{
    public static class GlobalMessages
    {
        #region AddVideo

        public const string InvalidVideoUrlMessage = "Линкът към видеото е невалиден.";
        public const string InvalidGalleryNameMessage = "Не е избрана категория.";
        public const string AddVideoSuccessMessage = "Видеото е добавено.";
        public const string AddVideoErrorMessage = "Видеото не може да бъде добавено.";
        public const string InvalidVideoTitleMessage = "Невалидно загалвие на видеото.";

        #endregion

        #region AddLocation

        public const string AddLocationSuccessMessage = "Локацията е добавена.";
        public const string AddLocationErrorMessage = "Локацията не може да бъде добавена. Проверете дали вече няма локация за това местоположение.";
        public const string InvalidLocationModelErrorMessage = "Всички полета без \"Информация\" са задължителни.";

        #endregion

        #region Moderator.FishController

        public const string FishAddedSuccessKey = "FishAddedSuccess";
        public const string FishAddedSuccessMessage = "Рибата е добавена успешно";
        public const string FishAddingErrorMessage = "Не е избрана снимка или размера на снимката е по-голям от 3 MB!";
        public const string FishAddingFailMessage = "Грешка при добавянето на рибата!";

        #endregion

        #region Moderator.LakeController

        // AddLake
        public const string AddLakeSuccessMessage = "Язовира е добавен успешно";
        public const string AddLakeErrorMessage = "Визникна грешка при добавянето на язовира. Проверете дали вече няма добавен язовир с това име.";

        // AddFish
        public const string AddingFishErrorMessage = "Възникна грешка при добавянето на на избраните риби.";
        public const string AddingFishSuccessMessageFormat = "Рибата е добавена във {0}.";

        // RemoveFish
        public const string RemoveFishErroMessage = "Възникна грешка при премахването на избраните риби.";
        public const string RemoveFishSuccessMessage = "Рибата е премахната успешно";

        // EditLake
        public const string EditLakeSuccessMessage = "Промените са направени!";
        public const string EditLakeFailMessage = "Промените не могът да бъдат направени в момента!";
        public const string SuccessEditKey = "LakeEditSuccess";
        public const string FailKey = "LakeEditFail";

        #endregion

        #region Moderator.NewsController

        public const string AddNewsSuccessMessage = "Новината е добавена";
        public const string AddNewsSuccessKey = "AddNewsSuccess";

        #endregion

        #region ModelsErrorMessages

        public const string AgeErrorMessage = "Годините на потребителя трябва да са в интервал {0} - {1}";
        public const string NameErrorMessage = "Полето {0} трябва да е между {1} и {2} символа.";
        public const string FishNameErrorMessage = "Името на рибата трябва да е между 2 и 35 символа.";
        public const string InfoErrorMessage = "Допълнителната информация не може да е с дължина над 3500 символа";

        #endregion

        #region AddComment

        public const string AddCommentSuccessMessage = "Вашето мнение е добавено";
        public const string AddCommentErrorMessage = "Мнението не може да бъде добавено";
        public const string AddCOmentInvalidModelStateErrorMessage = "Съдържанието не може да е празно.";

        #endregion

        #region RemoveVideo

        public const string RemoveVideoSuccessMessage = "Видеото е премахнато";
        public const string RemoveVideoErroMessage = "Визникна грешка при премахването на видеото";

        #endregion

        #region ImageController

        public const string NoFileErrorMessage = "Не е избран файл!";

        #endregion
    }
}
