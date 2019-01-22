﻿using UnityEngine;

namespace Breakout
{
	public class BoardController : MonoBehaviour
	{
		[SerializeField] private BoardInfo m_boardInfo;
		[SerializeField] private Vector2 m_initialBrickPosition;

		[SerializeField] private GameInfo m_gameInfo;

		private Vector2 currentBrickPlacePosition;
		
		private int numBricksRemaining { get; set; }
		
		public void PopulateBoard()
		{
			numBricksRemaining = 0;

			if (m_boardInfo != null)
			{
				currentBrickPlacePosition = m_initialBrickPosition;

				foreach (BrickHealth brickType in m_boardInfo.GetBrickRows())
				{
					if (brickType != null)
					{
						for (int i = 0; i < m_boardInfo.GetNumBrickColumns(); i++)
						{
							// Can tie in to events on a brick level here when we create them
							BrickHealth newBrick = Instantiate(brickType, currentBrickPlacePosition, Quaternion.identity, transform);
							newBrick.onDestroyedEvent.AddListener(BrickDestroyed);
							
							numBricksRemaining++;

							currentBrickPlacePosition.x += 1f;
						}
						currentBrickPlacePosition.x = m_initialBrickPosition.x;
					}
					currentBrickPlacePosition.y -= 0.5f;
				}
			}
		}

		private void BrickDestroyed(BrickHealth brickDestroyed)
		{
			numBricksRemaining--;

			if (m_gameInfo != null)
			{
				m_gameInfo.IncreaseScore(brickDestroyed.pointValue);

				if (numBricksRemaining <= 0)
				{
					m_gameInfo.NextLevel();
				}
			}

			brickDestroyed.onDestroyedEvent.RemoveListener(BrickDestroyed);
		}
	}
}