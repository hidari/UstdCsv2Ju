using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Hidari0415.UstdCsv2Ju
{
	internal static class UstdCsvReader
	{
		/// <summary>
		/// Read Metric data from CSV file.
		/// </summary>
		/// <param name="path">a CSV file path</param>
		/// <returns>All data that read from CSV file</returns>
		public static List<MetricRecord> ReadMetricRecords(string path)
		{
			List<MetricRecord> records = null;

			using (var reader = new CsvReader(new StreamReader(path, Encoding.Default)))
			{
				reader.Configuration.RegisterClassMap<MetricRecordMap>();

				try
				{
					records = reader.GetRecords<MetricRecord>().ToList();
				}
				catch (CsvTypeConverterException ex)
				{
					Console.WriteLine("Failed to read CSV file because of :");
					Console.WriteLine(ex.Message);
				}
			}

			return records ?? new List<MetricRecord>() ;
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
