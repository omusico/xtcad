using System;
using System.Collections.Generic;
using System.Text;
namespace com.ccepc.entities
{
	[Serializable]
	public class ReportTemplate
	{
		public long id;
		public int version;
		public string templateName;
		public string styleString;
		public string templateDesc;
		public User user;
		public ReportTemplateType reportTemplateType;
	}
}
