using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;

namespace Hidari0415.UstdCsv2Ju
{
	internal class UstdCsvReader
	{
		public static List<MetricRecord> ReadMetricRecords(string path)
		{
			List<MetricRecord> records;

			using (var reader = new CsvReader(new StreamReader(path, Encoding.Default)))
			{
				records = reader.GetRecords<MetricRecord>().ToList();
			}

			return records;
		}

	}
}
