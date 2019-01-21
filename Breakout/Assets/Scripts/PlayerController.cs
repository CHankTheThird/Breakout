﻿using UnityEngine;

namespace Breakout
{
	public class PlayerController : MonoBehaviour
	{
		[Header("References")]
		[SerializeField] private PaddleMovement m_paddleMovement;
		[SerializeField] private PlayerInfo m_playerInfo;

		private float m_horizontalInput;
		
		private bool isInitialized { get; set; }

		public void InitializePlayer()
		{
			m_playerInfo.InitializePlayerInfo();

			isInitialized = true;
		}

		private void Update()
		{
			if (!isInitialized)
				return;

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