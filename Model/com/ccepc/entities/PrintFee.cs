using System;
using System.Collections.Generic;
using System.Text;
namespace com.ccepc.entities
{
	[Serializable]
	public class PrintFee
	{
		public long id;
		public int version;
		public string feeName;
		public double sumFee;
		public double factFee;
		public double remainFee;
		public string feeDesc;
		public Project project;
	}
}
