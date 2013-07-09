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
		public Department department;
		public List<User> children;
		public List<User> managers;
		public bool selected;
	}
}
