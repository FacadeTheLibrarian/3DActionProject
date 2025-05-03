using System.Collections.Generic;
using UnityEngine;

internal sealed class SnakeParticles : BaseParticlePlayer {
    public void PlayParticle(AnimationParticleSelecter.e_snakeParticles index) {
        InnerPlayParticle((int)index);
    }
}