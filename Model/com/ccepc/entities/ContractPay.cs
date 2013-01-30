using System;
using System.Collections.Generic;
using System.Text;
namespace com.ccepc.entities
{
	[Serializable]
	public class ContractPay
	{
		public long id;
		public int version;
		public string payName;
		public string payDesc;
		public float payeFee;
		public DateTime doTime;
		public Project project;
		public User dealMan;
	}
}
