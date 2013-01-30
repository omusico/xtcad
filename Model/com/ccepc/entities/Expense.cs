using System;
using System.Collections.Generic;
using System.Text;
namespace com.ccepc.entities
{
	[Serializable]
	public class Expense
	{
		public long id;
		public int version;
		public string expenseName;
		public string expenseDesc;
		public float expenseFee;
		public DateTime doTime;
		public Project project;
		public User dealMan;
	}
}
