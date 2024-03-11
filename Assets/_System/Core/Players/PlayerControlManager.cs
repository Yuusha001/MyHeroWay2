using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyHeroWay
{
    public class PlayerControlManager : Singleton<PlayerControlManager>
    {
        [Header("Components")]
        public PlayerController playerController;
        public PlayerControls playerInput;
        private Collider2D selfCollider;
        public static Combat playerCombat;
        [Header("Flags Status")]
        public bool isDead;
        public bool initialized;
        public Transform MiniMap;

        private void Update()
        {
            if (!initialized) return;
            playerController.UpdateScript();
            UpdateMiniMap();
        }
        private void FixedUpdate()
        {
            if (!initialized) return;
            playerController.FixedUpdateScript();
        }

        public void ResetCharacter()
        {
            isDead = false;
            playerController.ResetController();
        }
        public void Initialize()
        {
            playerInput = new PlayerControls();
            playerInput.Enable();
            selfCollider = GetComponent<Collider2D>();
            playerController.Initialize(this);
            playerCombat = playerController.GetCombat();
            initialized = true;
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

        private void UpdateMiniMap()
        {
            switch (playerController.GetMovement().facingDirection)
            {
                case EFacingDirection.DOWN:
                    MiniMap.transform.eulerAngles = new Vector3(0, 0, 180);
                    break;
                case EFacingDirection.LEFT:
                    MiniMap.transform.eulerAngles = new Vector3(0, 0, 90);
                    break;
                case EFacingDirection.RIGHT:
                    MiniMap.transform.eulerAngles = new Vector3(0, 0, -90);
                    break;
                case EFacingDirection.UP:
                    MiniMap.transform.eulerAngles = Vector3.zero;
                    break;
                default:
                    break;
            }
        }
    }
}