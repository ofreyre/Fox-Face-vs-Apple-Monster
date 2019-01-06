using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManagement;

public class GameSettings : MonoBehaviour
{
    public static GameSettings instance;

    public GameConstants m_gameConstants;
    public GlobalFlow m_flow;
    public LevelsSettings m_settings;

    private void Awake()
    {
        instance = this;
    }

    public void Init()
    {
        LevelSettings settings = m_settings.settings[m_flow.AbsoluteLevel];

        Map map = gameObject.GetComponent<Map>();
        map.m_settings = settings.mapSettings;
        SpritesOrderManager spritesOrder = gameObject.GetComponent<SpritesOrderManager>();
        spritesOrder.m_map = settings.mapSettings;
        CollisionManager collisionManager = gameObject.GetComponent<CollisionManager>();
        collisionManager.m_settings = settings.mapSettings;
        HordesManager hordesManager = gameObject.GetComponent<HordesManager>();
        hordesManager.m_hordes = settings.hordes;
        GameInventory gameInventory = gameObject.GetComponent<GameInventory>();
        gameInventory.m_coins = settings.coins;
        UnitsSpawner unitSpawner = gameObject.GetComponent<UnitsSpawner>();
        MouseInput mouseInput = gameObject.GetComponent<MouseInput>();
        DefeatController defeatController = gameObject.GetComponent<DefeatController>();
        CoinsManager coinsManager = gameObject.GetComponent<CoinsManager>();
        GameUpgrades gameUpgrades = gameObject.GetComponent<GameUpgrades>();
        ThreepeaterSpawner threepeaterSpawner = gameObject.GetComponent<ThreepeaterSpawner>();
        ScoreManager scoreManager = gameObject.GetComponent<ScoreManager>();
        FillInventory fillInventory = FindObjectsOfType<FillInventory>()[0];
        BabyPacmanSpawner babypacmanSpawner = gameObject.GetComponent<BabyPacmanSpawner>();
        GameAudioPlayer audioPlayer = gameObject.GetComponent<GameAudioPlayer>();
        EarnMousetrap earnmousetrap = gameObject.GetComponent<EarnMousetrap>();
        EarnSonicPacman earnSonicPacman = gameObject.GetComponent<EarnSonicPacman>();

        audioPlayer.Init();
        spritesOrder.Init();
        gameInventory.Init();
        fillInventory.Init();
        map.Init();
        unitSpawner.Init(audioPlayer);
        collisionManager.Init(audioPlayer);
        hordesManager.Init();
        mouseInput.Init();
        defeatController.Init(earnSonicPacman);
        coinsManager.Init(audioPlayer);
        threepeaterSpawner.Init();
        scoreManager.Init(settings.hordes);
        babypacmanSpawner.Init(settings);
        gameUpgrades.Init();
        earnmousetrap.Init();
        earnSonicPacman.Init(defeatController);
    }
}
