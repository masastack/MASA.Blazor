namespace BlazorComponent
{
    public interface IThemeable
    {
        public bool Dark { get; set; }

        public bool Light { get; set; }

        public bool IsDark
        {
            get
            {
                if (this.Dark)
                {
                    // explicitly dark
                    return true;
                }
                else if (this.Light)
                {
                    // explicitly light
                    return false;
                }
                else
                {
                    // inherit from parent, or default false if there is none
                    return false;
                }
            }
        }
    }
}
