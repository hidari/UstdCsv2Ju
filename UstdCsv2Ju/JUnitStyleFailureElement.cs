namespace Hidari0415.UstdCsv2Ju
{
	internal class JUnitStyleFailureElement
	{
		public JUnitStyleFailureElement()
			: this(string.Empty, string.Empty)
		{
		}

		public JUnitStyleFailureElement(string type)
			: this(type, string.Empty)
		{
		}

		public JUnitStyleFailureElement(string type, string message)
		{
			Type = type;
			Message = message;
		}

		public string Type { get; private set; }
		public string Message { get; private set; }
	}
}
