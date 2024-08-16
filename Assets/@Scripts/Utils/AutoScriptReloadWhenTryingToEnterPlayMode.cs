using UnityEditor;

namespace Utils
{
    [InitializeOnLoad]
    public class AutoScriptReloadWhenTryingToEnterPlayMode
    {
        // register an event handler when the class is initialized
        static AutoScriptReloadWhenTryingToEnterPlayMode()
        {
            EditorApplication.playModeStateChanged += LogPlayModeState;
        }

        private static void LogPlayModeState(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
                AssetDatabase.Refresh();
        }
    }
}