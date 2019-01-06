using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBGPhysics2D
{
    public static class Bullet2D
    {
        public static float GetInitialSpeedToTarget(Vector3 v0, Vector3 v1, float radians, float g)
        {
            g = -g;
            float dx = v0.x < v1.x ? v1.x - v0.x : v0.x - v1.x;
            float dy = v1.y - v0.y;
            return (1 / Mathf.Cos(radians)) * Mathf.Sqrt((0.5f * g * dx * dx) / (dx * Mathf.Tan(radians) + dy));
        }

        public static float GetInitialSpeedToTarget(Vector3 v0, Vector3 v1, float cos, float tan, float g)
        {
            g = -g;
            float dx = v0.x < v1.x ? v1.x - v0.x : v0.x - v1.x;
            float dy = v1.y - v0.y;
            return (1 / cos) * Mathf.Sqrt((0.5f * g * dx * dx) / (dx * tan + dy));
        }

        public static float GetInitialSpeedToTarget(Vector3 v0, Vector2 v1, Vector3 direction, float g)
        {
            g = -g;
            float dx = v1.x - v0.x;
            float dy = v1.y - v0.y;
            return dx * Mathf.Sqrt(0.5f * g / (direction.x * (dx * direction.y + direction.x * (v0.y - dy))));
        }
        
        public static Vector3 GetImpulseToTarget(Vector3 p0, Vector3 p1, float speedX, float g)
        {
            float dx = p1.x - p0.x;
            float dy = p1.y - p0.y;
            float t = dx / speedX;
            return new Vector3(speedX, dy / t - t * g * 0.5f, 0);
        }

        public static Vector3 GetImpulseToTarget(Vector3 p0, Vector3 p1, float speedX0, float speedX1, float g)
        {
            float dx = p1.x - p0.x;
            float dy = p1.y - p0.y;
            float t = dx / (speedX0 - speedX1);
            float vy = dy / t - t * g * 0.5f;
            return new Vector3(speedX0, vy, 0);
        }
    }
}
