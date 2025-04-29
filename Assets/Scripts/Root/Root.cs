using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

internal enum e_sceneIndex {
    end = -2,
    none = -1,
    title = 0,
    tutorial = 1,
    select = 2,
    main = 3,
    gameover = 4,
    max,
}

internal sealed class Root : MonoBehaviour {
    private const int FPS = 60;

    private const string CONTROLLERS = "GameController";

    private readonly Dictionary<e_sceneIndex, string> SCENE_NAMES = new Dictionary<e_sceneIndex, string>() {
        { e_sceneIndex.title, "Title" },
        { e_sceneIndex.tutorial, "Tutorial" },
        { e_sceneIndex.select, "Select" },
        { e_sceneIndex.main, "Main" },
        { e_sceneIndex.gameover, "Gameover" },
    };

    private readonly Dictionary<e_sceneIndex, float> TIME_TO_DRAW_BACKBOARD = new Dictionary<e_sceneIndex, float>() {
        { e_sceneIndex.title, 1.0f },
        { e_sceneIndex.tutorial, 0.5f },
        { e_sceneIndex.select, 1.25f },
        { e_sceneIndex.main, 1.0f },
        { e_sceneIndex.gameover, 0.5f },
        { e_sceneIndex.end, 1.0f },
    };

    [SerializeField] private RootInitialization _initializer = default;

#if UNITY_EDITOR
    [SerializeField] private e_sceneIndex _sceneToLoadFirstlyForDbg = e_sceneIndex.none;
#endif

    private static Root _instance = default;
    [SerializeField] private bool _isBooted = false;

    private CancellationTokenSource _source = new CancellationTokenSource();
    private CancellationToken _token = default;

    private ISceneController _controller = default;
    private Scene _sceneToken = default;

    private e_sceneIndex _next = e_sceneIndex.none;
    private bool _isSceneChangeRequested = false;
    private bool _onTransition = false;

    private void Awake() {
        _ = Constructor();
    }

    private void Update() {
        if (_onTransition) {
            return;
        }
        e_sceneIndex possibleNext = _controller.SceneUpdate();
        e_sceneIndex current = _controller.GetToken();

        if (possibleNext != current) {
            _isSceneChangeRequested = true;
            _next = possibleNext;
        }
    }

    private void FixedUpdate() {
        if (_onTransition) {
            return;
        }
        _controller.SceneFixedUpdate();
        
    }

    private void LateUpdate() {
        if (!_isSceneChangeRequested) {
            return;
        }

        _isSceneChangeRequested = false;
        _onTransition = true;

        if (_next >= e_sceneIndex.title) {
            _ = SceneTransition(_next, _token);
            return;
        }

        _ = StartExitGame(_token);
    }

    private void OnDestroy() {

    }

    private async Task Constructor() {
        if (_isBooted) {
            return;
        }
        if (_instance == null) {
            Application.targetFrameRate = FPS;
            _onTransition = true;
            _isBooted = true;

            _instance = this;

            _initializer.Initialization();

            _token = _source.Token;
        }

#if UNITY_EDITOR
        if ((int)_sceneToLoadFirstlyForDbg < 0 || _sceneToLoadFirstlyForDbg == e_sceneIndex.max) {
            await SceneManager.LoadSceneAsync(SCENE_NAMES[e_sceneIndex.title], LoadSceneMode.Additive);
        }
        else {
            await SceneManager.LoadSceneAsync(SCENE_NAMES[_sceneToLoadFirstlyForDbg], LoadSceneMode.Additive);
        }
#else
        await SceneManager.LoadSceneAsync(SCENE_NAME[(int)e_sceneIndex.title], LoadSceneMode.Additive);
#endif

        FindSceneToken();
        _controller.OnSceneStart();
        _ = Backboard.GetInstance.ControllBackboardAsync(e_backboardIs.off, FPS, TIME_TO_DRAW_BACKBOARD[e_sceneIndex.title]);
        _onTransition = false;
    }

    private async Task SceneTransition(e_sceneIndex to, CancellationToken token) {
        try {
            _controller.OnSceneExit();
            await Backboard.GetInstance.ControllBackboardAsync(e_backboardIs.on, FPS, TIME_TO_DRAW_BACKBOARD[to]);
            token.ThrowIfCancellationRequested();

            //insert some loading animation here to...

            await SceneManager.UnloadSceneAsync(_sceneToken);
            token.ThrowIfCancellationRequested();
#if UNITY_EDITOR
            await Task.Delay(1000);
            token.ThrowIfCancellationRequested();
#endif
            await Resources.UnloadUnusedAssets();
            token.ThrowIfCancellationRequested();

            await SceneManager.LoadSceneAsync(SCENE_NAMES[to], LoadSceneMode.Additive);
            token.ThrowIfCancellationRequested();

            FindSceneToken();

            //here to stop the loading animation 

            _controller.OnSceneStart();
            _ = Backboard.GetInstance.ControllBackboardAsync(e_backboardIs.off, FPS, TIME_TO_DRAW_BACKBOARD[to]);
            _onTransition = false;
        }
        catch (TaskCanceledException) {
            Debug.LogWarning($"Scene transition canceled. This is a handled cancel.");
        }
    }

    private void FindSceneToken() {
        GameObject controllerObject = GameObject.FindWithTag(CONTROLLERS);
        _controller = controllerObject.GetComponent<ISceneController>();
        _sceneToken = controllerObject.scene;
    }

    private async Task StartExitGame(CancellationToken token) {
        _controller.OnSceneExit();
        try {
            await Backboard.GetInstance.ControllBackboardAsync(e_backboardIs.on, FPS, TIME_TO_DRAW_BACKBOARD[e_sceneIndex.end]);
            token.ThrowIfCancellationRequested();
            await SceneManager.UnloadSceneAsync(_sceneToken);
        }
        catch (TaskCanceledException) {
            Debug.LogWarning("Cancel fired while exitting game. This is a handled cancel");
        }

        ExitGame();
    }

    private void ExitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
