using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundControler : MonoBehaviour
{
    public int tokenUser;
    public Dice dice;
    private void OnMouseDown()
    {
        NewRound(); // 当鼠标点击时调用NewRound
    }
    public void NewRound()
    {
        Debug.Log("Round Start");
        Board.instance.passToken();
        tokenUser = Board.instance.getToken();
       
        dice.RollDice();
        /*
         * 直接使stem_cell_index号玩家移动
         */
        Board.instance.StemCellForward(tokenUser, dice.type);
    }
    // Start is called before the first frame update
    void Start()
    {
        dice = GetComponent<Dice>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
