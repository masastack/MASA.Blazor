using MediaPickSample.PlatformsIOS;
using MediaPickSample.Service;
using MobileCoreServices;

[assembly: Dependency(typeof(IOSPhotoPickerService))]
namespace MediaPickSample.PlatformsIOS
{
    public class IOSPhotoPickerService : IPhotoPickerService
    {
        public async Task<Dictionary<string, string>> GetImageAsync1()
        {
            var dic = new Dictionary<string, string>();
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.PickPhotoAsync();

                if (photo != null)
                {
                    using (Stream sourceStream = await photo.OpenReadAsync())
                    {
                        sourceStream.Seek(0, SeekOrigin.Begin);
                        byte[] bs = new byte[sourceStream.Length];
                        int log = Convert.ToInt32(sourceStream.Length);
                        sourceStream.Read(bs, 0, log);
                        var base64Str = Convert.ToBase64String(bs);
                        dic.Add(photo.FileName, base64Str);
                    }
                }
            }
            return dic;
        }
        public async Task<Dictionary<string, string>> GetImageAsync2()
        {
            var dic = new Dictionary<string, string>();
            if (MediaPicker.Default.IsCaptureSupported)
            {
                var photoList = await FilePicker.Default.PickMultipleAsync(new()
                {
                    PickerTitle = "Please select a image file",
                    FileTypes = FilePickerFileType.Images,
                });

                if (photoList != null && photoList.Any())
                {
                    foreach ( var photo in photoList)
                    {
                        using (Stream sourceStream = await photo.OpenReadAsync())
                        {
                            sourceStream.Seek(0, SeekOrigin.Begin);
                            byte[] bs = new byte[sourceStream.Length];
                            int log = Convert.ToInt32(sourceStream.Length);
                            sourceStream.Read(bs, 0, log);
                            var base64Str = Convert.ToBase64String(bs);
                            dic.Add(photo.FileName, base64Str);
                        }
                    }

                }
            }
            return dic;
        }

        public async Task<Dictionary<string, string>> GetImageAsync3()
        {
            var customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.iOS, new[] {UTType.Image.ToString() } }, // UTType values
                });

            PickOptions options = new()
            {
                PickerTitle = "Please select a image file",
                FileTypes = customFileType,
            };
            var dic = new Dictionary<string, string>();
            if (MediaPicker.Default.IsCaptureSupported)
            {
                var photoList = await FilePicker.Default.PickMultipleAsync(options);

                if (photoList != null && photoList.Any())
                {
                    foreach (var photo in photoList)
                    {
                        using (Stream sourceStream = await photo.OpenReadAsync())
                        {
                            sourceStream.Seek(0, SeekOrigin.Begin);
                            byte[] bs = new byte[sourceStream.Length];
                            int log = Convert.ToInt32(sourceStream.Length);
                            sourceStream.Read(bs, 0, log);
                            var base64Str = Convert.ToBase64String(bs);
                            dic.Add(photo.FileName, base64Str);
                        }
                    }

                }
            }
            return dic;
        }

        public async Task<Dictionary<string, string>> GetImageAsync4()
        {
            var dic = new Dictionary<string, string>();
            if (MediaPicker.Default.IsCaptureSupported)
            {
                var photoList = await FilePicker.Default.PickMultipleAsync(new()
                {
                    PickerTitle = "Please select a image file",
                    FileTypes = FilePickerFileType.Png,
                });

                if (photoList != null && photoList.Any())
                {
                    foreach (var photo in photoList)
                    {
                        using (Stream sourceStream = await photo.OpenReadAsync())
                        {
                            sourceStream.Seek(0, SeekOrigin.Begin);
                            byte[] bs = new byte[sourceStream.Length];
                            int log = Convert.ToInt32(sourceStream.Length);
                            sourceStream.Read(bs, 0, log);
                            var base64Str = Convert.ToBase64String(bs);
                            dic.Add(photo.FileName, base64Str);
                        }
                    }

                }
            }
            return dic;
        }
    }
}
