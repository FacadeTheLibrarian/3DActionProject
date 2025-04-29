using System.Collections.Generic;
using UnityEngine;

internal sealed class GroundCheck {

    private readonly float GROUND_OFFSET = default;
    private readonly float CHECK_SPHERE_RADIUS = default;
    private readonly LayerMask LAYERS = default;
    public GroundCheck(float groundOffset, float checkSphereRadius, LayerMask layers) {
        GROUND_OFFSET = groundOffset;
        CHECK_SPHERE_RADIUS = checkSphereRadius;
        LAYERS = layers;
    }
    public bool GroundedCheck(in Vector3 currentPosition) {
        Vector3 spherePosition = new Vector3(currentPosition.x, currentPosition.y - GROUND_OFFSET,
            currentPosition.z);
        return Physics.CheckSphere(spherePosition, CHECK_SPHERE_RADIUS, LAYERS,
            QueryTriggerInteraction.Ignore);
    }
}