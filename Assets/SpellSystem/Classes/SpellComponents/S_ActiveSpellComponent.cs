using UnityEngine;

[System.Serializable]
public class S_ActiveSpellComponent : SpellComponent
{
    [SerializeField] private int m_EnergyCost;
    [SerializeField] private int m_HeatlhCost;
    [SerializeField] private float m_CooldownInSeconds = 5;
    [SerializeField] private GameObject m_Effect;

    public bool onCooldown { get => currentCooldown > 0; }
    public float currentCooldown { get; protected set; } = 0;
    public GameObject effect { get => m_Effect; }

    public void DecreaseCooldown()
    {
        if (currentCooldown >= 0)
            currentCooldown -= Time.deltaTime;
    }
    public override void Init()
    {
        spell.events.GetEvent(SpellEventName.OnTryCast).Subscribe(TryCast);
    }
    public void TryCast()
    {
        if (!onCooldown)
        {
            currentCooldown = m_CooldownInSeconds;
            Cast(caster, target, target.GetPoint());
        }
    }
    protected virtual void Cast(Transform caster, Target target, Vector3 effectPostion)
    {
        //TODO: Take Energy Cost
        casterEntity.TakeDamage(m_HeatlhCost);
        spell.events.GetEvent(SpellEventName.OnCast).Trigger();
        if (m_Effect != null)
            GameObject.Instantiate(m_Effect, effectPostion, Quaternion.identity);
        else
            Debug.Log("No spell effect");
    }
}