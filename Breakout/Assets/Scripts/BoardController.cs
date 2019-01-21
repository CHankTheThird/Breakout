using System.Collections.Generic;
using UnityEngine;

namespace Breakout
{
	public class BoardController : MonoBehaviour
	{
		[SerializeField] private BoardInfo m_boardInfo;
		[SerializeField] private Vector2 m_initialBrickPosition;

		private Vector2 currentBrickPlacePosition;

		private List<BrickHealth> m_bricks;

		public int numBricksRemaining { get { return m_bricks != null ? m_bricks.Count : 0; } }

		private void Awake()
		{
			m_bricks = new List<BrickHealth>();
		}
		
		public void PopulateBoard()
		{
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

							m_bricks.Add(Instantiate(brickType, currentBrickPlacePosition, Quaternion.identity, transform));
							currentBrickPlacePosition.x += 1f;
						}
						currentBrickPlacePosition.x = m_initialBrickPosition.x;
					}
					currentBrickPlacePosition.y -= 0.5f;
				}
			}
		}
	}
}
