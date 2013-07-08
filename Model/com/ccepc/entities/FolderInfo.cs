using System;
using System.Collections.Generic;
using System.Text;
namespace com.ccepc.entities
{
	[Serializable]
	public class FolderInfo
	{
		public long id;
		public int version;
		public string folderOldName;
		public string folderNewName;
		public string folderPath;
		public string folderType;
		public string folderSubType;
		public HashSet<FolderInfo> children;
		public HashSet<FileInfo> folderFileInfos;
		public User creater;
		public FolderInfo parent;
		public Project project;
		public SubProject subProject;
		public DesignerConfig designerConfig;
	}
}
