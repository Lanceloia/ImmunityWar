using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCell : ImmuneCell
{
    
    void Awake()
    {
        rank = 1;
        attackPower = 3;
        attackRange = 1;
        attackSpeed = 1;
        attackLeft = attackSpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Upgrade(ShapeType shapeType)
    {
        byte temp = attackRange;
        // ���Դ˴��ȼ��ض�С��3������3ʱ�����߲��õ��ô˺�����
        Debug.Assert(rank < 3);
        rank++;
        SpriteChange();
        AttackChange();

        if (rank == 3)
        {
            GameObject immune_cell_grid = Board.instance.map.GetComponent<Maps>().GetGridsFromPosition(p);
            immune_cell_grid.GetComponent<ImmuneCellGrid>().state = ImmuneCellGridState.MaxRank;
        }
        
        if  (temp != attackRange)//���������Χ�ı䣬�����¼��㹥��Ŀ��
        {

            //Debug.Log("attackRange changed");
            GridsImmuneChange(shapeType);
            
        }
    }

    protected override void GridsImmuneChange(ShapeType shapeType)
    {
        Maps map = Board.instance.map;
        for (int i = 0; i < map.GridsList.Count; i++)
        {
            if (isInAttackRange(map.GridsList[i].GetComponent<Grids>().p,shapeType))
            {
                map.GridsList[i].GetComponent<Grids>().immuneCells.Add(tower);
            }
        }
    }

    private bool isInAttackRange(Position position,ShapeType shapeType)
    {
        bool isInRange = false;
        if  ((Mathf.Abs(position.x - p.x) <= attackRange ) && (Mathf.Abs(position.y - p.y) <= attackRange))
        {
            isInRange = true;
        }    
        if (shapeType == ShapeType.UpTriangle)
        {
            if ((Mathf.Abs(position.x - (p.x+1)) <= attackRange ) && (Mathf.Abs(position.y - p.y) <= attackRange))
            {
                isInRange = true;
            }
        }
        else if (shapeType == ShapeType.DownTriangle)
        {
            if ((Mathf.Abs(position.x - p.x) <= attackRange) && (Mathf.Abs(position.y - (p.y+1)) <= attackRange))
            {
                isInRange = true;
            }
        }
        return isInRange;
    }

    public override void SpriteChange()
    {
        //��ͼ����
        if (rank == 2)
            tower.GetComponent<SpriteRenderer>().color = Color.green;
        else if (rank == 3)
            tower.GetComponent<SpriteRenderer>().color = Color.blue;
    }


    public override void AttackChange()
    {
        //����������
        if (rank == 2)
        {
            attackPower = 4;
            attackSpeed = 2;
        }
        else if (rank == 3)
        {
            attackPower = 5;
            attackRange = 2;
            attackSpeed = 3;
        }
    }
    public override void attack(GameObject pathogen)
    {
        if (pathogen == null)
        {
            Debug.Log("pathogen is null");
            return;
        }
            
        while(attackLeft != 0 && pathogen != null)
        {
            attackLeft--;
            pathogen.GetComponent<Pathogen>().onHurt(attackPower);
        }
    }
}
