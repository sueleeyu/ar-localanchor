using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace FrameworkDesign.Example
{
    public class DIPExample : MonoBehaviour
    {
        // Start is called before the first frame update
       
        public interface IStorage
        {
            void SaveString(string key, string value);
            string LoadingString(string key, string defaultvalue = "");
        }

        public class PlayerPrefsStorage : IStorage
        {
            public string LoadingString(string key, string defaultvalue = "")
            {
                return PlayerPrefs.GetString(key,defaultvalue);
            }

            public void SaveString(string key, string value)
            {
                PlayerPrefs.SetString(key, value);
            }
        }

        public class EditorPrefsStorage : IStorage
        {
            public string LoadingString(string key, string defaultvalue = "")
            {
#if UNITY_EDITOR
                return EditorPrefs.GetString(key,defaultvalue);
#else
                return "";
#endif
            }

            public void SaveString(string key, string value)
            {
#if UNITY_EDITOR
                EditorPrefs.SetString(key,value);
#endif
            }
        }

        private void Start()
        {
            var container = new IOCContainer();
            container.Register<IStorage>(new PlayerPrefsStorage());
            var storage = container.Get<IStorage>();
            storage.SaveString("name", "运行时存储");
            Debug.Log(storage.LoadingString("name"));

            //切换实现
            container.Register<IStorage>(new EditorPrefsStorage());
            storage = container.Get<IStorage>();
            Debug.Log(storage.LoadingString("name"));


        }
    }

}