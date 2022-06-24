using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign.Example
{

    public class IOCExample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            //创建一个IOC容器
            var container = new IOCContainer();
            //注册一个蓝牙管理器实例
            container.Register<IBluetoothManager>(new BluetoothManager());
            var bluetoothManager = container.Get<IBluetoothManager>();
            //连接蓝牙
            bluetoothManager.Connect();

        }

        public interface IBluetoothManager
        {
            void Connect();
        }
        public class BluetoothManager:IBluetoothManager { 
            public void Connect()
            {
                Debug.Log("蓝牙连接成功");
            }
        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}
