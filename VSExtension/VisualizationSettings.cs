using System;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

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

        public string IncludeUsers;
        public string ExcludeUsers;

        public string IncludeFiles;
        public string ExcludeFiles;

		public DateTime DateFrom { get; set; }
		public DateTime DateTo { get; set; }

        public bool HideFileNames;
        public bool HideDirNames;
        
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
            DateFrom = new DateTime(1900, 1, 1);
            DateTo = new DateTime(2099, 1, 1);

            IncludeUsers = "*";
            ExcludeUsers = "";

            IncludeFiles = "*";
            ExcludeFiles = "";

            HideFileNames = true;
            TimeScale = TimeScaleOption.None;
            SecondsPerDay = 10;
            MaxFiles = 1000;

            ResolutionWidth = Screen.PrimaryScreen.Bounds.Width;
            ResolutionHeight = Screen.PrimaryScreen.Bounds.Height;
        }

        public static VisualizationSettings LoadFromFile(string fileName)
        {
            using (var myReader = new StreamReader(fileName, false))
            {
                var xsSubmit = new XmlSerializer(typeof(VisualizationSettings));
                //                XmlWriter writer = XmlWriter.Create(myWriter);
                var settings = (VisualizationSettings)xsSubmit.Deserialize(myReader);
                return settings;
            }
        }
/*
        private static void AddParameter(XmlElement root, string parameterName, string value)
        {
            var node = root.OwnerDocument.CreateElement(parameterName);
            node.InnerText = value;
            root.AppendChild(node);
        }
*/
        public void SaveToFile(string fileName)
        {
/*
            // Create the xml document containe
            var doc = new XmlDocument();// Create the XML Declaration, and append it to XML document
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);// Create the root element
            var root = doc.CreateElement("VisualizationSettings");
            doc.AppendChild(root);

            AddParameter(root, "xxx", "xxx");
 */
            using (var myWriter = new StreamWriter(fileName, false))
            {
                var xsSubmit = new XmlSerializer(typeof(VisualizationSettings));
//                XmlWriter writer = XmlWriter.Create(myWriter);
                xsSubmit.Serialize(myWriter, this);
            }
        }
   }
}
