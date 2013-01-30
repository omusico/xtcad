using System;
using System.Collections.Generic;
using System.Text;
namespace com.ccepc.entities
{
	[Serializable]
	public class Department
	{
		public long id;
		public int version;
		public string departmentName;
		public HashSet<Discipline> disciplines;
		public HashSet<Discipline> children;
	}
}
