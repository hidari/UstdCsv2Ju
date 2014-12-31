using NUnit.Framework;

namespace Hidari0415.UstdCsv2Ju.Tests
{
	public class JUnitStyleTestCaseTests
	{
		[Test]
		public void NoArgumentInitTest()
		{
			var jUnitStyleTestCase = new JUnitStyleTestCase();
			jUnitStyleTestCase.ClassName.Is("");
			jUnitStyleTestCase.Name.Is("");
			jUnitStyleTestCase.Time.Is("");
			jUnitStyleTestCase.IsFailed.Is(false);
			jUnitStyleTestCase.FailureElement.IsStructuralEqual(new JUnitStyleFailureElement());
		}

		[Test]
		public void ThreeArgumentsInitTest()
		{
			var jUnitStyleTestCase = new JUnitStyleTestCase("classname", "name", "time");
			jUnitStyleTestCase.ClassName.Is("classname");
			jUnitStyleTestCase.Name.Is("name");
			jUnitStyleTestCase.Time.Is("time");
			jUnitStyleTestCase.IsFailed.Is(false);
			jUnitStyleTestCase.FailureElement.IsStructuralEqual(new JUnitStyleFailureElement());
		}
	}
}
