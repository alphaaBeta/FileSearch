using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileSearch
{
	class CompanyWhitelist
	{
		List<string> _companies = new List<string>();

		public List<string> Companies
		{
			get
			{
				return _companies;
			}

			private set
			{	}

		}

		public CompanyWhitelist(string fileName)
		{
			ReadCompanies(fileName);
		}

		public CompanyWhitelist()
		{

		}

		public void AddCompany(string companyName)
		{
			Companies.Add(companyName);
		}

		public void ReadCompanies(string fileName)
		{

			StreamReader fileStream;

			try
			{
				fileStream = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), fileName));
			}
			catch(FileNotFoundException e)
			{
				Console.WriteLine("Error:{0} :{1}",e.HResult, e.Message);
				return;
			}
			
			while(!fileStream.EndOfStream)
			{
				AddCompany(fileStream.ReadLine());
			}

			fileStream.Close();
		}

		public void SaveCompanies(string fileName)
		{
			StreamWriter fileStream;

			try
			{
				fileStream = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), fileName), true);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return;
			}

			foreach (var company in Companies)
			{ 
				fileStream.WriteLine(company);
			}

			fileStream.Close();

		}
	}

}
