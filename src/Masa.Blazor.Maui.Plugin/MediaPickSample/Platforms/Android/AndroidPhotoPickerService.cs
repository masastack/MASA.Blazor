using Android.Content;
using Android.Provider;
using AndroidX.Activity.Result;
using AndroidX.Activity.Result.Contract;
using Java.Interop;
using MauiAppAgent.PlatformsAndroid;
using MediaPickSample;
using MediaPickSample.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndroidX.Activity;

[assembly: Dependency(typeof(AndroidPhotoPickerService))]
namespace MauiAppAgent.PlatformsAndroid
{
    public class AndroidPhotoPickerService : IPhotoPickerService
    {
        public async Task<Dictionary<string, string>> GetImageAsync1()
        {
            var dic = new Dictionary<string, string>();
            
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

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
        public  Task<Dictionary<string, string>> GetImageAsync2()
        {
            Intent intent = new Intent(Intent.ActionPick, null);
            intent.SetDataAndType(MediaStore.Images.Media.ExternalContentUri, "image/*");
            intent.PutExtra(Intent.ExtraAllowMultiple, true);
            MainActivity.Instance.StartActivityForResult(Intent.CreateChooser(intent, "Select Picture"),
                MainActivity.PickImageId);
            MainActivity.Instance.PickImageTaskCompletionSource = new TaskCompletionSource<Dictionary<string, string>>();
            return MainActivity.Instance.PickImageTaskCompletionSource.Task;
        }
        public Task<Dictionary<string, string>> GetImageAsync3()
        {
            MainActivity.pickMultipleMedia.Launch(new PickVisualMediaRequest.Builder()
                .SetMediaType(ActivityResultContracts.PickVisualMedia.ImageAndVideo.Instance).Build());
            MainActivity.Instance.PickImageTaskCompletionSource = new TaskCompletionSource<Dictionary<string, string>>();
           
            return MainActivity.Instance.PickImageTaskCompletionSource.Task;
        }


        //public class ActivityResultCallback : Java.Lang.Object, IActivityResultCallback
        //{
        //    readonly Action<ActivityResult> _callback;
        //    public ActivityResultCallback(Action<ActivityResult> callback) => _callback = callback;
        //    public ActivityResultCallback(TaskCompletionSource<ActivityResult> tcs) => _callback = tcs.SetResult;
        //    public void OnActivityResult(Java.Lang.Object p0) => _callback((ActivityResult)p0);
        //}
    }
}
