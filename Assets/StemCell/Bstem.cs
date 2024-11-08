using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bstem : StemCell
{
    protected override void Awake()
    {
        base.Awake();
        ATP = 1;
        ATPMax = 2;
        ATPspeed = 1;
    }

}
