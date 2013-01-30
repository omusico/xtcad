using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CAD
{
    public class UserItem
    {
        private string _key;
        private string _name;
        private string _tag;
        public UserItem(string strKey, string strName)
        {

            _key = strKey;
            _name = strName;
        }
        public UserItem(string strKey, string strName, string strTag)
        {

            _key = strKey;
            _name = strName;
            _tag = strTag;
        }
        public string Key { get { return _key; } set { _key = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Tag { get { return _tag; } set { _tag = value; } }
        public override string ToString()
        {
            return _name;
        }
    }
}
