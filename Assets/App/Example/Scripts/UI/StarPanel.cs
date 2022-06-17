using UnityEngine;
using UnityEngine.UI;

namespace FrameworkDesign.Example
{
    public class StarPanel : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            transform.Find("PrivacyContent/InformationPanel/StartButton").GetComponent<Button>().onClick.AddListener(()=>
                {                   
                    gameObject.SetActive(false);
                    new GameStartCommand().Excute();
                }
                
            );

        }

    }
}

