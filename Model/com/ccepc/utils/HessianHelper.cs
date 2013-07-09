using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hessiancsharp.client;

namespace com.ccepc.utils
{
    public class HessianHelper
    {
        private static CADService service;

        public static CADService getServiceInstance()
        {
            if(service == null)
            {
                CHessianProxyFactory factory = new CHessianProxyFactory();
                string url="http://localhost/cscad/hessian/cadservice";
                service = (CADService)factory.Create(typeof(CADService), url);
            }
            return service;
        }
    }
}
