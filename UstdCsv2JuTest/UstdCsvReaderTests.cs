using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using CsvHelper.TypeConversion;

namespace Hidari0415.UstdCsv2Ju.Tests
{
	public class UstdCsvReaderTests
	{
		[Test]
		public void ReadNormalUstdCsv()
		{
			File.WriteAllText("Ustd.csv", UstdCsv.NormalCsv);
			var actual = UstdCsvReader.ReadMetricRecords("Ustd.csv");
			var expect = new List<MetricRecord>
			{
				new MetricRecord {File = "src\\module\\hoge.cpp", Kind = "Public Function", Name = "DoSomething(int, int)", Value = 19},
				new MetricRecord {File = "src\\module\\fuga.cpp", Kind = "Private Function", Name = "GetSomething(LPCTSTR)", Value = 45}
			};

			actual.IsStructuralEqual(expect);
			
		}

		[Test]
		public void ReadingIncorrectCsvThrowsException()
		{
			File.WriteAllText("Ustd.csv", UstdCsv.IncorrectCsv);
			Assert.Throws(typeof(CsvTypeConverterException), () => UstdCsvReader.ReadMetricRecords("Ustd.csv"));
			
		}
	}
}
