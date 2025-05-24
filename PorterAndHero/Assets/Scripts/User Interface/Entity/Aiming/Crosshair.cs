using System;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace User_Interface.Entity
{
    public class Crosshair : MonoBehaviour
    {
        [SerializeField] private Transform target;
        private IAim _aim;
        
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
            
            if (!target) return;
            _aim = target.GetComponent<IAim>();
        }

        public void AttachCrosshair(Transform targetToFollow, IAim aim)
        {
            target = targetToFollow;
            _aim = aim;
        }

        public void DetachCrosshair()
        {
            target = null;
            _aim = null;
        }

        private void Update()
        {
            if (!target) return;
            UpdateCrosshair();
        }

        private void UpdateCrosshair()
        {
            var position = (Vector2)target.position + _aim.GetAimInput().normalized;
            var screenPosition = Camera.main.WorldToScreenPoint(position);
            _image.transform.position = screenPosition;
        }
    }
}
