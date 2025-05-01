using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStaminaVariables", menuName = "ScriptableObjects/PlayerStaminaVariables")]
internal sealed class PlayerStaminaVariables : ScriptableObject {
    [SerializeField] private float _baseStaminaMax = 100.0f;
    [SerializeField] private float _baseStaminaRecoveryAmount = 2.0f;
    [SerializeField] private float _baseStaminaConsumptionOnSprint = 0.5f;
    [SerializeField] private float _baseStaminaConsumptionOnDodge = 10.0f;

    public float GetBaseStaminaMax => _baseStaminaMax;
    public float GetBaseStaminaRecoveryAmount => _baseStaminaRecoveryAmount;
    public float GetBaseStaminaConsumptionOnSprint => _baseStaminaConsumptionOnSprint;
    public float GetBaseStaminaConsumptionOnDodge => _baseStaminaConsumptionOnDodge;

}