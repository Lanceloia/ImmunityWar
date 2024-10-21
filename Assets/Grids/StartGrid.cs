using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGrid : Grids
{
    private void _init()
    {
        type = GridsType.StartGrid;
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

    public override void onStemCellStay()
    {
        throw new System.NotImplementedException();
    }
    public override void onPathogenCellPassBy(GameObject pathogenCell)
    {
        for(int i = 0; i < immuneCells.Count; i++)
        {
            ImmuneCell immuneCell = immuneCells[i].GetComponent<ImmuneCell>();
            immuneCell.attack(pathogenCell); // ����
        }
    }

    public override void onPathogenCellStay(GameObject pathogenCell) 
    {
        throw new System.NotImplementedException();
    }
}
