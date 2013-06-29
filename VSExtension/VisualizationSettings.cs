﻿using System;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using Sitronics.TfsVisualHistory.VSExtension.Utility;

namespace Sitronics.TfsVisualHistory.VSExtension
{
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public enum PlayModeOption
        {
            History,
            Live
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
        
        public int SecondsPerDay;
        public TimeScaleOption TimeScale;
        public int MaxFiles;
        public bool LoopPlayback;

        public bool FullScreen;
        public bool SetResolution;
        public int ResolutionWidth;
        public int ResolutionHeight;

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

            TimeScale = TimeScaleOption.None;
            SecondsPerDay = 5;
            MaxFiles = 1000;

            ResolutionWidth = Screen.PrimaryScreen.Bounds.Width;
            ResolutionHeight = Screen.PrimaryScreen.Bounds.Height;
        }

        public static VisualizationSettings LoadFromFile(string fileName)
        {
            using (var myReader = new StreamReader(fileName, false))
            {
                var xsSubmit = new XmlSerializer(typeof(VisualizationSettings));
                var settings = (VisualizationSettings)xsSubmit.Deserialize(myReader);
                return settings;
            }
        }

        public void SaveToFile(string fileName)
        {
            using (var myWriter = new StreamWriter(fileName, false))
            {
                var xsSubmit = new XmlSerializer(typeof(VisualizationSettings));
                xsSubmit.Serialize(myWriter, this);
            }
        }
   }
}
