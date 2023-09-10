using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(S_GetDataFromEntityStatusEffectsComponent))]
public class S_GetDataFromEntityStatusEffectsComponentCustomPropertyDrawer : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        VisualElement root = new VisualElement();
        SerializedProperty getDataEffectProperty = property.FindPropertyRelative("S_GetDataFromEntityStatusEffectsComponent");
        PropertyField getDataEffectPropertyField = new PropertyField(getDataEffectProperty); 
        root.Add(getDataEffectPropertyField);
        return root;
    }
}