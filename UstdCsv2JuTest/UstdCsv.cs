namespace Hidari0415.UstdCsv2Ju.Tests
{
	public class UstdCsv
	{
		public static readonly string NormalCsv = "Kind,Name,File,Value\r\nPublic Function,\"DoSomething(int, int)\",src\\module\\hoge.cpp,19\r\nPrivate Function,GetSomething(LPCTSTR),src\\module\\fuga.cpp,45";
		public static readonly string IncorrectCsv = "Kind,Name,File,Value\r\nPublic Function,\"DoSomething(int, int)\",src\\module\\hoge.cpp,HOGE";
	}
}
