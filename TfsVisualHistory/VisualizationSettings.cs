using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using Sitronics.TfsVisualHistory.Utility;

namespace Sitronics.TfsVisualHistory
{
	[Serializable]
	public class VisualizationSettings
	{
		public enum TimeScaleOption
		{
			None,
			Slow8 = 1,
			Slow4 = 2,
			Slow2 = 3,
			Fast2 = 4,
			Fast3 = 5,
			Fast4 = 6
		}

		[SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		public enum PlayModeOption
		{
			History,
			Live,
			HistoryThenLive
		}

		public PlayModeOption PlayMode;

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
		public TimeScaleOption TimeScale;
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
			PlayMode = PlayModeOption.History;

			DateFrom = new DateTime(1900, 1, 1);
			DateTo = new DateTime(2099, 1, 1);

			UsersFilter = new StringFilter("*", "");
			FilesFilter = new StringFilter("*", "");

			ViewFileNames = false;
			ViewDirNames = true;
			ViewUserNames = true;
			ViewAvatars = true;

			TimeScale = TimeScaleOption.None;
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
				case TimeScaleOption.Slow8: return "0.125";
				case TimeScaleOption.Slow4: return "0.25";
				case TimeScaleOption.Slow2: return "0.5";
				case TimeScaleOption.Fast2: return "2";
				case TimeScaleOption.Fast3: return "3";
				case TimeScaleOption.Fast4: return "4";
				default: return null;
			}
		}
	}
}
