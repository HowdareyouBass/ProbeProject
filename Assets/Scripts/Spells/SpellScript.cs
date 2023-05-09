using UnityEngine;

public class SpellScript : MonoBehaviour
{
    //TODO: delete this after completing all the spells
    [SerializeField] private Transform m_Caster; 
    [SerializeField] private Transform m_Target;
    //

    public Transform caster { get => m_Caster; }
    public Transform target { get => m_Target; }

    private SpellEvents m_SpellEvents;
    private void Awake()
    {
        //TODO: delete this after completing all the spells
        Init(m_Caster, m_Target);
        m_SpellEvents = new SpellEvents();
    }
    public void Init(Transform caster, Transform target)
    {
        m_Caster = caster;
        m_Target = target;
        foreach (SpellComponent component in GetComponents<SpellComponent>())
        {
            component.caster = caster;
            component.target = target;
            component.spellScript = this;
        }
    }

    public GameEvent GetEvent(SpellEventName name)
    {
        return m_SpellEvents.events[name];
    }
}
