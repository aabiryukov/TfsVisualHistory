using System.IO;
using System.Reflection;

namespace Sitronics.TfsVisualHistory
{
	public class PathProvider
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PathProvider"/> class.
		/// </summary>
		public PathProvider()
		{
			var baseDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "unknown";

			if (baseDirectory.Contains("Test"))
			{
				baseDirectory += @"\..\..\..\VSExtension";
			}

			GourceExecutableFilePath = Path.Combine(baseDirectory, @"Gource\Gource.exe");
			DataDirectoryPath = Path.Combine(baseDirectory, @"Data");
			LogoFilePath = Path.Combine(DataDirectoryPath, "Logo.png");
			LogFilePath = Path.Combine(Path.GetTempPath(), "TfsHistoryLog.tmp.txt");
			AvatarsDirectoryPath = Path.Combine(Path.GetTempPath(), "TfsHistoryLog.tmp.Avatars");

			if (!Directory.Exists(AvatarsDirectoryPath))
			{
				Directory.CreateDirectory(AvatarsDirectoryPath);
			}
		}

		public string GourceExecutableFilePath { get; private set; }
		public string DataDirectoryPath { get; private set; }
		public string AvatarsDirectoryPath { get; private set; }
		public string LogoFilePath { get; private set; }
		public string LogFilePath { get; private set; }
	}
}
