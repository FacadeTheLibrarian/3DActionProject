using SimpleMan.VisualRaycast;
using System;
using System.Collections;
using UnityEngine;

internal sealed class DummyEnemy : BaseEnemy, IDamagableObjects {

    private const float BLOWAWAY_Y = 7.0f;
    private const float BLOWAWAY_FLUCTUATION = 0.5f;
    private const float BLOWAWAY_DISTANCE_FACTOR = 10.0f;

    [SerializeField] private CharacterController _controller = default;

    private float _blownAwayTimeFactor = 3.0f;
    private Vector3 _origin = default;

    private bool _isDead = false;

    private Vector3 _vector = default;
    private Action CurrentState = default;
    private bool _bogeyFound = false;

    private void Start() {
        _origin = transform.position;
        _animator = this.GetComponent<Animator>();
        _vector = SetVector();
        CurrentState = Roam;
    }
    protected override void InnerEnemyUpdate() {
        CurrentState();
    }

    protected override void ReviseBehaviour() {
        Search();
    }

    private Vector3 SetVector() {
        float randomRadian = UnityEngine.Random.Range(0.0f, Mathf.PI * 2.0f);
        float x = Mathf.Cos(randomRadian);
        float z = Mathf.Sin(randomRadian);

        float degree = randomRadian * Mathf.Rad2Deg;
        Vector3 eulerRotation = this.transform.eulerAngles;
        this.transform.rotation = Quaternion.Euler(eulerRotation.x, degree - 90.0f, eulerRotation.z);

        return new Vector3(x, 0.0f, z);
    }

    private void Roam() {
        _controller.Move(_vector * Time.deltaTime);
    }

    private void Search() {
        ComponentExtension.BoxOverlap(this.transform, this.transform.position, Vector3.one, this.transform.rotation, 255, true);
    }

    private void Chase() {

    }

    private void Attack() {
    }

    private void Run() {

    }

    public void GetHit(int damageAmount, in Vector3 playerForward) {
        if (_isDead) {
            return;
        }
        _hp -= damageAmount;
        if (_hp <= 0) {
            _isDead = true;
            float x = UnityEngine.Random.Range(playerForward.x - BLOWAWAY_FLUCTUATION, playerForward.x + BLOWAWAY_FLUCTUATION) * BLOWAWAY_DISTANCE_FACTOR;
            float z = UnityEngine.Random.Range(playerForward.z - BLOWAWAY_FLUCTUATION, playerForward.z + BLOWAWAY_FLUCTUATION) * BLOWAWAY_DISTANCE_FACTOR;
            Vector3 direction = new Vector3(x, BLOWAWAY_Y, z);
            _animator.Play("Death");
            StartCoroutine(BlownAway(direction));
            return;
        }
        _animator.Play("Damage");
    }
    private void ResetStatus() {
        _isDead = false;
        this.transform.position = _origin;
        _hp = 20;
    }

    private IEnumerator BlownAway(Vector3 towards) {
        float y = this.transform.position.y;
        Vector3 velocity = towards;

        while (true) {
            this.transform.position += velocity * Time.deltaTime * _blownAwayTimeFactor;
            velocity += Physics.gravity * Time.deltaTime * _blownAwayTimeFactor;
            if (this.transform.position.y <= y) {
                Defeated();
                ResetStatus();
                yield break;
            }
            yield return null;
        }
    }
}