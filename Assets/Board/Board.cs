using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum CurrentRound
{
    P1 = 0,
    P2 = 1,
    P3 = 2,
    P4 = 3,
    AI = 4,    //pathogen

}

public class Board : MonoBehaviour
{
    // 单例模式
    public static Board instance;
    
    private void Awake()
    {
        instance = this;
    }

    public List<GameObject> stemCellList;   // 存储4个干细胞对象
    public List<GameObject> pathogenList;   // 存储若干个病原体对象（随着游戏进程推进不断生成销毁新的对象）
    public List<GameObject> immuneCellList; // 存储若干个免疫细胞对象（随着游戏进程推进不断生成销毁新的对象）

    public Maps map;                  // 记得绑定游戏地图的脚本
    public List<GameObject> pathogenPrefabList;       // 绑定病原体的预制体对象，用于复制
    public List<GameObject> immuneCellPrefabList;     // 绑定免疫细胞的预制体对象，用于复制

    public CurrentRound token;              // 依靠token决定行动轮次
    public int totalRound;
    public int monsterRound;

    public bool isMove4Stem;    //有物体正在移动，用于实现连续运动
    public bool isMove4Pathogen;
    public StemCell moveStem;
    public Pathogen movePathogen;

    public void StartGame()
    {
        map.Init();                                               // 初始化地图信息
        List<Position> pos = map.StemCellsOriginPosition;         // 读取干细胞的初始位置

        if (stemCellList.Count < 4)
        {
            Debug.LogError("stemCellList does not have enough elements.");
        }

        // 将干细胞移动到初始位置
        StemCellMove(0, pos[0]);
        StemCellMove(1, pos[1]);
        StemCellMove(2, pos[2]);
        StemCellMove(3, pos[3]);
        
        //初始化轮次为玩家1
        token = 0;
        totalRound = 0;
        isMove4Stem = false;
        monsterRound = 1;
    }

    void Start()
    {
        // Debug.Log("Board Start");
        StartGame();
    }

    void Update()
    {
        // Debug.Log(string.Format("stemCellList.cnt={0}", stemCellList.Count));
        
    }

    // 将某个干细胞的移动到目标位置（逻辑位置和精灵图均移动）
    public void StemCellMove(int stem_cell_index, Position target_position)
    {
        stemCellList[stem_cell_index].GetComponent<StemCell>().p = target_position;
        stemCellList[stem_cell_index].transform.position = map.PositionChange(target_position);
    }

    public void StemCellSmoothMove(int stem_cell_index, Position target_position)
    {
        // Debug.Log(string.Format("move stem {0} to position({1},{2})", stem_cell_index, target_position.x, target_position.y));
        stemCellList[stem_cell_index].GetComponent<StemCell>().p = target_position;
        stemCellList[stem_cell_index].GetComponent<StemCell>().target = map.PositionChange(target_position);
        stemCellList[stem_cell_index].GetComponent<StemCell>().isMove = true;
        //stemCellList[stem_cell_index].transform.position = map.PositionChange(target_position);
        // Debug.Log(string.Format("cur pos at {0},{1}",
        //     stemCellList[stem_cell_index].GetComponent<StemCell>().p.x,
        //     stemCellList[stem_cell_index].GetComponent<StemCell>().p.y));
    }

    // 使某个干细胞沿着主路自动向前移动若干步数（会间接调用StemCellMove）
    public IEnumerator StemCellForward(int stem_cell_index, int forward_step)
    {
        // 目前，会在Dice.cs中，通过鼠标点击的响应函数调用这里
        //Debug.Log("move:"+forward_step);
        while (forward_step > 0)
        {
            Position p = stemCellList[stem_cell_index].GetComponent<StemCell>().p;
            Direction dir = map.GetGridsFromPosition(p).GetComponent<Grids>().next;

            Position np = p + dir;

            StemCellSmoothMove(stem_cell_index, np);
            yield return StartCoroutine(WaitForObjectUpdate(stem_cell_index));
            forward_step--;
            
        }

        // 完成前进后，调用停留格子的OnStemCellStay
        Position nnp = stemCellList[stem_cell_index].GetComponent<StemCell>().p;
        GameObject grid = map.GetGridsFromPosition(nnp);
        if (grid.GetComponent<Grids>().type == GridsType.MainWayGrid)
            grid.GetComponent<MainWayGrid>().onStemCellStay();

        
    }

