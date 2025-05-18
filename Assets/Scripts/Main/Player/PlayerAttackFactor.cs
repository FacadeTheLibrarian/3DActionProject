internal sealed class PlayerAttackFactor{
	private float _attackFactor = 1.0f;

    public PlayerAttackFactor() {
        _attackFactor = 1.0f;
    }
    public PlayerAttackFactor(float attackFactor) {
        _attackFactor = attackFactor;
    }

    public float GetAttackFactor {
        get {
            return _attackFactor;
        }
    }
}