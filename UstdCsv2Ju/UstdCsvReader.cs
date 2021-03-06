﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

namespace Hidari0415.UstdCsv2Ju
{
	public static class UstdCsvReader
	{
		/// <summary>
		/// Read Metric data from CSV file.
		/// </summary>
		/// <param name="path">a CSV file path</param>
		/// <returns>All data that read from CSV file</returns>
		public static List<MetricRecord> ReadMetricRecords(string path)
		{
			List<MetricRecord> records;

			using (var reader = new CsvReader(new StreamReader(path, Encoding.Default)))
			{
				reader.Configuration.RegisterClassMap<MetricRecordMap>();

				records = reader.GetRecords<MetricRecord>().ToList();
			}

			return records;
		}
	}

	internal sealed class MetricRecordMap : CsvClassMap<MetricRecord>
	{
		public MetricRecordMap()
		{
			Map(m => m.Kind).Index(0);
			Map(m => m.Name).Index(1);
			Map(m => m.File).Index(2);
			Map(m => m.Value).Index(3);
		}
	}
}
