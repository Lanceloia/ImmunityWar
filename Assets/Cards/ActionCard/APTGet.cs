using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APTGet : Card
{
    // Start is called before the first frame update
    void Start()
    {
        cardType = CardType.Action;
        index = 5;
        cardName = "Mitochondria";
        cardDiscription = "Get 1 ATP, lasting 3 rounds.";                
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void CardEffect(){
       
       Board.instance.stemCellList[(int)(Board.instance.token)].GetComponent<StemCell>().ATPbuffRound = 3;
    }
}
