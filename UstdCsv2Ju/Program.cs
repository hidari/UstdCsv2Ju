using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hidari0415.UstdCsv2Ju
{
	class Program
	{
		static int Main(string[] args)
		{
			//TODO: 引数に --version を加えて使用ライブラリを表示する
			var options = new HashSet<string> { "--input-csv", "--threshold", "--output-xml", "--help" };

			// Thank you for http://neue.cc/2009/12/13_229.html
			var key = string.Empty;
			var result = args
				.GroupBy(s => options.Contains(s) ? key = s : key)
				.ToDictionary(g => g.Key, g => g.Skip(1).FirstOrDefault());

			// Is "--help" argument contained?
			if (result.ContainsKey("--help"))
			{
				ShowHelp();
				return 0;
			}

			// Are Required arguments contained?
			if (!result.ContainsKey("--input-csv") ||
				!result.ContainsKey("--threshold") || 
				!result.ContainsKey("--output-xml"))
			{
				ShowHelp();
				return 0;
			}

			// Validate arguments.
			var inputCsv = result["--input-csv"];
			if (!File.Exists(inputCsv))
			{
				Console.WriteLine("{0} is not found.", inputCsv);
				return 0;
			}

			int threshold;
			if (!int.TryParse(result["--threshold"], out threshold))
			{
				Console.WriteLine("Threshold must be string that represent 32-bit signed integer.");
				return 0;
			}

			var outputXml = result["--output-xml"];

			// Execute
			var resultWriter = new ResultXmlWriter(inputCsv, threshold, outputXml);
			resultWriter.WriteResultFile();

			return 0;
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
");
		}
	}
}
