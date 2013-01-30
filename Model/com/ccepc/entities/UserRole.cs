using System;
using System.Collections.Generic;
using System.Text;
namespace com.ccepc.entities
{
	[Serializable]
	public class UserRole
	{
		public long id;
		public int version;
		public long userId;
		public long roleId;
	}
}
