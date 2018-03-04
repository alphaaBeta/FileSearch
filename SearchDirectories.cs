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

			try
			{

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

			switch (mode)
				{
					case PriorityMode.UnknownManufacturer:
						interestingFiles = from fileInfo in exeFilesInfo
										   where !(fileInfo.CompanyName == "Microsoft")//TODO: Allow adding a company to whitelist
										   orderby fileInfo.LastAccess descending
										   select fileInfo;
						break;

					case PriorityMode.LeastUsed:
						interestingFiles = from fileInfo in exeFilesInfo
										   orderby fileInfo.LastAccess ascending
										   select fileInfo;
						break;

					case PriorityMode.RecentUsed:
						interestingFiles = from fileInfo in exeFilesInfo
										   orderby fileInfo.LastAccess descending
										   select fileInfo;
						break;
				}


				return interestingFiles.ToList();

			}
			catch (UnauthorizedAccessException e)
			{
				Console.WriteLine("Error: {0}\n" +
									"At: {1}\n Unauthorized Access", e.HResult, e.Source);
				return null;
			}



		}

	}

}
