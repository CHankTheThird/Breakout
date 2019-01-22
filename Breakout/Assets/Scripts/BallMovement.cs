﻿using UnityEngine;
using UnityEngine.Events;

namespace Breakout
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class BallMovement : MonoBehaviour
	{
		[Header("Movement Settings")]
		[SerializeField] private float m_baseSpeed = 3f;
		[SerializeField] private float m_initialLaunchAngle = 35f;

		[Header("Events")]
		[SerializeField] private UnityEvent m_onBallCollision;

		private Vector2 m_startPosition;

		private Rigidbody2D ballBody { get; set; }

		private void Awake()
		{
			ballBody = GetComponent<Rigidbody2D>();
		}

		private void Start()
		{
			m_startPosition = transform.position;
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

			if (m_onBallCollision != null)
			{
				m_onBallCollision.Invoke();
			}

			ballBody.velocity = reflectedVelocity;
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

		private void SetInitialVelocity()
		{
			ballBody.velocity = Vector2.up.RotateVector2(Random.Range(-m_initialLaunchAngle, m_initialLaunchAngle) * Mathf.Deg2Rad) * m_baseSpeed;
		}
	}
}