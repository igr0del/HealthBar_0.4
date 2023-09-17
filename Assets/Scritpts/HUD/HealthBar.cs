using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Character))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _valueSlider;
    [SerializeField] private Slider _easeValueSlider;
    [SerializeField] private Character _character;
    [SerializeField] private float _lerpSpeed = 0.05f;

    private float _correctHealth;

    private void Awake()
    {   
        _character = GetComponent<Character>();

        SetMaxHealth(_character.MaxHealth, _character.Health);

        _character.HealthChanged += GetCorrectHealth;
    }

    private void SetMaxHealth(float MaxHealthValue, float Health)
    {
        _valueSlider.maxValue = MaxHealthValue;
        _easeValueSlider.maxValue = MaxHealthValue;

        _valueSlider.value = Health;
        _easeValueSlider.value = Health;
    }

    private void GetCorrectHealth(float health)
    {
        _correctHealth = _character.Health;
        var SetCorrectHealthIsJob = StartCoroutine(SetCorrectHealth());
    }

    private IEnumerator SetCorrectHealth()
    {
        if (_character.Health <= _valueSlider.value)
        {
            _valueSlider.value = _correctHealth;
            for (int i = 0; i < _correctHealth; i++)
            {
                _easeValueSlider.value = Mathf.Lerp(_easeValueSlider.value, _correctHealth, _lerpSpeed);
                yield return null;
            }
        }
        else 
        {
            _easeValueSlider.value = _correctHealth;
            for (int i = 0; i < _correctHealth; i++)
            {
                _valueSlider.value = Mathf.Lerp(_valueSlider.value, _correctHealth, _lerpSpeed);
                yield return null;
            }
        }
    }
}
