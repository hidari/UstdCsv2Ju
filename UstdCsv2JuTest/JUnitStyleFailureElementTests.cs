using NUnit.Framework;

namespace Hidari0415.UstdCsv2Ju.Tests
{
	public class JUnitStyleFailureElementTests
	{
		[SetUp]
		public void SetUp()
		{
			
		}

		[Test]
		public void NoArgumentInitTest()
		{
			var jUnitFailureElement = new JUnitStyleFailureElement();
			jUnitFailureElement.Message.Is("");
			jUnitFailureElement.Type.Is("");
		}

		[Test]
		public void SingleArgumentInitTest()
		{
			var jUnitFailureElement = new JUnitStyleFailureElement("type");
			jUnitFailureElement.Message.Is("");
			jUnitFailureElement.Type.Is("type");
		}

		[Test]
		public void TwoArgumentsInitTest()
		{
			var jUnitFailureElement = new JUnitStyleFailureElement("type", "message");
			jUnitFailureElement.Message.Is("message");
			jUnitFailureElement.Type.Is("type");
		}
	}
}
