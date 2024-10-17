using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmuneCellGrid : Grids
{
    public bool hasTower = false;
    public List<GameObject> towertype;
    private void _init()
    {
        //Debug.Log("ImmuneCellGrid");
        type = GridsType.ImmuneCellGrid;
        GameObject[] objectsInScene = GameObject.FindGameObjectsWithTag("Tower");

        for (int i = 0; i < objectsInScene.Length; i++)
        {
            towertype.Add(objectsInScene[i]);
        }
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
