﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BallMotor))]
public class Player : MonoBehaviour
{
    //TODO offload health into its own script (Health.cs)
    [SerializeField] int _maxHealth = 3;
    int _currentHealth;
    [SerializeField] ParticleSystem _killParticles;
    [SerializeField] AudioClip _killSound;

    public int _totalTreasure;
    [SerializeField] Text _treasureCount;

    BallMotor _ballMotor;

    public bool _invincible;

    private void Awake()
    {
        _ballMotor = GetComponent<BallMotor>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
        _totalTreasure = 0;
        _invincible = false;
    }

    private void FixedUpdate()
    {
        ProcessMovement();  
    }

    private void Update()
    {
        _treasureCount.text = "Treasure: " + _totalTreasure;
    }

    private void ProcessMovement()
    {
        //TODO move into Input script
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        _ballMotor.Move(movement);
    }

    public void IncreaseHealth(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        Debug.Log("Player's health: " + _currentHealth);

    }

    public void DecreaseHealth(int amount)
    {
        _currentHealth -= amount;
        Debug.Log("Player's health: " + _currentHealth);
        if (_currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        gameObject.SetActive(false);
        KillFeedback();
    }

    private void KillFeedback()
    {
        //particles
        if (_killParticles != null)
        {
            _killParticles = Instantiate(_killParticles, transform.position, Quaternion.identity);
        }

        //audio TODO-consider Object Pooling
        if (_killSound != null)
        {
            AudioHelper.PlayClip2D(_killSound, 1f);
        }
    }
}
