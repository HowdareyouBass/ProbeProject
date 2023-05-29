using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class TargetCastSpellComponent : ActiveSpellComponent
{
    [SerializeField] private int m_CastRange;

    public int castRange { get => m_CastRange / 100; }

    #if UNITY_EDITOR
    public override void DrawGUI()
    {
        base.DrawGUI();
        m_CastRange = EditorGUILayout.IntField("Cast Range", m_CastRange);
    }
    #endif
}