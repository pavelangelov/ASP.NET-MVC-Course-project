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

        #region AddLake

        public const string AddLakeSuccessMessage = "Язовира е добавен успешно";
        public const string AddLakeErrorMessage = "Визникна грешка при добавянето на язовира. Проверете дали вече няма добавен язовир с това име.";

        #endregion

        #region ModelsErrorMessages

        public const string AgeErrorMessage = "Годините на потребителя трябва да са в интервал {0} - {1}";
        public const string NameErrorMessage = "{0} на потребителя трябва да е между {1} и {2} символа.";
        public const string FishNameErrorMessage = "Името на рибата трябва да е между 2 и 35 символа.";
        public const string InfoErrorMessage = "Допълнителната информация не може да е с дължина над 2500 символа";

        #endregion
    }
}
