using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcophageCell : ImmuneCell
{
    public bool isEngulfing = false;//吞噬状态
    private int engulfSpeed = 3;//吞噬所需回合数
    private int engulfingTime = 2+1;//消化时间,实际停留时间为2回合
    private int engulfingLeft = 0;//吞噬剩余回合数
    private int count = 0;//用于检测是否连续在范围内

    public GameObject targetEnemy;//吞噬目标

    public Dictionary<AntigenType, int> antigens = new Dictionary<AntigenType, int>();//记录产出的抗原
    public Dictionary<GameObject, int> enemyInRange = new Dictionary<GameObject, int>();//记录病菌在攻击范围内几回合了 
    public Dictionary<GameObject, int> enemyInRange2 = new Dictionary<GameObject, int>();//记录病菌从第几回合开始在攻击范围内
    void Awake()
    {
        rank = 1;
        attackPower = 3;
        attackRange = 1;
        attackSpeed = 1;
        attackLeft = attackSpeed;
        ATPcost = 1;
        antigenCost = 0;
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
            if (isInAttackRange(map.GridsList[i].GetComponent<Grids>().p))
            {
                map.GridsList[i].GetComponent<Grids>().immuneCells.Add(tower);
            }
        }
    }

    private bool isInAttackRange(Position position)
    {
        bool isInRange = false;
        if  ((Mathf.Abs(position.x - p.x) <= attackRange ) && (Mathf.Abs(position.y - p.y) <= attackRange))
        {
            isInRange = true;
        }    
        else if ((Mathf.Abs(position.x - (p.x+1)) <= attackRange) && (Mathf.Abs(position.y - p.y) <= attackRange))
        {
            isInRange = true;
        }
        else if ((Mathf.Abs(position.x - p.x) <= attackRange) && (Mathf.Abs(position.y - (p.y+1)) <= attackRange))
        {
            isInRange = true;
        }
        else if ((Mathf.Abs(position.x - (p.x+1)) <= attackRange) && (Mathf.Abs(position.y - (p.y+1)) <= attackRange))
        {
            isInRange = true;
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

    public override void NextRound()
    {
        base.NextRound();
        
        List<GameObject> toRemove = new List<GameObject>();
        foreach (GameObject i in enemyInRange.Keys)
        {
            if (i == null)
            {
                toRemove.Add(i);
                continue;
            }
            
            if(enemyInRange[i] == engulfSpeed && isEngulfing == false)//连续三回合都在范围内，则吞噬
            {
                toRemove.Add(i);
                isEngulfing = true;
                engulfingLeft = engulfingTime;
                targetEnemy = i;
                break;
            }
            if (isEngulfing == true)
            {
                break;
            }
        }
        if (isEngulfing)
        {
            engulfingLeft--;
            if (engulfingLeft == 0)
            {
                if(antigens.ContainsKey(targetEnemy.GetComponent<Pathogen>().antigenType))
                {
                    antigens[targetEnemy.GetComponent<Pathogen>().antigenType] += 1;
                }
                else
                {
                    antigens.Add(targetEnemy.GetComponent<Pathogen>().antigenType,1);
                    isEngulfing = false;
                    engulfingLeft = engulfingTime;
                }
            }
        }
        foreach (GameObject i in toRemove)
        {
            Pathogen p = i.GetComponent<Pathogen>();
            p.onHurt(p.health);//吞噬时直接杀死
            enemyInRange.Remove(i);
        }
    }
    
}
