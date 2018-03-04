using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileSearch
{
	public abstract class Tests
	{

		public static void CompanyWhitelistTest()
		{
			CompanyInfo company1 = new CompanyInfo("XYZ");
			CompanyInfo company2 = new CompanyInfo("123");

			CompanyWhitelist companyWhitelist = new CompanyWhitelist();

			companyWhitelist.AddCompany(company1);
			companyWhitelist.AddCompany(company2);

			if (!companyWhitelist.Companies.Contains(company1) || !companyWhitelist.Companies.Contains(company2))
				throw new Exception("Test failed");

			string file = Path.GetRandomFileName();
			companyWhitelist.SaveCompanies(file);

			CompanyWhitelist otherCompanyWhitelist = new CompanyWhitelist();
			otherCompanyWhitelist.ReadCompanies(file);

			if (!otherCompanyWhitelist.Companies.Contains(company1) || !otherCompanyWhitelist.Companies.Contains(company2))
				throw new Exception("Test failed");

		}

		public void SearchDirectoriesTest()
		{
			throw new NotImplementedException();
		}
	}
}
