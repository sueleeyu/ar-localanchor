using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign.Example
{

    public class GameStartCommand : ICommand
    {
        public void Excute()
        {
            GameStartEvent.Trigger();
        }

    }
}
