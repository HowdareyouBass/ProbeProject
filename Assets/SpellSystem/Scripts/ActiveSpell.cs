using UnityEngine;

namespace obsolete
{
    public abstract class ActiveSpell : SpellComponent1
    {
        [SerializeField] private uint m_EnergyCost;
        [SerializeField] private uint m_HeatlhCost;
        [SerializeField] private float m_CooldownInSeconds;
        [SerializeField] private GameObject m_Effect;

        public bool onCooldown { get => currentCooldown > 0; }
        public float currentCooldown { get; protected set; } = 0;
        public GameObject effect { get => m_Effect; }

        public void TryCast(Transform caster, Transform target)
        {
            if (!onCooldown)
            {
                currentCooldown = m_CooldownInSeconds;
                Cast(caster, target);
            }
        }
        public void DecreaseCooldown()
        {
            if (currentCooldown > 0)
                currentCooldown -= Time.deltaTime;
        }
        protected virtual void Cast(Transform caster, Transform target)
        {
            //TODO: Take Energy Cost
            casterEntity.TakeDamage(m_HeatlhCost);
        }
    }
}
