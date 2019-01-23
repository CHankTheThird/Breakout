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

		/// <summary>
		/// A dictionary for tracking if a type of brick has been hit yet or not
		/// Operates on the assumption that no 2 bricks will have the same point value
		/// </summary>
		private Dictionary<int, bool> m_brickTypesHit;

		private int numBricksRemaining { get; set; }

		private AudioSource audioSource { get; set; }

		private void Awake()
		{
			m_bricks = new List<BrickHealth>();

			m_brickTypesHit = new Dictionary<int, bool>();

			audioSource = GetComponent<AudioSource>();
		}

		public void PopulateBoard()
		{
			if (m_gameInfo != null)
			{
				m_gameInfo.ResetBrickSpeedModifier();
			}

			numBricksRemaining = 0;

			if (m_boardInfo != null)
			{
				m_brickTypesHit.Clear();

				currentBrickPlacePosition = m_initialBrickPosition;

				foreach (BrickHealth brickType in m_boardInfo.GetBrickRows())
				{
					if (brickType != null)
					{
						if (!m_brickTypesHit.ContainsKey(brickType.pointValue))
						{
							m_brickTypesHit.Add(brickType.pointValue, false);
						}

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
			m_brickTypesHit.Clear();
		}

		private void BrickDamaged(BrickHealth brickDamaged)
		{
			if (audioSource != null && brickDamaged.damagedSound != null)
			{
				audioSource.PlayOneShot(brickDamaged.damagedSound);
			}

			if (m_brickTypesHit[brickDamaged.pointValue] == false)
			{
				if (m_gameInfo != null)
				{
					m_gameInfo.IncreaseBrickSpeedModifier();
				}

				m_brickTypesHit[brickDamaged.pointValue] = true;
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
