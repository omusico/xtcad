using System;
using System.Collections.Generic;
using System.Text;
namespace com.ccepc.entities
{
	[Serializable]
	public class Permission
	{
		public long id;
		public int version;
		public string permissionDesc;
		public string operate;
		public string type;
		public HashSet<Permission> children;
		public Permission parent;
		public bool selected;
	}
}
