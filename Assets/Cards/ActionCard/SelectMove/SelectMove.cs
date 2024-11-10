using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMove : Card
{
    // Start is called before the first frame update
    void Start()
    {
        cardType = CardType.Action;
        index = 2;
        cardName = "Chasing";
        cardDiscription = "Select your next move step.";
                
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void CardEffect(){
        Board.instance.isSelectMove = true;
        Board.instance.cardUIManager.GetComponent<CardUIManager>().DisplaySelector(0);
    }
}
