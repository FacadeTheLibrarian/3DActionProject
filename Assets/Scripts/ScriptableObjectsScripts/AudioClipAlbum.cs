using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipAlbum", menuName = "ScriptableObjects/AudipClipAlbum")]
public class AudioClipAlbum : ScriptableObject {
    [SerializeField] private List<AudioClip> _album = new List<AudioClip>();

    public AudioClip this[int index] {
        get {
            return _album[index];
        }
    }
}
