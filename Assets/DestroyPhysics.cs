using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPhysics : MonoBehaviour
{
    [SerializeField] private float m_TimeBeforeDestroyingInSeconds;
    

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DeletePhysics", m_TimeBeforeDestroyingInSeconds);
    }
    private void DeletePhysics()
    {
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<MeshCollider>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
