using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class DBmanager
{
    #region Reusable
    public static void Save<T>(T data, string relativePath)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.OpenWrite(Application.persistentDataPath + "/" + relativePath);
        bf.Serialize(file, data);
        file.Close();
    }

    public static T Load<T>(string relativePath) {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/" + relativePath, FileMode.Open);
        T data = (T)bf.Deserialize(file);
        file.Close();
        return data;
    }

    static void DeleteFile(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
    #endregion


    #region Levels
    public static void ResetLevels()
    {
        Globals globals = Resources.Load<Globals>("Globals");
        int levelsCount = globals.LevelsCount;
        int[] scores = new int[levelsCount];
        int[] stars = new int[levelsCount];

        //Unlock all
        /*for (int i = 0; i < levelsCount; i++)
        {
            stars[i] = 3;
        }*/
        /*for (int i = 0; i < 1; i++)
        {
            stars[i] = 3;
        }*/

        //DBlevels levels = new DBlevels(unlocked, scores, stars);

        DBlevels levels = new DBlevels(scores, stars, false, false);
        Save(levels, "levels");

    }

    public static DBlevels Levels
    {
        get
        {
            if (File.Exists(Application.persistentDataPath + "/levels"))
            {
                return Load<DBlevels>("levels");
            }
            else
            {
                ResetLevels();
                return Load<DBlevels>("levels");
            }
        }

        set
        {
            Save(value, "levels");
        }
    }

    public static void DeleteLevels()
    {
        DeleteFile(Application.persistentDataPath + "/levels");
    }

    /*public static void UnlockLevel(int level)
    {
        DBlevels levels = GetLevels();
        levels.unlocked[level] = true;
        Save(levels, "levels");
    }*/

    public static void SaveScore(int level, int value)
    {
        DBlevels levels = Levels;
        if (levels.score[level] < value)
        {
            levels.score[level] = value;
        }
        Save(levels, "levels");
    }

    public static int GetStars(int level)
    {
        return Levels.stars[level];
    }

    public static int GetScore(int level)
    {
        return Levels.score[level];
    }

    public static void SaveStars(int level, int value)
    {
        DBlevels levels = Levels;
        if (levels.stars[level] < value)
        {
            levels.stars[level] = value;
        }
        Save(levels, "levels");
    }

    public static int Stars {
        get
        {
            int[] levelsStars = Levels.stars;
            int levelsCount = Resources.Load<Globals>("Globals").LevelsCount;
            int starsTotal = 0;
            int stars;
            for (int i = 0; i < levelsCount; i++) {
                stars = levelsStars[i];
                if (stars == 0)
                {
                    //return starsTotal;
                }
                else {
                    starsTotal += stars;
                }
            }
            return starsTotal;
        }
    }

    public static void SaveLevel(int level, int score, int stars)
    {
        DBlevels levels = Levels;
        if (levels.score[level] < score)
        {
            levels.score[level] = score;
        }

        if (levels.stars[level] < stars)
        {
            levels.stars[level] = stars;
        }
        Save(levels, "levels");
    }

    public static int UnlockedLevelMax {
        get {
            int[] stars = Levels.stars;
            int i = 0;
            for (int n = stars.Length; i < n; i++) {
                if (stars[i] == 0) {
                    return i;
                }
            }
            return stars.Length - 1;
        }
    }

    public static int UnlockedStageMax {
        get {
            int levelMax = UnlockedLevelMax;
            if (levelMax < 0) {
                return -1;
            }
            return levelMax / 10;
        }
    }

    public static bool IsUnlocked(int level)
    {
        if (level == 0)
        {
            return true;
        }
        DBlevels levels = Levels;
        if (levels.stars[level] > 0 || levels.stars[level - 1] > 0)
        {
            return true;
        }
        return false;
    }

    public static int LastWonLevel
    {
        get
        {
            int[] stars = Levels.stars;
            for (int i = stars.Length - 1; i > -1; i--)
            {
                if (stars[i] > 0)
                {
                    return i;
                }
            }
            return -1;
        }
    }

    public static bool Win
    {
        get { return Levels.win; }
        set
        {
            DBlevels levels = Levels;
            levels.win = value;
            Levels = levels;
        }
    }

    public static bool WinFull
    {
        get { return Levels.winFull; }
        set
        {
            DBlevels levels = Levels;
            levels.winFull = value;
            Levels = levels;
        }
    }
    #endregion



    #region Inventory
    public static void ResetInventory()
    {
        DBinventory inventory = new DBinventory(0);
        //DBinventory inventory = new DBinventory(200000);
        Save(inventory, "inventory");
    }

    public static DBinventory Inventory
    {
        get
        {
            if (File.Exists(Application.persistentDataPath + "/inventory"))
            {
                return Load<DBinventory>("inventory");
            }
            else
            {
                ResetInventory();
                return Load<DBinventory>("inventory");
            }
        }

        set { Save(value, "inventory"); }
    }

    public static void DeleteInventory()
    {
        DeleteFile(Application.persistentDataPath + "/inventory");
    }

    public static int Coins {
        get {
            return Inventory.coins;
        }

        set {
            DBinventory inventory = Inventory;
            inventory.Coins = value;
            Inventory = inventory;
        }
    }

    public static int AddItems(INVENTORYITEM_TYPE type, int amount)
    {
        DBinventory inventory = Inventory;
        int i = inventory.AddItems(type, amount);
        Inventory = inventory;
        return i;
    }
    #endregion


    #region Units
    public static void ResetUnits()
    {
        DBunits units = new DBunits(null);
        Save(units, "units");
    }

    public static DBunits Units
    {
        get
        {
            if (File.Exists(Application.persistentDataPath + "/units"))
            {
                return Load<DBunits>("units");
            }
            else
            {
                ResetUnits();
                return Load<DBunits>("units");
            }
        }

        set
        {
            Save(value, "units");
        }
    }

    public static void DeleteUnits()
    {
        DeleteFile(Application.persistentDataPath + "/units");
    }

    public static void AddUnit(UNITTYPE type)
    {
        DBunits units = Units;
        units.Add(type);
        Units = units;
    }
    #endregion Units


    #region Settings
    public static void ResetSettings()
    {
        DBsettings settings = new DBsettings(true, 0.5f, 0.5f, 0, 1);
        Save(settings, "settings");
    }

    public static DBsettings Settings
    {
        get {
            if (File.Exists(Application.persistentDataPath + "/settings"))
            {
                return Load<DBsettings>("settings");
            }
            else
            {
                ResetSettings();
                return Load<DBsettings>("settings");
            }
        }

        set { Save(value, "settings"); }
    }

    public static void DeleteSettings()
    {
        DeleteFile(Application.persistentDataPath + "/settings");
    }

    public static float sfxVolume
    {
        get {
            DBsettings settings = Settings;
            return settings.sfxVolume;
        }

        set {
            DBsettings settings = Settings;
            settings.sfxVolume = value;
            Settings = settings;
        }
    }

    public static float musicVolume
    {
        get
        {
            DBsettings settings = Settings;
            return settings.musicVolume;
        }

        set
        {
            DBsettings settings = Settings;
            settings.musicVolume = value;
            Settings = settings;
        }
    }

    public static void SetShowAdds(bool showAdds)
    {
        DBsettings settings = Settings;
        settings.showAdds = showAdds;
        Settings = settings;
        Save(settings, "settings");
    }

    public static Vector2 mainScrollPosition
    {
        get
        {
            DBsettings settings = Settings;
            return settings.mainScrollPosition;
        }

        set
        {
            DBsettings settings = Settings;
            settings.mainScrollPosition = value;
            Settings = settings;
        }
    }
    #endregion



    #region Upgrades
    public static void ResetUpgrades()
    {
        UpgradesCatalog catalog = Resources.Load<UpgradesCatalog>("UpgradesCatalog");
        UpgradesCategoryCatalog[] catalogUpgrades = catalog.upgrades;
        UpgradesCategoryCatalog upgrade;
        UpgradeCategoryCount[] dbupgrades = new UpgradeCategoryCount[catalogUpgrades.Length];
        for (int i = 0, n = catalogUpgrades.Length; i < n; i++) {
            upgrade = catalogUpgrades[i];
            dbupgrades[i] = new UpgradeCategoryCount(upgrade.category, 0);
        }

        DBupgrades upgrades = new DBupgrades();
        upgrades.categoryCounts = dbupgrades;

        Save(upgrades, "upgrades");
    }

    public static DBupgrades Upgrades
    {
        get
        {
            if (File.Exists(Application.persistentDataPath + "/upgrades"))
            {
                return Load<DBupgrades>("upgrades");
            }
            else
            {
                ResetUpgrades();
                return Load<DBupgrades>("upgrades");
            }
        }

        set
        {
            Save(value, "upgrades");
        }
    }

    public static void DeleteUpgrades()
    {
        DeleteFile(Application.persistentDataPath + "/upgrades");
    }

    public static void SetUpgrade(INVENTORYITEM_CATEGORY category, int amount)
    {
        DBupgrades upgrades = Upgrades;
        upgrades.SetCount(category, amount);
        Upgrades = upgrades;
    }

    public static void AddUpgrade(INVENTORYITEM_CATEGORY category, int amount)
    {
        DBupgrades upgrades = Upgrades;
        upgrades.AddCount(category, amount);
        Upgrades = upgrades;
    }

    public static void ClearUpgrades()
    {
        DBupgrades upgrades = Upgrades;
        upgrades.Clear();
        Upgrades = upgrades;
    }
    #endregion


    #region CoinsPerRate
    public static void ResetCoinsForRate()
    {

        DBcoinsForRate coinsForRate = new DBcoinsForRate(150);
        Save(coinsForRate, "coinsForRate");
    }

    public static DBcoinsForRate CoinsForRate
    {
        get
        {
            if (File.Exists(Application.persistentDataPath + "/coinsForRate"))
            {
                return Load<DBcoinsForRate>("coinsForRate");
            }
            else
            {
                ResetCoinsForRate();
                return Load<DBcoinsForRate>("coinsForRate");
            }
        }

        set
        {
            Save(value, "coinsForRate");
        }
    }

    public static void DeleteCoinsForRate()
    {
        DeleteFile(Application.persistentDataPath + "/coinsForRate");
    }

    public static void SetCoinsForRate(int amount)
    {
        DBcoinsForRate coinsForRate = CoinsForRate;
        coinsForRate.amount = amount;
        CoinsForRate = coinsForRate;
    }
    #endregion





    public static void ResetAll() {
        ResetLevels();
        ResetInventory();
        ResetUnits();
        ResetUpgrades();
        ResetSettings();
        ResetCoinsForRate();
    }

    public static void DeleteAll()
    {
        DeleteLevels();
        DeleteInventory();
        DeleteUnits();
        DeleteUpgrades();
        DeleteSettings();
        DeleteCoinsForRate();
    }
}
