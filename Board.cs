using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;



public class Board : MonoBehaviour
{
    public List<GameObject> stemCellList = new List<GameObject>(4);   // 存储4个干细胞对象
    public GameObject map;                 // 存储游戏的地图  要先进行绑定

    void StartGame()
    {

        List<Position> pos = map.GetComponent<Maps>().StemCellsOriginPosition;
        // 看看返回的位置
        Debug.Log(pos[0].x);
        Debug.Log(pos[0].y);
        
    }

    public static Vector3 PositionChange(int x,int y)  //用于将格子坐标x转化成世界坐标
    {
        Vector3 pos = new Vector3(0,0,0);
        pos.x = (float)(-8.5 + (float)0.5*x + (float)0.5*y);
        pos.y = (float)(0.25 + (float)0.25*x - (float)0.25*y);
        return pos;
    }
/*
    public static float PositionyChange(int y)  //用于将格子坐标y转化成世界坐标
    {
        return (float)(0.75 + (float)0.25*y);
    }
*/    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
