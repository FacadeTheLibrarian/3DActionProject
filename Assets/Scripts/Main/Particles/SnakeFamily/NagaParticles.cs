using System.Collections.Generic;
using UnityEngine;

internal sealed class NagaParticles : BaseParticlePlayer {
	public void PlayParticle(AnimationParticleSelecter.e_nagaParticles index) {
		InnerPlayParticle((int)index);
	}
}