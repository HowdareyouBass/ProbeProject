public class SE_SilenceComponent : SE_TimeComponent
{
    public override void Init()
    {
        OnEffectApplied += targetEntity.Silence;
        OnEffectDeapplied += targetEntity.Desilence;
        base.Init();
    }
}