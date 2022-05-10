using System.Net.Mail;
using System.Threading;

namespace InterviewCodeReviewTest
{
	public class Test3
	{
		/**
		 * Code review:		 
		 * 1. may need to handle the amount of MAX email be sending once time.
		 * in case there are too many email e.g 100k, 200k the MS Outlook service may be crashed.
		 * recomment: using queue to store the pending emails
		 */

		// This class represents a queue for email sending.
		// There are multiple active queues at any given time and they have activities all the time.
		// Each queue can be handled by multiple threads.
		public class EmailSendQueue
		{
			public int SentCount { get; private set; }
			public int FailedCount { get; private set; }

			// Assign each email to different thread for performance
			public void SendNextEmail()
			{
				var thread = new Thread(SendEmail);
				thread.Start();
			}

			private void SendEmail()
			{
				var client = new SmtpClient();
				// Send email via Smtp and returns Result object...
				UpdateStatistics(result);
			}

			private void UpdateStatistics(Result result)
			{
				lock (typeof(EmailSendQueue))
				{
					if (result.IsSuccessful)
					{
						SentCount++;
					}
					else
					{
						FailedCount++;
					}
				}
			}
		}
	}
}
