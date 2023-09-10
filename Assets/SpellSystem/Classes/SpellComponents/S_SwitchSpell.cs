using UnityEngine;

public class S_SwitchSpell : SpellComponent
{
    [SerializeField] private Spell m_SpellToSwitchTo;
    [SerializeField] private bool m_GoOnCooldownAfterSwitch;

    public override void Init()
    {
        spell.events.GetEvent(SpellEventName.OnCast).Subscribe(SwitchSpell);
    }

    private void SwitchSpell()
    {
        caster.GetComponent<SpellInventory>().SwitchSpell(spell.InventorySlot, m_SpellToSwitchTo, m_GoOnCooldownAfterSwitch);
    }
}