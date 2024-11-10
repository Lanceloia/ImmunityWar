using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleMove : Card
{
    // Start is called before the first frame update
    void Start()
    {
        cardType = CardType.Action;
        index = 1;
        cardName = "Inflammatory";
        cardDiscription = "Double your next move.";
                
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void CardEffect(){
        Board.instance.isDoubleMove = true;
    }
}
