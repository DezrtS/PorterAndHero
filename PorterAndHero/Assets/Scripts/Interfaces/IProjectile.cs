using Scriptables.Projectiles;
using UnityEngine;

namespace Interfaces
{
    public interface IProjectile<T> where T : MonoBehaviour 
    {
        public delegate void ProjectileEventHandler(IProjectile<T> projectile);
        public delegate void ProjectileHitEventHandler(IProjectile<T> projectile, Collider2D collider);
        
        public ProjectileDatum ProjectileDatum { get; }
        public bool IsFired { get; }
        
        public void Initialize(ProjectileDatum projectileDatum);
        public void Fire(FireContext fireContext);
        public void Destroy();
    }
    
    public struct FireContext
    {
        public Vector2 FirePosition;
        public Vector2 TargetPosition;
        
        public bool DestroyObjectOnDestroy;

        public static FireContext Construct(Vector2 firePosition, Vector2 targetPosition, bool destroyObjectOnDestroy)
        {
            return new FireContext()
            {
                FirePosition = firePosition,
                TargetPosition = targetPosition,
                DestroyObjectOnDestroy = destroyObjectOnDestroy
            };
        }
        
        public Vector2 GetDirection() => (TargetPosition - FirePosition).normalized;
    }
}
