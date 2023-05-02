using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTest : MonoBehaviour
{
    public DirectedAtEnemy spell;
    public Transform target;
    public Transform caster;
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        spell.Cast(caster, target);
    }
}
