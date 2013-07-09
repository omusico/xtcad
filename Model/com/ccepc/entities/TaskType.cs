using System;
using System.Collections.Generic;
using System.Text;
namespace com.ccepc.entities
{
	[Serializable]
	public class TaskType
	{
		public long id;
		public int version;
		public string typeName;
		public Discipline sendDiscipline;
		public string receiveDisciplineIds;
		public List<Discipline> receiveDisciplines;
	}
}
