using UnityEngine;

internal sealed class EnemyControl : MonoBehaviour {

    [SerializeField] private PlayerGodClass _player = default;

    [SerializeField] private EnemyFactory _factory = default;
    [SerializeField] private Vector3 _startPosition = default;
    [SerializeField] private float _xOffset = default;
    [SerializeField] private float _yOffset = default;

    [SerializeField] private int _width = 3;
    [SerializeField] private int _height = 3;

    private void Start() {
        float y = _yOffset;
        float x = _xOffset;
        for (int i = 0; i < _height; i++) {
            for (int j = 0; j < _width; j++) {
                BaseEnemy enemy = _factory.GenerateFromPodOne();
                enemy.transform.position = new Vector3(_startPosition.x + (_xOffset * j), _startPosition.y, _startPosition.z + (_yOffset * i));
                enemy.transform.SetParent(this.transform, false);
            }
        }
    }
}