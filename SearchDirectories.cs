using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace FileSearch
{
	class SearchDirs
	{
		public static List<FileDetails> SearchDirectories(
			bool isRecurse,
			IEnumerable<string> dirs)
		{
			var searchOption = SearchOption.TopDirectoryOnly;
			if (isRecurse)
				searchOption = SearchOption.AllDirectories;

			var exeFiles = from directory in dirs
						   from file in Directory.GetFiles(directory, "*.exe", searchOption)
						   select file;

			var interestingFiles = from file in exeFiles
										let fInfo = new FileInfo(file)
										let fPath = Path.GetFullPath(file)
										let fVers = FileVersionInfo.GetVersionInfo(fPath)
								   where !(fVers.CompanyName=="Microsoft")
								   orderby fInfo.LastAccessTime ascending
								   select new FileDetails
								   {
									   Path = fPath,
									   LastAccess = fInfo.LastAccessTime,
									   Size = fInfo.Length
								   };

			return interestingFiles.ToList();

		}

	}
}
