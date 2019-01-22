using UnityEngine;
using UnityEngine.Events;

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