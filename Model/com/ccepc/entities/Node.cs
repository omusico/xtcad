using System;
using System.Collections.Generic;
using System.Text;
namespace com.ccepc.entities
{
	[Serializable]
	public class Node
	{
		public long id;
        public string label;
        public string type;
        public string roleDesc;
        public object data;
        public List<Node> children;
	}
}
