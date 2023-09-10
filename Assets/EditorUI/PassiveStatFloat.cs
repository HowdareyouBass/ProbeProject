#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class PassiveStatFloat : VisualElement
{
    #region Boilerplate for Showing up in UI Builder
    public new class UxmlFactory : UxmlFactory<PassiveStatFloat> { }
    public PassiveStatFloat() { }
    #endregion

    private FloatField StatValue => this.Q<FloatField>("stat-value");
    private FloatField PerLevel => this.Q<FloatField>("per-level");

    public PassiveStatFloat(SerializedProperty property, string label = "")
    {
        Init(property, label);
    }

    public void Init(SerializedProperty property, string label = "")
    {
        VisualTreeAsset asset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
            "Assets/Editor/PassiveStatWithAddPerLevel.uxml"
        );
        asset.CloneTree(this);

        StatValue.label = label;
        StatValue.BindProperty(property);
    }
}
#endif