using System.IO;

namespace LPLTools.SysUtils
{
    public class SysUtils
    {
        /// <summary>
        /// Ensures trailing backslash on folder path.
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static string PathToOk(string folder)
        {
            // Ensure trailing backslash
            if (folder[folder.Length - 1].ToString() != "\\")
            {
                folder = folder + "\\";
            }
            return folder;
        }

        /// <summary>
        /// Get Path to Application executable
        /// </summary>
        /// <returns></returns>
        public static string ApplicationPath()
        {
            System.Reflection.Assembly a = System.Reflection.Assembly.GetEntryAssembly();
            return Path.GetDirectoryName(a.Location);
        }

        /// <summary>
        /// Gets the Path to the Application Executeable with trailing backslash.
        /// </summary>
        /// <returns></returns>
        public static string PathToApplication()
        {
            return PathToOk(ApplicationPath());
        }
    
    }
}
