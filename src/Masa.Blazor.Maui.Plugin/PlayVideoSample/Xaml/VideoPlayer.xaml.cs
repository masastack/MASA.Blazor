using CommunityToolkit.Maui.Views;

namespace PlayVideoSample.Xaml;

public partial class VideoPlayerView
{
    public VideoPlayerView(string url)
    {
        InitializeComponent();
        mediaElement.Source = MediaSource.FromUri(url);
    }


    private void ReturnToBlazor_Clicked(object sender, EventArgs e)
    {
        mediaElement.Handler?.DisconnectHandler();
        Application.Current.MainPage.Navigation.PopModalAsync(true);
    }

}