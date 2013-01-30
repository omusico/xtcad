using System;
using System.Collections.Generic;
using System.Text;
namespace com.ccepc.entities
{
	[Serializable]
	public class Role
	{
		public long id;
		public int version;
		public string roleName;
		public string roleDesc;
	}
}
