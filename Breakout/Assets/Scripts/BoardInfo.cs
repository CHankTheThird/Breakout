using System.Collections.Generic;
using UnityEngine;

namespace Breakout
{
	/// <summary>
	/// Info class for holding level designs; would need to be expanded upon to make more robust and interesting levels
	/// </summary>
	[CreateAssetMenu(fileName = "BoardInfo_", menuName = "Breakout/New Board Info", order = 2)]
	public class BoardInfo : ScriptableObject
	{
		[SerializeField] private List<BrickHealth> m_brickRows;
		[SerializeField] private int m_numBrickColumns = 11;

		public List<BrickHealth> GetBrickRows()
		{
			return m_brickRows;
		}

		public int GetNumBrickColumns()
		{
			return m_numBrickColumns;
		}
	}
}