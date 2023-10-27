using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioManager))]
public class PlayerAudio : MonoBehaviour
{
    private AudioManager m_AudioManager;
    private Attack m_Attack;
    private SpellInventory m_SpellInventory;
    private Entity m_Player;

    private void Awake()
    {
        m_Attack = GetComponentInParent<Attack>();
        m_SpellInventory = GetComponentInParent<SpellInventory>();
        m_AudioManager = GetComponent<AudioManager>();
        m_Player = GetComponentInParent<PlayerScript>().GetEntity();
    }

    private void Start()
    {
        m_Attack.OnAttackPerformed += PlayAttackSound;
        m_Player.Events.GetEvent(EntityEventName.OnDeath).Subscribe(PlayDeathSound);
        for(int i = 0; i < SpellInventory.MAXIMUM_SPELLS; i++)
        {
            Spell spell = m_SpellInventory.GetSpell(i);
            m_AudioManager.AddSound(new Sound(spell.SpellSound, spell.Name, spell.SpellSoundVolume));
            spell.events.GetEvent(SpellEventName.OnCast).Subscribe(() => { m_AudioManager.Play(spell.Name); Debug.Log("Should play" + spell.Name); });
        }
    }
    private void OnDisable()
    {

    }
    private void PlayDeathSound()
    {
        m_AudioManager.Play("Death");
    }
    private void PlayAttackSound()
    {
        m_AudioManager.Play("Attack");
    }

    private void Update()
    {
        
    }
}
