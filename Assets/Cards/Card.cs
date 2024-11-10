using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    Action = 0,
    Event = 1,
    Ability = 2,
}

abstract public class Card : MonoBehaviour
{

    public CardType cardType;
    public int index;
    public string cardName;
    public string cardDiscription;
    public int ATPValue;
    public int antigenValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void CardEffect();
}
