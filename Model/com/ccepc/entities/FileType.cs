using System;
using System.Collections.Generic;
using System.Text;
namespace com.ccepc.entities
{
	[Serializable]
	public class FileType
	{
		public long id;
		public int version;
		public string fileTypeName;
		public string fileExtention;
	}
}
