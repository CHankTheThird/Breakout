using UnityEngine;

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

		[Header("Sound Settings")]
		[SerializeField] private AudioClip m_damagedSound;
		[SerializeField] private AudioClip m_destroyedSound;

		[Header("Events")]
		public UnityEvents.UnityEventBrickHealth onDamagedEvent;
		public UnityEvents.UnityEventBrickHealth onDestroyedEvent;

		private float m_currentHealth;

		public int pointValue { get { return m_pointValue; } }

		public AudioClip damagedSound { get { return m_damagedSound; } }
		public AudioClip destroyedSound { get { return m_destroyedSound; } }

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

			if (m_currentHealth <= 0)
			{
				// Give points
				if (onDestroyedEvent != null)
				{
					onDestroyedEvent.Invoke(this);
				}

				Destroy(gameObject);
			}
			else
			{
				if (onDamagedEvent != null)
				{
					onDamagedEvent.Invoke(this);
				}

				if (m_damagedSprite != null)
				{
					spriteRenderer.sprite = m_damagedSprite;
				}
			}
		}
	}
}