using System;

[Serializable]
public class S_SwitchablePassive : SpellComponent
{
    protected PassiveStats m_Passive1;
    protected PassiveStats m_Passive2;
    public event Action OnSwitch;

    public bool isSwitched { get; private set; }

    public override void Init()
    {
        targetEntity.ApplyPassive(m_Passive1);
        spell.events.GetEvent(SpellEventName.OnCast).Subscribe(Switch);
    }
    public override void Destroy()
    {
        spell.events.GetEvent(SpellEventName.OnCast).Unsubscribe(Switch);
    }

    private void Switch()
    {
        OnSwitch?.Invoke();
        isSwitched = !isSwitched;
        if (isSwitched)
        {
            targetEntity.DeapplyPassive(m_Passive1);
            targetEntity.ApplyPassive(m_Passive2);
        }
        else
        {
            targetEntity.ApplyPassive(m_Passive1);
            targetEntity.DeapplyPassive(m_Passive2);
        }
    }
}