using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TButton : MonoBehaviour
{

    public delegate void BuildClickAction();
    public event BuildClickAction OnBuildClicked;

    StemCell stemCell;//��ǰ�غϵ�stemcell

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
        //��õ�ǰ�غ���ҵ�ATP��
        byte atp =stemCell.ATP;
        //��õ�ǰ�غ����ָ����antigen��TODO
        AntigenType antigen = AntigenType.staph;
        Debug.Log("antigen is "+antigen);
        //��õ�ǰ��������ĸ���
        ImmuneCellGrid grid = Board.instance.map.GetGridsFromPosition(Board.instance.buildPosition).GetComponent<ImmuneCellGrid>();
        
        //�жϸ����Ƿ�Ϊ��
        if (grid.immune_cell == null)
        {
            ATPcost = 1;//�˰�ť��Ӧ�����Ľ���ʱ��ATP����
            antigencost = 1;//�˰�ť��Ӧ�����Ľ���ʱ��antigen����
        }
        else
        {
            ATPcost = grid.immune_cell.GetComponent<ImmuneCell>().ATPcost;
            antigencost = grid.immune_cell.GetComponent<ImmuneCell>().antigenCost;
        }

        //�ж�ATP�Ƿ��㹻����Ŀ����
        if (atp >= ATPcost)
        {
            //�۳�ATP
            stemCell.ATP -= ATPcost;
        }
        else
        {
            return false;
        }


        //�ж�antigen���Ƿ��㹻����Ŀ����
        if (stemCell.antigens[antigen] >= antigencost)
        {
            //�۳�antigen
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