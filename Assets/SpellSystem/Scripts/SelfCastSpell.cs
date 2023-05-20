using UnityEngine;

[RequireComponent(typeof(SpellScript))]
public class SelfCastSpell : SpellComponent, ICastable
{
    [SerializeField] private GameObject m_Effect;

    private void Awake()
    {
        GetComponent<SpellScript>().Init(transform.parent, null);
    }

    public void Cast(Transform caster, Transform target)
    {
        spellScript.events.GetEvent(SpellEventName.OnCast).Trigger();
        if (m_Effect != null)
            Instantiate(m_Effect, transform);
    }
}
