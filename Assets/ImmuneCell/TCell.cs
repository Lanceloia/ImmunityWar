using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCell : ImmuneCell
{
    AntigenType antigenType = AntigenType.staph;    //Tϸ��ֻ�ܹ����ض���ԭ���ݶ�Ϊstaph
    public HashSet<ImmuneCellGrid> gridSet =new HashSet<ImmuneCellGrid>();
    
    public int ReleaseSpeed = 2; //�ͷ�ϸ�����ӵ��ٶȣ�����Ļغϣ�1����ÿ�غ϶��ͷţ�
    public int ReleaseLeft;  //ϸ�������ͷ���ȴ
    void Awake()
    {
        rank = 1;
        attackPower = 3;
        attackRange = 1;
        attackSpeed = 1;
        attackLeft = attackSpeed;
        
        ATPcost = 1;
        antigenCost = 1;
        ReleaseLeft = ReleaseSpeed;
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
            if (isInAttackRange(map.GridsList[i].GetComponent<Grids>().p, shapeType))
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
            
        while(attackLeft != 0 && pathogen != null && pathogen.GetComponent<Pathogen>().antigenType == antigenType&& pathogen.GetComponent<Pathogen>().isIn == true)
        {
            attackLeft--;
            pathogen.GetComponent<Pathogen>().onHurt(attackPower);
        }
    }

    public override void NextRound()
    {
        base.NextRound();
        ReleaseLeft--;
        if(ReleaseLeft == 0)
        {
            ReleaseCytokine();
        }
        

    }

    private void ReleaseCytokine()  //ϸ�������ͷ�
    {
        ReleaseLeft = ReleaseSpeed; //ÿ���ͷŵ�ʱ��ˢ���ͷ�CD
        foreach(ImmuneCellGrid grid in gridSet)
        {
            if (grid.immune_cell != null)//�������ϸ���Ѿ�����
                grid.immune_cell.GetComponent<ImmuneCell>().CytokineAccepted(rank);
        }
    }

    public override void CytokineAccepted(int cyRank)
    {
        ;//Tϸ��������ϸ������
    }

    public void BuildInit()
    {
        Debug.Log("BuildInit");
        if (grid.GetComponent<Grids>().shape == ShapeType.UpTriangle)
        {
            foreach(GameObject grid in Board.instance.map.GridsList)
            {
                if(grid.GetComponent<Grids>().type == GridsType.ImmuneCellGrid)
                {
                    ImmuneCellGrid grids = grid.GetComponent<ImmuneCellGrid>();
                
                    if(Mathf.Abs(grids.p.x - p.x) <= attackRange && Mathf.Abs(grids.p.y - p.y) <= attackRange)
                    {

                        gridSet.Add(grids);
                    }
                    else if(Mathf.Abs(grids.p.x - (p.x+1)) <= attackRange && Mathf.Abs(grids.p.y - p.y) <= attackRange)
                    {
                        gridSet.Add(grids);
                    }
                }
            }
        }
        else if (grid.GetComponent<Grids>().shape == ShapeType.DownTriangle)
        {
            foreach (GameObject grid in Board.instance.map.GridsList) 
            {
                if (grid.GetComponent<Grids>().type == GridsType.ImmuneCellGrid)
                {
                    ImmuneCellGrid grids = grid.GetComponent<ImmuneCellGrid>();

                    if (Mathf.Abs(grids.p.x - p.x) <= attackRange*2 && Mathf.Abs(grids.p.y - p.y) <= attackRange*2)
                    {
                        gridSet.Add(grids);
                    }
                    else if (Mathf.Abs(grids.p.x - p.x) <= attackRange*2 && Mathf.Abs(grids.p.y - (p.y+1)) <= attackRange*2)
                    {
                        gridSet.Add(grids);
                    }
                }
            }
        }
    }
}
