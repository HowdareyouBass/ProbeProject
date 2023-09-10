#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;

public static class ExtendedEditor
{
    public static IEnumerable<SerializedProperty> GetChildren(SerializedProperty serializedProperty)
    {
        SerializedProperty currentProperty = serializedProperty.Copy();
        SerializedProperty nextSiblingProperty = serializedProperty.Copy();
        {
            nextSiblingProperty.Next(false);
        }
    
        if (currentProperty.Next(true))
        {
            do
            {
                if (SerializedProperty.EqualContents(currentProperty, nextSiblingProperty))
                    break;
    
                yield return currentProperty;
            }
            while (currentProperty.Next(false));
        }
    }
}
#endif