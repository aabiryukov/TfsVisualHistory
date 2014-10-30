using System;

namespace Sitronics.TfsVisualHistory.Utility
{
	[Serializable]
	public class StringFilter
	{
		public string IncludeMask { get; set; }
		public string ExcludeMask { get; set; }

		// Necessary for serialization
		public StringFilter()
		{
		}

		public StringFilter(string includeMask, string excludeMask)
		{
			IncludeMask = includeMask;
			ExcludeMask = excludeMask;
		}
	}
}
