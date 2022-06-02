using System.Reflection;

namespace Revtec.res
{
    /// <summary>
    /// Resource assembly helper methods
    /// </summary>
    public class ResourceAssembly
    {

        #region public methods

        /// <summary>
        /// Gets the current resource assembly
        /// </summary>
        /// <returns></returns>
        public static Assembly GetAssembly()
        {
            return  Assembly.GetExecutingAssembly();
        }

        /// <summary>
        /// Gets the current assembly working namespace
        /// </summary>
        /// <returns></returns>
        public static string GetNamespace()
        {
            return typeof(ResourceAssembly).Namespace + ".";
        }

        #endregion
    }
}
