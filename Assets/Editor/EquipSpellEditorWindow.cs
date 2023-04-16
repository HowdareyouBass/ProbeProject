using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EquipSpellEditorWindow : ExtendedEditorWindow
{
    private static SpellDatabase db;
    public static bool isInPlaymode = true;

    //private static List<string> projectileProperties = new List<string>();

    public static void Open(SpellDatabase _db)
    {
        EquipSpellEditorWindow window = GetWindow<EquipSpellEditorWindow>("Equip Spell");
        db = _db;
        window.serializedObject = new SerializedObject(db);

        //projectileProperties = new List<string>
        //{
        //    "Name",
        //    "Type"
        //};

    }

    private void OnGUI()
    {
        if (!isInPlaymode)
        {
            Close();
        }
        currentProperty = serializedObject.FindProperty("spells");
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));
        DrawSidebar(currentProperty);
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));
        if (selectedProperty != null)
        {
            EditorGUILayout.LabelField("Here you can temporarely change spell stats");
            if (db.spells[propertyIndex].GetSpellType() == Spell.Types.projectile)
            {
                //DrawPropertiesFromList(selectedProperty, projectileProperties, true);
            }
        }
        else
        {
            EditorGUILayout.LabelField("Select an item from the list");
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();

    }
}
