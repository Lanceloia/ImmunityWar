using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform testTransformObj;

    // Start is called before the first frame update
    void Start()
    {
        testTransformObj.SetParent(transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
