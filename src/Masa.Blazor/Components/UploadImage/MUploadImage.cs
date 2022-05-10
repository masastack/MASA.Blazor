namespace Masa.Blazor
{
    public class MUploadImage : BUploadImage
    {
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
