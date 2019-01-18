using UnityEngine;

namespace Breakout
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class PaddleMovement : MonoBehaviour
	{
		[Header("Movement Settings")]
		[SerializeField] private float m_movementSpeed = 5f;

		private Rigidbody2D paddleBody { get; set; }

		private void Awake()
		{
			paddleBody = GetComponent<Rigidbody2D>();
		}

		public void UpdateMovement(float deltaTime, float horizontalInput)
		{
			if (paddleBody == null)
				return;

			paddleBody.velocity = new Vector2(horizontalInput * m_movementSpeed, 0f);
		}
	}
}