using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Character))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _sliderHealth;
    [SerializeField] private Slider _easeSliderHealth;
    [SerializeField] private Character _character;
    [SerializeField] private float _lerpSpeed;

    private float _correctHealth;

    private void Awake()
    {   
        _character = GetComponent<Character>();

        SetMaxHealth(_character.MaxHealth, _character.Health);

        _character.HealthChanged += GetCorrectHealth;
    }

    private void SetMaxHealth(float MaxHealthValue, float Health)
    {
        _sliderHealth.maxValue = MaxHealthValue;
        _easeSliderHealth.maxValue = MaxHealthValue;

        _sliderHealth.value = Health;
        _easeSliderHealth.value = Health;
    }

    private void GetCorrectHealth(float health)
    {
        _correctHealth = _character.Health;
        var SetCorrectHealthIsJob = StartCoroutine(SetCorrectHealth());
    }

    private float Animate(float sliderValue, float correctValue)
    {
        sliderValue = Mathf.MoveTowards(sliderValue, correctValue, _lerpSpeed);
        return sliderValue;
    }

    private IEnumerator SetCorrectHealth()
    {
        if (_correctHealth <= _sliderHealth.value) 
        {
            _sliderHealth.value = _correctHealth;
            while (_sliderHealth.value != _easeSliderHealth.value)
            {
                _easeSliderHealth.value = Animate(_easeSliderHealth.value, _correctHealth);
                yield return null;
            }
        }
        else
        {
            _easeSliderHealth.value = _correctHealth;
            while (_sliderHealth.value != _easeSliderHealth.value)
            {
                _sliderHealth.value = Animate(_sliderHealth.value, _correctHealth);
                yield return null;
            }
        }
    }
}