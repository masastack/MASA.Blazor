namespace Masa.Blazor.Maui.Plugin.Audio
{
    /// <summary>
    /// Provides the ability to create <see cref="IAudioPlayer" /> instances.
    /// </summary>
    public interface IAudioManager
    {
        /// <summary>
        /// Creates a new <see cref="IAudioPlayer"/> with the supplied <paramref name="audioStream"/> ready to play.
        /// </summary>
        /// <param name="audioStream">The <see cref="Stream"/> containing the audio to play.</param>
        /// <returns>A new <see cref="IAudioPlayer"/> with the supplied <paramref name="audioStream"/> ready to play.</returns>
        IAudioPlayer CreatePlayer(Stream audioStream) => new AudioPlayer(audioStream);

        /// <summary>
        /// Creates a new <see cref="IAudioPlayer"/> with the supplied <paramref name="fileName"/> ready to play.
        /// </summary>
        /// <param name="fileName">The name of the file containing the audio to play.</param>
        /// <returns>A new <see cref="IAudioPlayer"/> with the supplied <paramref name="fileName"/> ready to play.</returns>
        IAudioPlayer CreatePlayer(string fileName) => new AudioPlayer(fileName);
    }
}
