using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class SceneController : EditorWindow
{
    private string[] sceneNames;
    private int selectedSceneIndex = 0;

    // Define a menu item to open the Scene Controller window
    [MenuItem("Window/Scene Controller")]
    public static void ShowWindow()
    {
        GetWindow<SceneController>("Scene Controller");
    }

    private void OnEnable()
    {
        // Initialize the scene list when the window is enabled
        RefreshSceneList();
    }

    private void OnGUI()
    {
        GUILayout.Label("Switch Scene", EditorStyles.boldLabel);

        // Display a dropdown with scene names for selection
        selectedSceneIndex = EditorGUILayout.Popup("Select Scene", selectedSceneIndex, sceneNames);

        // Button to switch to the selected scene
        if (GUILayout.Button("Switch Scene"))
        {
            // Check if current scenes are saved before switching
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                // Load the selected scene based on its name
                EditorSceneManager.OpenScene("Assets/" + sceneNames[selectedSceneIndex]);
            }
        }

        // Button to refresh the scene list
        if (GUILayout.Button("Refresh Scene List"))
        {
            // Refresh the list of available scenes
            RefreshSceneList();
        }
    }

    private void RefreshSceneList()
    {
        // Retrieve a list of available scene paths in the project
        string[] scenePaths = AssetDatabase.FindAssets("t:Scene");
        sceneNames = new string[scenePaths.Length];

        // Extract and store the scene names without extensions
        for (int i = 0; i < scenePaths.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(scenePaths[i]);
            sceneNames[i] = System.IO.Path.GetFileNameWithoutExtension(path);
        }
    }
}
