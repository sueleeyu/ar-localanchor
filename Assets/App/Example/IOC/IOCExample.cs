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
            //����һ��IOC����
            var container = new IOCContainer();
            //ע��һ������������ʵ��
            container.Register<IBluetoothManager>(new BluetoothManager());
            var bluetoothManager = container.Get<IBluetoothManager>();
            //��������
            bluetoothManager.Connect();

        }

        public interface IBluetoothManager
        {
            void Connect();
        }
        public class BluetoothManager:IBluetoothManager { 
            public void Connect()
            {
                Debug.Log("�������ӳɹ�");
            }
        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}
