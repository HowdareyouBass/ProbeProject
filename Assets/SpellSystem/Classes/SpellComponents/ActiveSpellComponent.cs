using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class ActiveSpellComponent : SpellComponent
{
    [SerializeField] private int m_EnergyCost;
    [SerializeField] private int m_HeatlhCost;
    [SerializeField] private float m_CooldownInSeconds;
    [SerializeField] private GameObject m_Effect;

    protected bool m_OnCooldown { get => currentCooldown > 0; }
    public float currentCooldown { get; protected set; } = 0;
    public GameObject effect { get => m_Effect; }

    public void DecreaseCooldown()
    {
        if (currentCooldown >= 0)
            currentCooldown -= Time.deltaTime;
    }
    public void TryCast()
    {
        if (!m_OnCooldown)
        {
            currentCooldown = m_CooldownInSeconds;
            Cast(caster, target);
            spell.events.GetEvent(SpellEventName.OnCast).Trigger();
        }
    }
    protected virtual void Cast(Transform caster, Transform target)
    {
        //TODO: Take Energy Cost
        casterEntity.TakeDamage(m_HeatlhCost);
        spell.events.GetEvent(SpellEventName.OnCast).Trigger();
        //Instantiate(m_Effect, caster.position, Quaternion.identity);
    }
    #if UNITY_EDITOR
    public override void DrawGUI()
    {
        m_EnergyCost = EditorGUILayout.IntField("Energy Cost", m_EnergyCost);
        m_HeatlhCost = EditorGUILayout.IntField("Health Cost", m_HeatlhCost);
        m_CooldownInSeconds = EditorGUILayout.FloatField("Cooldwon In Seconds", m_CooldownInSeconds);
        m_Effect = (GameObject)EditorGUILayout.ObjectField("Effect", m_Effect, typeof(GameObject), false);
    }
    #endif
}