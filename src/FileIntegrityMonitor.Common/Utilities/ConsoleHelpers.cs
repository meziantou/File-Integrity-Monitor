using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace FileIntegrityMonitor.Common.Utilities
{
    public class ConsoleHelpers
    {
        /// <summary>
        /// Ensures a console is available, even for a Windows process.
        /// </summary>
        /// <returns>true if the console was created; false otherwise.</returns>
        public static bool EnsureConsole()
        {
            return AllocConsole();
        }

        /// <summary>
        /// Attach the parent process console.
        /// </summary>
        /// <returns>true if attachment succeeded; false otherwise.</returns>
        public static bool AttachParentProcessConsole()
        {
            return AttachConsole(-1);
        }

        /// <summary>
        /// Attach to a given process console.
        /// </summary>
        /// <param name="process">The process. May not be null.</param>
        /// <returns>true if attachment succeeded; false otherwise.</returns>
        public static bool AttachConsole(Process process)
        {
            if (process == null)
                throw new ArgumentNullException("process");

            return AttachConsole(process.Id);
        }

        /// <summary>
        /// Closes the console.
        /// </summary>
        public static bool CloseConsole()
        {
            return FreeConsole();
        }

        [DllImport("kernel32.dll")]
        private extern static bool AllocConsole();

        [DllImport("kernel32.dll")]
        private extern static bool FreeConsole();

        [DllImport("kernel32.dll")]
        private extern static bool AttachConsole(int processId);

    }
}