using UnityEngine;

internal interface IPlayableCharacter {
    public void Move(float speed);
    public void StartDodge();
    public void EndDodge();
    public void NormalAttack();
    public void SpecialAttack();

}