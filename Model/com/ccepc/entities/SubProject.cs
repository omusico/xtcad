using System;
using System.Collections.Generic;
using System.Text;
namespace com.ccepc.entities
{
	[Serializable]
	public class SubProject
	{
		public long id;
		public int version;
		public string subProjectName;
		public string subProjectDesc;
		public DateTime startDate;
		public DateTime planFinishDate;
		public DateTime actualFinishDate;
		public DateTime pigeonholeDate;
		public double sumAmount;
		public int status;
		public double managerScale;
		public double managerPrizeScale;
		public double designScale;
		public double budgetScale;
		public int prizeStatus;
		public HashSet<FolderInfo> folderInfos;
		public HashSet<DisciplineConfig> disciplineConfigs;
		public HashSet<ProcessPlan> processPlans;
		public User creater;
		public User manager;
		public Project project;
		public Phase phase;
	}
}
