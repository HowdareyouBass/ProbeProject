using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public abstract class SpellComponent : ScriptableObject
{
    public Spell spell;
    public Transform caster;
    public Transform target;
    public Entity casterEntity => caster.GetComponent<EntityScript>().GetEntity();

    public virtual void OnEnable()
    {
        hideFlags = HideFlags.HideInHierarchy;
    }
    #if UNITY_EDITOR
    protected bool m_IsFolded { get; private set; }
    public void OnGUI()
    {
        m_IsFolded = EditorGUILayout.Foldout(m_IsFolded, this.GetType().ToString());
        if (m_IsFolded)
        {
            EditorGUI.indentLevel++;
            DrawGUI();
            EditorGUI.indentLevel--;
        }
    }
    public abstract void DrawGUI();
    #endif
    //MEGA ULTRA SUPER DUPER KOSTYL
    public void AddSelf(Spell spell)
    {
        spell.AddComponent(this);
    }
    public void Init()
    {
        
    }
}