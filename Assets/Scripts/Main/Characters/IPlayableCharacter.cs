using UnityEngine;

internal interface IPlayableCharacter {
    Animator GetAnimator();

    void SetSpeed(float speed);
    void NormalAttack();
    void SpecialAttack();
}