using System;
using System.Collections.Generic;
using System.Text;
namespace com.ccepc.entities
{
	[Serializable]
	public class CheckSuggestion
	{
		public long id;
		public int version;
		public string suggestion;
		public string flag;
		public FileInfo currentFile;
		public FileInfo parentFile;
		public User checkMan;
		public CheckStage checkStage;
	}
}
