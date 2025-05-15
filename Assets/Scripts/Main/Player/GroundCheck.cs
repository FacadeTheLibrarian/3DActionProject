using System.Collections.Generic;
using UnityEngine;

internal sealed class GroundCheck{
    public GroundCheck(){}
    public bool GroundedCheck(in Vector3 currentPosition, float groundOffset, float radius, LayerMask layer) {
        Vector3 spherePosition = new Vector3(currentPosition.x, currentPosition.y - groundOffset,
            currentPosition.z);
        return Physics.CheckSphere(spherePosition, radius, layer,
            QueryTriggerInteraction.Ignore);
    }
}