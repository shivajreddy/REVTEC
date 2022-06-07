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
            // yt video method
            var resImg = ResourceAssembly.GetNamespace() + "Images.Icons." + name;
            var stream = ResourceAssembly.GetAssembly().GetManifestResourceStream(resImg);

            var img = new BitmapImage();

            img.BeginInit();
            img.StreamSource = stream;
            img.EndInit();

            return img;


            // My method - Read the file as one string. 
            //string parentPath = null;
            //parentPath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;

            //// This parent path is throws null, in production. cause production directory not same
            //Console.WriteLine(parentPath);


            //string imagePath = Path.Combine(parentPath,"src","Revtec.res", "Images", "Icons", name);

            //Uri uri = new Uri(imagePath);

            //var image = new BitmapImage(uri);

            //return image;
        }

        #endregion
    }
}
