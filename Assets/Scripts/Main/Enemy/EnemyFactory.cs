using System.Collections.Generic;
using UnityEngine;

internal sealed class EnemyFactory : MonoBehaviour {
	[SerializeField] private List<BaseEnemy> _podOne = new List<BaseEnemy>();

	public BaseEnemy GenerateFromPodOne() {
		int index = Random.Range(0, _podOne.Count);
		BaseEnemy instance = Instantiate(_podOne[index]);
		return instance;
	}
}