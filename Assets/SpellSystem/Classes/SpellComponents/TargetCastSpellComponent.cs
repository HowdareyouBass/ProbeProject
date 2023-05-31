using UnityEngine;

[System.Serializable]
public class TargetCastSpellComponent : ActiveSpellComponent
{
    [SerializeField] private int m_CastRange;

    public int castRange { get => m_CastRange / 100; }
}