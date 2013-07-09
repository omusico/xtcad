using System;
using System.Collections.Generic;
using System.Text;
namespace com.ccepc.entities
{
	[Serializable]
	public class ProcessPlan
	{
		public long id;
		public int version;
		public DateTime startTime;
		public DateTime endTime;
		public DateTime overTime;
		public string processDesc;
		public string overNote;
		public ProcessPlan parent;
		public TaskType taskType;
		public SubProject subProject;
		public HashSet<ProcessPlan> children;
		public DesignerConfig sendDesignerConfig;
		public string receiveDesignerConfigIds;
		public List<DesignerConfig> receiveDesignerConfig;
	}
}
