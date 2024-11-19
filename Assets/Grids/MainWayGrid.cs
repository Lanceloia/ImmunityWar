using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWayGrid : Grids
{
    public bool canbuild2x2;                // ��ǰ��·�����ܷ���2x2�ķ�����
    public bool canbuild2x1;                // ��ǰ��·�����ܷ���2x1�ķ�����
    public ImmuneCellGridState state;            // ��Ӧ������ϸ�����ӵ�״̬
    public GameObject immuneCellGrid2x2;    // ���ܽ��죬��˴��󶨶�Ӧ����ϸ�����Ӷ���
    public GameObject immuneCellGrid2x1;    // ���ܽ��죬��˴��󶨶�Ӧ����ϸ�����Ӷ���

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

    //��ȡ��ԭ
    public override void onStemCellPassBy(GameObject stemCell)
    {
        if(immuneCellGrid2x2!=null&&immuneCellGrid2x2.GetComponent<ImmuneCellGrid>().state!= ImmuneCellGridState.CanBuild)
        {
            ImmuneCell ic = immuneCellGrid2x2.GetComponent<ImmuneCellGrid>().immune_cell.GetComponent<ImmuneCell>();
            switch (ic.type)
            {
                case ImmuneCellType.MacrophageCell:
                    //�Һ����byte��
                    //(�޿�)
                    Dictionary<AntigenType, int> dict1 = immuneCellGrid2x2.GetComponent<ImmuneCellGrid>().immune_cell.GetComponent<MarcophageCell >().antigens;
                    Dictionary<AntigenType, byte> dict2 = stemCell.GetComponent<StemCell>().antigens;
                    foreach (var entry in dict1)
                    {
                        if (!dict2.ContainsKey(entry.Key))
                        {
                            dict2.Add(entry.Key, (byte)entry.Value);
                        }
                        else
                        {
                            dict2[entry.Key] += (byte)entry.Value;
                        }   
                    }

                    // ���dict1
                    dict1.Clear();
                    break;
                
                default:
                    Debug.Log("error");
                    break;
            }
        }
        else if(immuneCellGrid2x1!=null&&immuneCellGrid2x1.GetComponent<ImmuneCellGrid>().state != ImmuneCellGridState.CanBuild)
        {
            ImmuneCell ic = immuneCellGrid2x1.GetComponent<ImmuneCellGrid>().immune_cell.GetComponent<ImmuneCell>();
            switch (ic.type)
            {
                case ImmuneCellType.TCell:
                    stemCell.GetComponent<StemCell>().forward_step++;//·��Tϸ�������ƶ���
                    //Todo,����һ���ṩ��ԭ�Ľ���
                    break;
                case ImmuneCellType.BCell:
                    //Toodo,����һ���ṩ��ԭ�Ľ���
                    break;
                default:
                    Debug.Log("error");
                    break;
            }
        }
    }

    public override IEnumerator onStemCellStay()
    {
        
        if (canbuild2x2) 
        {
            Position target_position = immuneCellGrid2x2.GetComponent<ImmuneCellGrid>().p;
            Board.instance.buildPosition = target_position;
            // ��ѯ�ɽ��� / ������״̬
            state = Board.instance.ImmuneCellQuery(target_position);
            
            // ����������
            if (state != ImmuneCellGridState.MaxRank)
            {
                yield return StartCoroutine(WaitForBuildSelect(target_position));   // ����Ƿ�ѡ������
                bool skip;
                skip = !(Board.instance.isBuilding);    // ��ǰ��������
                
                ImmuneCellType playerChoice = ImmuneCellType.MacrophageCell;

                if (!skip && playerChoice == ImmuneCellType.MacrophageCell)
                {
                    //���δ�������죬��������
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
            Board.instance.buildPosition = target_position;
            // ��ѯ�ɽ��� / ������״̬
            ImmuneCellGridState state = Board.instance.ImmuneCellQuery(target_position);

            // ����������
            if (state != ImmuneCellGridState.MaxRank)
            {
                yield return StartCoroutine(WaitFor2x1BuildSelect(target_position));   // ����Ƿ�ѡ������
                bool skip;
                skip = !(Board.instance.isBuilding); 

                ImmuneCellType playerChoice = ImmuneCellType.BCell; 
                if(Board.instance.buildingType == 1){
                    playerChoice = ImmuneCellType.TCell;
                }


                if (!skip && playerChoice ==ImmuneCellType.BCell)
                {
                    //���δ�������죬��������
                    if (state == ImmuneCellGridState.CanBuild)
                        Board.instance.ImmuneCellBuild((int)ImmuneCellType.BCell, target_position,immuneCellGrid2x1.GetComponent<Grids>().shape);
                    else
                        Board.instance.ImmuneCellUpgrade(target_position,immuneCellGrid2x1.GetComponent<Grids>().shape);
                }
                else if (!skip && playerChoice == ImmuneCellType.TCell)
                {
                    //���δ�������죬��������
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
        //��TB��x������ť�ֱ��Ƶ�Ŀ��λ��
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
            ic.attack(pathogenCell); // ����
        }

    }

    public override void onPathogenCellStay(GameObject pathogenCell) 
    {
        //�����������ڶ��������������ϸ������û���⣬����Ӧ���ɾ����¼
        foreach (GameObject immuneCell in immuneCells)
        {
            ImmuneCell ic = immuneCell.GetComponent<ImmuneCell>();
            if (ic.type == ImmuneCellType.MacrophageCell)
            {
                MarcophageCell mc = ic as MarcophageCell;
                if (mc.enemyInRange.ContainsKey(pathogenCell))
                    mc.enemyInRange[pathogenCell] += 1;
                else
                    mc.enemyInRange.Add(pathogenCell,1);
                if (mc.enemyInRange2.ContainsKey(pathogenCell))
                    mc.enemyInRange2[pathogenCell] = mc.count;
                else
                    mc.enemyInRange2.Add(pathogenCell, mc.count);

            }
        }
    }
}
