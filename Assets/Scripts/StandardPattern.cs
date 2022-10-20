using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardPattern : ProjectileBase // Inheritance
{


    private new void Start()
    {
        base.Start();
        pointValue = 1;
    }
}
