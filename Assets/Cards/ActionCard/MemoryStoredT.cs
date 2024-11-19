using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MemoryStoreT : Card
{
    // Start is called before the first frame update
    void Start()
    {
        cardType = CardType.Action;
        index = 10;
        cardName = "MemoryStored";
        cardDiscription = "3 random TCell get antigen buffed.";
                
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void CardEffect(){
        List<GameObject> TItems = Board.instance.immuneCellList.Where(item => (int)item.GetComponent<ImmuneCell>().type == 2).ToList();
        TItems = TItems.OrderBy(item => Random.value).ToList();
        List<GameObject> randomSelection = TItems.Take(3).ToList();
        foreach (var item in randomSelection)
        {
            //item.GetComponent<TCell>().getDoubleAntigen = true;
            item.GetComponent<TCell>().Upgrade(item.GetComponent<TCell>().grid.GetComponent<Grids>().shape);
        }
    }
}
