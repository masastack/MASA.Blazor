namespace MediaPickSample.Service
{
    public interface IPhotoPickerService
    {
        /// <summary>
        /// Maui-MediaPicker
        /// </summary>
        Task<Dictionary<string, string>> GetImageAsync1();

        /// <summary>
        /// MMaui-FilePicker
        /// </summary>
        Task<Dictionary<string, string>> GetImageAsync2();

        /// <summary>
        /// Intent
        /// </summary>
        Task<Dictionary<string, string>> GetImageAsync3();

        Task<Dictionary<string, string>> GetImageAsync4();
    }
}
