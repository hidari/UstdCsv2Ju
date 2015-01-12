using NUnit.Framework;

namespace Hidari0415.UstdCsv2Ju.Tests
{
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
	}
}
