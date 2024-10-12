using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hello : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("nohello");
        Dog dog1 = new("china yellow","zwt", "xixixi");
        dog1.Cry();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public class Pet
    {
        public string name;
        public string sound;
        public Pet(string name,string sound)
        {
            this.name = name;
            this.sound = sound;
        }
        public virtual void Cry()
        {
            Debug.Log("Aaaahhh");
        }
    }
    public class Dog : Pet
    {
        public string type;
        public Dog(string type,string name ,string sound):base(name,sound)
        {
            this.type = type;
        }
        public override void Cry()
        {
            Debug.Log($"{name} said: {sound}");
        }
    }
}
