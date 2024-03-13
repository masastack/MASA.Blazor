using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayVideoSample.Xaml
{
    public static class VideoPlayerService

    {
        public static void Play(string url)
        {
            var videoPlayerView = new VideoPlayerView(url);
            Application.Current.MainPage.Navigation.PushModalAsync(videoPlayerView);
        }
    }
}
