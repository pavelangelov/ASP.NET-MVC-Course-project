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
    }
}
