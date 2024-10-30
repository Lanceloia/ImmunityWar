using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundControler : MonoBehaviour
{
    public Dice dice;
    public bool tokenLock;

    private void OnMouseDown()
    {
        if (!tokenLock)
        {
            Board.instance.NextRound();
            dice.canBeUsed = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        tokenLock = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
