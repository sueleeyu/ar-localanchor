using System;
using UnityEngine;

namespace FrameworkDesign.Example
{

    public class Game : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            GameStartEvent.Register(onGameStart);
            
        }

        private void onGameStart()
        {
            transform.Find("TriAxesWithDebugText").gameObject.SetActive(true);
        }

       
        private void OnDestroy()
        {
            GameStartEvent.UnRegister(onGameStart);
        }
    }
}
