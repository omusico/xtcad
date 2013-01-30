using System;
using System.Collections.Generic;
using System.Text;
namespace com.ccepc.entities
{
	[Serializable]
	public class Discipline
	{
		public long id;
		public int version;
		public string disciplineName;
		public HashSet<User> users;
		public Department department;
		public HashSet<User> children;
		public List<User> managers;
	}
}
