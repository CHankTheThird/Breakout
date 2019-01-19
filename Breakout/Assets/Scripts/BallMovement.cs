using UnityEngine;

namespace Breakout
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class BallMovement : MonoBehaviour
	{
		[Header("Movement Settings")]
		[SerializeField] private float m_baseSpeed = 3f;

		private Rigidbody2D ballBody { get; set; }

		private void Awake()
		{
			ballBody = GetComponent<Rigidbody2D>();
		}

		private void Start()
		{
			ballBody.velocity = Random.insideUnitCircle.normalized * m_baseSpeed;
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			Vector2 reflectedVelocity = Vector2.zero;

			PaddleMovement paddle = collision.collider.GetComponent<PaddleMovement>();
			if (paddle != null)
			{
				// if we hit the paddle then we need to reflect our velocity at an angle relative to the center of the paddle
				reflectedVelocity = paddle.Reflect(ballBody.velocity, collision);
			}
			else
			{
				// otherwise we can just reflect the velocity at the normal of the contact point
				reflectedVelocity = Vector2.Reflect(ballBody.velocity, collision.GetContact(0).normal);
			}

			ballBody.velocity = reflectedVelocity;
		}
	}
}