using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;

public class CustomListDrawer : VisualElement
{
    #region Boilerplate for Showing up in UI Builder
    public new class UxmlFactory : UxmlFactory<CustomListDrawer> { }
    public CustomListDrawer() { }
    #endregion

    private VisualElement m_Root => this.Q("root");

    public CustomListDrawer(SerializedProperty Property)
    {
        Init(Property);
    }
    public void Init(SerializedProperty Property)
    {
        Debug.Log("Gos");
        foreach(SerializedProperty prop in Property)//
        {
            m_Root.Add(new PropertyField(prop));
        }
    }
}