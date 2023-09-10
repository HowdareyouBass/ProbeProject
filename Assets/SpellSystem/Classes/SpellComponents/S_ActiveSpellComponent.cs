using UnityEngine;

[System.Serializable]
public class S_ActiveSpellComponent : SpellComponent
{
    [SerializeField] private int m_EnergyCost;
    [SerializeField] private int m_HeatlhCost;
    [SerializeField] private float m_CooldownInSeconds = 5;
    [SerializeField] private GameObject m_Effect;

    public bool OnCooldown => CurrentCooldown > 0;
    public float CurrentCooldown { get; private set; } = 0;

    protected Vector3 effectPosition;

    public void DecreaseCooldown()
    {
        if (OnCooldown)
            CurrentCooldown -= Time.deltaTime;
    }
    public void GoOnCooldown()
    {
        CurrentCooldown = m_CooldownInSeconds * casterEntity.stats.SpellCooldownCoefficient;
    }
    public override void Init()
    {
        spell.events.GetEvent(SpellEventName.OnTryCast).Subscribe(TryCast);
    }
    public void TryCast()
    {
        if (!OnCooldown)
        {
            effectPosition = target.GetPoint();
            GoOnCooldown();
            Cast(caster, target);
        }
    }
    protected virtual void Cast(Transform caster, Target target)
    {
        //TODO: Take Energy Cost
        casterEntity.TakeDamage(m_HeatlhCost);
        spell.events.GetEvent(SpellEventName.OnCast).Trigger();
        if (m_Effect != null)
            GameObject.Instantiate(m_Effect, effectPosition, Quaternion.identity);
        else
            Debug.Log("No spell effect");
    }
}