using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace FrameworkDesign
{

    public class Singletom<T> where T : Singletom<T>
    {

        private static T mInstance;

        public static T Instance
        {
            get
            {
                if(mInstance == null)
                {
                    var type = typeof(T);
                    var ctros = type.GetConstructors(System.Reflection.BindingFlags.Instance|System.Reflection.BindingFlags.NonPublic);
                    var ctro = Array.Find(ctros, c => c.GetParameters().Length == 0);
                    if(ctro == null)
                    {
                        throw new Exception("non public constructor in " + type.Name);
                    }
                    mInstance = ctro.Invoke(null) as T;
                }
                return mInstance;
            }
        }
    }
}
