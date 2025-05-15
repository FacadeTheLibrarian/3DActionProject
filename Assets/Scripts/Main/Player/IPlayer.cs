using UnityEngine;

internal interface IPlayer {
    public Transform GetTransform();
    public Vector3 GetForward();
    public float GetAttackFactor();
}