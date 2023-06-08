
[System.Serializable]
public class SE_SleepComponent : SE_TimeComponent
{
    public override void Init()
    {
        OnEffectApplied += targetEntity.Sleep;
        OnEffectDeapplied += targetEntity.Awake;
        base.Init();
    }
}