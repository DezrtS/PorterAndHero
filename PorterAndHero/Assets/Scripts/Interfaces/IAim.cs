using UnityEngine;

namespace Interfaces
{
    public interface IAim
    {
        public Vector2 GetAimInput();

        public static Vector2 GetAimInput(GameObject target)
        {
            return !target.TryGetComponent(out IAim aim) ? Vector2.zero : aim.GetAimInput();
        }
    }
}
