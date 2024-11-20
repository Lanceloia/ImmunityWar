using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiBodyBuild : Card
{
    // Start is called before the first frame update
    void Start()
    {
        cardType = CardType.Action;
        index = 14;
        cardName = "AntiBodyBuild";
        ATPValue = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void CardEffect()
    {
       ///Board.instance.stemCellList[(int)(Board.instance.token)].GetComponent<Bstem>().AntiBodyBuild();
    }
}
