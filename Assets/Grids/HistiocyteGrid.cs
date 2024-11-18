using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistiocyteGrid : Grids
{
    public int hisIndex;

    // Start is called before the first frame update
    private void _init()
    {
        type = GridsType.HistiocyteGrid;
    }
    void Start()
    {
        _init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void onStemCellPassBy(GameObject stemCell)
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator onStemCellStay()
    {
        throw new System.NotImplementedException();
    }

    public override void onPathogenCellPassBy(GameObject pathogen)
    {
        Pathogen p = pathogen.GetComponent<Pathogen>();
        p.onHurt(10086);
        Board.instance.totalHealth--;
        Debug.Log("hp--");
    }

    public override void onPathogenCellStay(GameObject pathogen)
    {
        throw new System.NotImplementedException();
    }
}
