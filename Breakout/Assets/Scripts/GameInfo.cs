using UnityEngine;

namespace Breakout
{
	[CreateAssetMenu(fileName = "GameInfo_", menuName = "Breakout/New Game Info", order = 1)]
	public class GameInfo : ScriptableObject
	{
		[Header("Start Settings")]
		[SerializeField] private int m_initialLevel = 1;
		[SerializeField] private int m_initialLives = 3;
		[SerializeField] private int m_initialScore = 0;

		[HideInInspector] public UnityEvents.UnityEventInt gameLevelChanged;
		[HideInInspector] public UnityEvents.UnityEventInt gameLivesChanged;
		[HideInInspector] public UnityEvents.UnityEventInt gameScoreChanged;

		private int m_currentLevel;
		private int m_currentLives;
		private int m_currentScore;

		public int initialLevel { get { return m_initialLevel; } }
		public int initialLives { get { return m_initialLives; } }
		public int initialScore { get { return m_initialScore; } }

		public int currentLevel
		{
			get { return m_currentLevel; }
			private set
			{
				m_currentLevel = value;

				if (gameLevelChanged != null)
				{
					gameLevelChanged.Invoke(currentLevel);
				}
			}
		}

		public int currentLives
		{
			get { return m_currentLives; }
			private set
			{
				m_currentLives = value;

				if (gameLivesChanged != null)
				{
					gameLivesChanged.Invoke(currentLives);
				}
			}
		}

		public int currentScore
		{
			get { return m_currentScore; }
			private set
			{
				m_currentScore = value;

				if (gameScoreChanged != null)
				{
					gameScoreChanged.Invoke(currentScore);
				}
			}
		}

		public void ResetGameInfo()
		{
			currentLevel = m_initialLevel;
			currentLives = m_initialLives;
			currentScore = m_initialScore;
		}

		public void NextLevel()
		{
			currentLevel++;
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