using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMoveVariables", menuName = "ScriptableObjects/PlayerMoveVariables")]
public class PlayerMoveVariables : ScriptableObject {
    [SerializeField] private float _baseMoveSpeed = 0.0f;
    [SerializeField] private float _baseSprintSpeed = 0.0f;
    [SerializeField] private float _baseSpeedChangeRate = 0.0f;
    [SerializeField] private float _baseStaminaConsumptionOnSplint = 0.0f;
    [SerializeField] private float _stopThreshold = 0.0f;

    public float GetBaseMoveSpeed => _baseMoveSpeed;
    public float GetBaseSprintSpeed => _baseSprintSpeed;
    public float GetBaseSpeedChangeRate => _baseSpeedChangeRate;
    public float GetBaseStaminaConsumptionOnSplint => _baseStaminaConsumptionOnSplint;
    public float GetStopThreshold => _stopThreshold;
}