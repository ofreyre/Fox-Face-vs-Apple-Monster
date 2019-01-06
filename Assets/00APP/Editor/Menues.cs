using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using AudioManagement;
using DBGads;

public class Menues
{
    [MenuItem("Sonic Pacman/Create/Map Settings")]
    [MenuItem("Assets/Sonic Pacman/Create/Map Settings")]
    public static void MapSettings_new()
    {
        UtilsScriptableObject.CreateAsset<MapSettings>();
    }

    [MenuItem("Sonic Pacman/Create/Horde")]
    [MenuItem("Assets/Sonic Pacman/Create/Horde")]
    public static void Horde_new()
    {
        UtilsScriptableObject.CreateAsset<Horde>();
    }

    [MenuItem("Sonic Pacman/Create/Hordes")]
    [MenuItem("Assets/Sonic Pacman/Create/Hordes")]
    public static void Hordes_new()
    {
        UtilsScriptableObject.CreateAsset<Hordes>();
    }

    [MenuItem("Sonic Pacman/Create/Units bank")]
    [MenuItem("Assets/Sonic Pacman/Create/Units bank")]
    public static void UnitsBank_new()
    {
        UtilsScriptableObject.CreateAsset<UnitsBank>();
    }

    [MenuItem("Sonic Pacman/Create/Miss pacman bank")]
    [MenuItem("Assets/Sonic Pacman/Create/Miss pacman bank")]
    public static void MissPacmanBank_new()
    {
        UtilsScriptableObject.CreateAsset<MissPacmanBank>();
    }

    [MenuItem("Sonic Pacman/Create/Animator units states speed bank")]
    [MenuItem("Assets/Sonic Pacman/Create/Animator units states speed bank")]
    public static void AnimatorStatesSpeedBank_new()
    {
        UtilsScriptableObject.CreateAsset<AnimatorStatesSpeedBank>();
    }

    [MenuItem("Sonic Pacman/Create/Explosion range bank")]
    [MenuItem("Assets/Sonic Pacman/Create/Explosion range bank")]
    public static void ExplosionRangeBank_new()
    {
        UtilsScriptableObject.CreateAsset<ExplosionRangeBank>();
    }

    [MenuItem("Sonic Pacman/Create/Bullets bank")]
    [MenuItem("Assets/Sonic Pacman/Create/Bullets bank")]
    public static void BulletsBank_new()
    {
        UtilsScriptableObject.CreateAsset<BulletsBank>();
    }

    [MenuItem("Sonic Pacman/Create/Explosions bank")]
    [MenuItem("Assets/Sonic Pacman/Create/Explosions bank")]
    public static void ExplosionsBank_new()
    {
        UtilsScriptableObject.CreateAsset<ExplosionsBank>();
    }

    [MenuItem("Sonic Pacman/Create/Inventory Units Bank")]
    [MenuItem("Assets/Sonic Pacman/Create/Inventory Units Bank")]
    public static void InventoryUnitsBank_new()
    {
        UtilsScriptableObject.CreateAsset<InventoryUnitsBank>();
    }

    [MenuItem("Sonic Pacman/Create/Game stats")]
    [MenuItem("Assets/Sonic Pacman/Create/Game stats")]
    public static void GameStats_new()
    {
        UtilsScriptableObject.CreateAsset<GameStats>();
    }

    [MenuItem("Sonic Pacman/Create/Upgrades catalog")]
    [MenuItem("Assets/Sonic Pacman/Create/Upgrades catalog")]
    public static void UpgradesCatalog_new()
    {
        UtilsScriptableObject.CreateAsset<UpgradesCatalog>();
    }

    [MenuItem("Sonic Pacman/Create/Gamepedia catalog")]
    [MenuItem("Assets/Sonic Pacman/Create/Gamepedia catalog")]
    public static void GamepediaBank_new()
    {
        UtilsScriptableObject.CreateAsset<GamepediaBank>();
    }

    [MenuItem("Sonic Pacman/DB/Levels/Reset levels")]
    private static void ResetLevels()
    {
        DBmanager.ResetLevels();
    }

    [MenuItem("Sonic Pacman/DB/Levels/Delete levels")]
    private static void DeleteLevels()
    {
        DBmanager.DeleteLevels();
    }

    [MenuItem("Sonic Pacman/DB/Inventory/Reset inventory")]
    private static void ResetInventory()
    {
        DBmanager.ResetInventory();
    }

    [MenuItem("Sonic Pacman/DB/Inventory/Delete inventory")]
    private static void DeleteInventory()
    {
        DBmanager.DeleteInventory();
    }

    [MenuItem("Sonic Pacman/DB/Settings/Reset settings")]
    private static void ResetSettings()
    {
        DBmanager.ResetSettings();
    }

    [MenuItem("Sonic Pacman/DB/Settings/Delete settings")]
    private static void DeleteSettings()
    {
        DBmanager.DeleteSettings();
    }

