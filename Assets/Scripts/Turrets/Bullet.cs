using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Enemy _target;
    private float _bulletspeed = 2.0f;

    private void Update()
    {
        Vector3 direction = _target.transform.position - transform.position;
        transform.position += direction * _bulletspeed * Time.deltaTime;
    }
    public void SetBulletTarget(Enemy target)
    {
        _target = target;   
    }
}
