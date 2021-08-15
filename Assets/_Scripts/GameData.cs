public static class GameData {

    public const int F_HEALTHKIT_INCREASE = 15;
    public const int F_AMMOKIT_INCREASE = 4;

    public const int F_FALL_DAMAGE = 10;

    public static int W_LEVEL_COIN_UNLOCK;

    public static int ENEMY_BLOB_DAMAGE;
    public static int ENEMY_CHOMPER_DAMAGE;

    public static int playerHealth;
    public static int playerAmmo;
    public static int playerCoin;

    #region Trigger checks
    public static bool tc_coin;
    public static bool tc_pHealth;
    public static bool tc_pAmmo;

    #endregion

    public static bool isPlayerAlive = true;
}
