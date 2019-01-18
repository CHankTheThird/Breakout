using UnityEngine;

namespace Breakout
{
	public class PlayerController : MonoBehaviour
	{
		[Header("References")]
		[SerializeField] private PaddleMovement m_paddleMovement;

		private float m_horizontalInput;

		private void Update()
		{
			if (m_paddleMovement != null)
			{
				float deltaTime = Time.deltaTime;

				ProcessInput();

				m_paddleMovement.UpdateMovement(deltaTime, m_horizontalInput);
			}
		}

		private void ProcessInput()
		{
			m_horizontalInput = Input.GetAxis("Horizontal");
		}
	}
}