using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace FrameworkDesign
{

    public class IOCContainer
    {
        private Dictionary<Type, object> mInstance = new Dictionary<Type, object>();

        public void Register<T>(T instance)
        {
            var key = typeof(T);
            if (mInstance.ContainsKey(key))
            {
                mInstance[key] = instance;
            }
            else
            {
                mInstance.Add(key,instance);
            }
        }
        public T Get<T>() where T : class
        {
            var key = typeof(T);
            object retObj;
            if(mInstance.TryGetValue(key,out retObj))
            {
                return retObj as T;
            }
            return null;
        }
    }
}
