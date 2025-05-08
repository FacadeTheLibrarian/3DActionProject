using System.Collections.Generic;
using UnityEngine;

internal sealed class PlayerCommonParticles : BaseParticlePlayer {
 
    public void PlayParticle(AnimationParticleSelecter.e_playerCommon index) {
        InnerPlayParticle((int)index);
    }
}