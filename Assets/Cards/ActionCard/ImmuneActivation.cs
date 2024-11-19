using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmuneActivation : Card
{
    // Start is called before the first frame update
    void Start()
    {
        cardType = CardType.Action;
        index = 9;
        cardName = "ImmuneActivation";
        cardDiscription = "every player runs faster and gains more ATP in next 2 rounds.";
                
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void CardEffect(){
        Board.instance.immuneBuffRound = 2;
    }
}
