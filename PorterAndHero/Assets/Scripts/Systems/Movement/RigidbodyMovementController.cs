using UnityEngine;

namespace Systems.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RigidbodyMovementController : MovementController
    {
        protected override void Move(Vector2 input)
        {
            var trueInput = ForceController.GetRotation() * input;
            ForceController.ApplyForce(HandleMovement(trueInput), ForceMode2D.Impulse);
        }

        private Vector3 HandleMovement(Vector2 input)
        {
            var currentVelocity =  ForceController.GetVelocity();
            var targetVelocity = input.normalized * MovementControllerDatum.MaxSpeed;
            var targetSpeed = targetVelocity.magnitude;

            var velocityDifference = targetVelocity - currentVelocity;
            var differenceDirection = velocityDifference.normalized;
            float accelerationIncrement;

            if (currentVelocity.magnitude <= targetSpeed)
            {
                accelerationIncrement = MovementControllerDatum.Acceleration * Time.deltaTime;
            }
            else
            {
                accelerationIncrement = MovementControllerDatum.Deceleration * Time.deltaTime;
            }

            if (velocityDifference.magnitude < accelerationIncrement)
            {
                return velocityDifference;
            }
            else
            {
                return differenceDirection * accelerationIncrement;
            }
        }
    }
}