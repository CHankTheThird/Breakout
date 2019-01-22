using UnityEngine;
using UnityEngine.Events;

namespace Breakout
{
	[RequireComponent(typeof(SpriteRenderer))]
	public class BrickHealth : MonoBehaviour
	{
		[Header("Health/Point Settings")]
		[SerializeField] private float m_health = 2f;
		[SerializeField] private int m_pointValue = 1;

		[Header("Visual Settings")]
		[SerializeField] private Color m_brickColor;
		[SerializeField] private Sprite m_damagedSprite;

		[Header("Events")]
		public UnityEvent onDamagedEvent;
		public UnityEvents.UnityEventBrickHealth onDestroyedEvent;

		private float m_currentHealth;

		public int pointValue { get { return m_pointValue; } }

		private SpriteRenderer spriteRenderer { get; set; }

		private void Start()
		{
			m_currentHealth = m_health;

			spriteRenderer = GetComponent<SpriteRenderer>();

			if (spriteRenderer != null)
			{
				spriteRenderer.color = m_brickColor;
			}
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			TakeDamage();
		}

		private void TakeDamage()
		{
			m_currentHealth--;

			if (onDamagedEvent != null)
			{
				onDamagedEvent.Invoke();
			}

			if (m_damagedSprite != null)
			{
				spriteRenderer.sprite = m_damagedSprite;
			}

			if (m_currentHealth <= 0)
			{
				// Give points
				if (onDestroyedEvent != null)
				{
					onDestroyedEvent.Invoke(this);
				}

				Destroy(gameObject);
			}
		}
	}
}