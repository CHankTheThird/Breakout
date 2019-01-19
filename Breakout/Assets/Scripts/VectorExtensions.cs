using UnityEngine;

namespace Breakout
{
	public static class VectorExtensions
	{
		public static Vector2 RotateVector2(this Vector2 vectorToRotate, float angleToRotate)
		{
			float x = vectorToRotate.x;
			float y = vectorToRotate.y;
			float cosAngle = Mathf.Cos(angleToRotate);
			float sinAngle = Mathf.Sin(angleToRotate);

			return new Vector2(x * cosAngle - y * sinAngle, x * sinAngle + y * cosAngle);
		}
	}
}