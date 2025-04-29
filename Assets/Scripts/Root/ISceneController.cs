internal interface ISceneController {
    public e_sceneIndex GetToken();
    public void OnSceneStart();
    public e_sceneIndex SceneUpdate();
    public void SceneFixedUpdate();
    public void OnSceneExit();
}
