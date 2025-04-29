using UnityEngine;
using UnityEngine.InputSystem;

internal sealed class TitleRoot : MonoBehaviour, ISceneController {

    private const e_sceneIndex MYSELF = e_sceneIndex.title;
    private e_sceneIndex _nextScene = MYSELF;

    [SerializeField] InputActionAsset _assets = default;
    private TitleInput _inputs = default;

    public e_sceneIndex GetToken() {
        return MYSELF;
    }
    public void OnSceneStart() {
        _inputs = new TitleInput(_assets);
    }
    public e_sceneIndex SceneUpdate() {
        if(_inputs.MoveDirection != Vector2.zero) {
            return e_sceneIndex.main;
        }
        return _nextScene;
    }
    public void SceneFixedUpdate() {

    }
    public void OnSceneExit() {
        _inputs.Dispose();
    }
}
