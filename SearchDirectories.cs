using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace FileSearch
{
	public enum PriorityMode { LeastUsed, RecentUsed, UnknownManufacturer, CheckCert };

	class Search
	{
		public static List<FileDetails> SearchDirectories(
			bool isRecurse,
			IEnumerable<string> dirs,
			PriorityMode mode)
		{
			var searchOption = SearchOption.TopDirectoryOnly;
			if (isRecurse)
				searchOption = SearchOption.AllDirectories;

			var exeFilesInfo = from directory in dirs
							   from file in Directory.GetFiles(directory, "*.exe", searchOption)
								   let fInfo = new FileInfo(file)
								   let fPath = Path.GetFullPath(file)
								   let fVers = FileVersionInfo.GetVersionInfo(fPath)
							   select new FileDetails
							   {
								   Path = fPath,
								   LastAccess = fInfo.LastAccessTime,
								   Size = fInfo.Length,
								   CompanyName = fVers.CompanyName
							   };

			IEnumerable<FileDetails> interestingFiles = null;


			if (mode == PriorityMode.UnknownManufacturer)
			{

				interestingFiles = from fileInfo in exeFilesInfo
								   where !(fileInfo.CompanyName == "Microsoft")//TODO: Allow adding a company to whitelist
								   orderby fileInfo.LastAccess descending
								   select fileInfo;
			}

			else if(mode == PriorityMode.LeastUsed)
			{

				interestingFiles = from fileInfo in exeFilesInfo
								   orderby fileInfo.LastAccess ascending
								   select fileInfo;
			}

			else if(mode == PriorityMode.RecentUsed)
			{

				interestingFiles = from fileInfo in exeFilesInfo
								   orderby fileInfo.LastAccess descending
								   select fileInfo;
			}

			return interestingFiles.ToList();
		}

	}

}
