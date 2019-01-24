using UnityEngine;

namespace Breakout
{
	public class GameInfoListener : MonoBehaviour
	{
		[SerializeField] private GameInfo m_gameInfo;

		[SerializeField] private UnityEvents.UnityEventString m_gameLevelChangeEvent;
		[SerializeField] private UnityEvents.UnityEventString m_gameLivesChangeEvent;
		[SerializeField] private UnityEvents.UnityEventString m_gameScoreChangeEvent;

		private void Start()
		{
			RegisterEvents();
		}

		private void OnDestroy()
		{
			DeregisterEvents();
		}

		private void OnEnable()
		{
			RegisterEvents();

			RefreshListeners();
		}

		private void OnDisable()
		{
			DeregisterEvents();
		}

		private void RegisterEvents()
		{
			if (m_gameInfo != null)
			{
				// this assumes that the unityevent bindings won't be changed during the course of play
				if (m_gameLevelChangeEvent != null)
				{
					m_gameInfo.gameLevelChanged.AddListener(LevelChanged);
				}

				if (m_gameLivesChangeEvent != null)
				{
					m_gameInfo.gameLivesChanged.AddListener(LivesChanged);
				}

				if (m_gameScoreChangeEvent != null)
				{
					m_gameInfo.gameScoreChanged.AddListener(ScoreChanged);
				}
			}
		}

		private void DeregisterEvents()
		{
			if (m_gameInfo != null)
			{
				if (m_gameLevelChangeEvent != null)
				{
					m_gameInfo.gameLevelChanged.RemoveListener(LevelChanged);
				}

				if (m_gameLivesChangeEvent != null)
				{
					m_gameInfo.gameLivesChanged.RemoveListener(LivesChanged);
				}

				if (m_gameScoreChangeEvent != null)
				{
					m_gameInfo.gameScoreChanged.RemoveListener(ScoreChanged);
				}
			}
		}

		private void RefreshListeners()
		{
			if (m_gameInfo != null)
			{
				LevelChanged(m_gameInfo.currentLevel);
				LivesChanged(m_gameInfo.currentLives);
				ScoreChanged(m_gameInfo.currentScore);
			}
		}

		private void LevelChanged(int currentLevel)
		{
			if (m_gameLevelChangeEvent != null)
			{
				m_gameLevelChangeEvent.Invoke(currentLevel.ToString());
			}
		}

		private void LivesChanged(int currentLives)
		{
			if (m_gameLivesChangeEvent != null)
			{
				m_gameLivesChangeEvent.Invoke(currentLives.ToString());
			}
		}

		private void ScoreChanged(int currentScore)
		{
			if (m_gameScoreChangeEvent != null)
			{
				m_gameScoreChangeEvent.Invoke(currentScore.ToString());
			}
		}
	}
}