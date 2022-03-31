namespace Masa.Blazor
{
    public class MUploadImage : BUploadImage
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();

            DefaultImageUrl = "./_content/Masa.Blazor/images/upload/defaultImage.png";
        }

        protected override void SetComponentClass()
        {
            CssProvider
               .Merge(cssBuilder =>
               {
                   cssBuilder.Add("m-uploadImage");
               }, styleBuilder =>
               {

               });
        }
    }
}
