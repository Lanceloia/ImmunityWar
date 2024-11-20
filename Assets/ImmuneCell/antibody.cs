using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiBody : MonoBehaviour
{
    public GameObject bCell;//生产该抗体的b细胞
    public AntigenType antigenType ;
    public Position p;

    void start()
    {
        antigenType = bCell.GetComponent<BCell>().antigenType;
    }
    public void attack(GameObject target)
    {
        if(antigenType == target.GetComponent<Pathogen>().antigenType)
            target.GetComponent<Pathogen>().beAntibodyed(this);
    }



}
