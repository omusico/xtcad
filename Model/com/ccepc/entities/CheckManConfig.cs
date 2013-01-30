using System;
using System.Collections.Generic;
using System.Text;
namespace com.ccepc.entities
{
	[Serializable]
	public class CheckManConfig
	{
		public long id;
		public int version;
		public double checkManWorkScale;
		public double checkManPrizeScale;
		public User checkMan;
		public DesignerConfig designerConfig;
		public CheckStage checkStage;
		public string checkDesc;
	}
}
