using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

//TODO:
//[CustomEditor(typeof(Spell), true)]
public class SpellCustomDrawer : Editor
{
    [SerializeField] private VisualTreeAsset m_VisualTree;

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement inspector = new VisualElement();
        inspector.Add(m_VisualTree.Instantiate());
        
        CustomListDrawer components = inspector.Q<CustomListDrawer>("components");
        components.Init(serializedObject.FindProperty("m_Components"));

        Button addButton = inspector.Q<Button>("add");
        addButton.RegisterCallback<ClickEvent>((clickEvent) => HandleAdd());

        return inspector;
    }

    private void HandleAdd()
    {
        Spell spell = (Spell)target;
        spell.AddComponent(new S_ActiveSpellComponent());
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }
}
