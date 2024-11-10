using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMove : Card
{
    // Start is called before the first frame update
    void Start()
    {
        cardType = CardType.Action;
        index = 3;
        cardName = "Backword";
        cardDiscription = "Your next move will be reversed.";
                
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void CardEffect(){
        Board.instance.isBackMove = true;
    }
}
