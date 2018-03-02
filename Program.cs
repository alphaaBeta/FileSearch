using System;
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
			
			if (args.Length < 2)
			{
				ShowUsage();
				return;
			}

			int directoryIndex = 1;
			bool isRecurse = false;

			if (args[1] == "-recurse" || args[1] == "-r")
			{
				directoryIndex = 2;
				isRecurse = true;
			}

			if (args[0] == "-A")
			{
				
				
					//set ..
			}
			//else if args[0] ==...

			var directoriesToSearch = args.Skip(directoryIndex);

			List<FileDetails> filesFound = SearchDirectories(isRecurse, directoriesToSearch);

			
		}

		static void ShowUsage()
		{
			Console.WriteLine(	"Usage: \n" +
								"FileSearch [OPTION] [-recurse|-r] [DIRECTORIES]\n" +
								"Start search of a file with given option: \n" +
								"-A - show exes in order from least used to most recently used\n....")

		}
	}
}
