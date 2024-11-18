using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TButton : MonoBehaviour
{

    public delegate void BuildClickAction();
    public event BuildClickAction OnBuildClicked;

    StemCell stemCell;//当前回合的stemcell

    void Start()
    {

        if (OnBuildClicked == null)
        {

            OnBuildClicked += DefaultClickAction;
        }
    }

    void OnMouseDown()
    {
        stemCell = Board.instance. stemCellList[(int)Board.instance.token].GetComponent<StemCell>();

        if (OnBuildClicked != null && affordable())
        {
            OnBuildClicked.Invoke();
        }
    }
    private bool affordable()
    {
        byte antigencost;
        byte ATPcost;
        //获得当前回合玩家的ATP数
        byte atp =stemCell.ATP;
        //获得当前回合玩家指定的antigen，TODO
        AntigenType antigen = AntigenType.staph;
        Debug.Log("antigen is "+antigen);
        //获得当前建造的塔的格子
        ImmuneCellGrid grid = Board.instance.map.GetGridsFromPosition(Board.instance.buildPosition).GetComponent<ImmuneCellGrid>();
        
        //判断格子是否为空
        if (grid.immune_cell == null)
        {
            ATPcost = 1;//此按钮对应的塔的建造时的ATP消耗
            antigencost = 1;//此按钮对应的塔的建造时的antigen消耗
        }
        else
        {
            ATPcost = grid.immune_cell.GetComponent<ImmuneCell>().ATPcost;
            antigencost = grid.immune_cell.GetComponent<ImmuneCell>().antigenCost;
        }

        //判断ATP是否足够建造目标塔
        if (atp >= ATPcost)
        {
            //扣除ATP
            stemCell.ATP -= ATPcost;
        }
        else
        {
            return false;
        }


        //判断antigen数是否足够建造目标塔
        if (stemCell.antigens[antigen] >= antigencost)
        {
            //扣除antigen
            stemCell.antigens[antigen] -= antigencost;
            
        }
        else
        {
            return false;
        }
        Debug.Log("antigen is " + stemCell.antigens[antigen]);
        
        return true;
    }


    private void DefaultClickAction()
    {
        //Debug.Log("T");

        Board.instance.isBuilding = true;
        Board.instance.buildingType = 1;
        Board.instance.isSelectingBuild = false;
        

    }
}