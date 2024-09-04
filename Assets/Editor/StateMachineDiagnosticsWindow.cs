using Core.Infrastructure.StateMachine;
using UnityEditor;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Editor
{
    public class StateMachineDiagnosticsWindow : EditorWindow
    {
        private IObjectResolver _resolver;
        private GameStateMachine _stateMachine;
        private string _currentStateName;

        [MenuItem("Window/State Machine Diagnostics")]
        public static void ShowWindow()
        {
            GetWindow<StateMachineDiagnosticsWindow>("State Machine Diagnostics");
        }

        private void OnEnable()
        {
            EditorApplication.update += UpdateState;
        }

        private void OnDisable()
        {
            EditorApplication.update -= UpdateState;
        }

        private void UpdateState()
        {
            if (!Application.isPlaying) return;
            
            _resolver ??= FindObjectOfType<LifetimeScope>()?.Container;

            if (_resolver != null && _stateMachine == null)
            {
                _stateMachine = _resolver.Resolve<GameStateMachine>();
            }

            if (_stateMachine == null) return;
            _currentStateName = _stateMachine.CurrentState.GetType().Name;
            Repaint();
        }

        private void OnGUI()
        {
            if (!Application.isPlaying)
            {
                GUILayout.Label("Enter Play Mode");
                return;
            }
            
            if (_resolver == null)
            {
                GUILayout.Label("LifetimeScope not found");
                return;
            }

            if (_stateMachine == null)
            {
                GUILayout.Label("State Machine not found");
                return;
            }

            var stateStyle = new GUIStyle(EditorStyles.label)
            {
                fontSize = 19,
                fontStyle = FontStyle.Bold,
                //alignment = TextAnchor.UpperCenter
            };

            GUILayout.Label(_currentStateName, stateStyle);
        }
    }
}