    IEnumerator WaitForObjectUpdate(int stem_cell_index)
    {
        moveStem = stemCellList[stem_cell_index].GetComponent<StemCell>();
        isMove4Stem = true;
        moveStem.isMove = true;

        while (true)
        {
            // 调用对象的自定义 Update() 方法
            stemCellList[stem_cell_index].GetComponent<StemCell>().SendMessage("CustomUpdate", SendMessageOptions.DontRequireReceiver);
            
            
            // 判断条件
            if (!moveStem.isMove)
            {
                //Debug.Log("条件满足，停止等待");
                break;  // 满足条件时退出循环
            }
            
            // 等待下一帧继续循环
            yield return null;
        }
    }

    public void PathogenCreate(int pathogen_type, Position target_position)
    {
        // 创建一个新的类型为pathogen_type的对象，将它的精灵图移动到target_position位置
        // 可能需要返回这个新对象在列表中的序号
        // 使用GameObject.Instantiate方法拷贝预制体对象，将其添加到列表中
        // Debug.Log(string.Format("FFF pathogen_cnt={0}", pathogenList.Count));
        GameObject pathogen = Instantiate(pathogenPrefabList[pathogen_type]);
        pathogenList.Add(pathogen);
        //Debug.Log(string.Format("GGG pathogen_cnt={0}", pathogenList.Count));
        // 此处可能需要绑定index
        // 移动到初始位置
        PathogenMove(pathogenList.Count - 1, target_position);
    }

    // 将某个病原体的移动到目标位置（逻辑位置和精灵图均移动）
    public void PathogenMove(int pathogen_index, Position target_position)
    {
        // 几乎完全与StemCellMove的逻辑相同
        pathogenList[pathogen_index].GetComponent<Pathogen>().p = target_position;
        //Debug.Log(string.Format("Here!"));
        pathogenList[pathogen_index].transform.position = map.PositionChange(target_position);
    }

    public void PathogenSmoothMove(int pathogen_index, Position target_position)
    {
        // Debug.Log(string.Format("move stem {0} to position({1},{2})", stem_cell_index, target_position.x, target_position.y));
        pathogenList[pathogen_index].GetComponent<Pathogen>().p = target_position;
        pathogenList[pathogen_index].GetComponent<Pathogen>().target = map.PositionChange(target_position);
        pathogenList[pathogen_index].GetComponent<Pathogen>().isMove = true;
        
    }

    public IEnumerator PathogenForward(int pathogen_index, int forward_step)
    {
        // 几乎完全与StemCellForward的逻辑相同
        
        while (forward_step > 0)
        {
            Position p = pathogenList[pathogen_index].GetComponent<Pathogen>().p;
            Direction dir = map.GetGridsFromPosition(p).GetComponent<Grids>().next;

            Position np = p + dir;

            PathogenSmoothMove(pathogen_index, np);
            yield return StartCoroutine(WaitForObjectUpdate4Pathogen(pathogen_index));
            forward_step--;

            //每一步移动完后，调用路过格子的onPathogenCellPassBy()
            GameObject grid = map.GetGridsFromPosition(np);
            if(grid != null)
            {
                grid.GetComponent<Grids>().onPathogenCellPassBy(pathogenList[pathogen_index]);
            }




        }
    }

    IEnumerator WaitForObjectUpdate4Pathogen(int pathogen_index)
    {
        movePathogen = pathogenList[pathogen_index].GetComponent<Pathogen>();
        isMove4Pathogen = true;
        while (true)
        {
            // 调用对象的自定义 Update() 方法
            movePathogen.SendMessage("CustomUpdate", SendMessageOptions.DontRequireReceiver);
            
            // 判断条件
            if (!movePathogen.isMove)
            {

                Debug.Log("条件满足，停止等待");
                break;  // 满足条件时退出循环
            }

            // 等待下一帧继续循环
            yield return null;
        }
    }

