using UnityEngine;

[System.Serializable]
public class ActiveSpellComponent : SpellComponent
{
    [SerializeField] private int m_EnergyCost;
    [SerializeField] private int m_HeatlhCost;
    [SerializeField] private float m_CooldownInSeconds = 5;
    [SerializeField] private GameObject m_Effect;

    public bool onCooldown { get => currentCooldown > 0; }
    public float currentCooldown { get; protected set; } = 0;
    public GameObject effect { get => m_Effect; }

    public void DecreaseCooldown()
    {
        if (currentCooldown >= 0)
            currentCooldown -= Time.deltaTime;
    }
    public void TryCast()
    {
        if (!onCooldown)
        {
            currentCooldown = m_CooldownInSeconds;
            spell.events.GetEvent(SpellEventName.OnCast).Trigger();
            Cast(caster, target);
        }
    }
    protected virtual void Cast(Transform caster, Target target)
    {
        //TODO: Take Energy Cost
        casterEntity.TakeDamage(m_HeatlhCost);
        spell.events.GetEvent(SpellEventName.OnCast).Trigger();
        GameObject.Instantiate(m_Effect, target.GetPoint(), Quaternion.identity);
    }
}