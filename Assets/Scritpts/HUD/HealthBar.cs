using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Character))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _valueSlider;
    [SerializeField] private Slider _easeValueSlider;
    [SerializeField] private Character _character;
    [SerializeField] private float _lerpSpeed = 0.05f;

    private bool _isDamage;

    private void Awake()
    {
        _character = GetComponent<Character>();

        SetMaxHealth(_character.MaxHealth, _character.CorrectHealth);

        _character.HealthChanged += SetCorrectHealth;
    }

    private void Update()
    {
        SetCorrectHealth(_character.CorrectHealth);
    }

    private void SetMaxHealth(float MaxHealthValue, float CorrectHealth)
    {
        _valueSlider.maxValue = MaxHealthValue;
        _easeValueSlider.maxValue = MaxHealthValue;

        _valueSlider.value = CorrectHealth;
        _easeValueSlider.value = CorrectHealth;
    }

    private void SetCorrectHealth(float health)
    {
        if (_character.CorrectHealth <= _valueSlider.value)
        {
            _isDamage = true;
            _valueSlider.value = health;
            AnimateHealthBar(_isDamage, _easeValueSlider.value, health);
        }
        else 
        {
            _isDamage = false;
            _easeValueSlider.value = health;
            AnimateHealthBar(_isDamage, _valueSlider.value, health);
        }
    }

    private void AnimateHealthBar(bool IsDamage, float value, float target)
    {
        if (IsDamage)
            _easeValueSlider.value = Mathf.Lerp(value, target, _lerpSpeed);
        else
            _valueSlider.value = Mathf.Lerp(value, target, _lerpSpeed);
    }
}
