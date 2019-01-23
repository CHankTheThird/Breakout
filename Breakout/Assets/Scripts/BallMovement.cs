using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Breakout
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class BallMovement : MonoBehaviour
	{
		[Header("Movement Settings")]
		[SerializeField] private float m_baseSpeed = 3f;
		[SerializeField] private float m_initialLaunchAngle = 35f;

		[Header("References")]
		[SerializeField] private GameInfo m_gameInfo;

		[Header("Events")]
		[SerializeField] private UnityEvent m_onBallCollision;

		private Vector2 m_startPosition;

		private List<Collision2D> collisions { get; set; }

		/// <summary>
		/// This resolved an issue where collision2ds (even when displaying different normals in OnCollisionEnter)
		/// would all have the same normal when iterated through in Update
		/// </summary>
		private List<Vector2> normals { get; set; }

		private Rigidbody2D ballBody { get; set; }
		
		private void Awake()
		{
			ballBody = GetComponent<Rigidbody2D>();

			collisions = new List<Collision2D>();


			normals = new List<Vector2>();
		}

		private void Start()
		{
			m_startPosition = transform.position;

			if (m_gameInfo != null)
			{
				m_gameInfo.brickSpeedDeltaChanged.AddListener(AdjustBallSpeed);
			}
		}

		private void OnDestroy()
		{
			if (m_gameInfo != null)
			{
				m_gameInfo.brickSpeedDeltaChanged.RemoveListener(AdjustBallSpeed);
			}
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			collisions.Add(collision);
			normals.Add(collision.GetContact(0).normal);
		}

		private void Update()
		{
			if (collisions.Count > 0)
			{
				Vector2 reflectedVelocity = Vector2.zero;

				if (collisions.Count == 1)
				{
					Collision2D collision = collisions[0];

					// We need to perform this check in case the object was destroyed between registering the collision and the update
					Collider2D otherCollider = collision.collider;
					if (otherCollider != null)
					{
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
					}
					else
					{
						// otherwise we can just reflect the velocity at the normal of the contact point
						reflectedVelocity = Vector2.Reflect(ballBody.velocity, collision.GetContact(0).normal);
					}

					if (m_onBallCollision != null)
					{
						m_onBallCollision.Invoke();
					}

				}
				else
				{
					Vector2 combinedNormal = Vector2.zero;
					
					foreach (Vector2 normal in normals)
					{
						combinedNormal += normal;
					}
					combinedNormal.Normalize();
					
					reflectedVelocity = Vector2.Reflect(ballBody.velocity, combinedNormal);
				}

				ballBody.velocity = reflectedVelocity;
				collisions.Clear();
				normals.Clear();
			}
		}

		private void SetInitialVelocity()
		{
			ballBody.velocity = Vector2.up.RotateVector2(Random.Range(-m_initialLaunchAngle, m_initialLaunchAngle) * Mathf.Deg2Rad) * (m_baseSpeed + (m_gameInfo != null ? m_gameInfo.currentBallSpeedModifier : 1f));
		}

		public void LaunchBall()
		{
			SetInitialVelocity();
		}

		public void ResetBall()
		{
			ballBody.velocity = Vector2.zero;
			transform.position = m_startPosition;
		}

		public void Respawn()
		{
			ResetBall();

			Invoke("SetInitialVelocity", 2f);
		}

		private void AdjustBallSpeed(float ballSpeedModifier)
		{
			Vector2 direction = ballBody.velocity.normalized;
			ballBody.velocity = direction * (m_baseSpeed + ballSpeedModifier);
		}
	}
}