using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpellDatabaseEditorWindow : ExtendedEditorWindow
{
    private static SpellDatabase db;

    public static List<string> directedAtEnemyProperties {private set; get;}
    public static List<string> projectileProperties{private set; get;}
    public static List<string> directedAtGroundProperties {private set; get;}
    public static List<string> passiveProperties {private set; get;}
    public static List<string> playerCastProperties {private set; get;}

    public SpellDatabaseEditorWindow()
    {
        projectileProperties = new List<string>
        {
            "Name",
            "Type"
        };

        directedAtEnemyProperties = new List<string>
        {
            "Name",
            "Type"
        };

        directedAtGroundProperties = new List<string>
        {
            "Name",
            "Type"
        };

        passiveProperties = new List<string>
        {
            "Name",
            "Type"
        };

        playerCastProperties = new List<string>
        {
            "Name",
            "Type"
        };
    }

    public static void Open(SpellDatabase _db)
    {
        SpellDatabaseEditorWindow window = GetWindow<SpellDatabaseEditorWindow>("Spell Database Editor");
        db = _db;
        window.serializedObject = new SerializedObject(db);

        
    }

    private void OnGUI()
    {
        if (serializedObject == null)
        {
            db = Resources.Load<SpellDatabase>("SpellDatabase");
            serializedObject = new SerializedObject(db);
        }

        currentProperty = serializedObject.FindProperty("spells");
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(250), GUILayout.ExpandHeight(true));
        DrawSidebar(currentProperty);
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));
        if (selectedProperty != null)
        {
            Spell.Types spellType = db.spells[propertyIndex].GetSpellType();
            
            if (spellType == Spell.Types.projectile)
            {
                DrawPropertiesFromList(selectedProperty, projectileProperties, true);
            }
            else if (spellType == Spell.Types.directedAtEnemy)
            {
                DrawPropertiesFromList(selectedProperty, directedAtEnemyProperties, true);
            }
            else if (spellType == Spell.Types.directedAtGround)
            {
                DrawPropertiesFromList(selectedProperty, directedAtGroundProperties, true);
            }
            else if (spellType == Spell.Types.passive)
            {
                DrawPropertiesFromList(selectedProperty, passiveProperties, true);
            }
            else if (spellType == Spell.Types.playerCast)
            {
                DrawPropertiesFromList(selectedProperty, playerCastProperties, true);
            }
            else
            {
                DrawProperties(selectedProperty, true);
            }
            if (GUILayout.Button("Delete Spell"))
            {
                db.spells.RemoveAt(propertyIndex);
                serializedObject.Update();
            }
        }
        else
        {
            EditorGUILayout.LabelField("Select spell from the list");
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add"))
        {
            db.spells.Add(new Spell(""));
            serializedObject.Update();
        }
        EditorGUILayout.EndHorizontal();
    }

}
