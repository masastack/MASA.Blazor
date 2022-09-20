namespace Masa.Blazor.Maui.Plugin.Audio;

public class AudioManager : IAudioManager
{
    static IAudioManager? currentImplementation;

    public static IAudioManager Current => currentImplementation ??= new AudioManager();

    /// <inheritdoc />
    public IAudioPlayer CreatePlayer(Stream audioStream)
    {
        ArgumentNullException.ThrowIfNull(audioStream);

        return new AudioPlayer(audioStream);
    }

    /// <inheritdoc />
    public IAudioPlayer CreatePlayer(string fileName)
    {
        ArgumentNullException.ThrowIfNull(fileName);

        return new AudioPlayer(fileName);
    }
}