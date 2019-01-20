using System;
using UnityEngine.Events;

namespace Breakout
{
	public class UnityEvents
	{
		[Serializable]
		public class UnityEventInt : UnityEvent<int>
		{

		}

		[Serializable]
		public class UnityEventString : UnityEvent<string>
		{

		}
	}
}