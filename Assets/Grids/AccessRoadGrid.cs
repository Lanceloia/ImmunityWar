using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessRoadGrid : Grids
{
    // ͨ��nextָ����һ����֧·�����ӣ�������access road next

    private void _init()
    {
        type = GridsType.AccessRoadGrid;
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
    public override void onPathogenCellPassBy(GameObject pathogenCell)
    {
        // should not be here
        throw new System.NotImplementedException();

    }

    public override void onPathogenCellStay(GameObject pathogenCell)
    {
        // should not be here
        throw new System.NotImplementedException();
    }
}
