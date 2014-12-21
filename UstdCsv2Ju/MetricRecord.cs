namespace Hidari0415.UstdCsv2Ju
{
	internal class MetricRecord
	{
		//TODO: たぶんこのKindは必要ない。CSVHelperのConfigurationにIndex指定して読み込むのが良さそう
		public string Kind { get; set; }
		public string Name { get; set; }
		public string File { get; set; }
		public int Value { get; set; }
	}
}
