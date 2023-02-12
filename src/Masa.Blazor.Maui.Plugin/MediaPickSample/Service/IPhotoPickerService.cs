namespace MediaPickSample.Service
{
    public interface IPhotoPickerService
    {
        Task<Dictionary<string, string>> GetImageAsync1();
        Task<Dictionary<string, string>> GetImageAsync2();
        Task<Dictionary<string, string>> GetImageAsync3();
        Task<Dictionary<string, string>> GetImageAsync4();
    }
}
