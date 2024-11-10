using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPteammate : Card
{
    // Start is called before the first frame update
    void Start()
    {
        cardType = CardType.Action;
        index = 4;
        cardName = "Chemokines";
        cardDiscription = "Choose a teammate,TP him to you.";
                
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void CardEffect(){

        Board.instance.cardUIManager.GetComponent<CardUIManager>().DisplaySelector(1);
    }
}
