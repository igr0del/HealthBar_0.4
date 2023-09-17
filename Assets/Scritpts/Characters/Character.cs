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
    [SerializeField] private Damage _damageButton;
    [SerializeField] private Heal _healthButton;

    [Header("Характеристики персонажа")]
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _speedColorChange;

    private float _damageValue;
    private float _healthValue;
    private float _correctHealth;

    public event Action<float> HealthChanged;

    public float MaxHealth => _maxHealth;
    public float CorrectHealth => _correctHealth;

    private void Start()
    {
        _damageValue = _damageButton.DamageValue;
        _healthValue = _healthButton.HealValue;

        _correctHealth = _health;
    }

    public void TakeDamage()
    {
        _spriteRenderer.DOColor(Color.white, 0).SetLoops(2, LoopType.Yoyo);

        if (_correctHealth > 0)
        {
            _correctHealth -= _damageValue;
            HealthChanged?.Invoke(_correctHealth);
        }
        else
        {
            _correctHealth = 0;
        }

        _spriteRenderer.DOColor(Color.red, _speedColorChange).SetLoops(2, LoopType.Yoyo);
    }

    public void TakeHealth()
    {
        _spriteRenderer.DOColor(Color.white, 0).SetLoops(2, LoopType.Yoyo);

        if (_correctHealth < _maxHealth)
        {
            _correctHealth += _healthValue;
            HealthChanged?.Invoke(_correctHealth);
        }
        else
        {
            _correctHealth = _maxHealth;
        }

        _spriteRenderer.DOColor(Color.green, _speedColorChange).SetLoops(2, LoopType.Yoyo);
    }
}
