using System.Collections;
using UnityEngine;

public abstract class ActiveSpell : SpellComponent, ICastable
{
    [SerializeField] private uint m_EnergyCost;
    [SerializeField] private uint m_HeatlhCost;
    [SerializeField] private float m_CooldownInSeconds;
    [SerializeField] private GameObject m_Effect;

    protected bool m_OnCooldown { get; private set; } = false;
    public float currentCooldown { get; protected set; }
    public GameObject effect { get => m_Effect; }

    public void TryCast(Transform caster, Transform target)
    {
        if (!m_OnCooldown)
        {
            currentCooldown = m_CooldownInSeconds;
            StartCoroutine(CooldownRoutine());
            //TODO: Take Energy Cost
            Cast(caster, target);
        }
    }
    protected virtual void Cast(Transform caster, Transform target)
    {
        casterEntity.TakeDamage(m_HeatlhCost);
    }

    private IEnumerator CooldownRoutine()
    {
        m_OnCooldown = true;
        while (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
            yield return null;
        }
        m_OnCooldown = false;
        yield break;
    }
}