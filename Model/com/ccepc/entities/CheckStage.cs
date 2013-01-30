using System;
using System.Collections.Generic;
using System.Text;
namespace com.ccepc.entities
{
	[Serializable]
	public class CheckStage
	{
		public long id;
		public int version;
		public string stageName;
		public int checkOrder;
	}
}
