using UnityEngine;

public abstract class ActiveSpell : SpellComponent, ICastable
{
    [SerializeField] private uint m_EnergyCost;
    [SerializeField] private uint m_HeatlhCost;
    [SerializeField] private float m_CooldownInSeconds;
    [SerializeField] private GameObject m_Effect;

    private enum EffectPlacement { Caster, Target };
    public GameObject effect { get => m_Effect; }

    public virtual void Cast(Transform caster, Transform target)
    {
        casterEntity.TakeDamage(m_HeatlhCost);
        //TODO: Take Energy Cost
    }
}