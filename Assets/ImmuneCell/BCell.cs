using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCell : ImmuneCell
{
    AntigenType antigenType = AntigenType.staph;//B细胞识别的抗原类型，暂定为staph
    public GameObject prefabAntibody;
    void Awake()
    {
        rank = 1;
        attackPower = 3;
        attackRange = 1;
        attackSpeed = 1;
        attackLeft = attackSpeed;
        
        ATPcost = 1;
        antigenCost = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Upgrade(ShapeType shapeType)
    {
        byte temp = attackRange;
        // 断言此处等级必定小于3（等于3时调用者不该调用此函数）
        Debug.Assert(rank < 3);
        rank++;
        SpriteChange();
        AttackChange();

        if (rank == 3)
        {
            GameObject immune_cell_grid = Board.instance.map.GetComponent<Maps>().GetGridsFromPosition(p);
            immune_cell_grid.GetComponent<ImmuneCellGrid>().state = ImmuneCellGridState.MaxRank;
        }
        
        if  (temp != attackRange)//如果攻击范围改变，则重新计算攻击目标
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
        //贴图更新
        if (rank == 2)
            tower.GetComponent<SpriteRenderer>().color = Color.green;
        else if (rank == 3)
            tower.GetComponent<SpriteRenderer>().color = Color.blue;
    }


    public override void AttackChange()
    {
        //攻击力更新
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
        //这是病菌调用的攻击，Bcell不需要被调用
    }

    public override void NextRound()
    {
        base.NextRound();
        //todo: 每回合生成一个抗体
    }

    public override void CytokineAccepted(int cyRank)
    {
        switch (cyRank)
        {
            case 3:
                attackSpeed += 1;
                goto case 2;
            case 2:
                attackSpeed += 1;
                goto case 1;
            case 1:
                attackPower += 1; break;
            case 0:
                switch(tempCytokine)
                {
                    case 1:
                        attackPower -= 1;
                        break;
                    case 2:
                        attackSpeed -= 1;
                        goto case 1;
                    case 3:
                        attackSpeed -= 1;
                        goto case 2;
                    default:
                        Debug.Log("MarcophageCell cytokine error1");
                        break;
                }
                break;
            default:
                Debug.Log("MarcophageCell cytokine error2");
                break;
        }
        tempCytokine = cyRank;
        CytokineLeft = 2;
    }
}


