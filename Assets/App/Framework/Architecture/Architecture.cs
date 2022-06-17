using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign
{
    /// <summary>
    /// 架构
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Architecture<T> where T: Architecture<T>,new()
    {
        //类似单例模式但是仅在内部可访问
        private static T mArchitecture = null;

        private IOCContainer mContainer = new IOCContainer();
        static void MakeSureArchitecture()
        {
            if(mArchitecture == null)
            {
                mArchitecture = new T();
                mArchitecture.Init();
            }
        }
        //注册留给子类
        protected abstract void Init();
        //提供一个注册模块的api
        public void Register<T>(T instance)
        {
            MakeSureArchitecture();
            mArchitecture.mContainer.Register<T>(instance);
        }
        //提供一个api
        public static T Get<T>() where T : class
        {
            MakeSureArchitecture();
            return mArchitecture.mContainer.Get<T>();
        }
    }
}
