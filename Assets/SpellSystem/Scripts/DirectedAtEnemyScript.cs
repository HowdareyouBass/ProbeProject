using UnityEngine;

namespace obsolete
{
    [RequireComponent(typeof(TargetCastSpell))]
    public class DirectedAtEnemyScript : SpellComponent1
    {
        [SerializeField] private float m_Damage = 10;

        private void Start()
        {
            if (target.TryGetComponent<Health>(out Health health))
            {
                spellScript.events.GetEvent(SpellEventName.OnImpact).Trigger();
                health.TakeDamage(m_Damage);
                Destroy(gameObject, 10);
            }
        }
    }
}
