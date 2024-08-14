using UnityEditor;
using UnityEngine;

public class AutoPlayAfterScriptsReload : MonoBehaviour
{
    [MenuItem("Tools/Reload scripts and Enter Play Mode %w")]
    public static void OnScriptsCompiled()
    {
        AssetDatabase.Refresh();
        EditorApplication.EnterPlaymode();
    }
}
