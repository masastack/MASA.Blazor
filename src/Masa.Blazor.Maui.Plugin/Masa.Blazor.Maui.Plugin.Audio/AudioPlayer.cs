namespace Masa.Blazor.Maui.Plugin.Audio
{
    public partial class AudioPlayer : IAudioPlayer
    {
        public event EventHandler? PlaybackEnded;

        ~AudioPlayer()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
    }
}