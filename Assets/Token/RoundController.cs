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
        NewRound(); // �������ʱ����NewRound
    }
    public void NewRound()
    {
        
        Board.instance.passToken();
        tokenUser = Board.instance.getToken();
        dice.RollDice();

        /*
         * ֱ��ʹstem_cell_index������ƶ�
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
