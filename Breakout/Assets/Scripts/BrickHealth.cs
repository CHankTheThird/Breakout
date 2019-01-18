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

		private float m_currentHealth;

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

			if (m_damagedSprite != null)
			{
				spriteRenderer.sprite = m_damagedSprite;
			}

			if (m_currentHealth <= 0)
			{
				// Give points
				Debug.Log("Brick destroyed, " + m_pointValue + " given");

				Destroy(gameObject);
			}
		}
	}
}