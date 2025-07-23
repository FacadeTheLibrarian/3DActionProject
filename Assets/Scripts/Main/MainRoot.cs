using UnityEngine;
using UnityEngine.InputSystem;

internal sealed class MainRoot : MonoBehaviour, ISceneController {

    private const e_sceneIndex MYSELF = e_sceneIndex.main;
    private e_sceneIndex _nextScene = MYSELF;

    [SerializeField] private InputActionAsset _assets = default;
    [SerializeField] private MonsterData _monsterData = default;

    [SerializeField] private Player _player = default;
    [SerializeField] private EnemyControl _enemyControl = default;
    public e_sceneIndex GetToken() {
        return MYSELF;
    }
    public void OnSceneStart() {
        _player.PlayerStart(_monsterData);
        _enemyControl.EnemyControlStart(_player);
    }
    public e_sceneIndex SceneUpdate() {
        _player.PlayerUpdate();
        _enemyControl.EnemyControlUpdate();
        return MYSELF;
    }
    public void SceneFixedUpdate() {

    }
    public void OnSceneExit() {
        _player.Dispose();
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