using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tstem : StemCell
{
    
    protected override void Awake()
    {
        base.Awake();
        ATP = 1;
        ATPMax = 2;
        ATPspeed = 1;
    }
    

}
