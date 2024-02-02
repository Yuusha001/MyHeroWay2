using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyHeroWay
{
    public class PlayerControlManager : Singleton<PlayerControlManager>
    {
        [Header("Components")]
        public PlayerController playerController;
        public PlayerControls playerInput;
        private Collider2D selfCollider;
        [Header("Flags Status")]
        public bool isDead;

        protected override void Awake()
        {
            base.Awake();
            playerInput = new PlayerControls();
        }

        private void OnEnable()
        {
            playerInput.Enable();
            Initialize();
        }

        private void OnDisable()
        {
            playerInput.Disable();
        }

        private void Update()
        {
            InputHandler();
            playerController.UpdateScript();
        }
        private void FixedUpdate()
        {
            playerController.FixedUpdateScript();
        }

        public void InputHandler()
        {
           playerController.SetMovementDirection(playerInput.Movement.Move.ReadValue<Vector2>());
        }

        public void ResetCharacter()
        {
            isDead = false;
            playerController.ResetController();
        }
        public void SetPosition(Vector3 position)
        {
            position.z = -5;
            transform.position = position;
        }
        public void Initialize()
        {
            selfCollider = GetComponent<Collider2D>();
            playerController.Initialize(this);
        }

        

        public void PlayerDie()
        {
            if (isDead) return;
            isDead = true;
        }
        public Bounds GetBoundPlayer()
        {
            Vector2 center = selfCollider.bounds.center;
            Bounds bounds = new Bounds(center, selfCollider.bounds.size);
            return bounds;
        }

        public void OnWin()
        {
            
        }
        public void OnPause()
        {
            playerController.Pause();
        }
        public void OnResume()
        {
            playerController.Resume();
        }
    }
}
