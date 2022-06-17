using System;
using System.Collections.Generic;
using UnityEngine;


namespace FrameworkDesign.Example
{
    public class UI : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
           // GamePassEvent.Register(onGamePass);
        }

        private void onGamePass()
        {
            //transform.Find("Canvas/GamePassPanel").gameObject.SetActive(true);
        }

        // Update is called once per frame
        private void OnDestroy()
        {
           // GamePassEvent.UnRegister(onGamePass);
        }
    }
}
