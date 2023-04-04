using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBase
{
    public virtual GameObject CastEffect()
    {
        Debug.Log("Empty or passive");
        return new GameObject();
    }
}
