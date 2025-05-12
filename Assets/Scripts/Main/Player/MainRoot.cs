using UnityEngine;
using UnityEngine.InputSystem;

internal sealed class MainRoot : MonoBehaviour, ISceneController {

    private const e_sceneIndex MYSELF = e_sceneIndex.main;
    private e_sceneIndex _nextScene = MYSELF;

    [SerializeField] private InputActionAsset _assets = default;

    [SerializeField] private PlayerRoot _player = default;
    public e_sceneIndex GetToken() {
        return MYSELF;
    }
    public void OnSceneStart() {
        _player.PlayerStart();
    }
    public e_sceneIndex SceneUpdate() {
        _player.PlayerUpdate();
        return MYSELF;
    }
    public void SceneFixedUpdate() {

    }
    public void OnSceneExit() {

    }
#if UNITY_EDITOR
    private void Start() {
        OnSceneStart();
    }
    private void Update() {
        SceneUpdate();
    }
#endif
}