    [MenuItem("Sonic Pacman/DB/Upgrades/Reset")]
    private static void ResetUpgrades()
    {
        DBmanager.ResetUpgrades();
    }

    [MenuItem("Sonic Pacman/DB/Upgrades/Delete")]
    private static void DeleteUpgrades()
    {
        DBmanager.DeleteUpgrades();
    }

    [MenuItem("Sonic Pacman/DB/CoinsForRate/Reset")]
    private static void ResetCoinsForRate()
    {
        DBmanager.ResetCoinsForRate();
    }

    [MenuItem("Sonic Pacman/DB/CoinsForRate/Delete")]
    private static void DeleteCoinsForRate()
    {
        DBmanager.DeleteCoinsForRate();
    }

    [MenuItem("Sonic Pacman/DB/Reset all")]
    private static void DBresetAll()
    {
        DBmanager.ResetAll();
    }

    [MenuItem("Sonic Pacman/DB/Delete all")]
    private static void DBdeleteAll()
    {
        DBmanager.DeleteAll();
    }
    
    [MenuItem("Sonic Pacman/DB/Create/Items catalog")]
    [MenuItem("Assets/Sonic Pacman/DB/Create/Items catalog")]
    public static void Catalog_new()
    {
        
        DBcatalog catalog = UtilsScriptableObject.CreateAsset<DBcatalog>();

        INVENTORYITEM_TYPE[] types = UtilsEnum.Enum2Array<INVENTORYITEM_TYPE>();
        catalog.items = new DBinventoryItem[types.Length];
        DBinventoryItem item;
        for (int i = 0, n = types.Length; i < n; i++)
        {
            item = UtilsScriptableObject.CreateAsset<DBinventoryItem>();
            item.type = types[i];
            catalog.items[i] = item;

        }
    }

    [MenuItem("Sonic Pacman/DB/Create/Catalog item")]
    [MenuItem("Assets/Sonic Pacman/DB/Create/Catalog item")]
    public static void CatalogItem_new()
    {
        UtilsScriptableObject.CreateAsset<DBinventoryItem>();
    }

    #region Globals
    [MenuItem("Sonic Pacman/Globals/Global flow")]
    [MenuItem("Assets/Sonic Pacman/Globals/Global flow")]
    public static void GlobalFlow_new()
    {
        UtilsScriptableObject.CreateAsset<GlobalFlow>();
    }

    [MenuItem("Sonic Pacman/Globals/Globals")]
    [MenuItem("Assets/Sonic Pacman/Globals/Globals")]
    public static void Globals_new()
    {
        UtilsScriptableObject.CreateAsset<Globals>();
    }

    [MenuItem("Sonic Pacman/Globals/Game constants")]
    [MenuItem("Assets/Sonic Pacman/Globals/Game constants")]
    public static void GameConstants_new()
    {
        UtilsScriptableObject.CreateAsset<GameConstants>();
    }

    [MenuItem("Sonic Pacman/Create/Sprite orders")]
    [MenuItem("Assets/Sonic Pacman/Create/Sprite orders")]
    public static void SpriteOrders_new()
    {
        UtilsScriptableObject.CreateAsset<SpriteOrders>();
    }

    [MenuItem("Sonic Pacman/Create/New AudioBank")]
    [MenuItem("Assets/Sonic Pacman/Create/New AudioBank")]
    public static void AudioBank_new()
    {
        UtilsScriptableObject.CreateAsset<AudioBank>();
    }
    #endregion Globals

    #region Levels
    [MenuItem("Sonic Pacman/Levels/Level units bank")]
    [MenuItem("Assets/Sonic Pacman/Levels/Level units bank")]
    public static void LevelUnitsBank_new()
    {
        UtilsScriptableObject.CreateAsset<LevelUnitsBank>();
    }

    [MenuItem("Sonic Pacman/Levels/Level inventory")]
    [MenuItem("Assets/Sonic Pacman/Levels/Level inventory")]
    public static void LevelInventory_new()
    {
        UtilsScriptableObject.CreateAsset<LevelInventory>();
    }

    [MenuItem("Sonic Pacman/Levels/Level settings")]
    [MenuItem("Assets/Sonic Pacman/Levels/Level settings")]
    public static void LevelSettings_new()
    {
        UtilsScriptableObject.CreateAsset<LevelSettings>();
    }

    [MenuItem("Sonic Pacman/Levels/Levels settings")]
    [MenuItem("Assets/Sonic Pacman/Levels/Levels settings")]
    public static void LevelsSettings_new()
    {
        UtilsScriptableObject.CreateAsset<LevelsSettings>();
    }

    [MenuItem("Sonic Pacman/Levels/Babypacman spawner settings")]
    [MenuItem("Assets/Sonic Pacman/Levels/Babypacman spawner settings")]
    public static void BabypacmanSettings_new()
    {
        UtilsScriptableObject.CreateAsset<BabypacmanSettings>();
    }
    #endregion
}
