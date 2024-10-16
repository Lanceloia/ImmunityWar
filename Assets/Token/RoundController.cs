using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundControler : MonoBehaviour
{
    public int tokenUser;
    public Dice dice;
    public bool tokenBlock;
    private void OnMouseDown()
    {
        //Debug.Log("Round Start");
        NewRound(); // 当鼠标点击时调用NewRound
    }
    public void NewRound()
    {
        
        Board.instance.passToken();
        tokenUser = Board.instance.getToken();
        dice.RollDice();

        /*
         * 直接使stem_cell_index号玩家移动
         */
        StartCoroutine(Board.instance.StemCellForward(tokenUser, dice.type));
    }
    // Start is called before the first frame update
    void Start()
    {
        tokenBlock = false;
        //dice = GetComponent<Dice>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
