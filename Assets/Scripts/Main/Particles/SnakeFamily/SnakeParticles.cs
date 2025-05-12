internal sealed class SnakeParticles : BaseParticlePlayer {
    public void PlayParticle(AnimationParticleSelecter.e_snakeParticles index) {
        InnerPlayParticle((int)index);
    }
}