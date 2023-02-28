using Android.Content;
using Android.Provider;
using AndroidX.Activity.Result;
using AndroidX.Activity.Result.Contract;
using MediaPickSample.PlatformsAndroid;
using MediaPickSample.Service;

[assembly: Dependency(typeof(AndroidPhotoPickerService))]
namespace MediaPickSample.PlatformsAndroid
{
    public class AndroidPhotoPickerService : IPhotoPickerService
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
        public  Task<Dictionary<string, string>> GetImageAsync3()
        {
            Intent intent = new Intent(Intent.ActionPick);
            intent.SetDataAndType(MediaStore.Images.Media.ExternalContentUri, "image/*");
            intent.PutExtra(Intent.ExtraAllowMultiple,true);
            MainActivity.Instance.StartActivityForResult(Intent.CreateChooser(intent, "Select Picture"),
                MainActivity.PickImageId);
            MainActivity.Instance.PickImageTaskCompletionSource = new TaskCompletionSource<Dictionary<string, string>>();
            return MainActivity.Instance.PickImageTaskCompletionSource.Task;
        }

        public Task<Dictionary<string, string>> GetImageAsync4()
        {
            MainActivity.PickMultipleMedia.Launch(new PickVisualMediaRequest.Builder()
                .SetMediaType(ActivityResultContracts.PickVisualMedia.ImageAndVideo.Instance).Build());
            MainActivity.Instance.PickImageTaskCompletionSource = new TaskCompletionSource<Dictionary<string, string>>();
           
            return MainActivity.Instance.PickImageTaskCompletionSource.Task;
        }
    }
}
