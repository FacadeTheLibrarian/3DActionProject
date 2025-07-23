using UnityEngine;

public interface IDamagableObjects {
    void GetHit(int damageAmount, in Vector3 playerForward);
}