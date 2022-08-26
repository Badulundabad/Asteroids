using UnityEngine;

namespace Asteroids.FlyingObjects.Misc
{
    public class BoundsHelper 
    {
        private float xBound, yBound;
        private readonly float offsetFromBoundNear = 0.8f;
        private readonly float offsetFromBoundFar = 1f;
        private readonly float offsetFromCorner = 1f;

        public BoundsHelper()
        {
            yBound = Camera.main.orthographicSize;
            xBound = yBound * Screen.width / Screen.height;
        }

        public Vector3 GetRandomVectorFromPosition(Vector3 position)
        {
            float x = Random.Range(-xBound + offsetFromBoundFar, xBound - offsetFromBoundFar);
            float y = Random.Range(-yBound + offsetFromBoundFar, yBound - offsetFromBoundFar);

            return (new Vector3(x, y, -1) - position).normalized;
        }

        public Vector3 GetInBoundsRandomPosition()
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
            return new Vector3(x, y, -1f);
        }

        public bool IsPositionInBounds(Vector3 position)
        {
            return position.x < xBound + offsetFromBoundFar ||
                   position.x > -xBound - offsetFromBoundFar ||
                   position.y < yBound + offsetFromBoundFar ||
                   position.y > -yBound - offsetFromBoundFar;
        }
    }
}