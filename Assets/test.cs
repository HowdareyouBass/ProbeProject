using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerScript>(out var a))
        {
            transform.GetComponent<Attack>().AttackTarget(new Target(other.transform, other.transform.position, other));
        }
    }
}