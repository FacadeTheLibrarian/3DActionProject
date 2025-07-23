internal interface ISceneController {
    e_sceneIndex GetToken();
    void OnSceneStart();
    e_sceneIndex SceneUpdate();
    void SceneFixedUpdate();
    void OnSceneExit();
}
