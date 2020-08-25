using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(MeshRenderer))]
public abstract class PowerUpBase : MonoBehaviour
{
    protected abstract void PowerUp(Player player);

    [SerializeField] float _powerupDuration = 10;

    [SerializeField] float _movementSpeed = 1;
    protected float MovementSpeed { get { return _movementSpeed; } }

    [SerializeField] ParticleSystem _collectParticles;
    [SerializeField] AudioClip _collectSound;

    Rigidbody _rb;
    Collider _collider;
    MeshRenderer _visual;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _visual = GetComponent<MeshRenderer>();
    }

    private void FixedUpdate()
    {
        Movement(_rb);
    }

    protected virtual void Movement(Rigidbody rb)
    {
        //calculate rotation
        Quaternion turnOffset = Quaternion.Euler(MovementSpeed, MovementSpeed, MovementSpeed);
        rb.MoveRotation(_rb.rotation * turnOffset);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            PowerUp(player);
            //spawn particles and sfc because we need to diable object
            Feedback();
            _collider.enabled = false;
            _visual.enabled = false;
            StartCoroutine(PoweredUp(player));
        }
    }
    private void Feedback()
    {
        //particles
        if (_collectParticles != null)
        {
            _collectParticles = Instantiate(_collectParticles, transform.position, Quaternion.identity);
        }
        //audio TODO-consider Object Pooling
        if (_collectSound != null)
        {
            AudioHelper.PlayClip2D(_collectSound, 1f);
        }
    }

    IEnumerator PoweredUp(Player player)
    {
        yield return new WaitForSeconds(_powerupDuration);
        PowerDown(player);
        gameObject.SetActive(false);
    }

    protected abstract void PowerDown(Player player);
}
