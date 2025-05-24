using System;
using Systems.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace User_Interface.Entity
{
    public class PlayerHealthBar : HealthBar
    {
        private Image _image;
        
        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        protected override void HealthOnHealthChanged(int oldValue, int newValue, int maxValue)
        {
            var percentage = newValue / (float)maxValue;
            _image.fillAmount = percentage;
        }

        protected override void HealthOnHealthStateChanged(Health health, bool isDead)
        {
            // Trigger Effect?
        }
    }
}
