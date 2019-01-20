using UnityEngine;
using UnityEngine.Events;

namespace Breakout
{
	[CreateAssetMenu(fileName = "PlayerInfo_", menuName = "Breakout/New Player Info", order = 1)]
	public class PlayerInfo : ScriptableObject
	{
		[Header("Start Settings")]
		[SerializeField] private int m_initialLives = 3;
		[SerializeField] private int m_initialScore = 0;

		[HideInInspector] public UnityEvents.UnityEventInt playerLivesChanged;
		[HideInInspector] public UnityEvents.UnityEventInt playerScoreChanged;

		private int m_currentLives;
		private int m_currentScore;

		public int currentLives
		{
			get { return m_currentLives; }
			private set
			{
				m_currentLives = value;

				if (playerLivesChanged != null)
				{
					playerLivesChanged.Invoke(currentLives);
				}
			}
		}

		public int currentScore
		{
			get { return m_currentScore; }
			private set
			{
				m_currentScore = value;

				if (playerScoreChanged != null)
				{
					playerScoreChanged.Invoke(currentScore);
				}
			}
		}

		public void InitializePlayerInfo()
		{
			currentLives = m_initialLives;
			currentScore = m_initialScore;
		}

		public void LoseLife()
		{
			currentLives--;
		}

		public void GainLife()
		{
			currentLives++;
		}

		public void IncreaseScore(int increase)
		{
			currentScore += increase;
		}

		public void DecreaseScore(int decrease)
		{
			currentScore -= decrease;
		}
	}
}