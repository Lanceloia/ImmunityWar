using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathogenGrid : Grids
{
    private void _init()
    {
        type = GridsType.PathogenGrid;
    }
    void Start()
    {
        _init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void onStemCellPassBy()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator onStemCellStay()
    {
        throw new System.NotImplementedException();
    }

    public override void onPathogenCellPassBy(GameObject pathogen)
    {
        throw new System.NotImplementedException();
    }

    public override void onPathogenCellStay(GameObject pathogen) 
    {
        throw new System.NotImplementedException();
    }
}
