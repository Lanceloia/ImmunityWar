using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCell : ImmuneCell
{
    public AntigenType antigenType = AntigenType.staph;//B细胞识别的抗原类型，暂定为staph
    public GameObject prefabAntibody;
    public List<MainWayGrid> grids = new List<MainWayGrid>();//在B细胞攻击范围内的主路网格
    void Awake()
    {
        rank = 1;
        attackPower = 1;
        attackRange = 1;
        attackSpeed = 1;
        attackLeft = attackSpeed;
        
        ATPcost = 1;
        antigenCost = 1;

        type = ImmuneCellType.BCell;
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
                Grids g = map.GridsList[i].GetComponent<Grids>();
                g.immuneCells.Add(tower);
                grids.Add(map.GridsList[i].GetComponent<MainWayGrid>());
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
            attackPower = 2;
            attackSpeed = 2;
        }
        else if (rank == 3)
        {
            attackPower = 3;
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
        antibodyRelease(attackSpeed);
    }

    public void antibodyRelease(byte _attackSpeed)
    {
        GameObject antibody = (GameObject)Instantiate(prefabAntibody);
        for (int i = 0; i < _attackSpeed; i++)
        {
            //设置抗体位置,随机生成在grids中
            int index = Random.Range(0, grids.Count);
            int temp = 0;
            foreach (var grid in grids)
            {
                if (grid._antiBody == null)
                {
                    break;
                }
                temp++;
            }
            if (temp == grids.Count)
            {
                Debug.Log("no grid available");
                return;
            }
            //确保不重复
            while (grids[index]._antiBody != null)
            {
                index = Random.Range(0, grids.Count);
            }
            grids[index]._antiBody = antibody;
            //Debug.Log("index: " + index);
            //Debug.Log("grids count: " + grids.Count);
            antibody.transform.position = grids[index].transform.position+new Vector3(0,0.25f,0);//设置抗体位置
            //Debug.Log("antibody position: " + grids[index].x+","+grids[index].y);
            antibody.GetComponent<AntiBody>().p = grids[index].p;
        }
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


