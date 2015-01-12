using System.IO;
using NUnit.Framework;

namespace Hidari0415.UstdCsv2Ju.Tests
{
	[TestFixture]
	public class ResultXmlWriterTests
	{
		[Test]
		public void InitResultXmlWriterTest()
		{
			var resultXmlWriter = new ResultXmlWriter("hoge.csv", 10, "hoge.xml");
			resultXmlWriter.InputCsv.Is("hoge.csv");
			resultXmlWriter.Threshold.Is(10);
			resultXmlWriter.OutputXml.Is("hoge.xml");
		}

		[Test]
		public void CreateFailedTestCaseTest()
		{
			var resutXmlWriter = new ResultXmlWriter("hoge.csv", 20, "hoge.xml");
			var jUnitStyleTestCase =
				resutXmlWriter.CreateTestCase(new MetricRecord()
				{
					Kind = "Public Function",
					Name = "DoSomething(int, int)",
					File = "src\\module\\hoge.cpp",
					Value = 100
				});

			var expected = new JUnitStyleTestCase
			{
				ClassName = "src.module.hoge_cpp",
				FailureElement = new JUnitStyleFailureElement("OverThresholdException", "Threshold: 20\r\nActual: 100\r\nOver: 80"),
				IsFailed = true,
				Name = "DoSomething(int, int)",
				Time = "0.00"
			};

			jUnitStyleTestCase.IsStructuralEqual(expected);
		}

		[Test]
		public void CreateSucceededTestCase()
		{
			var resultXmlWriter = new ResultXmlWriter("hoge.csv", 20, "hoge.xml");
			var jUnitStyleTestCase =
				resultXmlWriter.CreateTestCase(new MetricRecord()
				{
					Kind = "Public Function",
					Name = "DoSomething(int, int)",
					File = "src\\module\\hoge.cpp",
					Value = 19
				});

			var expected = new JUnitStyleTestCase
			{
				ClassName = "src.module.hoge_cpp",
				FailureElement = new JUnitStyleFailureElement(),
				IsFailed = false,
				Name = "DoSomething(int, int)",
				Time = "0.00"
			};

			jUnitStyleTestCase.IsStructuralEqual(expected);
		}

		[Test]
		public void NormalResultXmlTest()
		{
			File.WriteAllText("Ustd.csv", UstdCsv.NormalCsv);
			var resultXmlWriter = new ResultXmlWriter("Ustd.csv", 20, "Ustd.xml");
			resultXmlWriter.WriteResultFile();

			var actual = File.ReadAllText("Ustd.xml");
			const string expect = "<testsuite>\r\n  <testcase classname=\"src.module.hoge_cpp\" name=\"DoSomething(int, int)\" time=\"0.00\" />\r\n  <testcase classname=\"src.module.fuga_cpp\" name=\"GetSomething(LPCTSTR)\" time=\"0.00\">\r\n    <failure type=\"OverThresholdException\" message=\"Threshold: 20&#xD;&#xA;Actual: 45&#xD;&#xA;Over: 25\" />\r\n  </testcase>\r\n</testsuite>";
			actual.Is(expect);
		}
	}
}
