using UnityEngine;

namespace Breakout
{
	public static class VectorExtensions
	{
		/// <summary>
		/// Returns the vector rotated by the angle given (in radians)
		/// </summary>
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