
using System.Reflection;

namespace Revtec.core
{
    /// <summary>
    /// The core assembly
    /// </summary>
    public class CoreAssembly
    {
        #region public methods

        public static string GetCoreAssemblyLocation()
        {
            return Assembly.GetExecutingAssembly().Location;
        }

        #endregion
    }
}
