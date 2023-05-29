using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class SpellComponent
{
    public Spell spell;
    public Transform caster;
    public Transform target;
    public Entity casterEntity => caster.GetComponent<EntityScript>().GetEntity();

    public virtual void OnEnable()
    {
        Debug.Log("OnEnable spellComponent called");
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
    public virtual void DrawGUI()
    {
    }
    #endif
    //MEGA ULTRA SUPER DUPER KOSTYL
    public void AddSelf(Spell spell)
    {
        //spell.AddComponent(this);
    }
}