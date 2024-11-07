using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCard : Card
{
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void CardEffect(){
        Debug.Log("using test card!");
    }
}
