using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal sealed class DummyEnemy : MonoBehaviour, IDamagableObjects{

	[SerializeField] private int _hp = 20;
	[SerializeField] private Animator _animator = default;
	[SerializeField] private CapsuleCollider _capsuleCollider = default;

	private const float BLOWAWAY_Y = 7.0f;
	private const float BLOWAWAY_FLUCTUATION = 0.5f;
	private const float BLOWAWAY_DISTANCE_FACTOR = 10.0f;

	private float _timeFactor = 3.0f;
	private Vector3 _origin = default;

	private bool _isDead = false;

    private void Start() {
        _origin = transform.position;
		_capsuleCollider = this.GetComponent<CapsuleCollider>();
		_animator = this.GetComponent<Animator>();
    }
    public void GetHit(int damageAmount, in Vector3 playerForward) {
        if (_isDead) {
			return;
        }
        _hp -= damageAmount;
		if(_hp <= 0) {
			_capsuleCollider.enabled = false;
			float x = Random.Range(playerForward.x - BLOWAWAY_FLUCTUATION, playerForward.x + BLOWAWAY_FLUCTUATION) * BLOWAWAY_DISTANCE_FACTOR;
			float z = Random.Range(playerForward.z - BLOWAWAY_FLUCTUATION, playerForward.z + BLOWAWAY_FLUCTUATION) * BLOWAWAY_DISTANCE_FACTOR;
			Vector3 direction = new Vector3(x, BLOWAWAY_Y, z);
			StartCoroutine(BlownAway(direction));
		}
		_animator.Play("Damage");
	}

	public Vector3 GetPosition() {
		return this.transform.position;
	}
    private void ResetStatus() {
		_isDead = false;
        this.transform.position = _origin;
		_hp = 20;
		_capsuleCollider.enabled = true;
    }

    private IEnumerator BlownAway(Vector3 towards) {
		float y = this.transform.position.y;
		Vector3 velocity = towards;

		while (true) {
			this.transform.position += velocity * Time.deltaTime * _timeFactor;
			velocity += Physics.gravity * Time.deltaTime * _timeFactor;
			if(this.transform.position.y <= y) {
				ResetStatus();
				yield break;
			}
			yield return null;
		}
	}
}