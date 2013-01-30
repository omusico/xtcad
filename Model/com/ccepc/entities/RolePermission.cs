using System;
using System.Collections.Generic;
using System.Text;
namespace com.ccepc.entities
{
	[Serializable]
	public class RolePermission
	{
		public long id;
		public int version;
		public long roleId;
		public long permissionId;
	}
}
