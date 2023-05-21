using UnityEngine;

public class Health : MonoBehaviour
{
    private Entity m_Entity;

    private void Start()
    {
        m_Entity = gameObject.GetComponent<EntityScript>().GetEntity();
    }

    private void FixedUpdate()
    {
        m_Entity.Regenerate();
    }

    public void TakeDamage(float amount)
    {
        if(amount == 0)
        {
            Debug.Log("Damage was 0");
            return;
        }
        m_Entity.TakeDamage(amount);
    }
}