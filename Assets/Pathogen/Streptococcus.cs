using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Streptococcus : Pathogen
{
    void Awake()
    {
        type = PathogenType. strep;
        antigenType = AntigenType.Strep;
        health = 10;
        speed = 3f;
        isIn = false;
    }

    public override void onHurt(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            
            Destroy(gameObject);
            Board.instance.pathogenList.Remove(this.gameObject);
        }
    }
}
