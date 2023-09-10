using UnityEngine;

public class S_SpotCastSpell : S_ActiveSpellComponent
{
    [SerializeField] private int m_CastRange;
    public int castRange { get => m_CastRange / 100; }
}