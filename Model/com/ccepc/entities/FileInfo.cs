using System;
using System.Collections.Generic;
using System.Text;
namespace com.ccepc.entities
{
	[Serializable]
	public class FileInfo
	{
		public long id;
		public int version;
		public string fileName;
		public string fileNewName;
		public int fileVersion;
		public int fileSize;
		public string fileDesc;
		public string filePath;
		public string pdfPath;
		public string swfPath;
		public string ext;
		public bool converted;
		public bool uploaded;
		public int naturePages;
		public double a1Pages;
		public byte[] fileContent;
		public DateTime fileDate;
		public byte[] fileIcon;
		public CheckStage lastCheck;
		public CheckStage currentCheck;
		public User owner;
		public User currentOperator;
		public FileType fileType;
		public FolderInfo folderInfo;
		public DesignerConfig designerConfig;
		public FileInfo parent;
		public HashSet<FileInfo> children;
	}
}
