using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthbarslide;
    

    public void updateHealthBar(float currentValue, float maxValue)
    {
        _healthbarslide.fillAmount = currentValue / maxValue;
    }
}