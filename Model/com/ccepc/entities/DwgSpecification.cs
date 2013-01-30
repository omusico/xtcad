using System;
using System.Collections.Generic;
using System.Text;
namespace com.ccepc.entities
{
	[Serializable]
	public class DwgSpecification
	{
		public long id;
		public int version;
		public string specificationName;
		public float amount;
	}
}
