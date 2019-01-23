using UnityEngine;

namespace Breakout
{
	public class GameController : MonoBehaviour
	{
		[SerializeField] private PlayerController m_player;
		[SerializeField] private BallMovement m_ball;
		[SerializeField] private BoardController m_board;
		[SerializeField] private RectTransform m_gameOverPanel;
		[SerializeField] private GameInfo m_gameInfo;
		
		private void Start()
		{
			// Display Start screen

			if (m_gameInfo != null)
			{
				m_gameInfo.ResetGameInfo();
			}

			// Populate board through BoardController
			if (m_board != null)
			{
				m_board.PopulateBoard();
			}
		}

		public void BeginPlay()
		{
			// Initialize PlayerController
			if (m_player != null)
			{
				m_player.InitializePlayer();
			}

			if (m_gameInfo != null)
			{
				m_gameInfo.ResetGameInfo();

				m_gameInfo.gameLivesChanged.AddListener(CheckLivesTotal);

				m_gameInfo.gameLevelChanged.AddListener(CheckNewLevelStatus);
			}

			// Launch ball
			if (m_ball != null)
			{
				m_ball.LaunchBall();
			}
		}

		private void CheckLivesTotal(int lives)
		{
			if (lives <= 0)
			{
				GameOver();
			}
			else
			{
				RespawnBall();
			}
		}

		private void CheckNewLevelStatus(int level)
		{
			if (level != m_gameInfo.initialLevel)
			{
				// the level has been increased so we need to reset for the new level
				ResetForNewLevel();
			}
		}

		private void RespawnBall()
		{
			if (m_ball != null)
			{
				m_ball.Respawn();
			}
		}

		private void ResetForNewLevel()
		{
			// Reset ball and paddle position
			RespawnBall();

			// Repopulate board
			if (m_board != null)
			{
				m_board.PopulateBoard();
			}

			if (m_gameInfo != null)
			{
				m_gameInfo.ResetBrickSpeedModifier();
			}
		}

		private void GameOver()
		{
			// Deregister listeners
			if (m_gameInfo != null)
			{
				m_gameInfo.gameLivesChanged.RemoveListener(CheckLivesTotal);

				m_gameInfo.gameLevelChanged.RemoveListener(CheckNewLevelStatus);
			}

			// Display end screen
			if (m_gameOverPanel != null)
			{
				m_gameOverPanel.gameObject.SetActive(true);
			}

			// Reset ball and paddle position
			if (m_ball != null)
			{
				m_ball.ResetBall();
			}

			if (m_player != null)
			{
				m_player.ResetPlayer();
			}
		}

		public void ResetForNewGame()
		{
			// Reset GameInfo (clear score and reset lives count)
			if (m_gameInfo != null)
			{
				m_gameInfo.ResetGameInfo();
			}

			// Reset Board
			if (m_board != null)
			{
				m_board.ClearBoard();
				m_board.PopulateBoard();
			}
		}
	}
}