using System;
using System.Collections.Generic;
using System.Text;
namespace com.ccepc.entities
{
	[Serializable]
	public class DisciplineConfig
	{
		public long id;
		public int version;
		public string configDesc;
		public DateTime startDate;
		public DateTime planFinishDate;
		public DateTime realFinishDate;
		public double disciplineScale;
		public double leaderScale;
		public double designeScale;
		public double leaderWorkScale;
		public double leaderPrizeScale;
		public SubProject subProject;
		public User leader;
		public Discipline discipline;
		public HashSet<DesignerConfig> designerConfigs;
	}
}
