using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimationParticleSelecter", menuName = "ScriptableObjects/AnimationParticleSelecter")]
public class AnimationParticleSelecter : ScriptableObject {
    public enum e_nagaParticles {
        bite = 0,
        flameThrower = 1,
        leftSlash = 2,
        rightSlash = 3,
    }
    public enum e_snakeParticles {
        bite = 0,
        tailAttack = 1,
        fireBall = 2,
    }
    public enum e_snakeletParticles {
        bite = 0,
        tailAttack = 1,
    }
}