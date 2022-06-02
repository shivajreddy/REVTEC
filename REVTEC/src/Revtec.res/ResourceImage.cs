using System;
using System.IO;
using System.Reflection;
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


            /// some problem with the below block //////
            //image.BeginInit();
            //image.StreamSource = stream;
            //image.EndInit();

            // Read the file as one string. 
            string parentPath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;

            string imagePath = Path.Combine(parentPath,"src","Revtec.res", "Images", "Icons", name);

            Uri uri = new Uri(imagePath);

            var image = new BitmapImage(uri);

            return image;
        }

        #endregion
    }
}
