using System.Linq;
using System.Xml;

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
			// CSVファイルを読み込んでレコードをMetricRecordのリストとして保持する
			var records = UstdCsvReader.ReadMetricRecords(InputCsv);

			// 読み込んだレコードをXMLの元になるJUnitStyleTestCaseに詰め込む
			var result = records.Select(CreateTestCase).ToList();

			// TODO: メソッドに切り分けるほうが良さそう
			// XmlDocumentを構築
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

			// 構築したXmlDocumentをファイルに書き出す
			xmlDocument.Save(OutputXml);
		}

		private JUnitStyleTestCase CreateTestCase(MetricRecord metricRecord)
		{
			var testCase = new JUnitStyleTestCase()
			{
				ClassName = metricRecord.File.Replace('.', '_').Replace('\\', '.'),
				Name = metricRecord.Name,
				Time = "0.00"
			};

			// Failureを判定
			if (metricRecord.Value > Threshold)
			{
				testCase.FailureElement = new JUnitStyleFailureElement(
					"OverThresholdException",
					CreateErrorDetailMessage(metricRecord)
				);

				testCase.IsFailed = true;
			}
			else
			{
				testCase.FailureElement = new JUnitStyleFailureElement();
			}

			return testCase;
		}

		private string CreateErrorDetailMessage(MetricRecord metricRecord)
		{
			const string messageFormat = @"Threshold: {0}
Actual:{1}
Over: {2}";

			return string.Format(
				messageFormat,
				Threshold,
				metricRecord.Value,
				(metricRecord.Value - Threshold)
			);
		}
	}
}
