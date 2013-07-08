using System;
using System.Collections.Generic;
using System.Text;
namespace com.ccepc.entities
{
	[Serializable]
	public class Project
	{
		public long id;
		public int version;
		public string projectNo;
		public string projectName;
		public DateTime regiestDate;
		public DateTime startDate;
		public DateTime planFinishDate;
		public DateTime actualFinishDate;
		public DateTime pigeonholeDate;
		public double contractAmount;
		public double distributeAmount;
		public double hadDistriuteAmount;
		public double remainAmount;
		public double factAmount;
		public string quality;
		public string designInstitute;
		public string constructInstitute;
		public string ownerInstitute;
		public string contactPerson;
		public string phone;
		public string address;
		public string email;
		public string projectDesc;
		public int status;
		public User creater;
		public User manager;
	}
}
