using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Board : MonoBehaviour
{
    public List<GameObject> stemCellList;   // 存储4个干细胞对象
    public GameObject map;                 // 存储游戏的地图  要先进行绑定

    void StartGame()
    {

        List<Position> pos = map.GetComponent<Maps>().StemCellsOriginPosition;
        // 看看返回的位置
        Debug.Log(pos[0].x);
        Debug.Log(pos[0].y);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
