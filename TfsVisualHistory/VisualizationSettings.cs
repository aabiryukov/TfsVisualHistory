using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using Sitronics.TfsVisualHistory.Utility;

namespace Sitronics.TfsVisualHistory
{
	[Serializable]
	public class VisualizationSettings
	{
		public PlayMode PlayMode;

		public StringFilter UsersFilter { get; set; }
		public StringFilter FilesFilter { get; set; }

		public DateTime DateFrom { get; set; }
		public DateTime DateTo { get; set; }

		public bool ViewFilesExtentionMap;

		public bool ViewFileNames;
		public bool ViewDirNames;
		public bool ViewUserNames;
		public bool ViewAvatars;

		public int SecondsPerDay;
		public TimeScale TimeScale;
		public int MaxFiles;
		public bool LoopPlayback;

		public bool FullScreen;
		public bool SetResolution;
		public int ResolutionWidth;
		public int ResolutionHeight;

		public CheckState ViewLogo;
		public string LogoFileName;

		public int FileIdleTime;

		public VisualizationSettings()
		{
			PlayMode = PlayMode.History;

			DateFrom = new DateTime(1900, 1, 1);
			DateTo = new DateTime(2099, 1, 1);

			UsersFilter = new StringFilter("*", "");
			FilesFilter = new StringFilter("*", "");

			ViewFileNames = false;
			ViewDirNames = true;
			ViewUserNames = true;
			ViewAvatars = true;

			TimeScale = TimeScale.None;
			SecondsPerDay = 5;
			MaxFiles = 10000;

			ResolutionWidth = Screen.PrimaryScreen.Bounds.Width;
			ResolutionHeight = Screen.PrimaryScreen.Bounds.Height;

			ViewLogo = CheckState.Indeterminate;
			LogoFileName = null;
		}

		public static VisualizationSettings LoadFromFile(string fileName)
		{
			using (var reader = new StreamReader(fileName, false))
			{
				var visualizationSettingsSerializer = new XmlSerializer(typeof(VisualizationSettings));
				return (VisualizationSettings)visualizationSettingsSerializer.Deserialize(reader);
			}
		}

		public void SaveToFile(string fileName)
		{
			using (var writer = new StreamWriter(fileName, false))
			{
				var visualizationSettingsSerializer = new XmlSerializer(typeof(VisualizationSettings));
				visualizationSettingsSerializer.Serialize(writer, this);
			}
		}

		internal string TimeScaleAsString()
		{
			switch (TimeScale)
			{
				case TimeScale.Slow8: return "0.125";
				case TimeScale.Slow4: return "0.25";
				case TimeScale.Slow2: return "0.5";
				case TimeScale.Fast2: return "2";
				case TimeScale.Fast3: return "3";
				case TimeScale.Fast4: return "4";
				default: return null;
			}
		}
	}
}
