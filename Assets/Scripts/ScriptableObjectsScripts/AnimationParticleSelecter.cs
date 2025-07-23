using UnityEngine;

[CreateAssetMenu(fileName = "AnimationParticleSelector", menuName = "ScriptableObjects/AnimationParticleSelector")]
public class AnimationParticleSelecter : ScriptableObject {

    public enum e_playerCommon {
        growth = 0,
    }
    public enum e_nagaParticles {
        bite = 0,
        flameThrower = 1,
        leftSlash = 2,
        rightSlash = 3,
        fireCharge = 4,
        fireLaunch = 5,
    }
    public enum e_snakeParticles {
        bite = 0,
        tailAttack = 1,
        headbatt = 2,
        fireCharge = 3,
        fireLaunch = 4,
    }
    public enum e_snakeletParticles {
        bite = 0,
        tailAttack = 1,
        fireCharge = 2,
    }
}