using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cardContainer; // 将你的 cardContainer 赋值到此处

    public void HideCardContainer()
    {
        cardContainer.SetActive(false); // 隐藏 cardContainer
        for(int i=0;i<Board.instance.cardUsedinThisRound.Count;i++){
            // for(int j = Board.instance.handcardList[turnIndex].Count-1;j>=0;j--){
            //     if(Board.instance.handcardList[turnIndex][j] == i){
            //         Board.instance.handcardList[turnIndex][j].
            //     }
            // }
            Debug.Log(Board.instance.cardUsedinThisRound[i]+"has removed!");
            Board.instance.handcardList[(int)Board.instance.token].Remove(Board.instance.cardUsedinThisRound[i]);
        }
    }
    void Start()
    {
        cardContainer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
