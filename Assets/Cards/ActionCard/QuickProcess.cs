using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuickProcess  : Card
{
    // Start is called before the first frame update
    void Start()
    {
        cardType = CardType.Action;
        index = 8;
        cardName = "EfficientProcess";
        cardDiscription = "3 random macrophages get double antigen in next round.";
                
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void CardEffect(){

        List<GameObject> macroItems = Board.instance.immuneCellList.Where(item => (int)item.GetComponent<ImmuneCell>().type == 0).ToList();
        macroItems = macroItems.OrderBy(item => Random.value).ToList();
        List<GameObject> randomSelection = macroItems.Take(3).ToList();
        foreach (var item in randomSelection)
        {
            item.GetComponent<MarcophageCell>().getDoubleAntigen = true;
        }
    }
}
