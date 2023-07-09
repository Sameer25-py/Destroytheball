using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class ShootBall : MonoBehaviour
    {
        public float TimeToDestroy = 5f;

        public static Action<GameObject, GameObject> BallHit;

        private void Start()
        {
            Destroy(gameObject, TimeToDestroy);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            BallHit?.Invoke(gameObject, col.gameObject);
        }
    }
}