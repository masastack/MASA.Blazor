namespace Masa.Blazor.Utils;

/// <summary>
/// The RenderRateLimiter class is used to limit the frequency of render operations.
/// It throws an InvalidOperationException if the render count exceeds the specified threshold within one second.
/// </summary>
public class RenderRateLimiter
{
    private int _renderCount;
    private long _renderStartTimestamp;
    private readonly string _errorMessage;
    private readonly int _threshold;

    /// <summary>
    /// Initializes a new instance of the RenderRateLimiter class.
    /// </summary>
    /// <param name="errorMessage">The error message to be thrown when the render count exceeds the threshold.</param>
    /// <param name="threshold">The maximum number of renders allowed within one second. Default is 20.</param>
    public RenderRateLimiter(string errorMessage, int threshold = 20)
    {
        _errorMessage = errorMessage;
        _threshold = threshold;
    }

    /// <summary>
    /// Records a render operation. If the render count exceeds the threshold within one second, an InvalidOperationException is thrown.
    /// </summary>
    public void RecordRender()
    {
        if (_renderCount == 0)
        {
            _renderStartTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            _renderCount++;
        }
        else
        {
            _renderCount++;
            var now = DateTimeOffset.Now.ToUnixTimeSeconds();
            if (now - _renderStartTimestamp > 1)
            {
                var copy = _renderCount;
                _renderCount = 0;

                if (copy > _threshold)
                {
                    throw new InvalidOperationException(_errorMessage);
                }
            }
        }
    }
}