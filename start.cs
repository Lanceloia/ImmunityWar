using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start : Board
{
    public void Startgame()
    {
        //SceneManager.LoadScene("TestMapScene");突发奇想写的笨蛋代码
        if (stemCellList.Count < 4)
        {
            Debug.LogError("stemCellList does not have enough elements.");
        }
        /*
        stemCellList[0].transform.position = new Vector3(PositionxChange(4), PositionyChange(1), 0);
        stemCellList[1].transform.position = new Vector3(PositionxChange(13), PositionyChange(6), 0);
        stemCellList[2].transform.position = new Vector3(PositionxChange(13), PositionyChange(11), 0);
        stemCellList[3].transform.position = new Vector3(PositionxChange(4), PositionyChange(11), 0);
        */
        stemCellList[0].transform.position = PositionChange(4, 1);
        stemCellList[1].transform.position = PositionChange(13,6);
        stemCellList[2].transform.position = PositionChange(13,16);
        stemCellList[3].transform.position = PositionChange(4,11);
    }
}
