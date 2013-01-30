using System;
using System.Collections.Generic;
using System.Text;
namespace com.ccepc.entities
{
	[Serializable]
	public class User
	{
		public long id;
		public int version;
		public string userName;
		public string password;
		public string realName;
		public string position;
		public string sex;
		public string jobTitle;
		public Discipline discipline;
		public HashSet<User> searchUsers;
	}
}
