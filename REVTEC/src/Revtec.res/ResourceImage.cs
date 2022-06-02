using System.Windows.Media.Imaging;

namespace Revtec.res
{
    /// <summary>
    /// Gets the embedded resource image from the Revtec.res assembly
    /// based on the provided file name with extension.
    /// </summary>
    public class ResourceImage
    {
        #region public methods

        public static BitmapImage GetIcon(string name)
        {
            // Create the resource reader stream
            var stream = ResourceAssembly.GetAssembly().GetManifestResourceStream(ResourceAssembly.GetNamespace() + "Images.Icons" + name);

            var image = new BitmapImage();

            ///// some problem with the below block //////
            //image.BeginInit();
            //image.StreamSource = stream;
            //image.EndInit();

            return image;
        }

        #endregion
    }
}
