using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ChangeScene : MonoBehaviour
{
    [HideInInspector]
    public int SceneIndex;

    public void LoadLevel()
    {
        LoadLevel(SceneIndex);   
    }

    public void LoadLevel(int i)
    {
        SceneManager.LoadScene(i);
    }
}

#if UNITY_EDITOR
//Custom editor code for scene drop down list
[CustomEditor(typeof(ChangeScene))]
public class SceneDropDown : Editor
{
    List<string> names;
    int choiceIndex;

    void OnEnable()
    {
        choiceIndex = serializedObject.FindProperty("SceneIndex").intValue;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        //update scenes list
        names = new List<string>();
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            if (EditorBuildSettings.scenes[i].enabled)
                names.Add(Path.GetFileNameWithoutExtension(EditorBuildSettings.scenes[i].path));
        }

        //display choice UI elements
        choiceIndex = EditorGUILayout.Popup("Scene:", choiceIndex, names.ToArray());

        //set values
        ChangeScene obj = target as ChangeScene;
        obj.SceneIndex = choiceIndex;
        EditorUtility.SetDirty(target);
    }
}
#endif
