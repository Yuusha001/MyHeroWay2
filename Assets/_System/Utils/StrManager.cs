namespace Utils.String
{
    public class StrManager 
    {
        #region Object Pooling
        public static readonly string ProjectilePool = "Projectile";
        public static readonly string VFXPool = "VFX";
        public static readonly string UIPool = "UI";
        public static readonly string ArrowGreenProjectile = "arrow_green";
        public static readonly string ExpOrb = "exp_orb";
        public static readonly string InventoryUI_Prefab = "InventorySlot";
        public static readonly string InventoryStackUI_Prefab = "InventoryStackSlot";
        #endregion

        #region Animations
        public static readonly string isWalking = "isWalking";
        public static readonly string isDead = "isDead";
        public static readonly string isInteracting = "isInteracting";
        public static readonly string isShoot = "isShoot";
        public static readonly string attackAnimation = "Attack";
        public static readonly string getHitAnimation = "getHit";
        public static readonly string moveX = "moveX";
        public static readonly string moveY = "moveY";
        public static readonly string TriggerDamageEvent = "TriggerDamage";
        public static readonly string ActiveComboEvent = "ActiveCombo";
        public static readonly string DeactiveComboEvent = "DeactiveCombo";
        #endregion

        #region Layers
        public static readonly string IDamageLayer = "IDamage";
        public static readonly string ObstacleLayer = "Obstacle";
        #endregion

        #region Tag
        public static readonly string LookAbleTag = "Lookable";
        public static readonly string EnemyTag = "Enemy";
        public static readonly string PlayerTag = "Player";
        #endregion

        #region AI State
        public static readonly string WanderingState = "WanderingState";
        public static readonly string ChasingState = "ChasingState";
        public static readonly string AttackingState = "AttackingState";
        #endregion

        #region Text Effect
        public static readonly string[] Missed = { "Miss", "Dodge", "Block" };
        #endregion
    }
}
