namespace Sitronics.TfsVisualHistory.VSExtension.Utility
{
    public class StringFilter
    {
        public string IncludeMask { get; set; }
        public string ExcludeMask { get; set; }

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
