

public class SE_BarrierComponent : SE_CountComponent
{
    public override void Init()
    {
        OnEffectApplied += targetEntity.EnableBarrier;
        OnEffectDeapplied += targetEntity.DisableBarrier;
        base.Init();
    }
}