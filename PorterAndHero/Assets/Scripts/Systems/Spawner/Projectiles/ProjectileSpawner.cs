using System;
using System.Collections;
using Interfaces;
using Scriptables.Projectiles;
using UnityEngine;

namespace Systems.Spawner
{
    public class ProjectileSpawner : MonoBehaviour
    {
        [SerializeField] private ProjectileDatum projectileDatum;
        [SerializeField] private Transform target;
        [Space] 
        [SerializeField] private float reloadDuration = 1;

        private void Awake()
        {
            StartCoroutine(ReloadRoutine());
        }

        private void FireProjectile()
        {
            var projectile = projectileDatum.Spawn();
            projectile.Fire(FireContext.Construct(transform.position, target.position, true));
        }
        
        private IEnumerator ReloadRoutine()
        {
            yield return new WaitForSeconds(reloadDuration);
            FireProjectile();
            StartCoroutine(ReloadRoutine());
        }
    }
}
