using System;
using System.Globalization;
using System.Windows.Forms;

namespace Sitronics.TfsVisualHistory
{
	internal class ArgumentGenerator
	{
		private readonly VisualizationSettings _visualizationSettings;
		private readonly PathProvider _pathProvider;

		public ArgumentGenerator(VisualizationSettings visualizationSettings, PathProvider pathProvider)
		{
			if (visualizationSettings == null) throw new ArgumentNullException("visualizationSettings");
			if (pathProvider == null) throw new ArgumentNullException("pathProvider");

			_visualizationSettings = visualizationSettings;
			_pathProvider = pathProvider;
		}

		internal string Generate(string sourceControlFolder)
		{
			switch (_visualizationSettings.PlayMode)
			{
				case PlayMode.History:
					return GenerateForHistory(sourceControlFolder);
				case PlayMode.Live:
					return GenerateForLive(sourceControlFolder);
				case PlayMode.HistoryThenLive:
					return GenerateForHistoryThenLive(sourceControlFolder);
				default:
					throw new NotImplementedException();
			}
		}

		private string GenerateForHistory(string sourceControlFolder)
		{
			return
				AddLogFile() +
				AddSecondsPerDay() +
				AddTimeScale() +
				AddLoop() +
				GenerateCommon(sourceControlFolder);
		}

		private string GenerateForHistoryThenLive(string sourceControlFolder)
		{
			return
				" --log-format custom -" +
				AddSecondsPerDay() +
				AddTimeScale() +
				GenerateCommon(sourceControlFolder);
		}

		private string GenerateForLive(string sourceControlFolder)
		{
			return
				" --realtime --log-format custom -" +
				GenerateCommon(sourceControlFolder);
		}

		private string GenerateCommon(string sourceControlFolder)
		{
			return
				AddFileIdleTime() +
				AddHighlightUsersAndTitle(sourceControlFolder) +
				AddLogoFile() +
				AddFullScreen() +
				SetResolution() +
				AddKey() +
				AddAvatarsDirectory() +
				AddHideItems() +
				AddMaxFiles() +
				AddDisableBloom();
		}

		private string AddLogFile()
		{
			return string.Format(CultureInfo.InvariantCulture, " \"{0}\" ", _pathProvider.LogFilePath);
		}

		private string AddHighlightUsersAndTitle(string sourceControlFolder)
		{
			var title = string.Empty;

			switch (_visualizationSettings.PlayMode)
			{
				case PlayMode.History:
					title = "History of " + sourceControlFolder;
					break;
				case PlayMode.Live:
					title = "Live changes of " + sourceControlFolder;
					break;
				case PlayMode.HistoryThenLive:
					title = "Live changes of " + sourceControlFolder + " with history";
					break;
			}

			return string.Format(CultureInfo.InvariantCulture, " --highlight-users --title \"{0}\"", title);
		}

		private string AddMaxFiles()
		{
			return " --max-files " + _visualizationSettings.MaxFiles.ToString(CultureInfo.InvariantCulture);
		}

		private string AddDisableBloom()
		{
			return SystemInformation.TerminalServerSession ? " --disable-bloom" : string.Empty;
		}

		private string AddKey()
		{
			if (_visualizationSettings.ViewFilesExtentionMap)
			{
				return " --key";
			}

			return string.Empty;
		}

		private string AddFullScreen()
		{
			var value = string.Empty;

			if (_visualizationSettings.FullScreen)
			{
				value += " --fullscreen";

				// By default gource not using full area of screen width ( It's a bug. Must be fixed in gource 0.41).
				// Fixing fullscreen resolution to real full screen.
				if (!_visualizationSettings.SetResolution)
				{
					var screenBounds = Screen.PrimaryScreen.Bounds;

					value += string.Format(
						CultureInfo.InvariantCulture,
						" --viewport {0}x{1}",
						screenBounds.Width,
						screenBounds.Height);
				}
			}

			return value;
		}

		private string AddAvatarsDirectory()
		{
			if (_visualizationSettings.ViewAvatars)
			{
				return string.Format(CultureInfo.InvariantCulture, " --user-image-dir \"{0}\"", _pathProvider.AvatarsDirectoryPath);
			}

			return string.Empty;
		}

		private string AddHideItems()
		{
			// Process "--hide" option
			var hideItems = string.Empty;

			if (!_visualizationSettings.ViewDirNames)
			{
				hideItems = "dirnames";
			}

			if (!_visualizationSettings.ViewFileNames)
			{
				if (hideItems.Length > 0)
					hideItems += ",";

				hideItems += "filenames";
			}

			if (!_visualizationSettings.ViewUserNames)
			{
				if (hideItems.Length > 0)
					hideItems += ",";

				hideItems += "usernames";
			}

			if (hideItems.Length > 0)
				return " --hide " + hideItems;

			return string.Empty;
		}

		private string SetResolution()
		{
			if (_visualizationSettings.SetResolution)
			{
				return string.Format(
					CultureInfo.InvariantCulture,
					" --viewport {0}x{1}",
					_visualizationSettings.ResolutionWidth,
					_visualizationSettings.ResolutionHeight);
			}

			return string.Empty;
		}

		private string AddLoop()
		{
			return _visualizationSettings.LoopPlayback ? " --loop" : string.Empty;
		}

		private string AddSecondsPerDay()
		{
			return " --seconds-per-day " + _visualizationSettings.SecondsPerDay.ToString(CultureInfo.InvariantCulture);
		}

		private string AddLogoFile()
		{
			if (_visualizationSettings.ViewLogo == CheckState.Unchecked)
				return string.Empty;

			var logoFile = _visualizationSettings.ViewLogo == CheckState.Indeterminate
				? _pathProvider.LogoFilePath
				: _visualizationSettings.LogoFileName;

			return string.Format(CultureInfo.InvariantCulture, " --logo \"{0}\"", logoFile);
		}

		private string AddTimeScale()
		{
			if (_visualizationSettings.TimeScale != TimeScale.None)
			{
				string optionValue;

				switch (_visualizationSettings.TimeScale)
				{
					case TimeScale.Slow8: optionValue = "0.125";
						break;
					case TimeScale.Slow4: optionValue = "0.25";
						break;
					case TimeScale.Slow2: optionValue = "0.5";
						break;
					case TimeScale.Fast2: optionValue = "2";
						break;
					case TimeScale.Fast3: optionValue = "3";
						break;
					case TimeScale.Fast4: optionValue = "4";
						break;
					default: throw new NotImplementedException();
				}

				return " --time-scale " + optionValue;
			}

			return string.Empty;
		}

		private string AddFileIdleTime()
		{
			// 60 is default in gource 0.40 and older. Since 0.41 default 0. 28800 is 8 hours (one work day).
			return " --file-idle-time " + _visualizationSettings.FileIdleTime;
		}
	}
}
