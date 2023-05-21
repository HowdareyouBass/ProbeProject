using UnityEngine;

public abstract class ActiveSpell : SpellComponent, ICastable
{
    [SerializeField] private uint m_EnergyCost;
    [SerializeField] private uint m_HeatlhCost;

    private enum EffectPlacement { Caster, Target };
    [SerializeField] protected GameObject m_Effect { get; private set; }

    public virtual void Cast(Transform caster, Transform target)
    {
        Debug.Log("Casterd");
        casterEntity.TakeDamage(m_HeatlhCost);
        //TODO: Take Energy Cost
    }
}