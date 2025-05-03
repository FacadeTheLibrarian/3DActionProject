using System.Collections.Generic;
using UnityEngine;

internal sealed class SnakeletParticles : BaseParticlePlayer {
    public void PlayParticle(AnimationParticleSelecter.e_snakeletParticles index) {
        InnerPlayParticle((int)index);
    }
}