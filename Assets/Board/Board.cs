﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


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
    public GameObject map;                  // 记得绑定游戏地图
    public List<GameObject> pathogenPrefabList;       // 绑定病原体的预制体对象，用于复制

    public int token;//依靠token决定行动轮次
    public bool tokenBlock;//上锁后轮次不再变化，上一轮次玩家持续行动

    public bool isMove;//有物体正在移动，用于实现连续运动

    public void StartGame()
    {


        map.GetComponent<Maps>().Init();                                               // 初始化地图信息
        List<Position> pos = map.GetComponent<Maps>().StemCellsOriginPosition;         // 读取干细胞的初始位置

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
        tokenBlock = false;

    }

    // Start is called before the first frame update
    void Start()
    {

        
        // Debug.Log("Board Start");
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(string.Format("stemCellList.cnt={0}", stemCellList.Count));
        
    }

    public void StemCellMove(int stem_cell_index, Position target_position)
    {
        stemCellList[stem_cell_index].GetComponent<StemCell>().p = target_position;
        stemCellList[stem_cell_index].transform.position = map.GetComponent<Maps>().PositionChange(target_position);
    }
    // 将某个干细胞的移动到目标位置（逻辑位置和精灵图均移动）
    public void StemCellSmoothMove(int stem_cell_index, Position target_position)
    {
        // Debug.Log(string.Format("move stem {0} to position({1},{2})", stem_cell_index, target_position.x, target_position.y));
        stemCellList[stem_cell_index].GetComponent<StemCell>().p = target_position;
        stemCellList[stem_cell_index].GetComponent<StemCell>().target = map.GetComponent<Maps>().PositionChange(target_position);
        stemCellList[stem_cell_index].GetComponent<StemCell>().isMove = true;
        //stemCellList[stem_cell_index].transform.position = map.GetComponent<Maps>().PositionChange(target_position);
        // Debug.Log(string.Format("cur pos at {0},{1}",
        //     stemCellList[stem_cell_index].GetComponent<StemCell>().p.x,
        //     stemCellList[stem_cell_index].GetComponent<StemCell>().p.y));
    }

    // 使某个干细胞沿着主路自动向前移动若干步数（会间接调用StemCellMove）
    public IEnumerator StemCellForward(int stem_cell_index, int forward_step)
    {
        // 目前，会在Dice.cs中，通过鼠标点击的响应函数调用这里
        // Debug.Log(string.Format("stem {0} should forward {1} step", stem_cell_index, forward_step));
        // Debug.Log("FFFF");
        while (forward_step > 0)
        {
            // Debug.Log("GGGG");
            // Debug.Log(string.Format("stemCellList.cnt={0}",stemCellList.Count));
            Position p = stemCellList[stem_cell_index].GetComponent<StemCell>().p;
            // Debug.Log("HHHH");
            // Position p = new Position(-1, -1);
            // Debug.Log(string.Format("cur pos at {0},{1}", p.x, p.y));
            Direction dir = map.GetComponent<Maps>().GetGridsFromPosition(p).GetComponent<Grids>().next;
            // Debug.Log(string.Format("next dir is {0}", dir));

            Position np = p + dir;
            // Position np = p;
            // stemCellList[stem_cell_index].GetComponent<StemCell>().p = np;
            StemCellSmoothMove(stem_cell_index, np);
            yield return StartCoroutine(WaitForObjectUpdate(stem_cell_index));
            forward_step--;
        }

         //此处调用Pathogen_Forward，仅做测试用
        if (pathogenList.Count == 0)
        {
            // 读取第一个刷怪点
            Position defaultPathogenPos = map.GetComponent<Maps>().PathogensOriginPosition[0];
            PathogenCreate(0, defaultPathogenPos);
        }
        // 固定每次前进2格
        PathogenForward(0, 2);
    }

    IEnumerator WaitForObjectUpdate(int stem_cell_index)
    {
        while (true)
        {
            // 调用对象的自定义 Update() 方法
            stemCellList[stem_cell_index].GetComponent<StemCell>().SendMessage("CustomUpdate", SendMessageOptions.DontRequireReceiver);

            // 判断条件
            if (!stemCellList[stem_cell_index].GetComponent<StemCell>().isMove)
            {
                Debug.Log("条件满足，停止等待");
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
        Debug.Log(string.Format("GGG pathogen_cnt={0}", pathogenList.Count));
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
        pathogenList[pathogen_index].transform.position = map.GetComponent<Maps>().PositionChange(target_position);
    }

    public void PathogenForward(int pathogen_index, int forward_step)
    {
        // 几乎完全与StemCellForward的逻辑相同
        while (forward_step > 0)
        {
            Position p = pathogenList[pathogen_index].GetComponent<Pathogen>().p;
            Direction dir = map.GetComponent<Maps>().GetGridsFromPosition(p).GetComponent<Grids>().next;
            Position np = p + dir;
            PathogenMove(pathogen_index, np);
            forward_step--;
        }
    }


    public void passToken()
    {
        if (!tokenBlock)
        {
            token++;
            token %= 4;
        }
    }
    public int getToken()
    {
        return token;
    }
    public void setTokenBlock(bool block)
    {
        Debug.Log("Lock change to"+block);
        tokenBlock = block;
    }
}
