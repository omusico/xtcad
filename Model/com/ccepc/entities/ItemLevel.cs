using System;
using System.Collections.Generic;
using System.Text;
namespace com.ccepc.entities
{
	[Serializable]
	public class ItemLevel
	{
		public long id;
		public int version;
		public int level;
		public string note;
		public User user;
		public Project project;
		public SubProject subProject;
		public DisciplineConfig disciplineConfig;
		public DesignerConfig designerConfig;
		public CheckStage checkStage;
	}
}
