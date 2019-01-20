using UnityEngine;
using UnityEngine.Events;

namespace Breakout
{
	public class PlayerInfoListener : MonoBehaviour
	{
		[SerializeField] private PlayerInfo m_playerInfo;

		[SerializeField] private UnityEvents.UnityEventString m_playerLivesChangeEvent;
		[SerializeField] private UnityEvents.UnityEventString m_playerScoreChangeEvent;

		private void Start()
		{
			if (m_playerInfo != null)
			{
				// this assumes that the unityevent bindings won't be changed during the course of play
				if (m_playerLivesChangeEvent != null)
				{
					m_playerInfo.playerLivesChanged.AddListener(LivesChanged);
				}

				if (m_playerScoreChangeEvent != null)
				{
					m_playerInfo.playerScoreChanged.AddListener(ScoreChanged);
				}
			}
		}

		private void LivesChanged(int currentLives)
		{
			if (m_playerLivesChangeEvent != null)
			{
				m_playerLivesChangeEvent.Invoke(currentLives.ToString());
			}
		}

		private void ScoreChanged(int currentScore)
		{
			if (m_playerScoreChangeEvent != null)
			{
				m_playerScoreChangeEvent.Invoke(currentScore.ToString());
			}
		}
	}
}