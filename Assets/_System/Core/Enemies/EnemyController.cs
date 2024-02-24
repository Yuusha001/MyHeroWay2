using MyHeroWay.Stats;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.U2D.Animation;

namespace MyHeroWay
{
    public abstract class EnemyController : Controller
    {
        public EEnemyType Type;
        [Dropdown("GetEnemyNames")]
        public EEnemyName Name;
        public int currentLevel;
        [Foldout("Animator")]
        public EnemyAnimator enemyAnimator;
        public AIState currentState;
        public AIState attackingState;
        public AIState chasingState;
        public AIState wanderingState;
        public SpriteBar hpBar;
        public SpriteBar mpBar;
        public FOV fieldOfView;
        public Controller target;
        public Weapon weapon;
        public NavMeshAgent navMeshAgent;
        public float warnderingRange;
        public float reinforcementRange;
        public SpriteLibrary spriteLibrary;
        public TextMesh levelTxt;
        public EAIBehavior AIBehavior;
        public virtual void Initialize()
        {
            core.Initialize(this);
            animatorHandle = enemyAnimator;
            enemyAnimator.Initialize(this);
            isActive = true;
            CaculateStats();
            GetCombat().OnStatsChange += StatsChangeHandler;
            hpBar?.InitializeBar();
            mpBar?.InitializeBar();
            fieldOfView?.Initialize(this);
            weapon?.Initialize(this);
            NavMeshAgentSetup();
            VisualSetup();
        }


        private void VisualSetup()
        {
            var enemyDictionary = DataManager.Instance.enemyDictionary;
            spriteLibrary.spriteLibraryAsset = enemyDictionary.GetEnemyData(Type).GetData(Name).spriteLibraryAsset;
        }

        private void NavMeshAgentSetup()
        {
            navMeshAgent.updateRotation = false;
            navMeshAgent.updateUpAxis = false;
            navMeshAgent.speed = GetMovement().movementSpeed - 1;
            navMeshAgent.acceleration = navMeshAgent.speed + 0.25f;
        }

        private void StatsChangeHandler()
        {
            hpBar?.UpdateBar(GetCombat().NormalizeHealth());
            mpBar?.UpdateBar(GetCombat().NormalizeMana());
            if (runtimeStats.health == 0 && isActive)
            {
                Die(true);
            }
        }

        public virtual void UpdateLogic()
        {
            if (!IsActive) return;
            currentState?.UpdateLogic();
            core?.UpdateLogic();
            fieldOfView?.UpdateLogic();
            enemyAnimator?.UpdateLogic();
            Debug.Log(currentState.stateName);
        }
        public virtual void UpdatePhysic()
        {
            if (!IsActive) return;
            currentState?.UpdatePhysic();
            core?.UpdatePhysic();
            enemyAnimator.UpdatePhysic();
        }

        public void SwitchState(AIState newState)
        {
            currentState?.ExitState();
            currentState = newState;
            currentState?.EnterState();
        }


        public override void Die(bool deactiveCharacter)
        {
            base.Die(deactiveCharacter);
            hpBar?.Deactive();
            mpBar?.Deactive();
            this.gameObject.SetActive(false);
            GetCombat().OnStatsChange -= StatsChangeHandler;
        }

        [Button("Caculate Stats")]
        private void CaculateStats()
        {
            levelTxt.text = currentLevel.ToString();
            var data = DataManager.Instance.enemyDictionary.GetEnemyData(Type).GetData(Name);
            originalStats = StatsCaculation.GetFinalCharacterStats(currentLevel, data.characterStatsModifier);
            runtimeStats = new CharacterStats() + StatsCaculation.GetFinalCharacterStats(currentLevel, data.characterStatsModifier);
            runtimeStats.level = currentLevel;
        }


        private EEnemyName[] GetEnemyNames()
        {
            EEnemyName[] EnemyNames;
            switch (Type)
            {
                case EEnemyType.Slime:
                    EnemyNames = new EEnemyName[]
                    {
                        EEnemyName.Aquamarine_Slime, 
                        EEnemyName.Blue_Slime, 
                        EEnemyName.BlueGreen_Slime, 
                        EEnemyName.DarkBluee_Slime, 
                        EEnemyName.Gold_Slime, 
                        EEnemyName.Green_Slime, 
                        EEnemyName.Lightblue_Slime, 
                        EEnemyName.Maroon_Slime, 
                        EEnemyName.Orange_Slime, 
                        EEnemyName.Pale_Slime, 
                        EEnemyName.Pink_Smile, 
                        EEnemyName.Purple_Slime, 
                        EEnemyName.Red_Smile

                    };
                    break;
                case EEnemyType.MiniSmile:
                    EnemyNames = new EEnemyName[]
                    {
                        EEnemyName.Aquamarine_Baby_Slime,
                        EEnemyName.Blue_Baby_Slime,
                        EEnemyName.BlueGreen_Baby_Slime,
                        EEnemyName.DarkBluee_Baby_Slime,
                        EEnemyName.Gold_Baby_Slime,
                        EEnemyName.Green_Baby_Slime,
                        EEnemyName.Lightblue_Baby_Slime,
                        EEnemyName.Maroon_Baby_Slime,
                        EEnemyName.Orange_Baby_Slime,
                        EEnemyName.Pale_Baby_Slime,
                        EEnemyName.Pink_Baby_Smile,
                        EEnemyName.Purple_Baby_Slime,
                        EEnemyName.Red_Baby_Smile,
                    };
                    break;
                default:
                    EnemyNames = new EEnemyName[]
                    {
                        EEnemyName.None
                    };
                    break;
            }
            return EnemyNames;
        }

    }
}

