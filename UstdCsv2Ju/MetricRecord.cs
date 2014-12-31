namespace Hidari0415.UstdCsv2Ju
{
	
	public class MetricRecord
	{
		public MetricRecord()
		{
			Kind = string.Empty;
			Name = string.Empty;
			File = string.Empty;
			Value = 0;
		}

		public string Kind { get; set; }
		public string Name { get; set; }
		public string File { get; set; }
		public int Value { get; set; }
	}
}
