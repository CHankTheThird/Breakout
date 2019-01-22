using System.Collections.Generic;
using UnityEngine;

namespace Breakout
{
	public class BoardController : MonoBehaviour
	{
		[SerializeField] private BoardInfo m_boardInfo;
		[SerializeField] private Vector2 m_initialBrickPosition;

		[SerializeField] private GameInfo m_gameInfo;

		private Vector2 currentBrickPlacePosition;

		private List<BrickHealth> m_bricks;

		private int numBricksRemaining { get; set; }

		private AudioSource audioSource { get; set; }

		private void Awake()
		{
			m_bricks = new List<BrickHealth>();

			audioSource = GetComponent<AudioSource>();
		}

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
							newBrick.onDamagedEvent.AddListener(BrickDamaged);
							m_bricks.Add(newBrick);

							numBricksRemaining++;

							currentBrickPlacePosition.x += 1f;
						}
						currentBrickPlacePosition.x = m_initialBrickPosition.x;
					}
					currentBrickPlacePosition.y -= 0.5f;
				}
			}
		}

		public void ClearBoard()
		{
			foreach (BrickHealth brick in m_bricks)
			{
				brick.onDestroyedEvent.RemoveListener(BrickDestroyed);
				brick.onDamagedEvent.RemoveListener(BrickDamaged);
				Destroy(brick.gameObject);
			}

			m_bricks.Clear();
		}

		private void BrickDamaged(BrickHealth brickDamaged)
		{
			if (audioSource != null && brickDamaged.damagedSound != null)
			{
				audioSource.PlayOneShot(brickDamaged.damagedSound);
			}
		}

		private void BrickDestroyed(BrickHealth brickDestroyed)
		{
			numBricksRemaining--;

			if (audioSource != null && brickDestroyed.destroyedSound != null)
			{
				audioSource.PlayOneShot(brickDestroyed.destroyedSound);
			}

			if (m_gameInfo != null)
			{
				m_gameInfo.IncreaseScore(brickDestroyed.pointValue);

				if (numBricksRemaining <= 0)
				{
					m_gameInfo.NextLevel();
				}
			}

			m_bricks.Remove(brickDestroyed);

			brickDestroyed.onDestroyedEvent.RemoveListener(BrickDestroyed);
			brickDestroyed.onDamagedEvent.RemoveListener(BrickDamaged);
		}
	}
}
