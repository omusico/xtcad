using System;
using System.Collections.Generic;
using System.Text;
namespace com.ccepc.entities
{
	[Serializable]
	public class ReportTemplateType
	{
		public long id;
		public int version;
		public string typeName;
		public string typeDesc;
		public User user;
		public List<ReportTemplate> children;
	}
}
