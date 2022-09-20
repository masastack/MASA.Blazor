namespace Masa.Blazor.Maui.Plugin.Audio
{
    /// <summary>
    /// Provides the ability to play audio.
    /// </summary>
    public interface IAudioPlayer : IDisposable
    {
        ///<Summary>
        /// Raised when audio playback completes successfully.
        ///</Summary>
        event EventHandler PlaybackEnded;

        ///<Summary>
        /// Gets the length of audio in seconds.
        ///</Summary>
        double Duration { get; }

        ///<Summary>
        /// Gets the current position of audio playback in seconds.
        ///</Summary>
        double CurrentPosition { get; }

        ///<Summary>
        /// Gets or sets the playback volume 0 to 1 where 0 is no-sound and 1 is full volume.
        ///</Summary>
        double Volume { get; set; }

        ///<Summary>
        /// Gets or sets the balance left/right: -1 is 100% left : 0% right, 1 is 100% right : 0% left, 0 is equal volume left/right.
        ///</Summary>
        double Balance { get; set; }

        ///<Summary>
        /// Gets a value indicating whether the currently loaded audio file is playing.
        ///</Summary>
        bool IsPlaying { get; }

        ///<Summary>
        /// Gets or sets whether the player will continuously repeat the currently playing sound.
        ///</Summary>
        bool Loop { get; set; }

        ///<Summary>
        /// Gets a value indicating whether the position of the loaded audio file can be updated.
        ///</Summary>
        bool CanSeek { get; }

        ///<Summary>
        /// Begin playback or resume if paused.
        ///</Summary>
        void Play();

        ///<Summary>
        /// Pause playback if playing (does not resume).
        ///</Summary>
        void Pause();

        ///<Summary>
        /// Stop playback and set the current position to the beginning.
        ///</Summary>
        void Stop();

        ///<Summary>
        /// Set the current playback position (in seconds).
        ///</Summary>
        void Seek(double position);
    }
}
