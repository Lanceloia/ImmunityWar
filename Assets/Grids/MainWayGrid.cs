using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWayGrid : Grids
{
    public bool canbuild2x2;                // 当前主路格子能否建造2x2的防御塔
    public GameObject immuneCellGrid2x2;    // 若能建造，则此处绑定对应的免细胞格子对象

    private void _init()
    {
        type = GridsType.MainWayGrid;
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
        if (canbuild2x2) 
        {
            // 此处应该弹出2个按钮
            // 如果没有建造，则弹出 “唤醒” 和 “跳过”
            // 如果已经建造，则弹出 “升级” 和 “跳过”

            Position target_position = immuneCellGrid2x2.GetComponent<ImmuneCellGrid>().p;
            //Debug.Log(string.Format("Query state of pos({0}, {1})", target_position.x, target_position.y));

            // 查询可建造 / 可升级状态
            ImmuneCellGridState state = Board.instance.ImmuneCell2x2Query(target_position);

            // 满级则跳过
            if (state == ImmuneCellGridState.MaxRank)
                return;

            // 玩家是否选择跳过
            bool skip;

            // 当前不可跳过
            skip = false;
            if (skip)
                return;

            if (state == ImmuneCellGridState.CanBuild)
                Board.instance.ImmuneCell2x2Build((int)ImmuneCellType.MacrophageCell, target_position);
            else
                Board.instance.ImmuneCell2x2Upgrade(target_position);


        }

    }
    public override void onPathogenCellPassBy(GameObject pathogenCell)
    {
        foreach (GameObject immuneCell in immuneCells)
        {
            ImmuneCell ic = immuneCell.GetComponent<ImmuneCell>();
            ic.attack(pathogenCell); // 攻击
        }

    }

    public override void onPathogenCellStay(GameObject pathogenCell) 
    {
        throw new System.NotImplementedException();
    }
}
