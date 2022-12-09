namespace Masa.Docs.Shared.Examples.data_iterators;

public class Usage : Components.Usage
{
    public Usage() : base(typeof(MDataIterator<Dessert>))
    {
    }

    //protected override RenderFragment GenChildContent() => builder =>
    //{
    //    //builder.OpenComponent<Default>(0);
    //};

    public class Dessert
    {
        public string Name { get; set; }

        public int Calories { get; set; }

        public double Fat { get; set; }

        public int Carbs { get; set; }

        public double Protein { get; set; }

        public int Sodium { get; set; }

        public string Calcium { get; set; }

        public string Iron { get; set; }
    }
}
