using UnityEngine;

internal interface IPlayerState {
    public Transform GetTransform();
    public float GetPlayerAttackFactor();
    public float GetPlayerHitPoint();
    public float GetPlayerStamina();
}