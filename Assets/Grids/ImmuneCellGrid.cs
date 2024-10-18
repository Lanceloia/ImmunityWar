using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ImmuneCellGridState
{
    CanBuild = 0,
    CanUpgrade = 1,
    MaxRank = 2
}

public class ImmuneCellGrid : Grids
{
    public ImmuneCellGridState state = ImmuneCellGridState.CanBuild;   // �ɽ���/������״̬
    public GameObject immune_cell;      // ָ��������Ķ���

    private void _init()
    {
        type = GridsType.ImmuneCellGrid;
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
}
