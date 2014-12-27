using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Hidari0415.UstdCsv2Ju
{
	class Program
	{
		private static readonly ProductInfo productInfo = new ProductInfo();
		private static bool _isExistDummy;
		static int Main(string[] args)
		{
			var options = new HashSet<string> { "--input-csv", "--threshold", "--output-xml", "--help", "--version" };

			// Thank you for http://neue.cc/2009/12/13_229.html
			var key = string.Empty;
			var argsDict = args
				.GroupBy(s => options.Contains(s) ? key = s : key)
				.ToDictionary(g => g.Key, g => g.Skip(1).FirstOrDefault());

			// Is "--help" argument contained?
			if (argsDict.ContainsKey("--help"))
			{
				ShowHelp();
				return 0;
			}

			// Is "--version" argument contained?
			if (argsDict.ContainsKey("--version"))
			{
				ShowVersion();
				return 0;
			}

			// Are Required arguments contained?
			if (!argsDict.ContainsKey("--input-csv") ||
				!argsDict.ContainsKey("--threshold") || 
				!argsDict.ContainsKey("--output-xml"))
			{
				ShowHelp();
				return 0;
			}

			// Validate arguments.
			var inputCsv = argsDict["--input-csv"];
			if (!File.Exists(inputCsv))
			{
				Console.WriteLine("{0} is not found.", inputCsv);
				return 0;
			}

			int threshold;
			if (!int.TryParse(argsDict["--threshold"], out threshold))
			{
				Console.WriteLine("Threshold must be string that represent 32-bit signed integer.");
				return 0;
			}

			var outputXml = argsDict["--output-xml"];

			// powershellで -NoTypeInformation を付けずに出力されたCSVの一行目を除去し一時ファイルを作る。
			if (IsExistTypeInfo(inputCsv))
			{
				inputCsv = CreateTemporaryFile(inputCsv);
				_isExistDummy = true;
			}

			// Execute
			var resultWriter = new ResultXmlWriter(inputCsv, threshold, outputXml);
			resultWriter.WriteResultFile();

			// delete temp file if exists.
			if (_isExistDummy)
			{
				File.Delete(inputCsv);
			}

			return 0;
		}

		private static string CreateTemporaryFile(string path)
		{
			var tmpCsv = File.ReadLines(path).Skip(1);
			File.WriteAllLines(path + ".tmp", tmpCsv);
			return path + ".tmp";
		}

		private static bool IsExistTypeInfo(string path)
		{
			string firstLine;
			using (var reader = new StreamReader(path, Encoding.Default))
			{
				firstLine = reader.ReadLine();
			}

			return firstLine != null && firstLine.Contains("#TYPE");
		}

		private static void ShowVersion()
		{
			Console.WriteLine(@" 
{0}
Version: {1}
Project Home: https://github.com/hidari/UstdCsv2Ju

{0} is OSS released under The MIT License.
{2}

Libraries:
{3}
", productInfo.Title, productInfo.VersionString, productInfo.Copyright, GetExternalLibraries());
		}

		private static string GetExternalLibraries()
		{
			var libraries = new StringBuilder();
			foreach (var library in productInfo.Libraries)
			{
				libraries.Append(string.Format("  {0}({1})\r\n", library.Name, library.Uri));
			}

			return libraries.ToString();
		}

		/// <summary>
		/// Show help message.
		/// </summary>
		private static void ShowHelp()
		{
			Console.WriteLine(@"
USAGE:

UstdCsv2Ju.exe --input-csv <CSV_FILE> --threshold <NUMBER> --output-xml <XML_FILE>

  Export JUnit style XML file. Exported XML can be used by CI tool like Jenkins. (All argument is need to be specified.)

UstdCsv2Ju.exe --help

  Show this message. If you set invalid argument, this program shows this message too.

UstdCsv2Ju.exe --version

  Show message about UstdCsv2Ju.
");
		}
	}
}
