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
			Vector2 relativeImpactPoint = collision.GetContact(0).point - new Vector2(transform.position.x, transform.position.y);

			if (Mathf.Abs(relativeImpactPoint.x) > Mathf.Abs(relativeImpactPoint.y))
			{
				ballBody.velocity = new Vector2(-ballBody.velocity.x, ballBody.velocity.y);
			}
			else
			{
				ballBody.velocity = new Vector2(ballBody.velocity.x, -ballBody.velocity.y);
			}
		}
	}
}