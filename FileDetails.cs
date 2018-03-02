using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace FileSearch
{
	class FileDetails
	{
		public string Path { get; set; }
		public DateTime LastAccess { get; set; }
		public long Size { get; set; }
		public string CompanyName { get; set; }
	}
}
