using System.Collections;
using UnityEngine;

namespace obsolete
{
    [RequireComponent(typeof(SelfCastSpell))]
    public class ActivateStatusEffectOnCast : SpellComponent1
    {
        [SerializeField] private StatusEffectScript m_StatusEffect;
        [SerializeField] private float m_DurationInSeconds;

        private bool m_IsActive;

        private void Awake()
        {
            m_StatusEffect.enabled = false;
        }
        private void OnEnable()
        {
            spellScript.events.GetEvent(SpellEventName.OnCast).Subscribe(ActivateStatusEffect);
        }
        private void OnDisable()
        {
            spellScript.events.GetEvent(SpellEventName.OnCast).Unsubscribe(ActivateStatusEffect);
        }
        private void ActivateStatusEffect()
        {
            if (!m_IsActive)
            {
                m_IsActive = true;
                StartCoroutine(ActivateStatusEffectRoutine());
            }
        }
        private IEnumerator ActivateStatusEffectRoutine()
        {
            m_StatusEffect.enabled = true;
            yield return new WaitForSeconds(m_DurationInSeconds);
            m_StatusEffect.enabled = false;
            m_IsActive = false;
            yield break;
        }
    }
}