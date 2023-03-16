        
function init() {
    var splitter = document.getElementById('splitter');
    var code = document.getElementById('code');
    var render = document.getElementById('render');

    var mouseDownHandler = function(e) {
        // Prevent text selection
        e.preventDefault();

        // Set initial positions
        var startPos = e.clientX;
        var startLeftWidth = code.offsetWidth;
        var startRightWidth = render.offsetWidth;

        // Define the mouse move handler
        var mouseMoveHandler = function(e) {
            // Calculate the new widths
            var newLeftWidth = startLeftWidth + (e.clientX - startPos);
            var newRightWidth = startRightWidth - (e.clientX - startPos);

            // Update the widths
            code.style.width = newLeftWidth + 'px';
            render.style.width = newRightWidth + 'px';
        };

        // Define the mouse up handler
        var mouseUpHandler = function() {
            // Remove the handlers
            document.removeEventListener('mousemove', mouseMoveHandler);
            document.removeEventListener('mouseup', mouseUpHandler);
        };

        // Add the handlers
        document.addEventListener('mousemove', mouseMoveHandler);
        document.addEventListener('mouseup', mouseUpHandler);
    };

    splitter.addEventListener('mousedown', mouseDownHandler);
}
export {
    init
}