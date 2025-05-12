using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

internal enum e_backboardIs {
    off = 0,
    on = 1,
    max,
}

internal sealed class Backboard : IDisposable {
    private const float RIGHT = 1.0f;
    private const float LEFT = -1.0f;

    private const float ALPHA_MAX = 1.0f;
    private const float ALPHA_MIN = 0.0f;
    private const float SECOND = 1.0f;
    private const float MILLI_SECOND = 1000.0f;

    private static Backboard _instance = default;
    private Image _backboard = default;

    private CancellationTokenSource _source = new CancellationTokenSource();
    private CancellationToken _token = default;


    public static Backboard GetInstance {
        get {
            return _instance;
        }
    }

    public Backboard(in Image backboard) {
        if (_instance != null) {
            return;
        }
        _instance = this;

        _backboard = backboard;
        _token = _source.Token;
    }

    public void Dispose() {
        if (!_token.IsCancellationRequested) {
            _source.Cancel();
        }
        _source.Dispose();

        _backboard = null;
        _instance = null;
    }

    public async Task ControllBackboardAsync(e_backboardIs targetAlpha, float fps, float time) {
        _source.Cancel();
        _source = new CancellationTokenSource();
        _token = _source.Token;

        float deltaTime = SECOND / (fps * time);
        int timeToSleep = (int)(SECOND / fps * MILLI_SECOND);
        float direction = _backboard.color.a < (float)targetAlpha ? RIGHT : LEFT;

        while (true) {
            _backboard.color += new Color(0.0f, 0.0f, 0.0f, direction * deltaTime);

            if (!MathUtility.IsInsideExclusive(_backboard.color.a, ALPHA_MIN, ALPHA_MAX)) {
                _backboard.color = new Color(_backboard.color.r, _backboard.color.g, _backboard.color.b, (float)targetAlpha);
                break;
            }

            try {
                await Task.Delay(timeToSleep, _token);
            } catch {
                Debug.LogWarning($"NewBackboard.ControllBackboardAsync is cancelled.\n This is a handled cancel");
                return;
            }
        }
    }
}