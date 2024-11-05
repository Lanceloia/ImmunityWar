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

    public override IEnumerator onStemCellStay()
    {
        throw new System.NotImplementedException();
    }
    public override void onPathogenCellPassBy(GameObject pathogenCell)
    {
        foreach (GameObject immuneCell in immuneCells)
        {
            ImmuneCell ic = immuneCell.GetComponent<ImmuneCell>();
            ic.attack(pathogenCell); // ¹¥»÷
        }

    }

    public override void onPathogenCellStay(GameObject pathogenCell) 
    {
        throw new System.NotImplementedException();
    }
}
