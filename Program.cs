﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileSearch
{
	class Program
	{
		static void Main(string[] args)
		{
			
			if (args.Length < 1)
			{
				ShowUsage();
				return;
			}

			if(args[0].Equals("-test"))
			{
				Tests.CompanyWhitelistTest();
				return;
			}

			int directoryIndex = 1;
			bool isRecurse = false;
			PriorityMode mode = PriorityMode.UnknownManufacturer;


			CompanyWhitelist companyWhitelist = null;

			if (args.Length >= 2)
			{
				if (args[1] == "-recurse" || args[1] == "-r")
				{
					directoryIndex = 2;
					isRecurse = true;
				}
			}

			switch(args[0])
			{
				case ("-A"):
					mode = PriorityMode.UnknownManufacturer;
					companyWhitelist = new CompanyWhitelist("whitelist.txt");
					break;

				case ("-R"):
					mode = PriorityMode.RecentUsed;
					break;

				case ("-L"):
					mode = PriorityMode.LeastUsed;
					break;
			}

			var directoriesToSearch = (args.Skip(directoryIndex)).ToList();
			if (!directoriesToSearch.Any())
				directoriesToSearch.Add(Directory.GetCurrentDirectory());
			

			List<FileDetails> filesFound = Search.SearchDirectories(isRecurse, directoriesToSearch, mode, companyWhitelist);

			Print(filesFound);

			return;
			
		}

		static void ShowUsage()
		{
			Console.WriteLine("Usage: \n" +
								"FileSearch [OPTION] [-recurse|-r] [DIRECTORIES]\n" +
								"Start search of a file with given option: \n" +
								"-A - show exes in order from least used to most recently used\n....");

		}

		static void Print( List<FileDetails> list)
		{
			Console.WriteLine("Number\tName\tLast Used\tSize\tCompany\tPath");
			int i = 0;
			foreach(var ffile in list)
			{
				Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", i++, Path.GetFileNameWithoutExtension(ffile.Path),
																ffile.LastAccess, ffile.Size,
																ffile.CompanyName, ffile.Path);				
			}
		}
	}
}
