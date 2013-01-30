using System;
using System.Collections.Generic;
using System.Text;
namespace com.ccepc.entities
{
	[Serializable]
	public class DesignerConfig
	{
		public long id;
		public int version;
		public string configDesc;
		public DateTime startDate;
		public DateTime planFinishDate;
		public DateTime realFinishDate;
		public double taskProcess;
		public double designerWorkScale;
		public double designerPrizeScale;
		public double designeScale;
		public CheckStage lastCheck;
		public CheckStage currentCheck;
		public User designer;
		public DisciplineConfig disciplineConfig;
		public HashSet<CheckManConfig> checkManConfigs;
		public HashSet<FileInfo> fileInfos;
		public HashSet<FolderInfo> folderInfos;
	}
}
