using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal sealed class PlayerStamina {
    private readonly float BASE_STAMINA_MAX = 100.0f;
    private readonly float BASE_STAMINA_RECOVERY_AMOUNT = 2.0f;
    private readonly float BASE_STAMINA_CONSUMPTION_ON_DODGE = 10.0f;

    private float _staminaMax = 0.0f;
    private float _staminaRecoveryAmount = 0.0f;
    private float _staminaConsumptionOnDodge = 0.0f;

    private StaminaGraphicController _staminaBar = default;

    public PlayerStamina(in PlayerStaminaVariables variables, in StaminaGraphicController staminaBar) {
        BASE_STAMINA_MAX = variables.GetBaseStaminaMax;
        BASE_STAMINA_RECOVERY_AMOUNT = variables.GetBaseStaminaRecoveryAmount;
        BASE_STAMINA_CONSUMPTION_ON_DODGE = variables.GetBaseStaminaConsumptionOnDodge;
        _staminaBar = staminaBar;

        _staminaMax = BASE_STAMINA_MAX;
        _staminaRecoveryAmount = BASE_STAMINA_RECOVERY_AMOUNT;
        _staminaConsumptionOnDodge = BASE_STAMINA_CONSUMPTION_ON_DODGE;
    }
}