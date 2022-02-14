using System;
using System.Data.SqlClient;

namespace InterviewCodeReviewTest
{
	public class Test2
	{
		// Record customer purchase and update customer reward programme
		public Result UpdateCustomerHistory(Purchase customerPurchase)
		{
			var connPruchase = new SqlConnection("data source=TestPurchaseServer;initial catalog=PurchaseDB;Trusted_Connection=True");
			var connReward = new SqlConnection("data source=TestRewardServer;initial catalog=RewardDB;Trusted_Connection=True");

			var cmdPurchase = new SqlCommand("INSERT INTO dbo.Purchase..."); // omitted the columns
			var cmdReward = new SqlCommand("INSERT INTO dbo.Reward..."); // omitted the columns

			SqlTransaction tranPurchase = null;
			SqlTransaction tranReward = null;

			try
			{
				connPruchase.Open();
				tranPurchase = connPruchase.BeginTransaction();
				cmdPurchase.ExecuteNonQuery();

				connReward.Open();
				tranReward = connReward.BeginTransaction();
				cmdReward.ExecuteNonQuery();

				tranPurchase.Commit();
				tranReward.Commit();

				return Result.Success();
			}
			catch (Exception ex)
			{
				tranPurchase.Rollback();
				tranReward.Rollback();

				return Result.Failed();
			}
		}
	}

	public class Purchase
	{
		// Some members
	}

	public class Result
	{
		public bool IsSuccessful { get; private set; }

		public static Result Success()
		{
			return new Result { IsSuccessful = true };
		}

		public static Result Failed()
		{
			return new Result { IsSuccessful = false };
		}
	}
}
