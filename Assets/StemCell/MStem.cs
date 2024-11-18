using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MStem : StemCell
{
    protected override void Awake()
    {
        base.Awake();
        ATP = 2;
        ATPMax = 3;
        ATPspeed = 2;
    }
    

}
