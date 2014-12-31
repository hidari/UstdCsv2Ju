using NUnit.Framework;

namespace Hidari0415.UstdCsv2Ju.Tests
{
	public class MetricRecordTests
	{
		[Test]
		public void InitMetricRecord()
		{
			var metricRecord = new MetricRecord();
			metricRecord.Kind.Is("");
			metricRecord.Name.Is("");
			metricRecord.File.Is("");
			metricRecord.Value.Is(0);
		}
	}
}
