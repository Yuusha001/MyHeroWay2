namespace Utils.String
{
    public class StrManager 
    {
        #region Object Pooling
        public static readonly string ProjectilePool = "Projectile";
        public static readonly string VFXPool = "VFX";
        public static readonly string ArrowGreenProjectile = "arrow_green";

        #endregion

        #region Animations
        public static readonly string isWalking = "isWalking";
        public static readonly string isInteracting = "isInteracting";
        public static readonly string isShoot = "isShoot";
        public static readonly string getHit = "getHit";
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
        #endregion
    }
}
