using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class ShootBallController : MonoBehaviour
    {
        public  Rigidbody2D Rb2D;
        private Camera      _camera;

        public float ForceSpeed = 250f;

        public static Action FingerLift;

        private void Start()
        {
            _camera = Camera.main;
        }

        public void AssignBall(GameObject ball)
        {
            Rb2D = ball.GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Rb2D == null) return;
            if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject(0))
            {
                Vector2 mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction          = (mouseWorldPosition - Rb2D.position).normalized;

                Rb2D.gameObject.AddComponent<ShootBall>();
                Rb2D.AddForce(direction * ForceSpeed);
                Rb2D = null;
                FingerLift?.Invoke();
            }
        }
    }
}