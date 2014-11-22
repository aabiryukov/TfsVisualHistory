using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Sitronics.TfsVisualHistory.Utility
{
	internal static class FileUtils
	{
// ReSharper disable once InconsistentNaming
		private const int MAX_PATH = 255;

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern int GetShortPathName(
			[MarshalAs(UnmanagedType.LPWStr)]
         string path,
			[MarshalAs(UnmanagedType.LPWStr)]
         StringBuilder shortPath,
			int shortPathLength
			);

		public static string GetShortPath(string path)
		{
			var shortPath = new StringBuilder(MAX_PATH);
			if (GetShortPathName(path, shortPath, MAX_PATH) == 0)
			{
				throw new InvalidDataException(string.Format(CultureInfo.InvariantCulture, "GetShortPathName failed for path '{0}'", path));
			}
			return shortPath.ToString();
		}

		public static string GetTempPath()
		{
			// Fix gource problem with unicode file names. F.e. C:\users\Александр\AppData\ -> C:\Users\8523~1\AppData\
			return GetShortPath(Path.GetTempPath());
		}
	}
}
