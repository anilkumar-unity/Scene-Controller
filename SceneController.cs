using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class SceneController : EditorWindow
{
    private string[] sceneNames;
    private int selectedSceneIndex = 0;

    [MenuItem("Window/Scene Controller")]
    public static void ShowWindow()
    {
        GetWindow<SceneController>("Scene Controller");
    }

    private void OnEnable()
    {
        RefreshSceneList();
    }
    private void OnGUI()
    {
        GUILayout.Label("Switch Scene", EditorStyles.boldLabel);

        // Display a dropdown with scene names
        selectedSceneIndex = EditorGUILayout.Popup("Select Scene", selectedSceneIndex, sceneNames);

        if (GUILayout.Button("Switch Scene"))
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                // Load the selected scene
                EditorSceneManager.OpenScene("Assets/" + sceneNames[selectedSceneIndex]);
            }
        }

        if (GUILayout.Button("Refresh Scene List"))
        {
            RefreshSceneList();
        }
    }

    private void RefreshSceneList()
    {
        // Get all available scene paths in the project
        string[] scenePaths = AssetDatabase.FindAssets("t:Scene");
        sceneNames = new string[scenePaths.Length];

        for (int i = 0; i < scenePaths.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(scenePaths[i]);
            sceneNames[i] = System.IO.Path.GetFileName(path);
        }
    }
}
