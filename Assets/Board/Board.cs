using System.Collections;
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
    public GameObject map;                  // 记得绑定游戏地图

    public int token;//依靠token决定行动轮次
    public bool tokenBlock;//上锁后轮次不再变化，上一轮次玩家持续行动


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

    // 将某个干细胞的移动到目标位置（逻辑位置和精灵图均移动）
    public void StemCellMove(int stem_cell_index, Position target_position)
    {
        // Debug.Log(string.Format("move stem {0} to position({1},{2})", stem_cell_index, target_position.x, target_position.y));
        stemCellList[stem_cell_index].GetComponent<StemCell>().p = target_position;
        stemCellList[stem_cell_index].transform.position = map.GetComponent<Maps>().PositionChange(target_position);
        // Debug.Log(string.Format("cur pos at {0},{1}",
        //     stemCellList[stem_cell_index].GetComponent<StemCell>().p.x,
        //     stemCellList[stem_cell_index].GetComponent<StemCell>().p.y));
    }

    public void StemCellForward(int stem_cell_index, int forward_step)
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
            stemCellList[stem_cell_index].GetComponent<StemCell>().p = np;

            StemCellMove(stem_cell_index, np);
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
