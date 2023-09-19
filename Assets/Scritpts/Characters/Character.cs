using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using System;

[RequireComponent(typeof(SpriteRenderer))]
public class Character : MonoBehaviour
{
    [Header("Sprite Renderer")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
        
    [Header("Объекты взаимодействия")]
    [SerializeField] private Damage _takeDamage;
    [SerializeField] private Heal _heal;

    [Header("Характеристики персонажа")]
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _speedColorChange;

    private float _damageValue;
    private float _healValue;

    public event Action<float> HealthChanged;

    public float MaxHealth => _maxHealth;
    public float Health => _health;

    private void Start()
    {
        _damageValue = _takeDamage.DamageValue;
        _healValue = _heal.HealValue;
    }

    public void TakeDamage()
    {
        _spriteRenderer.DOColor(Color.white, 0).SetLoops(2, LoopType.Yoyo);

        if (_health > 0)
        {
            _health -= _damageValue;
            HealthChanged?.Invoke(_health);
        }
        else
        {
            _health = 0;
        }

        _spriteRenderer.DOColor(Color.red, _speedColorChange).SetLoops(2, LoopType.Yoyo);
    }

    public void Heal()
    {
        _spriteRenderer.DOColor(Color.white, 0).SetLoops(2, LoopType.Yoyo);

        if (_health < _maxHealth)
        {
            _health += _healValue;
            HealthChanged?.Invoke(_health);
        }
        else
        {
            _health = _maxHealth;
        }

        _spriteRenderer.DOColor(Color.green, _speedColorChange).SetLoops(2, LoopType.Yoyo);
    }
}
