using System;
using System.Collections.Generic;
using System.Text;
namespace com.ccepc.entities
{
	[Serializable]
	public class Message
	{
		public long id;
		public int version;
		public string content;
		public string fileName;
		public byte[] file;
		public bool isPrivateTalk;
		public User sender;
		public User receiver;
		public DateTime time;
		public int status;
		public string type;
	}
}
