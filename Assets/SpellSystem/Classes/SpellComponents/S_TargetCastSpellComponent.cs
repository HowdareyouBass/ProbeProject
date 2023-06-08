using UnityEngine;

[System.Serializable]
public class S_TargetCastSpellComponent : S_ActiveSpellComponent
{
    [SerializeField] private int m_CastRange;

    public int castRange { get => m_CastRange / 100; }
}