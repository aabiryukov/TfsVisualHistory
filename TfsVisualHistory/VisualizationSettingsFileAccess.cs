using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sitronics.TfsVisualHistory
{
	internal class VisualizationSettingsFileAccess
	{
		public static VisualizationSettings Read(string fileName)
		{
			using (var reader = new StreamReader(fileName, false))
			{
				var visualizationSettingsSerializer = new XmlSerializer(typeof(VisualizationSettings));
				return (VisualizationSettings)visualizationSettingsSerializer.Deserialize(reader);
			}
		}

		public static void Save(VisualizationSettings visualizationSettings, string fileName)
		{
			using (var writer = new StreamWriter(fileName, false))
			{
				var visualizationSettingsSerializer = new XmlSerializer(typeof(VisualizationSettings));
				visualizationSettingsSerializer.Serialize(writer, visualizationSettings);
			}
		}
	}
}
