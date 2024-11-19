using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManyDouble : Card
{
    // Start is called before the first frame update
    void Start()
    {
        cardType = CardType.Action;
        index = 7;
        cardName = "InflammatoryPro";
        cardDiscription = "Get 3 Inflammatory cards in next round.";
                
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void CardEffect(){
        Board.instance.handcardList[(int)(Board.instance.token)].Add(1);
        Board.instance.handcardList[(int)(Board.instance.token)].Add(1);
        Board.instance.handcardList[(int)(Board.instance.token)].Add(1);
        //Board.instance.cardUIManager.GetComponent<CardUIManager>().DisplayCardsForTurn((int)(Board.instance.token));
    }
}

