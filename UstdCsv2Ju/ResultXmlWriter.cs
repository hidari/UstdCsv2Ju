using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using CsvHelper;

namespace Hidari0415.UstdCsv2Ju
{
	internal class ResultXmlWriter
	{
		public string InputCsv { get; private set; }
		public int Threshold { get; private set; }
		public string OutputXml { get; private set; }

		public ResultXmlWriter(string inputCsv, int threshold, string outputXml)
		{
			InputCsv = inputCsv;
			Threshold = threshold;
			OutputXml = outputXml;
		}

		internal void WriteResultFile()
		{
			List<MetricRecord> records;

			using (var reader = new CsvReader(new StreamReader(InputCsv, Encoding.Default)))
			{
				records = reader.GetRecords<MetricRecord>().ToList();
			}

			var result = new List<JUnitStyleTestCase>();

			foreach (var metricRecord in records)
			{
				var testCase = new JUnitStyleTestCase()
				{
					ClassName = metricRecord.File.Replace('.', '_').Replace('\\', '.'),
					Name = metricRecord.Name,
					Time = "0.00"
				};

				if (metricRecord.Value > Threshold)
				{
					testCase.FailureElement = new JUnitStyleFailureElement(
						"Over threshold.",
						"Value is " + (metricRecord.Value - Threshold) + " over."
					);
					testCase.IsFailed = true;
				}
				else
				{
					testCase.FailureElement = new JUnitStyleFailureElement();
				}

				result.Add(testCase);
			}

			var xmlDocument = new XmlDocument();
			var testSuite = xmlDocument.CreateElement("testsuite");
			xmlDocument.AppendChild(testSuite);

			foreach (var jUnitStyleTestCase in result)
			{
				var testCase = xmlDocument.CreateElement("testcase");
				testCase.SetAttribute("classname", jUnitStyleTestCase.ClassName);
				testCase.SetAttribute("name", jUnitStyleTestCase.Name);
				testCase.SetAttribute("time", jUnitStyleTestCase.Time);

				if (jUnitStyleTestCase.IsFailed)
				{
					var failure = xmlDocument.CreateElement("failure");
					failure.SetAttribute("type", jUnitStyleTestCase.FailureElement.Type);
					failure.SetAttribute("message", jUnitStyleTestCase.FailureElement.Message);
					testCase.AppendChild(failure);
				}

				testSuite.AppendChild(testCase);
			}

			xmlDocument.Save(OutputXml);
		}
	}
}
