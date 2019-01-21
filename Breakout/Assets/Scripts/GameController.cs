using UnityEngine;

namespace Breakout
{
	public class GameController : MonoBehaviour
	{
		[SerializeField] private PlayerController m_player;
		[SerializeField] private BallMovement m_ball;
		[SerializeField] private BoardController m_board;

		private void Start()
		{
			// Display Start screen
			BeginPlay();
		}

		public void BeginPlay()
		{
			// Initialize PlayerController
			if (m_player != null)
			{
				m_player.InitializePlayer();
			}

			// Hide Start Screen

			// Populate board through BoardController
			if (m_board != null)
			{
				m_board.PopulateBoard();
			}

			// Launch ball
			if (m_ball != null)
			{
				m_ball.LaunchBall();
			}

			// Register listeners for gameover and level clear events
		}

		private void ResetForNewLevel()
		{
			// Reset ball and paddle position
			// Repopulate board
		}

		private void GameOver()
		{
			// Display end screen
			// Reset ball and paddle position

		}

		public void ResetForNewGame()
		{
			// Reset Player (clear score and reset lives count)

		}
	}
}