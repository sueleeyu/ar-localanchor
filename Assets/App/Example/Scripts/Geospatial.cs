using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign.Example
{

    public class Geospatial : Architecture<Geospatial>
    {
        protected override void Init()
        {
            Register(new GameModel());
        }
    }
}
