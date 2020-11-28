using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSkin : MonoBehaviour
{
    public Sprite[] sprites;
    public static int tankNumber;
    public int nonNumber;
    
    void Start()
    {
        
        if (gameObject.name == "TankBody")
        {
            tankNumber = 1;
        }
        nonNumber = tankNumber;
        this.GetComponent<SpriteRenderer>().sprite = sprites[nonNumber - 1];

    }
    void Update()
    {
        nonNumber = tankNumber;
        this.GetComponent<SpriteRenderer>().sprite = sprites[nonNumber - 1];
    }


    public void ChangeSkin(int number)
    {
        tankNumber = number;
    }

    
}
