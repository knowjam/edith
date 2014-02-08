using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(TestScript))]
[CanEditMultipleObjects]
public class TestScriptEditor : Editor
{
    SerializedProperty damageProp;

    public void OnEnable()
    {
        damageProp = serializedObject.FindProperty("damage");
        Debug.Log(damageProp);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.IntSlider(damageProp, 0, 100, new GUIContent("Damage"));
        // Only show the damage progress bar if all the objects have the same damage value:
        if (!damageProp.hasMultipleDifferentValues)
        {
            ProgressBar(damageProp.intValue / 100.0f, "Damage");
        }

        if (GUILayout.Button("Test"))
        {
            Debug.Log("It's alive: " + target.name);
        }

        if (GUILayout.Button("Reset All"))
        {
            (target as TestScript).transform.rotation = Quaternion.identity;
            (target as TestScript).transform.localPosition = Vector3.zero;
        }

        serializedObject.ApplyModifiedProperties();
    }

    // Custom GUILayout progress bar.
    void ProgressBar(float value, string label)
    {
        // Get a rect for the progress bar using the same margins as a textfield:
        var rect = GUILayoutUtility.GetRect(18, 18, "TextField");
        EditorGUI.ProgressBar(rect, value, label);
        EditorGUILayout.Space();
    }
}
