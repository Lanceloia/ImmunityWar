using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Board : MonoBehaviour
{
    public List<GameObject> stemCellList;   // �洢4����ϸ������
    public GameObject map;                 // �洢��Ϸ�ĵ�ͼ  Ҫ�Ƚ��а�

    void StartGame()
    {

        List<Position> pos = map.GetComponent<Maps>().StemCellsOriginPosition;
        // �������ص�λ��
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
