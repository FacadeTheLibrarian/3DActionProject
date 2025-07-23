using System.Collections.Generic;
using UnityEngine;

internal sealed class EnemyControl : MonoBehaviour {
    [SerializeField] private IPlayerState _player = default;

    [SerializeField] private EnemyFactory _factory = default;
    [SerializeField] private List<Vector3> _enemyAppearancePoints = new List<Vector3>();

    private List<BaseEnemy> _enemyHandler = default;

    public void EnemyControlStart(in IPlayerState player) {
        _enemyHandler = new List<BaseEnemy>();
        for (int i = 0; i < _enemyAppearancePoints.Count; i++) {
            BaseEnemy enemy = _factory.GenerateFromPodOne(_enemyAppearancePoints[i]);
            enemy.transform.SetParent(this.transform, true);
            enemy.EnemyInitialization(player);
            _enemyHandler.Add(enemy);
        }
    }

    public void EnemyControlUpdate() {
        foreach(BaseEnemy enemy in _enemyHandler) {
            enemy.EnemyUpdate();
        }
    }
}