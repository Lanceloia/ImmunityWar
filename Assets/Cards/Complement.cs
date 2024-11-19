using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Complement : Card
{
    // Start is called before the first frame update
    void Start()
    {
        cardType = CardType.Action;
        index = 11;
        cardName = "Complement";
        cardDiscription = "xxx";
                
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void CardEffect(){
        
    }
}
