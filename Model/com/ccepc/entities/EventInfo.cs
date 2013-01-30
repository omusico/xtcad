using System;
using System.Collections.Generic;
using System.Text;
namespace com.ccepc.entities
{
	[Serializable]
	public class EventInfo
	{
		public long id;
		public int version;
		public string eventDesc;
		public string eventOrigin;
		public DateTime operateTime;
		public string userName;
		public string entityInfo;
	}
}
