using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Card card;
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI cardDiscrip;
    public TextMeshProUGUI cardValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayCard(){
        cardName.text = card.cardName;
        cardDiscrip.text = card.cardDiscription;
        cardValue.text = card.cardValue.ToString();
    } 
}
