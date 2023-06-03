using UnityEngine;
using System.Threading.Tasks;

[System.Serializable]
public class SleepComponent : StatusEffectComponent
{
    [SerializeField] private float m_DurationInSeconds;
    private float m_CurrentDuration = 0f;

    public override void Init()
    {
        m_CurrentDuration = m_DurationInSeconds;
        statusEffect.effectTasks.Add(Sleep());
        //Task.Run(Sleep);
    }

    private async Task Sleep()
    {
        statusEffect.targetEntity.Sleep();
        while (!statusEffect.stopEffectToken.IsCancellationRequested && m_CurrentDuration > 0)
        {
            m_CurrentDuration -= Time.deltaTime;
            await Task.Yield();
        }
        statusEffect.targetEntity.Awake();
    }
}