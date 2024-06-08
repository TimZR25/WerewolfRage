using Assets.Scripts;
using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float _offset = 0;

    [SerializeField] private AudioClip _bulletAudioClip;
    private AudioSource _audioSource;

    [SerializeField] private float _shotWidth = 0.8f;
    [SerializeField] private float _shotLength = 3.0f;

    [SerializeField] private Transform _bulletSpawner;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _fireRate = 1;
    [SerializeField] private int _damage = 3;

    [SerializeField] private Player _player;

    private float _nextFire = 0;

    private WeaponRotation _weaponRotation = new(false);
    private SpriteRenderer _sr;

    public void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        (transform.rotation, _sr.flipY) = _weaponRotation.ChangeRotation(transform.position, transform.rotation, _offset);

        if (Input.GetMouseButtonDown(0) && Time.time >= _nextFire)
        {
            _nextFire = Time.time + 1.0f / _fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        _audioSource.PlayOneShot(_bulletAudioClip);
        StartCoroutine(BulletEffect());
        _bulletSpawner.localPosition += new Vector3(_shotLength / 2 - 0.5f, 0, 0);
        _player.PushAway(_bulletSpawner.position, 1000);
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(_bulletSpawner.position, new Vector2(_shotLength, _shotWidth), transform.rotation.eulerAngles.z);
        _bulletSpawner.localPosition -= new Vector3(_shotLength / 2 - 0.5f, 0, 0);

        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.TryGetComponent(out Enemy enemy))
            {
                enemy.ApplyDamage(_damage);
            }
        }      
    }

    private IEnumerator BulletEffect()
    {
        GameObject bullet = Instantiate(_bullet, _bulletSpawner.position, transform.rotation);
        yield return new WaitForSeconds(0.4f);
        Destroy(bullet);
    }
}
