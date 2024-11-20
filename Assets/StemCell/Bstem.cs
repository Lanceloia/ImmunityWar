using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bstem : StemCell
{
    public GameObject prefabAntibody;
    protected override void Awake()
    {
        base.Awake();
        ATP = 1;
        ATPMax = 2;
        ATPspeed = 1;
    }
/*
    public void AntiBodyBuild()
    {
        GameObject antibody = (GameObject)Instantiate(prefabAntibody);
        if(GridsDict[p].GetComponent<Grids>.type ==  MainWayGrid && GridsDict[p].GetComponent<MainWayGrids>._antiBody == null)
        {
            antibody.transform.position = GridsDict[p].transform.position + new Vector3(0,0.25f,0);
            GridsDict[p].GetComponent<MainWayGrids>()._antiBody = antibody;
        }
    }
*/
}
