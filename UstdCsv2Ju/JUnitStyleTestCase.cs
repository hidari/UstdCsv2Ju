﻿namespace Hidari0415.UstdCsv2Ju
{
	internal class JUnitStyleTestCase
	{
		public JUnitStyleTestCase():this(string.Empty, string.Empty, string.Empty)
		{
			
		}

		public JUnitStyleTestCase(string className, string name, string time)
		{
			ClassName = className;
			Name = name;
			Time = time;
		}

		public string ClassName { get; set; }
		public string Name { get; set; }
		public string Time { get; set; }
		public bool IsFailed { get; set; }
		public JUnitStyleFailureElement FailureElement { get; set; }
	}
}
