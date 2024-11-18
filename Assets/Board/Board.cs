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

    //UIs
    public List<GameObject> arrowList;
    public bool isSelectingAccess;
    public bool isGoingAccess;
    public Direction nextAccessDirection;

    public List<GameObject> buildList;//升级ui列表
    public bool isSelectingBuild;
    public bool isBuilding; //用于在玩家选择时决定是否可以升级/建造
    public Position buildPosition;//升级/建造的防御塔的位置
    public int buildingType;

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

    public GameObject roundController;//所有操作完成时允许进入下一回合
    public bool hasSelectedAccess;
    public bool hasSelectedTower;
    public bool hasClickedDice;

    public int totalHealth;
    public int totalAPT;

    public GameObject cameraController;

    public List<GameObject> cardsList;//储存了不同种类的卡，根据index可以在本数组中找到对应卡
    public List<List<int>> handcardList; //二维list储存手牌的index,index对应cardsList中的index
    public GameObject cardUIManager;//选择卡牌的UI管理
    public List<int> cardUsedinThisRound;

    //用于实现卡牌具体效果
    public bool isDoubleMove = false;//用于实现DoubleMove卡牌
    public bool isSelectMove = false;//用于实现SelectMove卡牌
    public int selectNum;
    public bool isBackMove = false;//用于实现BackMove卡牌
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
        totalHealth = 15;

        handcardList  = new List<List<int>>();
        for (int i = 0; i < 4; i++)
        {
            List<int> row = new List<int>();
            // for (int j = 0; j < 2; j++)
            // {
            //     row.Add(0); // 仅用于测试，每个角色手牌添加两张测试牌
            // }
            row.Add(4);
            row.Add(1);
            row.Add(2);
            row.Add(3);
            handcardList.Add(row);
        }
        cardUIManager.GetComponent<CardUIManager>().DisplayCardsForTurn(0);
    }

    void Start()
    {
        // Debug.Log("Board Start");
        StartGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
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

            // 如果干细胞当前位置的格子是交叉路口，则进入选择箭头状态
            if (map.GetGridsFromPosition(p).GetComponent<Grids>().accessRoad&&!isBackMove)
            {
                

                Direction acdir = map.GetGridsFromPosition(p).GetComponent<Grids>().accessRoadNext;
                yield return StartCoroutine(WaitForArrowSelect((int)dir, (int)acdir,p+dir+dir,p+acdir+acdir));

                np = p + nextAccessDirection;

            }

            //如果反向了则沿pre移动
            if(isBackMove){
                np = p+ map.GetGridsFromPosition(p).GetComponent<Grids>().pre;
            }

            Debug.Log("remain:"+forward_step);
            StemCellSmoothMove(stem_cell_index, np);
            yield return StartCoroutine(WaitForObjectUpdate(stem_cell_index));

            forward_step--;
            //每一步移动完后，调用移动格子的OnStemPassBy
            GameObject grid1 = map.GetGridsFromPosition(np);
            if (grid1.GetComponent<Grids>().type == GridsType.MainWayGrid)
                grid1.GetComponent<MainWayGrid>().onStemCellPassBy(stemCellList[stem_cell_index]);

            
        }

        // 完成前进后，调用停留格子的OnStemCellStay
        Position nnp = stemCellList[stem_cell_index].GetComponent<StemCell>().p;
        GameObject grid = map.GetGridsFromPosition(nnp);
        if (grid.GetComponent<Grids>().type == GridsType.MainWayGrid)
            yield return StartCoroutine(grid.GetComponent<MainWayGrid>().onStemCellStay());

        //清楚反向标记
        if(isBackMove){
            isBackMove = false;
        }
    }

    IEnumerator WaitForArrowSelect(int ori,int access,Position oriP,Position accessP)
    {
        ArrowMove(ori, oriP);
        ArrowMove(access, accessP);
        
        isSelectingAccess = true;
        GameObject oriObj = arrowList[ori].GetComponent<GameObject>();
        GameObject accObj = arrowList[access].GetComponent<GameObject>();
        
        while (isSelectingAccess)
        {
            
            yield return new WaitForEndOfFrame();

            
        }
        ArrowMove(ori, new Position(-20, -20));
        ArrowMove(access, new Position(-20, -20));


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

    public void PathogenCreate(int pathogen_type, Position target_position, int target_index)
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
        pathogen.GetComponent<Pathogen>().targetIndex = target_index;
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

    //用于改变list中记录的数值
    public void PathogenSmoothMove(GameObject pathogen, Position target_position)
    {
        // Debug.Log(string.Format("move stem {0} to position({1},{2})", stem_cell_index, target_position.x, target_position.y));
        pathogen.GetComponent<Pathogen>().p = target_position;
        pathogen.GetComponent<Pathogen>().target = map.PositionChange(target_position);
        pathogen.GetComponent<Pathogen>().isMove = true;
        
    }

    public IEnumerator PathogenForward(int pathogen_index, int forward_step)
    {
        // 几乎完全与StemCellForward的逻辑相同
        GameObject pathogen = pathogenList[pathogen_index];
        while (forward_step > 0)
        {
            Position p = pathogen.GetComponent<Pathogen>().p;
            Direction dir = map.GetGridsFromPosition(p).GetComponent<Grids>().next;

            Position np = p + dir;

            if (map.GetGridsFromPosition(p).GetComponent<Grids>().nearHistiocyte)
            {
                if(map.GetGridsFromPosition(p+ map.GetGridsFromPosition(p).GetComponent<Grids>().HistiocyteNext).GetComponent<HistiocyteGrid>().hisIndex == pathogen.GetComponent<Pathogen>().targetIndex)
                {
                    np = p + map.GetGridsFromPosition(p).GetComponent<Grids>().HistiocyteNext;
                }
            }

            PathogenSmoothMove(pathogen, np);
            yield return StartCoroutine(WaitForObjectUpdatePathogen(pathogen));
            forward_step--;

            //每一步移动完后，调用路过格子的onPathogenCellPassBy()
            GameObject grid = map.GetGridsFromPosition(np);
            if(grid != null)
            {
                //Debug.Log("move pathogen " + pathogen_index);
                grid.GetComponent<Grids>().onPathogenCellPassBy(pathogen);
                if (pathogen == null)
                    break;
            }
        }
        if (pathogen != null)
        {
            Position nnp = pathogen.GetComponent<Pathogen>().p;
            GameObject grid1 = map.GetGridsFromPosition(nnp);
            if (grid1 != null)
            {
                grid1.GetComponent<Grids>().onPathogenCellStay(pathogen);
            }
        }
    }

    IEnumerator WaitForObjectUpdatePathogen(GameObject pathogen)
    {
        movePathogen = pathogen.GetComponent<Pathogen>();
        isMove4Pathogen = true;
        while (true)
        {
            // 调用对象的自定义 Update() 方法
            if (movePathogen != null)
                movePathogen.SendMessage("CustomUpdate", SendMessageOptions.DontRequireReceiver);
            else 
                isMove4Pathogen = false;
            // 判断条件
            if ((!movePathogen.isMove) || !isMove4Pathogen)
            {

                //Debug.Log("条件满足，停止等待");
                break;  // 满足条件时退出循环
            }

            // 等待下一帧继续循环
            yield return null;
        }
    }

    public void newRoundReset()//将回合操作锁重置
    {
        hasClickedDice = false;
        hasSelectedAccess = false;
        hasSelectedTower = false;
        roundController.GetComponent<RoundController>().tokenLock = true;
    }

    public void NextRound()
    {
        token++;          // token 传给下一个
        totalRound++;
        
        
        if(token!= CurrentRound.AI)
        {
            
            cameraController.GetComponent<CameraController>().SetTarget(stemCellList[(int)token].GetComponent<StemCell>().transform);
            cameraController.GetComponent<CameraController>().isFollowing = true;

            cardUIManager.GetComponent<CardUIManager>().DisplayCardsForTurn((int)token);
        }
        switch (token)
        {
            case CurrentRound.P1:
                break;
            case CurrentRound.P2:
                stemCellList[1].GetComponent<StemCell>().TurnStart();
                break;
            case CurrentRound.P3:
                stemCellList[2].GetComponent<StemCell>().TurnStart(); 
                break;
            case CurrentRound.P4:
                break;
            case CurrentRound.AI:
                // ImmuneCell行动
                ImmuneCellAction();
                /*int pathogen_index = 0;
                int forward_step = 2;

                //此处调用Pathogen_Forward，仅做测试用
                if (pathogenList.Count == 0)
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
                    PathogenCreate(defaultPathogenType, defaultPathogenPos, ((totalRound) / 4) % 4);
                }
                //// 所有怪物每次前进随机格

                for (int i = pathogenList.Count - 1; i >= 0; i--)
                {
                    //Debug.Log(i);
                    StartCoroutine(PathogenForward(i, UnityEngine.Random.Range(1, 7)));
                    //to do :增加交互
                }

                
                


                //p1的回合在AI回合结束后开始
                token = CurrentRound.P1;  
                cameraController.GetComponent<CameraController>().SetTarget(stemCellList[(int)CurrentRound.P1].GetComponent<StemCell>().transform);
                cameraController.GetComponent<CameraController>().isFollowing = true;
                cardUIManager.GetComponent<CardUIManager>().DisplayCardsForTurn(0);
                //Debug.Log("P1 turn");
                stemCellList[0].GetComponent<StemCell>().TurnStart();
        
                break;
            default:
                Debug.Log("Token error");
                break;
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

    public ImmuneCellGridState ImmuneCellQuery(Position target_position)
    {
        //Debug.Log(string.Format("query state"));
        // 查询目标免疫细胞区域的可建造/可升级状态
        GameObject immune_cell_grid = map.GetGridsFromPosition(target_position);
        return immune_cell_grid.GetComponent<ImmuneCellGrid>().state;
    }


    public void ImmuneCellBuild(int immune_cell_type, Position target_position,ShapeType shapeType)
    {
        // 创建一个新的类型为immune_cell_type的对象，并将它的精灵图移动到target_position位置
        GameObject immune_cell = Instantiate(immuneCellPrefabList[immune_cell_type]);
        immuneCellList.Add(immune_cell);
        //Debug.Log(string.Format("GGG immune_cell_cnt={0}", immuneCellList.Count));

        // 精灵图移动到目标位置，并登记序号
        immune_cell.GetComponent<ImmuneCell>().p = target_position;
        immune_cell.GetComponent<ImmuneCell>().index = immuneCellList.Count - 1;
        
        // 根据形状调整位置
        if (shapeType == ShapeType. BigSquare)
            immune_cell.transform.position = map.PositionChange(target_position) + new Vector3(0.5f, 0, 0);
        else if (shapeType == ShapeType.UpTriangle )
            immune_cell.transform.position = map.PositionChange(target_position) + new Vector3(0.25f, 0.125f, 0);
        else if(shapeType == ShapeType.DownTriangle)
            immune_cell.transform.position = map.PositionChange(target_position) + new Vector3(0.25f, -0.125f, 0);

        // 向免疫细胞区注册自身
        GameObject immune_cell_grid = map.GetGridsFromPosition(target_position);
        immune_cell_grid.GetComponent<ImmuneCellGrid>().state = ImmuneCellGridState.CanUpgrade;
        immune_cell_grid.GetComponent<ImmuneCellGrid>().immune_cell = immune_cell;

        // 向攻击范围内的道路格子注册自身
        GridsAddImmune(immune_cell,shapeType);

    }

    private void GridsAddImmune(GameObject immune_cell,ShapeType shapeType)
    {
       
        for (int i = 0; i < map.GridsList.Count; i++)
        {
            if (isInAttackRange(immune_cell,map.GridsList[i].GetComponent<Grids>().p,shapeType))
            {
                map.GridsList[i].GetComponent<Grids>().immuneCells.Add(immune_cell);
            }
        }
    }

    public bool isInAttackRange(GameObject immune_cell,Position posion,ShapeType shapeType)
    {
        byte attackRange = immune_cell.GetComponent<ImmuneCell>().attackRange;
        Position ip = immune_cell.GetComponent<ImmuneCell>().p;
        
        bool isInRange = false;
        
        //正方形攻击范围

        if (shapeType == ShapeType.BigSquare)   //根据ImmuneGrids的类型判断所需判断的方式
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
        else if (shapeType == ShapeType.UpTriangle )
        {
            if (Mathf.Abs(ip.x - posion.x) <= attackRange && Mathf.Abs(ip.y - posion.y) <= attackRange)
            {
                isInRange = true;
            }
            else if (Mathf.Abs(ip.x+1- posion.x) <= attackRange && Mathf.Abs(ip.y - posion.y) <= attackRange)
            {
                isInRange = true;
            }
        }

        else if (shapeType == ShapeType.DownTriangle )
        {
            if (Mathf.Abs(ip.x - posion.x) <= attackRange && Mathf.Abs(ip.y - posion.y) <= attackRange)
            {
                isInRange = true; 
            }
            else if (Mathf.Abs(ip.x - posion.x) <= attackRange && Mathf.Abs(ip.y+1 - posion.y) <= attackRange)
            {
                isInRange = true;
            }
        }
        return isInRange;
    }

    internal void ImmuneCellUpgrade(Position target_position,ShapeType shapeType)
    {
        GameObject immune_cell_grid = map.GetGridsFromPosition(target_position);
        immune_cell_grid.GetComponent<ImmuneCellGrid>().immune_cell.GetComponent<ImmuneCell>().Upgrade(shapeType);
    }

    public void ArrowMove(int direction, Position target_position)
    {        
        arrowList[direction].transform.position = map.PositionChange(target_position);
    }

}
