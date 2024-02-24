using UnityEngine;
namespace MyHeroWay
{
    public class Core : MonoBehaviour
    {
        public Controller controller { get; private set; }
        public Movement movement;
        public CollisionSenses collisionSenses;
        public Combat combat;

        bool pause;
        public void Initialize(Controller controller)
        {
            this.controller = controller;
            movement.Initialize(this);
            collisionSenses.Initialize(this);
            combat.Initialize(this);
            pause = false;
        }
        public void UpdateLogic()
        {
            if (pause) return;
            movement.UpdateLogic();
            combat.UpdateLogic();
        }

        public void UpdatePhysic()
        {
            if (pause) return;
            movement.UpdatePhysic();
        }

        public void Active()
        {
            pause = false;
            gameObject.SetActive(true);
        }
        public void Deactive()
        {
            gameObject.SetActive(false);
        }
        public void Pause()
        {
            pause = true;
        }
        public void Resume()
        {
            pause = false;
        }

#if UNITY_EDITOR
        private void Reset()
        {
            movement = GetComponentInChildren<Movement>();
            combat = GetComponentInChildren<Combat>();
            collisionSenses = GetComponentInChildren<CollisionSenses>();
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif

    }
}
