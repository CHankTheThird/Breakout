using UnityEngine;

namespace Breakout
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class PaddleMovement : MonoBehaviour
	{
		[Header("Movement Settings")]
		[SerializeField] private float m_movementSpeed = 5f;

		[Header("Reflection Settings")]
		[SerializeField] private AnimationCurve m_reflectionAngleCurve;

		private Rigidbody2D paddleBody { get; set; }

		private Vector2 initialPosition { get; set; }

		private void Awake()
		{
			paddleBody = GetComponent<Rigidbody2D>();
		}

		private void Start()
		{
			initialPosition = transform.position;
		}

		public void UpdateMovement(float deltaTime, float horizontalInput)
		{
			if (paddleBody == null)
				return;

			paddleBody.velocity = new Vector2(horizontalInput * m_movementSpeed, 0f);
		}

		public Vector2 Reflect(Vector2 incomingVelocity, Collision2D collision)
		{
			Vector2 collisionPoint = collision.GetContact(0).point;
			Vector2 paddlePosition = collision.collider.transform.position;

			// assuming the scale is modified to give us the paddle's length, we can use the x scale to give us a normalized distance value
			// (where 0 indicates a position close to the center of the paddle and 1 (or -1) indicates the furthest edge of the paddle)
			float distanceFromCenter = (collisionPoint - paddlePosition).x / (collision.collider.transform.localScale.x / 2);
			
			Vector2 adjustedNormal = collision.GetContact(0).normal.RotateVector2(m_reflectionAngleCurve.Evaluate(distanceFromCenter) * Mathf.Deg2Rad);

			return Vector2.Reflect(incomingVelocity, adjustedNormal);
		}

		public void ResetPaddle()
		{
			paddleBody.velocity = Vector2.zero;
			transform.position = initialPosition;
		}
	}
}