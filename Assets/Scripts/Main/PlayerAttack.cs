using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

internal sealed class PlayerAttack{

    public void Act() {

    }

    //private void AttackCast(Vector3 forward, Vector3 currentPosition, float yAddend) {
    //    currentPosition.y += yAddend;
    //    Vector3 castPosition = currentPosition + (forward * 3.0f);
    //    Collider[] results = new Collider[CAST_BUFFER_SIZE];
    //    int numberOfCollider = Physics.OverlapSphereNonAlloc(castPosition, 2.0f, results, _layer);
    //    //Physics.SphereCastNonAlloc(this.transform.position, 2.0f, _forward, results, 10.0f, _layer);

    //    if (numberOfCollider == 0) {
    //        return;
    //    }
    //    for (int i = 0; i < numberOfCollider; i++) {
    //        if (results[i].TryGetComponent<IEnemy>(out IEnemy possibleEnemy)) {
    //            possibleEnemy.GetHit();
    //        }
    //    }
    //}
}