    public void NextRound()
    {
        token++;          // token 传给下一个
        totalRound++;

        // ImmuneCell行动
        ImmuneCellAction();
            
        
        // 如果token在AI这里，则轮到AI行动
        if (token == CurrentRound.AI)
        {
            int pathogen_index = 0;
            int forward_step = 2;

            //此处调用Pathogen_Forward，仅做测试用
            /*if (pathogenList.Count == 0)
            {
                // 读取第一个刷怪点

                Position defaultPathogenPos = map.PathogensOriginPosition[0];
                int defaultPathogenType = 0;
                PathogenCreate(defaultPathogenType, defaultPathogenPos);
            }*/
            if ((totalRound ) % monsterRound == 0&& totalRound!=0)
            {
                Position defaultPathogenPos = map.PathogensOriginPosition[((totalRound)/4)%4];
                int defaultPathogenType = 0;
                PathogenCreate(defaultPathogenType, defaultPathogenPos);
            }
            //// 所有怪物每次前进随机格
            
            for(int i=0;i< pathogenList.Count; i++)
            {
                StartCoroutine(PathogenForward(i, UnityEngine.Random.Range(1, 7)));
                //to do :增加交互
            }

            token = CurrentRound.P1;   // AI行动完token传回给P1
        }
    }
    private void ImmuneCellAction()
    {
        // 每个免疫细胞行动
        for (int i = 0; i < immuneCellList.Count; i++)
        {
            immuneCellList[i].GetComponent<ImmuneCell>().NextRound();
        }
    }

    public ImmuneCellGridState ImmuneCell2x2Query(Position target_position)
    {
        //Debug.Log(string.Format("query state"));
        // 查询目标免疫细胞区域的可建造/可升级状态
        GameObject immune_cell_grid = map.GetGridsFromPosition(target_position);
        return immune_cell_grid.GetComponent<ImmuneCellGrid>().state;
    }


    public void ImmuneCell2x2Build(int immune_cell_type, Position target_position)
    {
        // 创建一个新的类型为immune_cell_type的对象，并将它的精灵图移动到target_position位置
        GameObject immune_cell = Instantiate(immuneCellPrefabList[immune_cell_type]);
        immuneCellList.Add(immune_cell);
        //Debug.Log(string.Format("GGG immune_cell_cnt={0}", immuneCellList.Count));

        // 精灵图移动到目标位置，并登记序号
        immune_cell.GetComponent<ImmuneCell>().p = target_position;
        immune_cell.GetComponent<ImmuneCell>().index = immuneCellList.Count - 1;
        immune_cell.transform.position = map.PositionChange(target_position) + new Vector3(0.5f, 0.45f, 0);

        // 向免疫细胞区注册自身
        GameObject immune_cell_grid = map.GetGridsFromPosition(target_position);
        immune_cell_grid.GetComponent<ImmuneCellGrid>().state = ImmuneCellGridState.CanUpgrade;
        immune_cell_grid.GetComponent<ImmuneCellGrid>().immune_cell = immune_cell;

        // 向攻击范围内的道路格子注册自身
        GridsAddImmune(immune_cell);

    }

    private void GridsAddImmune(GameObject immune_cell)
    {
       
        for (int i = 0; i < map.GridsList.Count; i++)
        {
            if (isInAttackRange(immune_cell,map.GridsList[i].GetComponent<Grids>().p))
            {
                map.GridsList[i].GetComponent<Grids>().immuneCells.Add(immune_cell);
            }
        }
    }

    public bool isInAttackRange(GameObject immune_cell,Position posion)
    {
        byte attackRange = immune_cell.GetComponent<ImmuneCell>().attackRange;
        Position ip = immune_cell.GetComponent<ImmuneCell>().p;
        
        bool isInRange = false;
        
        //正方形攻击范围

        if (true)   //根据ImmuneGrids的类型判断所需判断的方式
        {
            if (Mathf.Abs(ip.x - posion.x) <= attackRange && Mathf.Abs(ip.y - posion.y) <= attackRange)
            {
                isInRange = true;
            }
            else if (Mathf.Abs(ip.x+1 - posion.x) <= attackRange && Mathf.Abs(ip.y - posion.y) <= attackRange)
            {
                isInRange = true;
            }
            else if (Mathf.Abs(ip.x - posion.x) <= attackRange && Mathf.Abs(ip.y+1 - posion.y) <= attackRange)
            {
                isInRange = true;
            }
            else if (Mathf.Abs(ip.x+1 - posion.x) <= attackRange && Mathf.Abs(ip.y+1 - posion.y) <= attackRange)
            {
                isInRange = true;
            }
        }
       
        return isInRange;
    }

    internal void ImmuneCell2x2Upgrade(Position target_position)
    {
        GameObject immune_cell_grid = map.GetGridsFromPosition(target_position);
        immune_cell_grid.GetComponent<ImmuneCellGrid>().immune_cell.GetComponent<ImmuneCell>().Upgrade();
    }
}
