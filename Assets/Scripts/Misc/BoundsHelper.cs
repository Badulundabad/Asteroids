using UnityEngine;

namespace Asteroids.Misc
{
    public static class BoundsHelper
    {
        private static float xBound, yBound;
        private static readonly float offsetFromBoundNear = 0.8f;
        private static readonly float offsetFromBoundFar = 1.2f;
        private static readonly float offsetFromCorner = 1f;

        static BoundsHelper()
        {
            yBound = Camera.main.orthographicSize;
            xBound = yBound * Screen.width / Screen.height;
        }

        public static Vector2 GetRandomDirection()
        {
            float x = Random.Range(-1f, 1f);
            float y = Random.Range(-1f, 1f);
            return new Vector2(x, y);
        }

        public static Vector2 GetRandomInBoundsDirection(Vector2 position)
        {
            float x = Random.Range(-xBound + offsetFromBoundFar, xBound - offsetFromBoundFar);
            float y = Random.Range(-yBound + offsetFromBoundFar, yBound - offsetFromBoundFar);

            return (new Vector2(x, y) - position).normalized;
        }

        public static Vector2 GetInBoundsRandomPosition()
        {
            int num = Random.Range(0, 3);
            float x, y;
            if (num < 2) // top or bottom variants
            {
                x = Random.Range(-xBound + offsetFromCorner, xBound - offsetFromCorner);
                y = num == 0 ? yBound + offsetFromBoundNear : -yBound - offsetFromBoundNear;
            }
            else // right or left variants
            {
                y = Random.Range(-yBound + offsetFromCorner, yBound - offsetFromCorner);
                x = num == 2 ? xBound + offsetFromBoundNear : -xBound - offsetFromBoundNear;
            }
            return new Vector2(x, y);
        }

        public static bool IsOutsideBounds(Vector2 position)
        {
            return position.x > xBound + offsetFromBoundFar ||
                   position.x < -xBound - offsetFromBoundFar ||
                   position.y > yBound + offsetFromBoundFar ||
                   position.y < -yBound - offsetFromBoundFar;
        }

        public static Vector2 GetPositionForTeleportation(Vector2 position)
        {
            position = new Vector2(-position.x, -position.y);
            if (position.x > xBound + offsetFromBoundFar)
            {

            }
            else if (position.x < -xBound - offsetFromBoundFar)
            {

            }
            else if (position.y > yBound + offsetFromBoundFar)
            {

            }
            else if (position.y < -yBound - offsetFromBoundFar)
            {

            }
            return position;
        }
    }
}