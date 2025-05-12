using System;
using UnityEngine;

internal sealed class JukeBox : IDisposable {
    private static JukeBox _instance = default;
    public static JukeBox GetInstance {
        get { return _instance; }
    }

    private AudioSource _bgmSource = default;
    private AudioSource _seSource = default;
    private AudioClipAlbum _bgmAlbum = default;
    private AudioClipAlbum _seAlbum = default;

    public JukeBox(in AudioSource bgmSource, in AudioSource seSource, in AudioClipAlbum bgmAlbum, in AudioClipAlbum seAlbum) {
        if (_instance != null) {
            throw new Exception($"Constructor {this.GetType()} is called twice, this is not allowed since it is Singleton");
        }
        _instance = this;
        _bgmSource = bgmSource;
        _seSource = seSource;
        _bgmAlbum = bgmAlbum;
        _seAlbum = seAlbum;
    }

    public void Dispose() {
        _bgmSource = null;
        _seSource = null;
        _bgmAlbum = null;
        _seAlbum = null;
        _instance = null;
    }
}
