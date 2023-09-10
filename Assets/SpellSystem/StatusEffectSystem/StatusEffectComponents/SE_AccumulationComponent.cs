using UnityEngine;

public class SE_AccumulationComponent : SE_CountComponent
{
    [field: SerializeField] public StatusEffect EffectAppliedAfterAccumulated { get; private set; }
    public override void Init()
    {
        OnEffectDeapplied += (float amountOverpassed) => { ApplyEffect(); };
        base.Init();
    }

    private void ApplyEffect()
    {
        target.GetComponent<StatusEffectHandler>().AddEffect(ScriptableObject.Instantiate(EffectAppliedAfterAccumulated));
    }
}