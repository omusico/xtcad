using System;
using System.Collections;
using hessiancsharp.server;

namespace com.ccepc.hessian
{
	public class Service:CHessianHandler, IService
	{

        #region IService ��Ա

        public string Hello(string name)
        {
            return "Hello " + name;
        }

        #endregion

    }
}
