using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWayGrid : Grids
{
    public bool canbuild2x2;                // ��ǰ��·�����ܷ���2x2�ķ�����
    public bool canbuild2x1;                // ��ǰ��·�����ܷ���2x1�ķ�����
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

    public override void onStemCellPassBy()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator onStemCellStay()
    {
        if (canbuild2x2) 
        {
            // �˴�Ӧ�õ���2����ť
            // ���û�н��죬�򵯳� �����ѡ� �� ��������
            // ����Ѿ����죬�򵯳� �������� �� ��������

            Position target_position = immuneCellGrid2x2.GetComponent<ImmuneCellGrid>().p;
            //Debug.Log(string.Format("Query state of pos({0}, {1})", target_position.x, target_position.y));

            // ��ѯ�ɽ��� / ������״̬
            ImmuneCellGridState state = Board.instance.ImmuneCell2x2Query(target_position);

            // ����������
            if (state == ImmuneCellGridState.MaxRank)
                yield break;
            yield return StartCoroutine(WaitForBuildSelect(target_position));
            // ����Ƿ�ѡ������
            bool skip;

            // ��ǰ��������
            skip = !(Board.instance.isBuilding);
            if (skip)
                yield break;

            if (state == ImmuneCellGridState.CanBuild)
                Board.instance.ImmuneCell2x2Build((int)ImmuneCellType.MacrophageCell, target_position);
            else
                Board.instance.ImmuneCell2x2Upgrade(target_position);


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
        throw new System.NotImplementedException();
    }
}
