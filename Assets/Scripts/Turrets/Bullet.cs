using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject _target;
    private float _bulletSpeed = 10.0f;
    private float _hitThreshold = 0.1f;

    private void Update()
    {
        Vector3 direction = _target.transform.position - transform.position;
        float distanceThisFrame = _bulletSpeed * Time.deltaTime;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = targetRotation;
        transform.position += direction.normalized * distanceThisFrame;
    }
    public void SetBulletTarget(GameObject target)
    {
        _target = target;   
    }
}
