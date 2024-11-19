using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VC : Card
{
    // Start is called before the first frame update
    void Start()
    {
        cardType = CardType.Action;
        index = 6;
        cardName = "VC";
        cardDiscription = "After 3 rounds, get 0~3 ATP.";                
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void CardEffect(){
       byte reward = (byte)Random.Range(0, 4);
       Board.instance.stemCellList[(int)(Board.instance.token)].GetComponent<StemCell>().extraReward = reward;
       Board.instance.stemCellList[(int)(Board.instance.token)].GetComponent<StemCell>().rewardRound = 3;
    }
}
