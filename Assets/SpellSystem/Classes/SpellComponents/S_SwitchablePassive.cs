using UnityEngine;

public class S_SwitchablePassive : SpellComponent
{
    [SerializeField] private SpellStats m_Passive1;
    [SerializeField] private SpellStats m_Passive2;

    private bool m_IsSwitched;

    public override void Init()
    {
        spell.events.GetEvent(SpellEventName.OnCast).Subscribe(Switch);
    }
    public override void Destroy()
    {
        spell.events.GetEvent(SpellEventName.OnCast).Unsubscribe(Switch);
    }

    private void Switch()
    {
        m_IsSwitched = !m_IsSwitched;
        if (m_IsSwitched)
        {
            //targetEntity.DeapplyPassive(m_Passive1);
            //targetEntity.ApplyPassive(m_Passive2);
        }
        else
        {
            //targetEntity.ApplyPassive(m_Passive1);
            //targetEntity.DeapplyPassive(m_Passive2);
        }
    }
}