using System;
using System.Collections.Generic;
using System.Text;
namespace com.ccepc.entities
{
	[Serializable]
	public class PrintFeeChild
	{
		public long id;
		public int version;
		public string feeName;
		public string feeDesc;
		public float feeCount;
		public DateTime doTime;
		public User dealMan;
		public PrintFee printFee;
	}
}
