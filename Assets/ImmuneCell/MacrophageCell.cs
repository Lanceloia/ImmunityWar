using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcophageCell : ImmuneCell
{
    public bool isEngulfing = false;//����״̬
    private int engulfSpeed = 3;//��������غ���
    private int engulfingTime = 2+1;//����ʱ��,ʵ��ͣ��ʱ��Ϊ2�غ�
    private int engulfingLeft = 0;//����ʣ��غ���
    private int count = 0;//���ڼ���Ƿ������ڷ�Χ��

    public GameObject targetEnemy;//����Ŀ��

    public Dictionary<AntigenType, int> antigens = new Dictionary<AntigenType, int>();//��¼�����Ŀ�ԭ
    public Dictionary<GameObject, int> enemyInRange = new Dictionary<GameObject, int>();//��¼�����ڹ�����Χ�ڼ��غ��� 
    public Dictionary<GameObject, int> enemyInRange2 = new Dictionary<GameObject, int>();//��¼�����ӵڼ��غϿ�ʼ�ڹ�����Χ��
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
            
            if(enemyInRange[i] == engulfSpeed && isEngulfing == false)//�������غ϶��ڷ�Χ�ڣ�������
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
            p.onHurt(p.health);//����ʱֱ��ɱ��
            enemyInRange.Remove(i);
        }
    }
    
}
