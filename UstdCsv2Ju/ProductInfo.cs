using System;
using System.Collections.Generic;
using System.Reflection;

namespace Hidari0415.UstdCsv2Ju
{
	public class ProductInfo
	{
		private readonly Assembly assembly = Assembly.GetExecutingAssembly();
		private string _Title;
		private string _Description;
		private string _Company;
		private string _Product;
		private string _Copyright;
		private string _Trademark;
		private Version _Version;
		private string _VersionString;
		private IReadOnlyCollection<Library> _Libraries;

		public string Title
		{
			get
			{
				return this._Title ?? 
					(this._Title = ((AssemblyTitleAttribute) Attribute.GetCustomAttribute(this.assembly, typeof (AssemblyTitleAttribute))).Title);
			}
		}

		public string Description
		{
			get 
			{ 
				return this._Description ??
					(this._Description = ((AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(this.assembly, typeof(AssemblyDescriptionAttribute))).Description);
			}
		}

		public string Company
		{
			get
			{
				return this._Company ?? 
					(this._Company = ((AssemblyCompanyAttribute) Attribute.GetCustomAttribute(this.assembly, typeof(AssemblyCompanyAttribute))).Company);
			}
		}

		public string Product
		{
			get
			{
				return this._Product ?? 
					(this._Product = ((AssemblyProductAttribute)Attribute.GetCustomAttribute(this.assembly, typeof(AssemblyProductAttribute))).Product);
			}
		}

		public string Copyright
		{
			get
			{
				return this._Copyright ??
					(this._Copyright = ((AssemblyCopyrightAttribute) Attribute.GetCustomAttribute(this.assembly, typeof (AssemblyCopyrightAttribute))).Copyright);
			}
		}

		public string Trademark
		{
			get
			{
				return this._Trademark ??
				       (this._Trademark = ((AssemblyTrademarkAttribute)Attribute.GetCustomAttribute(this.assembly, typeof (AssemblyTrademarkAttribute))).Trademark);
			}
		}

		public Version Version
		{
			get { return this._Version ?? (this._Version = assembly.GetName().Version); }
		}

		public string VersionString
		{
			get
			{
				return this._VersionString ??
				       (this._VersionString = string.Format(
						   "{0}{1}{2}",
						   this.Version.ToString(3), 
						   this.IsBataRelease ? " β" : "",
						   this.Version.Revision == 0 ? "" : " rev." + this.Version.Revision));
			}
		}

		public bool IsBataRelease
		{
#if BETA
			get { return true; }
#else
			get { return false; }
#endif
		}

		public IReadOnlyCollection<Library> Libraries
		{
			get
			{
				return this._Libraries ?? (this._Libraries = new List<Library>
				{
					new Library("CSVHelper", new Uri("http://joshclose.github.io/CsvHelper/"))
				});
			}
		}
	}

	public class Library
	{
		public string Name { get; private set; }
		public Uri Uri { get; private set; }
			
		public Library(string name, Uri uri)
		{
			this.Name = name;
			this.Uri = uri;
		}
	}
}
