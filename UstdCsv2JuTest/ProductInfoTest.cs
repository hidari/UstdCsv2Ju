using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Hidari0415.UstdCsv2Ju.Tests
{
	[TestFixture]
	public class ProductInfoTest
	{
		private ProductInfo _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new ProductInfo();
		}

		[Test]
		public void GetProductInfomation()
		{
			_sut.Title.Is("UstdCsv2Ju");
			_sut.Description.Is("");
			_sut.Company.Is("hidari-lab.net");
			_sut.Product.Is("UstdCsv2Ju");
			_sut.Copyright.Is("Copyright Hidari0415 2014.");
			_sut.Trademark.Is("");
			_sut.VersionString.Is("0.1.1 β");
			_sut.Libraries.IsStructuralEqual(new List<Library>
			{
				new Library("CSVHelper", new Uri("http://joshclose.github.io/CsvHelper/"))
			});
		}
	}
}
