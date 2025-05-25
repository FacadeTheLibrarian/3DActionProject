using System.Collections.Generic;
using UnityEngine;

internal sealed class PlayerGrowth : MonoBehaviour {
	private PlayerExpPoint _playerExpPoint = default;

    private PlayerInputs _inputs;
    private AnimationStateController _animationStateController = default;

    public PlayerGrowth(in PlayerInputs input, in AnimationStateController animationStateController,in MonsterHandler handler, in PlayerExpPoint expPoint) {
        _inputs = input;
        _animationStateController = animationStateController;
        _playerExpPoint = expPoint;
    }
}