using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWayGrid : Grids
{
    public bool canbuild2x2;                // 当前主路格子能否建造2x2的防御塔
    public bool canbuild2x1;                // 当前主路格子能否建造2x1的防御塔
    public GameObject immuneCellGrid2x2;    // 若能建造，则此处绑定对应的免细胞格子对象
    public GameObject immuneCellGrid2x1;    // 若能建造，则此处绑定对应的免细胞格子对象

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

    public override IEnumerator onStemCellStay()
    {
        
        if (canbuild2x2) 
        {
            Position target_position = immuneCellGrid2x2.GetComponent<ImmuneCellGrid>().p;
            // 查询可建造 / 可升级状态
            ImmuneCellGridState state = Board.instance.ImmuneCellQuery(target_position);

            // 满级则跳过
            if (state != ImmuneCellGridState.MaxRank)
            {
                yield return StartCoroutine(WaitForBuildSelect(target_position));   // 玩家是否选择跳过
                bool skip;
                skip = !(Board.instance.isBuilding);    // 当前不可跳过
                
                ImmuneCellType playerChoice = ImmuneCellType.MacrophageCell;

                if (!skip && playerChoice == ImmuneCellType.MacrophageCell)
                {
                    //如果未建造则建造，否则升级
                    if (state == ImmuneCellGridState.CanBuild)
                        Board.instance.ImmuneCellBuild((int)ImmuneCellType.MacrophageCell, target_position,immuneCellGrid2x2.GetComponent<Grids>().shape);
                    else
                        Board.instance.ImmuneCellUpgrade(target_position,immuneCellGrid2x2.GetComponent<Grids>().shape);
                }
            }
        }
    
        if (canbuild2x1)
        {
            Position target_position = immuneCellGrid2x1.GetComponent<ImmuneCellGrid>().p;
            // 查询可建造 / 可升级状态
            ImmuneCellGridState state = Board.instance.ImmuneCellQuery(target_position);

            // 满级则跳过
            if (state != ImmuneCellGridState.MaxRank)
            {
                yield return StartCoroutine(WaitFor2x1BuildSelect(target_position));   // 玩家是否选择跳过
                bool skip;
                skip = !(Board.instance.isBuilding); 

                ImmuneCellType playerChoice = ImmuneCellType.BCell; 
                if(Board.instance.buildingType == 1){
                    playerChoice = ImmuneCellType.TCell;
                }


                if (!skip && playerChoice ==ImmuneCellType.BCell)
                {
                    //如果未建造则建造，否则升级
                    if (state == ImmuneCellGridState.CanBuild)
                        Board.instance.ImmuneCellBuild((int)ImmuneCellType.BCell, target_position,immuneCellGrid2x1.GetComponent<Grids>().shape);
                    else
                        Board.instance.ImmuneCellUpgrade(target_position,immuneCellGrid2x1.GetComponent<Grids>().shape);
                }
                else if (!skip && playerChoice == ImmuneCellType.TCell)
                {
                    //如果未建造则建造，否则升级
                    if (state == ImmuneCellGridState.CanBuild)
                        Board.instance.ImmuneCellBuild((int)ImmuneCellType.TCell, target_position,immuneCellGrid2x1.GetComponent<Grids>().shape);
                    else
                        Board.instance.ImmuneCellUpgrade(target_position,immuneCellGrid2x1.GetComponent<Grids>().shape);
                }
            }
        }

    }

    public void ButtonMove(Position target_position)
    {
        Board.instance.buildList[0].transform.position = Board.instance.map.PositionChange(target_position + (Direction)3 + (Direction)3);
        Board.instance.buildList[1].transform.position = Board.instance.map.PositionChange(target_position + (Direction)2 + (Direction)2);
    }

    IEnumerator WaitForBuildSelect(Position target_position)
    {
        ButtonMove(target_position);

        Board.instance.isSelectingBuild = true;


        while (Board.instance.isSelectingBuild)
        {

            yield return new WaitForEndOfFrame();

        }
        ButtonMove(new Position(-25, -25));

    }
    public void Button1x2Move(Position target_position)
    {
        Board.instance.buildList[2].transform.position = Board.instance.map.PositionChange(target_position + (Direction)1 + (Direction)1);
        Board.instance.buildList[3].transform.position = Board.instance.map.PositionChange(target_position + (Direction)0 );
        Board.instance.buildList[1].transform.position = Board.instance.map.PositionChange(target_position + (Direction)2 + (Direction)2);

    }
    IEnumerator WaitFor2x1BuildSelect(Position target_position)
    {
        Button1x2Move(target_position);

        Board.instance.isSelectingBuild = true;


        while (Board.instance.isSelectingBuild)
        {

            yield return new WaitForEndOfFrame();

        }
        Button1x2Move(new Position(-25, -25));

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
