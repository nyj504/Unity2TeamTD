using UnityEngine;

public class TurretHead : MonoBehaviour
{
    private Enemy _target;
    [SerializeField] private float _rotationSpeed = 5.0f;

    private Bullet _bulletPrefabs;
    private void Awake()
    {
        _bulletPrefabs = Resources.Load<Bullet>("Prefabs/Bullets/Bullet");
    }

    private void Update()
    {
        if (_target == null)
            return;

        Vector3 direction = _target.transform.position - transform.position;
        direction.y = 0f;
        Debug.Log(direction);

        if (direction == Vector3.zero)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);

        AttackTarget(_target);
    }

    public void SetTarget(Enemy target)
    {
        _target = target;
    }

    private void AttackTarget(Enemy target)
    {
        Instantiate(_bulletPrefabs, transform.position, Quaternion.identity);
        _bulletPrefabs.SetBulletTarget(_target);
    }
}
