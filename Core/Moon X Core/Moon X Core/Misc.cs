using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon_X_Core
{
    public static class Misc
    {
        /// <summary>
        /// Check if a drive exists or not
        /// </summary>
        /// <param name="name">Driver name with colon and slash</param>
        /// <returns>A boolean</returns>
        public static bool DriveExists(string name)
        {
            return DriveInfo.GetDrives().Any(x => x.Name == name);
        }
    }
}
