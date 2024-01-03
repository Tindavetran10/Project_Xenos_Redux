using UnityEngine;

namespace _Scripts.Projectiles
{
    public class ProjectileOLD : MonoBehaviour
    {
        private float _damage;
        private float _speed;
        private float _travelDistance;
        private float _xStartPos;

        [SerializeField] private float gravity;
        [SerializeField] private float damageRadius;

        private Rigidbody2D _rb;

        private bool _isGravityOn;
        private bool _hasHitGround;

        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private LayerMask whatIsPlayer;
        [SerializeField] private Transform damagePosition;

        private void Start()
        {
            var projectileTransform = transform;
            _rb = GetComponent<Rigidbody2D>();

            _rb.gravityScale = 0.0f;
            _rb.velocity = projectileTransform.right * _speed;

            _isGravityOn = false;
            _xStartPos = projectileTransform.position.x;
        }

        private void Update()
        {
            if (!_hasHitGround)
            {
                if (_isGravityOn)
                {
                    var velocity = _rb.velocity;
                    var angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
            }
        }

        private void FixedUpdate()
        {
            if (!_hasHitGround)
            {
                var position = damagePosition.position;
                var damageHit = Physics2D.OverlapCircle(position, damageRadius, whatIsPlayer);
                var groundHit = Physics2D.OverlapCircle(position, damageRadius, whatIsGround);
                
                if(damageHit) Destroy(gameObject);

                if (groundHit)
                {
                    _hasHitGround = true;
                    _rb.gravityScale = 0f;
                    _rb.velocity = Vector2.zero;
                }

                if (Mathf.Abs(_xStartPos - transform.position.x) >= _travelDistance && !_isGravityOn)
                {
                    _isGravityOn = true;
                    _rb.gravityScale = gravity;
                }
            }
            else Destroy(gameObject, 5f);
        }

        public void FireProjectile(float damage, float speed, float travelDistance)
        {
            _damage = damage;
            _speed = speed;
            _travelDistance = travelDistance;
        }

        private void OnDrawGizmos() => Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }
}