using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiBody : MonoBehaviour
{
    public GameObject bCell;//�����ÿ����bϸ��
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
