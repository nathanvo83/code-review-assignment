using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace InterviewCodeReviewTest
{
	public class Test1
	{
		/**
		 * Code review:
		 * 1. type data: status should be particular e.g active, deactive, pending.
		 * -> can use enum type here
		 * 2. need to handle status data to prevent form SQL injection attack, 
		 * DON'T pass the parameters directly
		 * -> recomment: using store proceture
		 * 3. need to handle connection.
		 * -> please have a view at sample below
		 */

		// Called by web API and returns list of strongly typed customer address for given status
		// CustomerAddress is populated by external import and could be dirty
		public IEnumerable<Address> GetCustomerNumbers(string status)
		{
			var connection = new SqlConnection("data source=TestServer;initial catalog=CustomerDB;Trusted_Connection=True");
			var cmd = new SqlCommand($"SELECT CustomerAddress FROM dbo.Customer WHERE Status = '{status}'", connection);

			try
			{
				var addressStrings = new List<string>();

				connection.Open();
				var reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					addressStrings.Add(reader.GetString(0));
				}

				return addressStrings
					.Select(StringToAddress)
					.Where(x => x != null)
					.ToList();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			/*sample code*/
			//finally
			//{			
			//	connection.Close();
			//	connection.Dispose();
			//}


		}

		private static Address StringToAddress(string addressString)
		{
			return new Address(addressString);
		}
	}

	public class Address
	{
		// Some members...

		public Address(string addressString)
		{
			// Assume there are logic here to parse address and return strongly typed object
		}
	}
}
