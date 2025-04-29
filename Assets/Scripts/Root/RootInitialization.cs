using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class RootInitialization : MonoBehaviour {
    [SerializeField] private AudioSource _BGMChannel = default;
    [SerializeField] private AudioSource _SEChannel = default;
    [SerializeField] private AudioClipAlbum _bgmAlbum = default;
    [SerializeField] private AudioClipAlbum _seAlbum = default;

    [SerializeField] private Image _backBoard = default;

    private Backboard _backBoardInstance = default;
    private JukeBox _jukeBoxInstance = default;

    public void Initialization() {
        _backBoardInstance = new Backboard(_backBoard);
        _jukeBoxInstance = new JukeBox(_BGMChannel, _SEChannel, _bgmAlbum, _seAlbum);
    }

    private void OnDestroy() {
        _backBoardInstance.Dispose();
        _jukeBoxInstance.Dispose();
    }
}
