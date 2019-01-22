using UnityEngine;
using UnityEngine.Events;

namespace Breakout
{
	[RequireComponent(typeof(Collider2D))]
	public class KillPlane : MonoBehaviour
	{
		[SerializeField] private UnityEvent m_onKillEvent;

		[SerializeField] private GameInfo m_gameInfo;

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (m_onKillEvent != null)
			{
				m_onKillEvent.Invoke();
			}

			if (m_gameInfo != null)
			{
				m_gameInfo.LoseLife();
			}
		}
	}
}