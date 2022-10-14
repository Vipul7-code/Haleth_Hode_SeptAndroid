using HelthHolde;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.UI;
public class BattleManager : MonoBehaviour
{
    public PlayerConfigurationLibrary playersLibrary;
    public PlayerItemLibrary playerItemLibrary;
    public PlayerItemLibrary companionItemLibrary, companionWithoutMarium, companionWithoutTucker, FirstCompanionCombo, secondCompanionCombo, thirdCompanionCombo, johnLibrary, mariumLibrary, tuckerLibrary;
    PlayerItemLibrary activeLibrary, companionLibrary;
    List<PlayerItemLibrary> randomsLibrary = new List<PlayerItemLibrary>();
    [SerializeField]
    PlayerItemLibrary soldierCampsiteLibrary, wagonCaravanLibrary, secondSoldierLibrary, bodyGuardLibrary, eliteAndCaptainLib, atwaterLibrary, monestryTuckerLibrary, motteyAndBailey1, motteyAndBailey2, motteyAndBailey3, motteyAndBailey4, captainLibrary, soldierLib, guardLib, throneEntrance, tutorialLibrary, brigandLierLibrary, houndLibrary, wolfLibrary, pantherLibrary, randomLibrary, randomPetrolLib, scoutEnemyLib;

    [SerializeField]
    PlayerItemLibrary barghestLibrary, conscriptSolLib, deathWightLibrary, brigandLibrary, brigandSolLibrary, brigandArcherLibrary, completeBrigandArcher, huntingtonLibrary, huntingtonCastle1Library, huntingtonThroneLibrary, huntingtonFinalThrone, sargentLibrary, priestLibrary, smithLibrary, scoutLibrary, acolyteLibrary, onlyAcolyte, smithFLibrary, scoutFLibrary, acolyteFLibrary, onlyAcolyteF, introLibrary, skeletonArcher, skeletonPriest, skeletonWarrior, zombie;

    public PlayerItemLibrary enemyItemLibrary;
    Vector3[] startPos;
    [SerializeField]
    Transform[] playerSpawnPos;
    [SerializeField]
    GameObject playerPos2;
    Vector3 pos2, Companionposs, Companionpos1;
    float speed = 1f, companionPos = 3.12f, barPosy = 141, lerpSpeed = 1f, t;
    [SerializeField]
    public Camera mainCamera;
    [SerializeField]
    public GameObject deathPoint, targetPoint, bar, missAttack, specialAttackPanel, attackPanel, bandagePanel;
    [HideInInspector]
    public PlayerItem attacker, animPlayer, TargetObject;
    [HideInInspector]
    public PlayerItem character;
    [HideInInspector]
    public List<PlayerItem> enemys = new List<PlayerItem>(), companionList = new List<PlayerItem>(), companionAi = new List<PlayerItem>(), playersInBattle = new List<PlayerItem>(), myTeam = new List<PlayerItem>(), deadEnemy = new List<PlayerItem>(), tempList = new List<PlayerItem>(), removeList = new List<PlayerItem>();
    [HideInInspector]
    public int playerIndex = 0, BattleHealth, enemyCount, animCount, OtherIndex;
    [HideInInspector]
    public bool isChoose = false, enableButtons, isRemove, isHeal, isDefence, isLightning, iscritical, isStealth, isdeadEye;
    public GameObject GameOver, healAssets;
    float posy, barPos;
    [HideInInspector]
    public Vector3 bossNewPos, effectOriginalPos, pos, moveB;
    [HideInInspector]
    public string attackerName;
    public int count = 0, deathCount = 0, waveCount = 0, localXp, enemyWaveCount;
    float total, health, result;
    [SerializeField]
    Button Defence, Heal, Attack;
    GameObject healObject;
    DatabaseManager db;
    Vector3 pos1;
    [SerializeField]
    AudioSource bg, otherAudio, otherAudio2;
    [SerializeField]
    AudioClip[] bgClip;
    public List<GameObject> test = new List<GameObject>();
    [SerializeField]
    Text battleText, goldText;
    bool johnDefence, protagnistDefence, mariumDefence, tuckerDefence, attackDone;
    [SerializeField]
    PlayableDirector playble;
    [SerializeField]
    GameObject arrow;
    public int barghestPetrifyCount = 4;
    public int tempcount = 0;
    void Start()
    {
        //Debug.Log("is promotion::" + Globals.afterPromotion + "  is lightning::" + Globals.isLightening);
        db = FindObjectOfType<DatabaseManager>();
        Globals.battleManager = this;
        Globals.isBattle = true;
        Globals.thirdWave = false;
        goldText.text = Globals.shopMerchant.Gold.ToString();
        BackgroundMusic();
        StartBattle();

    }

    void BackgroundMusic()
    {
        bg.GetComponent<AudioSource>().Play();
    }
    void StartBattle()
    {
        Debug.Log("gere. start battle .........");
        if (!Globals.isSmith && !Globals.isArcher && !Globals.isAcolyte && !Globals.isSmithF && !Globals.isAcolyteF && !Globals.isArcherF)
            SpawnPlayer();
        else
            IntroBattle();
    }
    public void ClickEffect()
    {
        foreach (var v in enemys)
        {
            v.GetComponent<EntityGroup>().selectionEffect.SetActive(false);
            v.GetComponent<BoxCollider2D>().enabled = true;
        }
        ButtonSetting();
        CancelInvoke("TimeBar");
        CancelInvoke("CheckingForTimer");
        StopAllCoroutines();
    }
    public void GiveAttack(string name)
    {
        if (name == "P")
            MoveForward();
        else if (name == "C")
            MoveForward();
        FadePlayersEffect();
    }
    void FadePlayersEffect()
    {
        foreach (var v in myTeam)
        {
            v.GetComponent<EntityGroup>().selectionEffect.SetActive(false);
        }
        foreach (var e in enemys)
        {
            e.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    void ProtagnistTeamSetting(bool choice)
    {
        foreach (var v in myTeam)
        {
            v.GetComponent<BoxCollider2D>().enabled = choice;
        }
    }
    void SpawnPlayer()
    {
        character = Instantiate(Globals.activePlayer.GetComponent<PlayerItem>(), playerSpawnPos[Random.Range(0, playerSpawnPos.Length)].position, Quaternion.identity);
        character.InitializePlayerItem(playerItemLibrary.PlayerCharacterLibrary[0]);
        character.tag = "Player";
        character.GetComponent<BoxCollider2D>().enabled = false;
        character.GetComponent<EntityGroup>().originalPos = character.transform.localPosition;
        character.GetComponent<EntityGroup>().prespFaceL.GetComponent<MeshRenderer>().sortingOrder = -2;
        character.GetComponent<EntityGroup>().selectionEffect.GetComponent<SpriteRenderer>().sortingOrder = -3;
        character.transform.localScale = new Vector3(0.7f, 0.7f, 0);
        character.GetComponent<EntityGroup>().controlPanel.SetActive(false);
        character.GetComponent<EntityGroup>().noAttack = 0;
        character.GetComponent<EntityGroup>().lighteningBolt = 0;
        character.GetComponent<EntityGroup>().bandageProperty = 0;
        Globals.ActiveFaces(character.gameObject, false, false, true, false);
        playersInBattle.Add(character);
        myTeam.Add(character);
        Globals.isShop = true;
        battleText.text = "Battle No -" + (waveCount + 1);
        SpawnCompanion();
    }
    void GenerateOneEnemy()
    {
        PositionSet();
        UpdateFaces(character, false, false, false, true);
        foreach (PlayerConfiguration item in soldierLib.PlayerCharacterLibrary)
        {

            PlayerItem enemy;
            pos1 = new Vector3((pos1.x + 2), character.transform.position.y, 0);
            enemy = Instantiate(item.playerItemPrefab, pos1, Quaternion.identity);
            enemy.transform.localScale = new Vector3(0.7f, 0.7f, 0);
            BossEnemy(item, enemy, 20, 30, 2, 1, 0);
            enemy.GetComponent<EntityGroup>().originalPos = enemy.transform.localPosition;
            enemy.InitializePlayerItem(item);
        }
        tempList.AddRange(playersInBattle);
        StartCoroutine(SwitchTurn());
    }
    void SpawnCompanion()
    {
        if (Globals.activeScene == Globals.CurrentScene.SoldierCampsite && Globals.soldierCampsiteVisit == 0)
        {
            companionLibrary = companionWithoutMarium;
            Companionpos1 = new Vector3(character.transform.position.x + 1.5f, character.transform.position.y - 2f, 0);
        }
        else
        {
            if (Globals.activeScene == Globals.CurrentScene.MotteAndBaileyCastle || Globals.activeScene == Globals.CurrentScene.BarghestVillage || Globals.activeScene == Globals.CurrentScene.BarghestLairDengeon || Globals.activeScene == Globals.CurrentScene.TheDeathWeight || Globals.activeScene == Globals.CurrentScene.TheDeathWeightDengeon || Globals.activeScene == Globals.CurrentScene.TheBrigand || Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon || Globals.activeScene == Globals.CurrentScene.HuntingtonVillage || Globals.activeScene == Globals.CurrentScene.HuntingtonThroneRoom || Globals.activeScene == Globals.CurrentScene.HuntingtonCastle || Globals.activeScene == Globals.CurrentScene.HuntingtonCastleCourtyard || Globals.activeScene == Globals.CurrentScene.CastleEscapeTunnel || (Globals.activeScene == Globals.CurrentScene.RandomAttack && Globals.atWaterCount >= 5))
            {
                Debug.Log(".....................................");
                Companionposs = new Vector3(character.transform.localPosition.x + 2f, character.transform.localPosition.y + 2f, 0);//2.62
                Companionpos1 = new Vector3(character.transform.position.x + 2f, character.transform.position.y - 1.5f, 0);//2.62
                pos2 = new Vector3(character.transform.position.x, character.transform.position.y - 3.8f, 0);//3.8
                companionLibrary = companionItemLibrary;
            }
            else
            {
                Debug.Log("?????????????????");
                Companionposs = new Vector3(character.transform.localPosition.x + 2f, character.transform.localPosition.y + 2f, 0);//2.65  2.62
                Companionpos1 = new Vector3(character.transform.position.x + 2f, character.transform.position.y - 1.5f, 0);//2.62  //1.9
                companionLibrary = companionWithoutTucker;
            }
        }
        foreach (PlayerConfiguration item in companionLibrary.PlayerCharacterLibrary)
        {
            PlayerItem companion;
            companion = Instantiate(item.playerItemPrefab, Companionposs, Quaternion.identity);
            companion.InitializePlayerItem(item);
            companion.GetComponent<EntityGroup>().originalPos = companion.transform.localPosition;
            UpdateFaces(companion, false, false, false, true);
            companion.transform.localScale = new Vector3(0.7f, 0.7f, 0);
            companion.GetComponent<BoxCollider2D>().enabled = false;
            companion.GetComponent<EntityGroup>().noAttack = 0;
            companion.GetComponent<EntityGroup>().lighteningBolt = 0;
            companion.GetComponent<EntityGroup>().bandageProperty = 0;
            companionList.Add(companion);
            foreach (var v in companionList)
            {
                if (v.name == "Tucker(Clone)")
                    v.GetComponent<EntityGroup>().prespFaceL.GetComponent<MeshRenderer>().sortingOrder = -1;
            }
            myTeam.Add(companion);
            playersInBattle.Add(companion);
            PositionForCompanion();
        }
        GenerateEnemies(Globals.activeScene);
    }

    void ButtonActive()
    {
        if (tempList[OtherIndex].GetComponent<EntityGroup>().noAttack == 0)
        {
            Debug.Log("no attack true");
            Attack.interactable = true;
            Attack.GetComponent<Animator>().enabled = true;
        }

        if (tempList[OtherIndex].tag == "Player")
        {
            if (Globals.inventoryProtagnist.WoodenBuckler >= 1 || Globals.inventoryProtagnist.WoodenSmallRounded >= 1 || Globals.inventoryProtagnist.WoodenMediumShield >= 1 || Globals.inventoryProtagnist.MetalBuckler >= 1 || Globals.inventoryProtagnist.MetalSmallRounded >= 1 || Globals.inventoryProtagnist.MetalMediumShield >= 1 || Globals.inventoryProtagnist.MetalKiteShield >= 1 || Globals.inventoryProtagnist.PaddedArmour >= 1 || Globals.inventoryProtagnist.LeatherArmour >= 1 || Globals.inventoryProtagnist.ScaleArmour >= 1 || Globals.inventoryProtagnist.HideArmour >= 1 || Globals.inventoryProtagnist.BandedMailArmour >= 1 || Globals.inventoryProtagnist.BreastPlateArmour >= 1 || Globals.inventoryProtagnist.BrigadineArmor >= 1 || Globals.inventoryProtagnist.ChainArmour >= 1 || Globals.inventoryProtagnist.LeatherCap >= 1 || Globals.inventoryProtagnist.NesalHelmet >= 1 || Globals.inventoryProtagnist.KettleHat >= 1 || Globals.inventoryProtagnist.Aventail >= 1 || Globals.inventoryProtagnist.MailCoif >= 1)
            {
                Defence.interactable = true;
                Defence.GetComponent<Animator>().enabled = true;
            }
            else
                Defence.interactable = false;
        }
        else
        {
            if (tempList[OtherIndex].name == "JohnCompanion(Clone)")
            {
                if (Globals.inventoryJohn.WoodenBuckler >= 1 || Globals.inventoryJohn.WoodenSmallRound >= 1 || Globals.inventoryJohn.WoodenMedium >= 1 || Globals.inventoryJohn.metalBuckler >= 1 || Globals.inventoryJohn.metalSmallRound >= 1 || Globals.inventoryJohn.metalMedium >= 1 || Globals.inventoryJohn.PaddedArmour >= 1 || Globals.inventoryJohn.LeatherArmour >= 1 || Globals.inventoryJohn.ScaleArmour >= 1 || Globals.inventoryJohn.HideArmour >= 1 || Globals.inventoryJohn.BrigadineArmour >= 1 || Globals.inventoryJohn.ChainArmour >= 1 || Globals.inventoryJohn.LeatherCap >= 1 || Globals.inventoryJohn.NasalHelmet >= 1 || Globals.inventoryJohn.KettleHat >= 1 || Globals.inventoryJohn.Avaintail >= 1 || Globals.inventoryJohn.MailCoif >= 1)
                {
                    Defence.interactable = true;
                    Defence.GetComponent<Animator>().enabled = true;
                }
                else
                    Defence.interactable = false;

            }
            else if (tempList[OtherIndex].name == "Marium(Clone)")
            {
                if (Globals.inventoryMarium.WoodenBuckler >= 1 || Globals.inventoryMarium.woodenSmall >= 1 || Globals.inventoryMarium.MetalBuckler >= 1 || Globals.inventoryMarium.MetalSmall >= 1 || Globals.inventoryMarium.PaddedArmour >= 1 || Globals.inventoryMarium.LeatherArmour >= 1 || Globals.inventoryMarium.HideArmour >= 1 || Globals.inventoryMarium.BrigadineArmour >= 1 || Globals.inventoryMarium.LeatherCap >= 1 || Globals.inventoryMarium.NasalHelmet >= 1 || Globals.inventoryMarium.KettleHat >= 1)
                {
                    Defence.interactable = true;
                    Defence.GetComponent<Animator>().enabled = true;
                }
                else
                    Defence.interactable = false;
            }
            else if (tempList[OtherIndex].name == "Tucker(Clone)")
            {
                if (Globals.inventoryTucker.WoodenBuckler >= 1 || Globals.inventoryTucker.WoodenSmall >= 1 || Globals.inventoryTucker.MetalBuckler >= 1 || Globals.inventoryTucker.MetalSmall >= 1 || Globals.inventoryTucker.MetalMedium >= 1 || Globals.inventoryTucker.PaddedArmour >= 1 || Globals.inventoryTucker.LeatherArmour >= 1 || Globals.inventoryTucker.HideArmour >= 1 || Globals.inventoryTucker.LeatherCap >= 1 || Globals.inventoryTucker.KettleHat >= 1)
                {
                    Defence.interactable = true;
                    Defence.GetComponent<Animator>().enabled = true;
                }
                else
                    Defence.interactable = false;
            }
        }
        if (tempList[OtherIndex].GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount <= 0.7f)
        {
            if (Globals.shopMerchant.Ale >= 1 || Globals.shopMerchant.HealPotion >= 1 || Globals.shopMerchant.Meat >= 1 || Globals.shopMerchant.Food >= 1 || Globals.shopMerchant.Rum >= 1 || Globals.shopMerchant.CurePotion >= 1)
            {
                Heal.interactable = true;
                Heal.GetComponent<Animator>().enabled = true;
            }
            else
                Heal.interactable = false;
        }

    }
    void PositionForCompanion()
    {
        for (int i = 0; i < companionList.Count; i++)
        {
            if (Globals.activeScene == Globals.CurrentScene.MotteAndBaileyCastle || Globals.activeScene == Globals.CurrentScene.BarghestVillage || Globals.activeScene == Globals.CurrentScene.BarghestLairDengeon || Globals.activeScene == Globals.CurrentScene.TheDeathWeight || Globals.activeScene == Globals.CurrentScene.TheDeathWeightDengeon || Globals.activeScene == Globals.CurrentScene.TheBrigand || Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon || Globals.activeScene == Globals.CurrentScene.HuntingtonVillage || Globals.activeScene == Globals.CurrentScene.HuntingtonCastle || Globals.activeScene == Globals.CurrentScene.HuntingtonCastleCourtyard || Globals.activeScene == Globals.CurrentScene.HuntingtonThroneRoom || Globals.activeScene == Globals.CurrentScene.CastleEscapeTunnel || (Globals.activeScene == Globals.CurrentScene.RandomAttack && Globals.atWaterCount >= 5))
            {
                if (i == 0)
                {
                    companionList[i].transform.localPosition = pos2;
                    companionList[i].GetComponent<EntityGroup>().originalPos = companionList[i].transform.localPosition;
                }
                else if (i == 2)
                {
                    companionList[i].transform.localPosition = Companionpos1;
                    companionList[i].GetComponent<EntityGroup>().originalPos = companionList[i].transform.localPosition;
                }
            }
            else
            {
                if (i % 2 == 0)
                {
                    companionList[i].transform.localPosition = Companionpos1;
                    companionList[i].GetComponent<EntityGroup>().originalPos = companionList[i].transform.localPosition;
                }
            }
        }
    }
    void BossEnemy(PlayerConfiguration item, PlayerItem p, int h, int d, int l, int a, int ad)
    {
        enemys.Add(p);
        playersInBattle.Add(p);
        p.health = h;
        p.defence = d;
        p.level = l;
        p.attack = a;
        p.armorDefence = ad;
        p.name = p.name + count;
        p.GetComponent<BoxCollider2D>().enabled = false;
        count++;
    }
    void PositionSet()
    {
        if (enemyWaveCount == 1)
            pos1 = new Vector3(character.transform.localPosition.x - 8.3f, character.transform.localPosition.y + 1.8f, 0); //2.7//0.27
        else if (enemyWaveCount == 2)
            pos1 = new Vector3(character.transform.localPosition.x - 5.3f, character.transform.localPosition.y - 0.8f, 0);
        else if (enemyWaveCount == 3)
            pos1 = new Vector3(character.transform.localPosition.x - 8.3f, character.transform.position.y - 2.8f, 0);
    }
    void IntroBattle()
    {

        if (Globals.isSmith)
            activeLibrary = smithLibrary;
        else if (Globals.isArcher)
            activeLibrary = scoutLibrary;
        else if (Globals.isAcolyte)
        {
            if (Globals.isFirstTut)
                activeLibrary = acolyteLibrary;
            else
                activeLibrary = onlyAcolyte;
        }
        else if (Globals.isSmithF)
            activeLibrary = smithFLibrary;
        else if (Globals.isArcherF)
            activeLibrary = scoutFLibrary;
        else if (Globals.isAcolyteF)
        {
            if (Globals.isFirstTut)
                activeLibrary = acolyteFLibrary;
            else
                activeLibrary = onlyAcolyteF;
        }
        foreach (PlayerConfiguration item in activeLibrary.PlayerCharacterLibrary)
        {
            PlayerItem player;
            player = Instantiate(item.playerItemPrefab, playerSpawnPos[Random.Range(0, playerSpawnPos.Length)].position, Quaternion.identity);
            player.transform.localScale = new Vector3(0.7f, 0.7f, 0);
            player.InitializePlayerItem(item);
            player.tag = "Player";
            player.GetComponent<BoxCollider2D>().enabled = false;
            UpdateFaces(player, false, false, false, true);
            player.GetComponent<EntityGroup>().controlPanel.SetActive(false);
            player.GetComponent<EntityGroup>().originalPos = player.transform.localPosition;
            player.GetComponent<EntityGroup>().lighteningBolt = 0;
            myTeam.Add(player);
            playersInBattle.Add(player);
            character = player;
            Globals.isShop = true;
            battleText.text = "Battle No -" + (waveCount + 1);
        }
        if (Globals.isAcolyte || Globals.isAcolyteF)
        {
            if (Globals.isFirstTut)
                GeneratePriest();
            else
                GenerateEnemies(Globals.activeScene);
        }
        else
            GenerateEnemies(Globals.activeScene);
    }
    void GeneratePriest()
    {
        foreach (PlayerConfiguration item in priestLibrary.PlayerCharacterLibrary)
        {
            PlayerItem priest;
            Companionpos1 = new Vector3(character.transform.position.x + 1.5f, character.transform.position.y - 2f, 0);
            priest = Instantiate(item.playerItemPrefab, Companionpos1, Quaternion.identity);
            priest.transform.localScale = new Vector3(2f, 2f, 0);
            priest.GetComponent<EntityGroup>().originalPos = priest.transform.localPosition;
            Globals.ActiveFaces(priest.gameObject, false, false, true, false);
            myTeam.Add(priest);
            playersInBattle.Add(priest);
            priest.InitializePlayerItem(item);
        }
        GenerateEnemies(Globals.activeScene);
    }
    void SetLayer(PlayerItem e)
    {
        if (enemyWaveCount == 1)
            e.GetComponent<EntityGroup>().prespFaceR.GetComponent<MeshRenderer>().sortingOrder = -2;
        else if (enemyWaveCount == 2)
            e.GetComponent<EntityGroup>().prespFaceR.GetComponent<MeshRenderer>().sortingOrder = -1;
        else
            e.GetComponent<EntityGroup>().prespFaceR.GetComponent<MeshRenderer>().sortingOrder = 0;
    }
    void GenerateEnemies(Globals.CurrentScene scene)
    {
        Debug.Log("scene::" + scene);
        switch (scene)
        {
            case Globals.CurrentScene.SoldierCampsite:
                if (Globals.soldierCampsiteVisit == 0)
                {
                    foreach (PlayerConfiguration item in soldierCampsiteLibrary.PlayerCharacterLibrary)//soldier campsite
                    {
                        enemyWaveCount++;
                        PlayerItem enemy;
                        PositionSet();
                        enemy = Instantiate(item.playerItemPrefab, pos1, Quaternion.identity);
                        enemy.transform.localScale = new Vector3(0.7f, 0.7f, 0);
                        SetLayer(enemy);
                        enemy.GetComponent<EntityGroup>().originalPos = pos1;
                        if (waveCount == 0)
                            BossEnemy(item, enemy, 80, 10, 1, 3, 5); // h:12 a:3
                        else
                            BossEnemy(item, enemy, 60, 10, 1, 3, 5); // h:12 a:3
                        enemy.InitializePlayerItem(item);
                    }
                }
                else
                {
                    foreach (PlayerConfiguration item in secondSoldierLibrary.PlayerCharacterLibrary)
                    {
                        enemyWaveCount++;
                        PlayerItem enemy;
                        PositionSet();
                        enemy = Instantiate(item.playerItemPrefab, pos1, Quaternion.identity);
                        enemy.transform.localScale = new Vector3(0.7f, 0.7f, 0);
                        enemy.GetComponent<EntityGroup>().originalPos = pos1;
                        SetLayer(enemy);
                        BossEnemy(item, enemy, 100, 25, 3, 5, 10); //h:30 a:15
                        enemy.InitializePlayerItem(item);
                    }
                }
                break;
            case Globals.CurrentScene.WagonCaravan:
                if (waveCount == 0)
                    activeLibrary = wagonCaravanLibrary;
                else if (waveCount == 1)
                    activeLibrary = soldierCampsiteLibrary;
                foreach (PlayerConfiguration item in activeLibrary.PlayerCharacterLibrary)
                {
                    enemyWaveCount++;
                    PlayerItem enemy;
                    PositionSet();
                    enemy = Instantiate(item.playerItemPrefab, pos1, Quaternion.identity);
                    if (waveCount == 0)
                        BossEnemy(item, enemy, 70, 30, 1, 3, 10); //h:20 a:15
                    else
                        BossEnemy(item, enemy, 70, 20, 1, 3, 5); //h:10 a:15
                    enemy.transform.localScale = new Vector3(0.7f, 0.7f, 0);
                    SetLayer(enemy);
                    enemy.GetComponent<EntityGroup>().originalPos = pos1;
                    enemy.InitializePlayerItem(item);
                }
                break;
            case Globals.CurrentScene.Huntsville:

                foreach (PlayerConfiguration item in introLibrary.PlayerCharacterLibrary)
                {
                    enemyWaveCount++;
                    PlayerItem enemy;
                    PositionSet();
                    enemy = Instantiate(item.playerItemPrefab, pos1, Quaternion.identity);
                    BossEnemy(item, enemy, 1, 2, 1, 1, 0);
                    enemy.transform.localScale = new Vector3(0.7f, 0.7f, 0);
                    SetLayer(enemy);
                    enemy.GetComponent<EntityGroup>().originalPos = pos1;
                    enemy.InitializePlayerItem(item);
                }
                break;
            case Globals.CurrentScene.Tutorial:
                foreach (PlayerConfiguration item in tutorialLibrary.PlayerCharacterLibrary)
                {
                    enemyWaveCount++;
                    PlayerItem enemy;
                    PositionSet();
                    enemy = Instantiate(item.playerItemPrefab, pos1, Quaternion.identity);
                    BossEnemy(item, enemy, 1, 2, 1, 1, 0);
                    enemy.transform.localScale = new Vector3(0.7f, 0.7f, 0);
                    SetLayer(enemy);
                    enemy.GetComponent<EntityGroup>().originalPos = pos1;
                    enemy.InitializePlayerItem(item);
                }
                break;
            case Globals.CurrentScene.AtwaterVillage:
                foreach (PlayerConfiguration item in atwaterLibrary.PlayerCharacterLibrary)
                {
                    enemyWaveCount++;
                    PlayerItem enemy;
                    PositionSet();
                    enemy = Instantiate(item.playerItemPrefab, pos1, Quaternion.identity);
                    enemy.transform.localScale = new Vector3(0.7f, 0.7f, 0);
                    SetLayer(enemy);
                    enemy.GetComponent<EntityGroup>().originalPos = pos1;
                    if (waveCount == 0)
                        BossEnemy(item, enemy, 200, 35, 4, 10, 30);  //h:40 a:30
                    else
                        BossEnemy(item, enemy, 200, 35, 4, 10, 20);  //h:45 a:35
                    enemy.InitializePlayerItem(item);
                }
                break;
            case Globals.CurrentScene.RandomAttack:
                if (Globals.carvan1 || Globals.caravan2 || Globals.caravan3)
                {
                    if (Globals.atWaterCount < 6)
                    {
                        if (waveCount == 0)
                            activeLibrary = wagonCaravanLibrary;
                        else if (waveCount == 1)
                            activeLibrary = soldierCampsiteLibrary;
                    }
                    else
                    {
                        if (waveCount == 0)
                            activeLibrary = soldierCampsiteLibrary;
                        else
                            activeLibrary = conscriptSolLib;
                    }
                }
                else if (Globals.petrol1 || Globals.petrol2 || Globals.petrol3)
                {
                    if (Globals.atWaterCount < 6)
                    {
                        if (Globals.petrol1 || Globals.petrol2)
                        {
                            if (waveCount == 0)
                                activeLibrary = wagonCaravanLibrary;
                            else if (waveCount == 1)
                                activeLibrary = soldierCampsiteLibrary;
                            else if (waveCount == 2)
                                activeLibrary = randomLibrary;
                        }
                        else if (Globals.petrol3)
                        {
                            if (waveCount == 0)
                                activeLibrary = scoutEnemyLib;
                            else if (waveCount == 1)
                                activeLibrary = secondSoldierLibrary;
                            else
                                activeLibrary = randomPetrolLib;
                        }
                    }
                    else
                    {
                        if (waveCount == 0)
                            activeLibrary = atwaterLibrary;
                        else if (waveCount == 1)
                            activeLibrary = secondSoldierLibrary;
                    }
                }
                foreach (PlayerConfiguration item in activeLibrary.PlayerCharacterLibrary)
                {
                    enemyWaveCount++;
                    PlayerItem enemy;
                    PositionSet();
                    enemy = Instantiate(item.playerItemPrefab, pos1, Quaternion.identity);
                    BossEnemy(item, enemy, 120, 80, 3, 15, 40); //h:40 a: 25
                    enemy.transform.localScale = new Vector3(0.7f, 0.7f, 0);
                    SetLayer(enemy);
                    enemy.GetComponent<EntityGroup>().originalPos = pos1;
                    enemy.InitializePlayerItem(item);
                }
                break;
            case Globals.CurrentScene.monastery:
            case Globals.CurrentScene.CellarInt:
                foreach (PlayerConfiguration item in soldierLib.PlayerCharacterLibrary)
                {
                    enemyWaveCount++;
                    PlayerItem enemy;
                    PositionSet();
                    enemy = Instantiate(item.playerItemPrefab, pos1, Quaternion.identity);
                    enemy.transform.localScale = new Vector3(0.7f, 0.7f, 0);
                    SetLayer(enemy);
                    enemy.GetComponent<EntityGroup>().originalPos = pos1;
                    BossEnemy(item, enemy, 120, 110, 4, 15, 50); //h:110 a:80 l:7 ad:70
                    enemy.InitializePlayerItem(item);
                }
                break;
            case Globals.CurrentScene.CellarTucker:
                if (waveCount == 0 || waveCount == 1)
                    activeLibrary = soldierLib;
                else
                    activeLibrary = monestryTuckerLibrary;
                foreach (PlayerConfiguration item in activeLibrary.PlayerCharacterLibrary)
                {
                    enemyWaveCount++;
                    PlayerItem enemy;
                    PositionSet();
                    enemy = Instantiate(item.playerItemPrefab, pos1, Quaternion.identity);
                    enemy.transform.localScale = new Vector3(0.7f, 0.7f, 0);
                    SetLayer(enemy);
                    enemy.GetComponent<EntityGroup>().originalPos = pos1;
                    if (waveCount == 0 || waveCount == 1)
                        BossEnemy(item, enemy, 95, 110, 6, 30, 50); //a:80
                    else
                        BossEnemy(item, enemy, 200, 120, 6, 30, 65); // h:100 a:85
                    enemy.InitializePlayerItem(item);
                }
                break;
            case Globals.CurrentScene.MotteAndBaileyCastle:
                Debug.Log("second::" + Globals.secondFight + " third ::" + Globals.thirdFight + " forth::" + Globals.forthFight + "  before mottye::" + Globals.beforeMottey);
                if (Globals.secondFight || Globals.thirdFight)
                {
                    if (Globals.secondFight)
                    {
                        if (waveCount == 0 || waveCount == 1)
                            activeLibrary = motteyAndBailey2;
                        else
                            activeLibrary = motteyAndBailey4;
                    }
                    else
                        activeLibrary = motteyAndBailey2;
                    foreach (PlayerConfiguration item in activeLibrary.PlayerCharacterLibrary)
                    {
                        enemyWaveCount++;
                        PlayerItem enemy;
                        PositionSet();
                        enemy = Instantiate(item.playerItemPrefab, pos1, Quaternion.identity);
                        BossEnemy(item, enemy, 300, 120, 7, 60, 100);  // h: 230 a:100
                        enemy.transform.localScale = new Vector3(0.7f, 0.7f, 0);
                        SetLayer(enemy);
                        enemy.GetComponent<EntityGroup>().originalPos = pos1;
                        enemy.InitializePlayerItem(item);
                    }
                }
                else
                {
                    if (!Globals.isPart1Battle || Globals.beforeMottey)
                    {
                        if (waveCount == 0 || waveCount == 1)
                            activeLibrary = atwaterLibrary;
                        else if (waveCount == 2 || waveCount == 3)
                            activeLibrary = guardLib;
                    }
                    else if (Globals.forthFight)
                    {
                        if (waveCount == 0 || waveCount == 1)
                            activeLibrary = guardLib;
                        else if (waveCount == 2)
                            activeLibrary = motteyAndBailey3;

                    }

                    foreach (PlayerConfiguration item in activeLibrary.PlayerCharacterLibrary)
                    {
                        enemyWaveCount++;
                        PlayerItem enemy;
                        PositionSet();
                        enemy = Instantiate(item.playerItemPrefab, pos1, Quaternion.identity);
                        if (!Globals.isPart1Battle || Globals.beforeMottey)
                        {
                            if (waveCount == 0 || waveCount == 1)
                                BossEnemy(item, enemy, 300, 110, 5, 70, 70); //h:180 a:85
                            else
                                BossEnemy(item, enemy, 300, 140, 5, 70, 100); //h:200 a:90
                        }
                        else if (Globals.forthFight)
                        {
                            if (item.playerItemPrefab.name == "LordAlfredEnemy")
                            {
                                Debug.Log("lord alfred...............");
                                BossEnemy(item, enemy, 500, 170, 9, 115, 110);  //h:360
                            }
                            else if (waveCount == 0 || waveCount == 1 || waveCount == 2)
                            {
                                Debug.Log("lord alfred nott................");
                                BossEnemy(item, enemy, 300, 190, 9, 90, 100);//190,150,8
                            }
                            else
                            {
                                Debug.Log("lord alfred nott................");
                                BossEnemy(item, enemy, 380, 280, 11, 160, 130);//320,200,10
                            }
                        }
                        enemy.transform.localScale = new Vector3(0.7f, 0.7f, 0);
                        enemy.GetComponent<EntityGroup>().originalPos = pos1;
                        SetLayer(enemy);
                        enemy.InitializePlayerItem(item);
                    }
                }
                break;
            case Globals.CurrentScene.BarghestVillage:
            case Globals.CurrentScene.BarghestLairDengeon:
                CriticalRenew();
                if (Globals.isHound)
                    activeLibrary = houndLibrary;
                else if (Globals.isWolf)
                    activeLibrary = wolfLibrary;
                else
                {
                    if (waveCount == 0)
                        activeLibrary = houndLibrary;
                    else if (waveCount == 1)
                        activeLibrary = wolfLibrary;
                    else
                        activeLibrary = barghestLibrary;
                }
                foreach (PlayerConfiguration item in activeLibrary.PlayerCharacterLibrary)
                {
                    enemyWaveCount++;
                    PlayerItem enemy;
                    PositionSet();
                    enemy = Instantiate(item.playerItemPrefab, pos1, Quaternion.identity);
                    if (Globals.isHound)
                        BossEnemy(item, enemy, 370, 300, 8, 100, 95); // h:220 d:150
                    else if (Globals.isWolf)
                        BossEnemy(item, enemy, 370, 300, 8, 100, 95); // h:220 d:150
                    else
                    {
                        if (waveCount == 0)
                            BossEnemy(item, enemy, 400, 200, 8, 95, 95); // h:220 d:150
                        else if (waveCount == 1)
                            BossEnemy(item, enemy, 400, 200, 8, 95, 95); // h:220 d:150
                        else
                        {
                            if (item.playerItemPrefab.name == "Barghest(Black Hound)")
                            {
                                Debug.Log("in generate enemy barghest......");
                                BossEnemy(item, enemy, 900, 350, 10, 90, 130);//320
                            }
                            else
                            {
                                Debug.Log("in generate enemy");
                                BossEnemy(item, enemy, 400, 200, 10, 50, 100);//320
                            }
                        }
                    }

                    enemy.transform.localScale = new Vector3(0.7f, 0.7f, 1);  // z = 0
                    enemy.GetComponent<EntityGroup>().originalPos = pos1;
                    SetLayer(enemy);
                    enemy.InitializePlayerItem(item);
                }
                break;
            case Globals.CurrentScene.TheDeathWeightDengeon:
            case Globals.CurrentScene.TheDeathWeight:
                CriticalRenew();
                if (Globals.isWolf)
                    activeLibrary = skeletonPriest;
                else if (Globals.isHound)
                    activeLibrary = skeletonArcher;
                else if (Globals.isskelton)
                    activeLibrary = skeletonWarrior;
                else if (Globals.iszombie || Globals.isBarghest)
                    activeLibrary = zombie;
                else
                    activeLibrary = deathWightLibrary;
                foreach (PlayerConfiguration item in activeLibrary.PlayerCharacterLibrary)
                {
                    enemyWaveCount++;
                    PlayerItem enemy;
                    PositionSet();
                    enemy = Instantiate(item.playerItemPrefab, pos1, Quaternion.identity);
                    if (Globals.isWolf)
                    {
                        Debug.Log("wolf");
                        BossEnemy(item, enemy, 280, 220, 10, 125, 115);
                    }
                    else if (Globals.isHound || Globals.isskelton)
                    {
                        Debug.Log("hond skeleton");
                        BossEnemy(item, enemy, 300, 220, 10, 125, 115);
                    }
                    else if (Globals.iszombie)
                    {
                        Debug.Log("zombie");
                        BossEnemy(item, enemy, 330, 200, 9, 125, 135);
                    }
                    else
                    {
                        Debug.Log("generate death wight..........");
                        BossEnemy(item, enemy, 330, 125, 9, 115, 215); //h:330
                    }

                    enemy.transform.localScale = new Vector3(0.7f, 0.7f, 1);
                    enemy.GetComponent<EntityGroup>().originalPos = pos1;
                    SetLayer(enemy);
                    enemy.InitializePlayerItem(item);
                }
                break;
            case Globals.CurrentScene.TheBrigand:
                CriticalRenew();
                if (waveCount == 0 || waveCount == 1)
                    activeLibrary = brigandSolLibrary;
                else if (waveCount == 2)
                    activeLibrary = brigandArcherLibrary;
                foreach (PlayerConfiguration item in activeLibrary.PlayerCharacterLibrary)
                {
                    enemyWaveCount++;
                    PlayerItem enemy;
                    PositionSet();
                    Debug.Log("here brigand:: " + waveCount);
                    enemy = Instantiate(item.playerItemPrefab, pos1, Quaternion.identity);
                    if (waveCount == 0 || waveCount == 1)
                        BossEnemy(item, enemy, 400, 190, 10, 115, 105); //h: 215 d:200
                    else if (waveCount == 2)
                        BossEnemy(item, enemy, 400, 190, 10, 115, 105); //h:215 d:200
                    enemy.transform.localScale = new Vector3(0.7f, 0.7f, 0);
                    enemy.GetComponent<EntityGroup>().originalPos = pos1;
                    SetLayer(enemy);
                    enemy.InitializePlayerItem(item);
                }
                break;
            case Globals.CurrentScene.BrigandLairDengeon:
                CriticalRenew();
                if (Globals.brigandCount == 1)
                {
                    if (Globals.InnVisit == 1)
                    {
                        randomsLibrary.Add(wolfLibrary);
                        randomsLibrary.Add(brigandSolLibrary);
                        randomsLibrary.Add(pantherLibrary);
                        activeLibrary = randomsLibrary[Random.Range(0, randomsLibrary.Count)];
                        Globals.lastActiveLib = activeLibrary;
                    }
                    else
                    {
                        randomsLibrary.Add(wolfLibrary);
                        randomsLibrary.Add(brigandSolLibrary);
                        randomsLibrary.Add(pantherLibrary);
                        randomsLibrary.Remove(Globals.lastActiveLib);
                        activeLibrary = randomsLibrary[Random.Range(0, randomsLibrary.Count)];
                    }
                }
                else
                {
                    if (!Globals.isPart1Battle)
                        activeLibrary = brigandSolLibrary;
                    else
                    {
                        if (Globals.isVeteran)
                            activeLibrary = brigandLibrary;
                        else if (Globals.isArcherBrigand)
                            activeLibrary = completeBrigandArcher;
                        else
                        {
                            if (Globals.secondFight)
                                activeLibrary = brigandSolLibrary;
                            else if (Globals.thirdFight)
                            {
                                if (waveCount == 0)
                                    activeLibrary = completeBrigandArcher;
                                else if (waveCount == 1)
                                    activeLibrary = brigandLibrary;
                                else
                                    activeLibrary = brigandLierLibrary;
                            }
                        }
                    }
                }
                foreach (PlayerConfiguration item in activeLibrary.PlayerCharacterLibrary)
                {
                    Debug.Log("brigand count :: " + Globals.brigandCount + " is part1 battle " + Globals.isPart1Battle + " is veteran " + Globals.isVeteran + " second fight : " + Globals.secondFight);
                    enemyWaveCount++;
                    PlayerItem enemy;
                    PositionSet();
                    enemy = Instantiate(item.playerItemPrefab, pos1, Quaternion.identity);
                    if (Globals.brigandCount == 1)
                        BossEnemy(item, enemy, 415, 250, 11, 115, 105); // h: 205 d:250
                    else
                    {
                        if (!Globals.isPart1Battle)
                            BossEnemy(item, enemy, 400, 300, 11, 115, 105); // h:205 d:200
                        else
                        {
                            if (Globals.isVeteran)
                                BossEnemy(item, enemy, 450, 210, 11, 115, 105); // h: 255
                            else if (Globals.isArcherBrigand)
                                BossEnemy(item, enemy, 450, 210, 11, 115, 105); // h: 255
                            else
                            {
                                if (Globals.secondFight)
                                    BossEnemy(item, enemy, 450, 280, 11, 115, 105); //h:215 d:250
                                else if (Globals.thirdFight)
                                {
                                    if (waveCount == 0)
                                        BossEnemy(item, enemy, 450, 300, 11, 115, 105);//325,14  h: 215 d:220
                                    else if (waveCount == 1)
                                        BossEnemy(item, enemy, 450, 300, 11, 105, 105); //h:215 d:220
                                    else
                                    {
                                        if (item.playerItemPrefab.name == "BrigandLeader")
                                        {
                                            Debug.Log("in generate enemy brigand......");
                                            BossEnemy(item, enemy, 1500, 235, 12, 90, 115);//320
                                        }
                                        else
                                            BossEnemy(item, enemy, 900, 235, 12, 100, 115);//405
                                    }
                                }
                            }
                        }
                    }
                    enemy.transform.localScale = new Vector3(0.7f, 0.7f, 0);
                    enemy.GetComponent<EntityGroup>().originalPos = pos1;
                    SetLayer(enemy);
                    enemy.InitializePlayerItem(item);
                }
                break;
            case Globals.CurrentScene.HuntingtonVillage:
                CriticalRenew();
                foreach (PlayerConfiguration item in huntingtonLibrary.PlayerCharacterLibrary)
                {
                    Debug.Log("here......00000000000");
                    enemyWaveCount++;
                    PlayerItem enemy;
                    PositionSet();
                    enemy = Instantiate(item.playerItemPrefab, pos1, Quaternion.identity);
                    BossEnemy(item, enemy, 405, 340, 15, 120, 150);  //h:405 d :345
                    enemy.transform.localScale = new Vector3(0.7f, 0.7f, 0);
                    enemy.GetComponent<EntityGroup>().originalPos = pos1;
                    SetLayer(enemy);
                    enemy.InitializePlayerItem(item);
                }
                break;
            case Globals.CurrentScene.CastleEscapeTunnel:
                CriticalRenew();
                foreach (PlayerConfiguration item in motteyAndBailey2.PlayerCharacterLibrary)
                {
                    Debug.Log("here......111111111111");
                    enemyWaveCount++;
                    PlayerItem enemy;
                    PositionSet();
                    enemy = Instantiate(item.playerItemPrefab, pos1, Quaternion.identity);
                    BossEnemy(item, enemy, 350, 345, 14, 120, 195); // h:350  d:340
                    enemy.transform.localScale = new Vector3(0.7f, 0.7f, 0);
                    enemy.GetComponent<EntityGroup>().originalPos = pos1;
                    SetLayer(enemy);
                    enemy.InitializePlayerItem(item);
                }
                break;
            case Globals.CurrentScene.HuntingtonCastle:
                CriticalRenew();
                if (Globals.secondFight)
                {
                    Debug.Log("here......222222");
                    activeLibrary = huntingtonCastle1Library;
                }

                else if (Globals.thirdFight)
                {
                    Debug.Log("here......3333333333" + Globals.thirdFight + " wave count : " + waveCount);
                    if (waveCount == 0)
                        activeLibrary = guardLib;
                    else
                        activeLibrary = throneEntrance;

                }
                else
                {
                    Debug.Log("here......else");
                    activeLibrary = motteyAndBailey2;
                }

                foreach (PlayerConfiguration item in activeLibrary.PlayerCharacterLibrary)
                {
                    enemyWaveCount++;
                    PlayerItem enemy;
                    PositionSet();
                    enemy = Instantiate(item.playerItemPrefab, pos1, Quaternion.identity);
                    if (Globals.secondFight)
                    {
                        if (item.playerItemPrefab.name == "AbbotChesterEnemy")
                        {
                            Debug.Log("abott chester enemy...............");
                            BossEnemy(item, enemy, 1500, 350, 15, 135, 180);//350,270   h: 950 d:180 a: 200
                        }
                        else
                            BossEnemy(item, enemy, 1000, 350, 14, 135, 180);//350,270   h: 950 d:180 a: 200
                    }
                    else
                        BossEnemy(item, enemy, 500, 300, 14, 105, 155); //h:320  a:355 d: 290
                    enemy.transform.localScale = new Vector3(0.7f, 0.7f, 1);
                    enemy.GetComponent<EntityGroup>().originalPos = pos1;
                    SetLayer(enemy);
                    // BossEnemy(item, enemy, 60, 140, 6, 155);
                    enemy.InitializePlayerItem(item);
                }
                break;
            case Globals.CurrentScene.HuntingtonThroneRoom:
                CriticalRenew();
                if (Globals.secondFight)
                {
                    if (waveCount == 0)
                        activeLibrary = bodyGuardLibrary;
                    else
                        activeLibrary = huntingtonFinalThrone;
                }
                else
                {
                    if (waveCount == 0 || waveCount == 1)
                        activeLibrary = bodyGuardLibrary;
                    else if (waveCount == 2)
                        activeLibrary = eliteAndCaptainLib;
                    else
                        activeLibrary = huntingtonThroneLibrary;
                }
                foreach (PlayerConfiguration item in activeLibrary.PlayerCharacterLibrary)
                {
                    enemyWaveCount++;
                    PlayerItem enemy;
                    PositionSet();
                    enemy = Instantiate(item.playerItemPrefab, pos1, Quaternion.identity);
                    if (Globals.secondFight)
                    {
                        if (waveCount == 0)
                            BossEnemy(item, enemy, 410, 250, 14, 150, 135); //h:310 d:180
                        else if (item.playerItemPrefab.name == "LordEdwardReeve Variant")
                        {
                            Debug.Log("lord edward variant...............");
                            BossEnemy(item, enemy, 1800, 400, 15, 135, 175);
                        }
                        else
                            BossEnemy(item, enemy, 610, 200, 14, 125, 135); //h:460 d:250
                    }
                    else
                    {
                        if (waveCount == 0 || waveCount == 1)
                            BossEnemy(item, enemy, 600, 180, 14, 150, 135); //310
                        else if (waveCount == 2)
                            BossEnemy(item, enemy, 610, 180, 14, 150, 135);
                        else
                            BossEnemy(item, enemy, 1400, 300, 14, 150, 155); // h:380 d: 230 // lord edward  //1000
                    }
                    enemy.transform.localScale = new Vector3(0.7f, 0.7f, 1);
                    enemy.GetComponent<EntityGroup>().originalPos = pos1;
                    SetLayer(enemy);
                    // BossEnemy(item, enemy, 60, 140, 6, 155);
                    enemy.InitializePlayerItem(item);
                }
                break;
        }
        AutoMove();
    }
    void CriticalRenew()
    {
        foreach (var v in myTeam)
        {
            v.GetComponent<EntityGroup>().criticalStrike = 0;
        }
    }
    void UpdateFaces(PlayerItem _player, bool front, bool back, bool right, bool left)
    {
        _player.GetComponent<EntityGroup>().backFace.SetActive(back);
        _player.GetComponent<EntityGroup>().frontFaces.SetActive(front);
        _player.GetComponent<EntityGroup>().prespFaceL.SetActive(left);
        _player.GetComponent<EntityGroup>().prespFaceR.SetActive(right);
    }

    void AutoMove()
    {
        AfterUpdateArmor();
        foreach (var v in playersInBattle)
        {
            v.GetComponent<EntityGroup>().namePopup.SetActive(true);
        }
        tempList.Clear();
        tempList.AddRange(playersInBattle);
        StartCoroutine(SwitchTurn());
    }
    void AfterUpdateArmor()
    {
        EntityGroup entity = character.GetComponent<EntityGroup>();
        entity.backFace.SetActive(false);
        entity.frontFaces.SetActive(false);
        entity.prespFaceR.SetActive(false);
    }
    int brigandCount = 0;
    bool brigandSetting;
    IEnumerator SwitchTurn()
    {
        Debug.Log("switch turn......");
        healAssets.SetActive(false);
        if (Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon && Globals.thirdFight && waveCount == 2)
            Debug.Log("count::" + tempList.Count + " enemy count::" + enemys.Count);
        attackDone = false;
        missAttack.SetActive(false);
        if (Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon && Globals.thirdFight && waveCount == 2)
            Debug.Log("count::" + tempList.Count);
        if (tempList.Count == 0)
            tempList.AddRange(playersInBattle);
        if (tempList.Count == 1)
        {
            OtherIndex = 0;
            if (Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon && Globals.thirdFight)
            {
                if (waveCount == 2 && !rochesterDead)
                {
                    Debug.Log("here for rochester dialogue come" + enemys.Count);
                    brigandCount++;
                    BrigandSetting();
                }
            }
        }
        else
        {
            OtherIndex = Random.Range(0, tempList.Count);

            if (Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon && Globals.thirdFight)
            {
                Debug.Log("other index::" + OtherIndex + "  enemy count::" + enemys.Count + "brigand count::" + brigandCount);
                if (waveCount == 2 && enemys.Count == 1 && !rochesterDead && !brigandSetting)
                {
                    Debug.Log("inside this");
                    brigandSetting = true;
                    EnablesSetting();
                }
            }
        }
        if (tempList[OtherIndex].name == "BrigandLeader(Clone)7" && !brigandOn && !brigandSetting)// && brigandCount>1)
        {
            Debug.Log("turn switch");
            StartCoroutine(SwitchTurn());
        }
        if (!isChoose)
            yield return new WaitForSeconds(1.9f);
        else
            yield return new WaitForSeconds(2.3f);
        MoveForward();
    }
    PlayerItem rochester;
    bool brigandOn = true;
    void BrigandSetting()
    {

        if (enemys.Count == 1 && !rochesterDead)
        {
            Debug.Log("if");
            EnablesSetting();
        }
        else
        {
            Debug.Log("else::" + brigandCount);
            if (!rochesterDead)
            {
                if (brigandCount % 2 == 0)
                    EnablesSetting();
                else
                {
                    //   if (enemyCount > 1)
                    {
                        Debug.Log("brigand gayab krdo");
                        brigandOn = false;
                        EnableBrigandLeader(false);
                        playble.playableAsset = Resources.Load("Playables/Battle Scene/Rochester_Leaving_Dialog") as TimelineAsset;
                        playble.Play();
                    }
                }
            }
        }
    }
    void EnablesSetting()
    {
        if (!brigandOn)
        {
            Debug.Log("enable setting");
            brigandOn = true;
            EnableBrigandLeader(true);
            foreach (var v in enemys)
            {
                if (v.name == "BrigandLeader(Clone)7")
                {
                    v.gameObject.GetComponent<EntityGroup>().HealthBar = 1;
                    v.gameObject.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount = 1;
                }

            }
            playble.playableAsset = Resources.Load("Playables/Battle Scene/Rochester_Return_Dialog") as TimelineAsset;
            playble.Play();
        }
    }
    void EnableBrigandLeader(bool bri)
    {
        foreach (var v in enemys)
        {
            if (v.name == "BrigandLeader(Clone)7")
            {
                Debug.Log("inside this");
                v.gameObject.SetActive(bri);
            }

        }

    }
    void MyTeamSetUp()
    {
        Debug.Log("team setup");
        if (tempList[OtherIndex].tag == "Player" || tempList[OtherIndex].tag == "Companion")
        {

            MyTeamSetting(true);
            foreach (var v in myTeam)
            {
                v.GetComponent<BoxCollider2D>().enabled = false;
            }
            tempList[OtherIndex].GetComponent<EntityGroup>().selectionEffect.SetActive(true);
            ButtonActive();
        }
        else
        {
            MyTeamSetting(false);
            ButtonSetting();
        }
    }

    void MyTeamSetting(bool b)
    {
        foreach (var v in myTeam)
        {
            v.GetComponent<BoxCollider2D>().enabled = b;
        }

    }
    void GiveDamage()
    {
        if (!isChoose)
        {
            if ((tempList[OtherIndex].tag == "Player" || tempList[OtherIndex].tag == "Companion"))//&& enemys.Count != 0)
            {
                if (Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon && Globals.thirdFight)
                    Debug.Log("templist name::" + tempList[OtherIndex].name);

                DamageForPlayer();
            }
            else if (tempList[OtherIndex].tag == "Enemy")// && myTeam.Count != 0)
                DamageForBoss();
        }
        else
            DamageForPlayer();
    }
    void DamageForPlayer()
    {
        Debug.Log("..................................." + brigandOn + "enemy count " + enemyCount);
        if (!isChoose)
        {
            if (Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon && Globals.thirdFight)
            {
                Debug.Log("count::" + enemys.Count);
                foreach (var v in enemys)
                {
                    Debug.Log("enemy name::" + v.name);
                }
            }
            TargetObject = enemys[Random.Range(0, enemys.Count)];
            //if (Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon && Globals.thirdFight)
            //    Debug.Log("target object" + TargetObject.name);

            //    BrigandLeaderCondn();
        }
        else
            TargetObject = enemys[enemyCount];

        if (TargetObject.name == "BrigandLeader(Clone)7" && !brigandOn)
        {
            Debug.Log("rochester gayab.......... again other player");
            DamageForPlayer();
        }
        if ((animPlayer.name == "Marium(Clone)" && Globals.inventoryMarium.WeaponAttack == "shortBow") || (animPlayer.name == "Marium(Clone)" && Globals.inventoryMarium.WeaponAttack == "longBow") || ((animPlayer.name == "ArcherMale(Clone)" || animPlayer.name == "ArcherFemale(Clone)" || animPlayer.name == "WarriorMale(Clone)" || animPlayer.name == "WarriorFemale(Clone)" || animPlayer.name == "PriestMale(Clone)" || animPlayer.name == "PriestFemale(Clone)") && (Globals.inventoryProtagnist.AttackWeapon == "shortBow" || Globals.inventoryProtagnist.AttackWeapon == "longBow")) || (animPlayer.name == "JohnCompanion(Clone)" && (Globals.inventoryJohn.WeaponAttack == "shortBow" || Globals.inventoryJohn.WeaponAttack == "longBow")))
        {
            Debug.Log("here for bow attack arrow animation");
            StartCoroutine(DestroyArrow());
            foreach (var v in playersInBattle)
            {
                v.GetComponent<EntityGroup>().namePopup.SetActive(false);
            }
            return;
            // iTween.moveTo(arrow, { "time": 2, "x":-25.38265, "z":16.37207, "y":20, "oncomplete":"LookAfterMoving", "transition":"easeInOutSine"});
        }
        Debug.Log("here after ............battle ");
        foreach (var v in playersInBattle)
        {
            v.GetComponent<EntityGroup>().namePopup.SetActive(false);
        }
        StartCoroutine(ShowHealthBar(tempList[OtherIndex]));
    }
    IEnumerator DestroyArrow()
    {
        yield return new WaitForSeconds(2f);  //1.8f
        Debug.Log("........" + TargetObject.transform.localPosition);

        GameObject arrow = Instantiate(animPlayer.GetComponent<EntityGroup>().arrow, animPlayer.GetComponent<EntityGroup>().parent.transform);
        Vector3 toPos = new Vector3(arrow.transform.localPosition.x - 320f, arrow.transform.localPosition.y, 0);
        iTween.MoveTo(arrow, iTween.Hash("position", toPos, "time", 0.2f, "islocal", true, "easeType", iTween.EaseType.linear, "looptype", iTween.LoopType.none));
        // iTween.MoveTo(arrow, TargetObject.GetComponent<EntityGroup>().lootImage.GetComponent<RectTransform>().localPosition, 0.5f);
        // yield return new WaitForSeconds(0.1f);
        //  iTween.ValueTo(arrow, iTween.Hash( "from", 1f, "to", 0f,"time", 0.4f, "easetype", "linear"));
        StartCoroutine(ShowHealthBar(tempList[OtherIndex]));
        yield return new WaitForSeconds(0.2f);

        Invoke("RemoveEffect", 0.2f);
        Destroy(arrow);


    }
    void BrigandLeaderCondn()
    {
        if (TargetObject.name == "BrigandLeader(Clone)7" && brigandCount % 2 != 0)
        {
            Debug.Log("inside boss condition");
            DamageForPlayer();
        }
    }
    void DamageForBoss()
    {
        TargetObject = myTeam[Random.Range(0, myTeam.Count)];
        //  Debug.Log("target object type::" + TargetObject.Type);
        StartCoroutine(ShowHealthBar(tempList[OtherIndex]));
    }
    IEnumerator ShowHealthBar(PlayerItem activePlayer)
    {
        Debug.Log("target object::" + TargetObject.name + " bool::" + Globals.isBarghest);
        EntityGroup entity = TargetObject.GetComponent<EntityGroup>();
        TargetObject.GetComponent<EntityGroup>().namePopup.SetActive(true);
        AttackPlayer(activePlayer, TargetObject);
        yield return new WaitForSeconds(0.2f);
        PlayerItem playerItem = TargetObject.GetComponent<PlayerItem>();
        Debug.Log("player item result :: " + playerItem.result);
        result = playerItem.result;
        bool dealthWightMiss = false;
        if (Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon && Globals.thirdFight && waveCount == 2)
        {
            if (!brigandOn && !rochesterDead && enemyCount == 1)
                EnablesSetting();
        }

        if (TargetObject.name == "LordEdwardReeve Variant(Clone)4" && TargetObject.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount < 0.5) //player.name== "LordEdwardReeve Variant(Clone)4"
        {
            if (Globals.isLightening && isLightning && activePlayer.tag == "Player" && (Globals.inventoryProtagnist.AttackWeapon == "MagicSword" || Globals.inventoryProtagnist.AttackWeapon == "Magic Bow") && TargetObject.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount < 0.2)
            {
                Debug.Log("lord edward reanimated ::  die with lightining bolt ");
                entity.bar.GetComponent<Image>().fillAmount = 0;
                activePlayer.GetComponent<EntityGroup>().lighteningBolt = 1;
                isLightning = false;
            }
            else if (TargetObject.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount < 0.2)
            {
                Debug.Log("no damage ");
                result = 0f;
            }
            else
            {
                result = 0.1f;
                Debug.Log("result variant 0.1f ");
            }
        }
        if ((TargetObject.name == "DeathWeight(Clone)3" && Globals.isBarghest))// || TargetObject.name == "LordEdwardEnemy(Clone)10" || TargetObject.name== "LordEdwardReeve Variant(Clone)5")
        {

            if (TargetObject.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount >= 0.5)
            {
                Debug.Log("if 22");
                result = 0.1f;
                entity.bar.GetComponent<Image>().fillAmount -= 0.1f;
                //Debug.Log("if");
                //if (Globals.isLightening && activePlayer.tag == "Player" && (Globals.inventoryProtagnist.AttackWeapon == "MagicSword" || Globals.inventoryProtagnist.AttackWeapon == "Magic Bow"))
                //{
                //    Debug.Log("if 11");
                //    entity.bar.GetComponent<Image>().fillAmount = 0;
                //    activePlayer.GetComponent<EntityGroup>().lighteningBolt = 1;
                //    isLightning = false;
                //}
                //else
                //{
                //    Debug.Log("if 22");
                //    result = 0.1f;
                //}
            }
            else if (TargetObject.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount < 0.5f)
            {
                Debug.Log("else");
                if (animPlayer.tag == "Player" || animPlayer.tag == "Companion")
                {
                    activePlayer.GetComponent<EntityGroup>().lighteningBoltDeathWight = 1;
                }
                if (Globals.isLightening && isLightning && activePlayer.tag == "Player" && (Globals.inventoryProtagnist.AttackWeapon == "MagicSword" || Globals.inventoryProtagnist.AttackWeapon == "Magic Bow"))
                {
                    Debug.Log("Death Wight ::  die with lightining bolt ");
                    entity.bar.GetComponent<Image>().fillAmount = 0;
                    activePlayer.GetComponent<EntityGroup>().lighteningBolt = 1;
                    isLightning = false;
                }
                else if (TargetObject.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount <= 0.2)
                {
                    // entity.bar.GetComponent<Image>().fillAmount = 
                    missAttack.gameObject.SetActive(true);
                    dealthWightMiss = true;

                }
                else
                {
                    entity.bar.GetComponent<Image>().fillAmount -= 0.1f;
                    // missAttack.gameObject.SetActive(true);
                }
            }

        }
        if (iscritical)
        {
            if (TargetObject.name == "DeathWeight(Clone)3" && Globals.isBarghest)
                missAttack.SetActive(true);
            else
                entity.bar.GetComponent<Image>().fillAmount -= result * 2;
            activePlayer.GetComponent<EntityGroup>().criticalStrike = 1;
            iscritical = false;
        }
        else if (dealthWightMiss)
        {
            Debug.Log("no damage.............");
        }
        else if (isdeadEye || isStealth)
        {
            if (TargetObject.name == "DeathWeight(Clone)3" && Globals.isBarghest)
                missAttack.SetActive(true);
            else
                entity.bar.GetComponent<Image>().fillAmount -= result * 2;
            activePlayer.GetComponent<EntityGroup>().deadEye = 1;
            isStealth = false;
            isdeadEye = false;
        }
        else if (entity.name == "HuntsvillePriest(Clone)")
            entity.bar.GetComponent<Image>().fillAmount -= 1f;
        else if ((activePlayer.name == "Hunter(Clone)0" || activePlayer.name == "Hunter(Clone)1" || activePlayer.name == "Hunter(Clone)2" || activePlayer.name == "Hunter(Clone)3" || activePlayer.name == "Hunter(Clone)4" || activePlayer.name == "Hunter(Clone)5" || /*activePlayer.name == "ArcherMale(Clone)" || activePlayer.name == "ArcherFemale(Clone)" || activePlayer.name == "Marium(Clone)" ||*/ activePlayer.name == "Skeleton1(Clone)0" || activePlayer.name == "Skeleton1(Clone)1" || activePlayer.name == "Skeleton1(Clone)2" || activePlayer.name == "Skeleton1(Clone)3" || activePlayer.name == "Skeleton1(Clone)4" || activePlayer.name == "Skeleton1(Clone)5"))
            StartCoroutine(HealthDamageDelay());
        else
        {
            //  Debug.Log("target name::" + TargetObject.name + "  bool::" + Globals.isBarghest);
            if (TargetObject.name == "DeathWeight(Clone)3" && Globals.isBarghest)
            {
                //if (entity.bar.GetComponent<Image>().fillAmount >= 0.65f)
                //{
                //    entity.bar.GetComponent<Image>().fillAmount -= 0.1f;
                //    Debug.Log("fill amount after::" + entity.bar.GetComponent<Image>().fillAmount);
                //}
                //else
                //    missAttack.SetActive(true);
            }
            else
            {
                if (activePlayer.name == "AbbotChesterEnemy(Clone)1" && activePlayer.GetComponent<EntityGroup>().alchemy == 1)
                {
                    activePlayer.GetComponent<EntityGroup>().alchemy = 0;
                    foreach (PlayerItem item in myTeam)
                    {
                        Debug.Log("item :: " + item.name + " fill amount " + item.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount);
                        item.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount -= Mathf.Abs(result);
                        Debug.Log("item ::  after" + item.name + " fill amount " + item.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount);
                    }
                }
                else if (activePlayer.name == "LordEdwardEnemy(Clone)10" && activePlayer.GetComponent<EntityGroup>().swordSlash == 1)
                {
                    activePlayer.GetComponent<EntityGroup>().swordSlash = 0;
                    foreach (PlayerItem item in myTeam)
                    {
                        Debug.Log("item :: " + item.name + " fill amount " + item.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount);
                        item.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount -= Mathf.Abs(result);
                        Debug.Log("item ::  after" + item.name + " fill amount " + item.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount);
                    }
                }
                else if (activePlayer.name == "DeathWeight(Clone)3" && Globals.healthDrainAttack && Globals.isBarghest)
                {
                    Debug.Log("here........... death wight ..........");
                    Debug.Log("inside death weight :: " + entity.bar.GetComponent<Image>().fillAmount);
                    if (entity.bar.GetComponent<Image>().fillAmount <= 0.25f)
                    {
                        Debug.Log("here.............");
                        entity.bar.GetComponent<Image>().fillAmount = 0f;
                    }
                    else
                    {
                        Debug.Log("here.............no here");
                        entity.bar.GetComponent<Image>().fillAmount = 0.2f;
                    }
                    Globals.healthDrainAttack = false;
                }
                //else if (activePlayer.name == "LordEdwardReeve Variant(Clone)4" && activePlayer.GetComponent<EntityGroup>().darkMagicReanimated)
                //{
                //    activePlayer.GetComponent<EntityGroup>().darkMagicReanimated = true;
                //    foreach (PlayerItem item in myTeam)
                //    {
                //        Debug.Log("item :: " + item.name + " fill amount " + item.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount);
                //        item.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount -= Mathf.Abs(result);
                //        Debug.Log("item ::  after" + item.name + " fill amount " + item.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount);
                //    }
                //}
                else
                {
                    Debug.Log("item :: else" + entity.name);
                    entity.bar.GetComponent<Image>().fillAmount -= Mathf.Abs(result);
                }

                if (TargetObject.name == "BrigandLeader(Clone)7")
                {
                    if (entity.bar.GetComponent<Image>().fillAmount <= 0)
                        rochesterDead = true;
                }
            }
        }
        //if(activePlayer.name == "ArcherMale(Clone)" || activePlayer.name == "ArcherFemale(Clone)" || activePlayer.name == "Marium(Clone)")
        //{
        //    yield return new WaitForSeconds()
        //}
        DamageEffect(); ;
    }
    bool rochesterDead;
    IEnumerator HealthDamageDelay()
    {
        yield return new WaitForSeconds(2.5f); //2.2 1
        Debug.Log("player health reduce after delay");
        TargetObject.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount -= result;
    }
    void AttackPlayer(PlayerItem fromAttack, PlayerItem toAttack)
    {
        toAttack.TakeDamage(fromAttack, toAttack);
    }
    void DamageCaluclate()
    {
        isChoose = false;
        if (playerIndex >= playersInBattle.Count)
            playerIndex -= 1;
        if (OtherIndex >= tempList.Count)
            OtherIndex -= 1;
        tempList.Remove(tempList[OtherIndex]);
        if (myTeam.Count == 0 || enemys.Count == 0)
            StopCoroutine(SwitchTurn());
        else
            StartCoroutine(SwitchTurn());
    }
    float randomChance;
    string lootImages;
    List<string> lootItemList = new List<string>();
    IEnumerator ForPart1()
    {

        Debug.Log("active scene::" + Globals.activeScene + "wave count :: " + waveCount);
        if (enemys.Count == 0)
        {
            yield return new WaitForSeconds(0);
            if (waveCount == 0)
            {
                {
                    randomChance = 0.75f;
                    if (Globals.activeScene == Globals.CurrentScene.BarghestVillage)
                    {
                        foreach (var v in deadEnemy)
                        {
                            if (v.name == "DireWolf(Clone)0" || v.name == "DireWolf(Clone)1" || v.name == "DireWolf(Clone)2" || v.name == "Hound(Clone)0" || v.name == "Hound(Clone)1" || v.name == "Hound(Clone)2" || v.name == "Panther(Clone)0" || v.name == "Panther(Clone)1" || v.name == "Panther(Clone)2")
                                lootImages = "LootImages/Food";
                            else if (v.name == "Barghest(Black Hound)(Clone)0" || v.name == "Barghest(Black Hound)(Clone)1" || v.name == "Barghest(Black Hound)(Clone)2")
                            {
                                randomChance = 1f;
                                lootImages = "LootImages/Food";
                            }
                            else
                            {
                                lootImages = "LootImages/Gold Coin";
                            }
                            lootItemList.Add(lootImages);
                        }

                    }
                    else if (Globals.activeScene == Globals.CurrentScene.Huntsville || Globals.activeScene == Globals.CurrentScene.Tutorial)
                    {
                        randomChance = 0;
                        lootImages = "LootImages/Food";
                        lootItemList.AddRange(new string[] { lootImages, lootImages, lootImages });
                    }
                    else if (Globals.activeScene == Globals.CurrentScene.TheDeathWeight || Globals.activeScene == Globals.CurrentScene.TheDeathWeightDengeon)
                    {
                        foreach (var v in deadEnemy)
                        {
                            if (v.name == "Skeleton2(Clone)0" || v.name == "Skeleton2(Clone)1" || v.name == "Skeleton2(Clone)2" || v.name == "Skeleton1(Clone)0" || v.name == "Skeleton1(Clone)1" || v.name == "Skeleton1(Clone)2" || v.name == "Skeleton0(Clone)0" || v.name == "Skeleton0(Clone)1" || v.name == "Skeleton0(Clone)2")
                            {
                                randomChance = 0.85f;
                                lootImages = "LootImages/Bones";
                                Globals.shopMerchant.Bones += 1;
                                db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                            }
                            else if (v.name == "Skeleton3(Clone)0" || v.name == "Skeleton3(Clone)1" || v.name == "Skeleton3(Clone)2")
                            {
                                randomChance = 0.85f;
                                lootImages = "LootImages/Rotten Flesh";
                                Globals.shopMerchant.RottenFlesh += 1;
                                db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);

                            }
                            else if (v.name == "DeathWeight(Clone)0" || v.name == "DeathWeight(Clone)1" || v.name == "DeathWeight(Clone)2 || DeathWeight(Clone)3") // added 
                            {

                            }

                            else
                                lootImages = "LootImages/Gold Coin";
                            lootItemList.Add(lootImages);
                            //  else if (v.name == "Skeleton2(Clone)0" || v.name == "Skeleton2(Clone)1" || v.name == "Skeleton2(Clone)2" || v.name == "Skeleton1(Clone)0" || v.name == "Skeleton1(Clone)1" || v.name == "Skeleton1(Clone)2" || v.name == "Skeleton0(Clone)0" || v.name == "Skeleton0(Clone)1" || v.name == "Skeleton0(Clone)2")
                        }
                    }
                    else if (Globals.activeScene == Globals.CurrentScene.TheBrigand || Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon)
                    {
                        foreach (var v in deadEnemy)
                        {
                            Debug.Log("here brigand or brigand dungaoun");
                            if (v.name == "BrigandVeteran(Clone)0" || v.name == "BrigandVeteran(Clone)1" || v.name == "BrigandVeteran(Clone)2")
                            {
                                randomChance = 1;
                                lootImages = "LootImages/Gold Coin";
                            }
                            else if (v.name == "Panther(Clone)0" || v.name == "Panther(Clone)1" || v.name == "Panther(Clone)2" || v.name == "DireWolf(Clone)0" || v.name == "DireWolf(Clone)1" || v.name == "DireWolf(Clone)2")
                            {
                                randomChance = 0.75f;
                                lootImages = "LootImages/Food";
                                Debug.Log("here brigand or brigand dungaoun");
                            }
                            else
                                lootImages = "LootImages/Gold Coin";

                            lootItemList.Add(lootImages);
                        }
                    }
                    else if (Globals.activeScene == Globals.CurrentScene.HuntingtonCastle)
                    {
                        foreach (var v in deadEnemy)
                        {
                            if (v.name == "AbbotChesterEnemy(Clone)1")//  AbottChester(Clone)1
                            {
                                Debug.Log("abott chester.......enemy");
                                randomChance = 1;
                                lootImages = "LootImages/Mace-min";
                                Globals.shopMerchant.Mace += 1;
                                db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                            }
                            else
                                lootImages = "LootImages/Gold Coin";
                            lootItemList.Add(lootImages);
                        }
                    }
                    else
                    {
                        lootImages = "LootImages/Gold Coin";
                        lootItemList.AddRange(new string[] { lootImages, lootImages, lootImages });
                    }


                    RemoveDeadBodies();
                }
            }
            else if (waveCount == 1)
            {
                //if (Globals.activeScene == Globals.CurrentScene.Huntsville)
                //    StartCoroutine(BodiesRemoved());
                //else
                {
                    // randomChance = 0;
                    if (Globals.activeScene == Globals.CurrentScene.BarghestVillage)
                    {
                        foreach (var v in deadEnemy)
                        {
                            if (v.name == "DireWolf(Clone)0" || v.name == "DireWolf(Clone)1" || v.name == "DireWolf(Clone)2" || v.name == "Hound(Clone)0" || v.name == "Hound(Clone)1" || v.name == "Hound(Clone)2")
                                lootImages = "LootImages/Food";
                            else if (v.name == "Barghest(Black Hound)(Clone)0" || v.name == "Barghest(Black Hound)(Clone)1" || v.name == "Barghest(Black Hound)(Clone)2")
                            {
                                randomChance = 1f;
                                lootImages = "LootImages/Barghest Tooth";
                            }
                            else
                                lootImages = "LootImages/Long Sword";
                            lootItemList.Add(lootImages);
                        }

                    }
                    else if (Globals.activeScene == Globals.CurrentScene.TheDeathWeight || Globals.activeScene == Globals.CurrentScene.TheDeathWeightDengeon)
                    {
                        foreach (var v in deadEnemy)
                        {
                            if (v.name == "Skeleton2(Clone)0" || v.name == "Skeleton2(Clone)1" || v.name == "Skeleton2(Clone)2" || v.name == "Skeleton1(Clone)0" || v.name == "Skeleton1(Clone)1" || v.name == "Skeleton1(Clone)2" || v.name == "Skeleton0(Clone)0" || v.name == "Skeleton0(Clone)1" || v.name == "Skeleton0(Clone)2")
                            {
                                lootImages = "LootImages/Bones";
                            }

                            else if ((v.name == "DeathWeight(Clone)0" || v.name == "DeathWeight(Clone)1" || v.name == "DeathWeight(Clone)2" || v.name == "DeathWeight(Clone)3"))
                            {
                                Debug.Log("barghest value :: " + Globals.isBarghest);
                                Debug.Log("Helloooo :: " + Globals.deathWightNumber);
                                if (Globals.isBarghest)
                                {
                                    randomChance = 1f;
                                    lootImages = "LootImages/Death Wight-SoulGem";
                                    Globals.shopMerchant.SoulGem += 1;
                                }
                                //else if (Globals.deathWightNumber == 1) // charu
                                //{
                                //    lootImages = "LootImages/Deathwight Mace";
                                //    Globals.shopMerchant.Mace += 1;  // charu
                                //}

                                //else if (Globals.deathWightNumber == 2) // charu
                                //{
                                //    lootImages = "LootImages/Death Wight Cloak";
                                //    Globals.shopMerchant.DeathWightCloak += 1;  // charu
                                //}
                                else
                                {
                                    randomChance = 0;
                                }

                                db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                            }
                            else
                            {
                                if (v.name != "Skeleton3(Clone)0" && v.name != "Skeleton3(Clone)1" && v.name != "Skeleton3(Clone)2" && v.name != "DeathWeight(Clone)3")
                                    lootImages = "LootImages/Gold Coin";
                            }
                            lootItemList.Add(lootImages);
                            //  else if (v.name == "Skeleton2(Clone)0" || v.name == "Skeleton2(Clone)1" || v.name == "Skeleton2(Clone)2" || v.name == "Skeleton1(Clone)0" || v.name == "Skeleton1(Clone)1" || v.name == "Skeleton1(Clone)2" || v.name == "Skeleton0(Clone)0" || v.name == "Skeleton0(Clone)1" || v.name == "Skeleton0(Clone)2")
                        }
                    }
                    else if (Globals.activeScene == Globals.CurrentScene.TheBrigand || Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon)
                    {
                        foreach (var v in deadEnemy)
                        {
                            if (v.name == "BrigandVeteran(Clone)0" || v.name == "BrigandVeteran(Clone)1" || v.name == "BrigandVeteran(Clone)2")
                                randomChance = 0;
                            else
                                lootImages = "LootImages/Long Sword"; //Gold Coin
                            lootItemList.Add(lootImages);

                        }
                    }
                    else if (Globals.activeScene == Globals.CurrentScene.HuntingtonThroneRoom && Globals.secondFight)
                    {
                        foreach (var v in deadEnemy)
                        {
                            if (v.name == "LordEdwardReeve Variant(Clone)4") //  LordEdwardEnemy(Clone)4
                            {
                                randomChance = 1;
                                lootImages = "LootImages/Soul Eye Gem";
                                Globals.shopMerchant.SoulEyeGems += 2;
                                db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                            }
                            else
                                lootImages = "LootImages/Long Sword";
                            lootItemList.Add(lootImages);
                        }
                    }

                    else
                    {
                        lootImages = "LootImages/Long Sword";
                        lootItemList.AddRange(new string[] { lootImages, lootImages, lootImages });
                    }


                    RemoveDeadBodies();
                }
            }
            else if (waveCount == 2)
            {
                //if (Globals.activeScene == Globals.CurrentScene.Huntsville)
                //    StartCoroutine(BodiesRemoved());
                //else
                {
                    // randomChance = 0.1f;
                    if (Globals.activeScene == Globals.CurrentScene.BarghestVillage)
                    {
                        foreach (var v in deadEnemy)
                        {
                            if (v.name == "DireWolf(Clone)6" || v.name == "DireWolf(Clone)8" || v.name == "Hound(Clone)0" || v.name == "Hound(Clone)1" || v.name == "Hound(Clone)2")
                            {
                                randomChance = 0;
                            }
                            else if (v.name == "Barghest(Black Hound)(Clone)7")
                            {
                                randomChance = 1f;
                                lootImages = "LootImages/Barghest Heart";
                                Globals.shopMerchant.BarghestHeart = 1;
                                db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                            }
                            else
                                lootImages = "LootImages/Armor";
                            lootItemList.Add(lootImages);
                        }

                    }
                    else if (Globals.activeScene == Globals.CurrentScene.TheDeathWeight || Globals.activeScene == Globals.CurrentScene.TheDeathWeightDengeon)
                    {
                        foreach (var v in deadEnemy)
                        {
                            if (v.name == "Skeleton2(Clone)0" || v.name == "Skeleton2(Clone)1" || v.name == "Skeleton2(Clone)2" || v.name == "Skeleton1(Clone)0" || v.name == "Skeleton1(Clone)1" || v.name == "Skeleton1(Clone)2" || v.name == "Skeleton0(Clone)0" || v.name == "Skeleton0(Clone)1" || v.name == "Skeleton0(Clone)2")
                                lootImages = "LootImages/Bones";
                            else if (v.name == "DeathWeight(Clone)0" || v.name == "DeathWeight(Clone)1" || v.name == "DeathWeight(Clone)2" || v.name == "DeathWeight(Clone)3")
                            {
                                randomChance = 1;
                                lootImages = "LootImages/Soul Eye Gem";
                                Globals.shopMerchant.SoulGem = 1;
                                db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                            }
                            else
                                lootImages = "LootImages/Gold Coin";
                            lootItemList.Add(lootImages);
                            //  else if (v.name == "Skeleton2(Clone)0" || v.name == "Skeleton2(Clone)1" || v.name == "Skeleton2(Clone)2" || v.name == "Skeleton1(Clone)0" || v.name == "Skeleton1(Clone)1" || v.name == "Skeleton1(Clone)2" || v.name == "Skeleton0(Clone)0" || v.name == "Skeleton0(Clone)1" || v.name == "Skeleton0(Clone)2")
                        }
                    }
                    else if (Globals.activeScene == Globals.CurrentScene.TheBrigand || Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon)
                    {
                        foreach (var v in deadEnemy)
                        {
                            if (v.name == "BrigandVeteran(Clone)0" || v.name == "BrigandVeteran(Clone)1" || v.name == "BrigandVeteran(Clone)2")
                                lootImages = "LootImages/Gold Coin"; //randomChance = 0;
                            else if (v.name == "BrigandLeader(Clone)7")
                            {
                                randomChance = 1f;
                                lootImages = "LootImages/Gold Coin";
                                // Globals.shopMerchant.Gold += 250;
                                //  db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                            }
                            else
                            {
                                lootImages = "LootImages/Armor";
                                //randomChance = 1f;
                                //lootImages = "LootImages/Gold Coin"; 
                                //Globals.shopMerchant.Gold += 250;
                                //db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                            }
                            lootItemList.Add(lootImages);

                        }
                    }
                    else if (Globals.activeScene == Globals.CurrentScene.monasterywave)
                    {
                        Debug.Log("here.........");
                        foreach (var v in deadEnemy)
                        {
                            if (v.name == "Soldier(Clone)6" || v.name == "Soldier(Clone)7" || v.name == "Soldier(Clone)8")
                            {
                                lootImages = "LootImages/Shield";
                            }
                            lootItemList.Add(lootImages);
                        }

                    }
                    else if (Globals.activeScene == Globals.CurrentScene.MotteAndBaileyCastle)
                    {
                        Debug.Log("here......... motte and balle");
                        foreach (var v in deadEnemy)
                        {
                            randomChance = 1f;
                            if (v.name == "LordAlfredEnemy(Clone)7")
                            {
                                lootImages = "LootImages/Long Sword";
                                Globals.shopMerchant.LongSword += 1;
                                db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                            }
                            else
                            {
                                Debug.Log("without lord edward");
                                lootImages = "LootImages/Armor";
                            }

                            lootItemList.Add(lootImages);
                        }

                    }
                    else
                    {
                        randomChance = 0.1f;
                        //lootImages = "LootImages/Armor";
                        //lootItemList.AddRange(new string[] { lootImages, lootImages, lootImages });
                        foreach (var v in deadEnemy)
                        {
                            if (v.name == "Hunter(Clone)6" || v.name == "Hunter(Clone)7" || v.name == "Hunter(Clone)8" || v.name == "Soldier(Clone)6" || v.name == "Soldier(Clone)7" || v.name == "Soldier(Clone)8" || v.name == "BrigandSol(Clone)6" || v.name == "BrigandSol(Clone)7" || v.name == "BrigandSol(Clone)8" || v.name == "Captain(Clone)6" || v.name == "Captain(Clone)7" || v.name == "Captain(Clone)8")
                            {
                                lootImages = "LootImages/Shield";
                            }
                            else
                                lootImages = "LootImages/Armor";
                            lootItemList.Add(lootImages);
                        }
                    }
                    RemoveDeadBodies();
                }
            }
            else if (waveCount == 3)
            {
                if (Globals.activeScene == Globals.CurrentScene.HuntingtonThroneRoom)
                {
                    foreach (var v in deadEnemy)
                    {

                        if (v.name == "LordEdwardEnemy(Clone)10")
                        {
                            randomChance = 1;
                            lootImages = "LootImages/Long Sword";
                            Globals.shopMerchant.LongSword += 2;
                            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                        }
                        else
                        {
                            randomChance = 1;
                            if (v.name == "Hunter(Clone)6" || v.name == "Hunter(Clone)7" || v.name == "Hunter(Clone)8" || v.name == "Soldier(Clone)6" || v.name == "Soldier(Clone)7" || v.name == "Soldier(Clone)8" || v.name == "BrigandSol(Clone)6" || v.name == "BrigandSol(Clone)7" || v.name == "BrigandSol(Clone)8" || v.name == "Captain(Clone)6" || v.name == "Captain(Clone)7" || v.name == "Captain(Clone)8")
                            {
                                lootImages = "LootImages/Shield";
                            }
                            else
                                lootImages = "LootImages/Armor";
                        }
                        lootItemList.Add(lootImages);
                    }

                }
                else
                {
                    randomChance = 0.1f;
                    foreach (var v in deadEnemy)
                    {
                        if (v.name == "Hunter(Clone)6" || v.name == "Hunter(Clone)7" || v.name == "Hunter(Clone)8" || v.name == "Soldier(Clone)6" || v.name == "Soldier(Clone)7" || v.name == "Soldier(Clone)8" || v.name == "BrigandSol(Clone)6" || v.name == "BrigandSol(Clone)7" || v.name == "BrigandSol(Clone)8" || v.name == "Captain(Clone)6" || v.name == "Captain(Clone)7" || v.name == "Captain(Clone)8")
                        {
                            lootImages = "LootImages/Shield";
                        }
                        else
                            lootImages = "LootImages/Armor";
                        lootItemList.Add(lootImages);
                    }


                }
                //   lootItemList.AddRange(new string[] { lootImages, lootImages, lootImages });
                RemoveDeadBodies();
            }
            else if (waveCount == 4)
            {
                randomChance = 0.1f;
                //lootImages = "LootImages/Armor";
                //lootItemList.AddRange(new string[] { lootImages, lootImages, lootImages });
                foreach (var v in deadEnemy)
                {
                    if (v.name == "Hunter(Clone)6" || v.name == "Hunter(Clone)7" || v.name == "Hunter(Clone)8" || v.name == "Soldier(Clone)6" || v.name == "Soldier(Clone)7" || v.name == "Soldier(Clone)8" || v.name == "BrigandSol(Clone)6" || v.name == "BrigandSol(Clone)7" || v.name == "BrigandSol(Clone)8" || v.name == "Captain(Clone)6" || v.name == "Captain(Clone)7" || v.name == "Captain(Clone)8")
                    {
                        lootImages = "LootImages/Shield";
                    }
                    else
                        lootImages = "LootImages/Armor";
                    lootItemList.Add(lootImages);
                }
                RemoveDeadBodies();
            }
            else if (waveCount == 5)
            {
                randomChance = 0.1f;
                //lootImages = "LootImages/Armor";
                //lootItemList.AddRange(new string[] { lootImages, lootImages, lootImages });
                foreach (var v in deadEnemy)
                {
                    if (v.name == "Hunter(Clone)6" || v.name == "Hunter(Clone)7" || v.name == "Hunter(Clone)8" || v.name == "Soldier(Clone)6" || v.name == "Soldier(Clone)7" || v.name == "Soldier(Clone)8" || v.name == "BrigandSol(Clone)6" || v.name == "BrigandSol(Clone)7" || v.name == "BrigandSol(Clone)8" || v.name == "Captain(Clone)6" || v.name == "Captain(Clone)7" || v.name == "Captain(Clone)8")
                    {
                        lootImages = "LootImages/Shield";
                    }
                    else
                        lootImages = "LootImages/Armor";
                    lootItemList.Add(lootImages);
                }
                RemoveDeadBodies();
            }
            else if (waveCount == 6)
            {
                randomChance = 0.1f;
                //lootImages = "LootImages/Armor";
                //lootItemList.AddRange(new string[] { lootImages, lootImages, lootImages });
                foreach (var v in deadEnemy)
                {
                    if (v.name == "Hunter(Clone)6" || v.name == "Hunter(Clone)7" || v.name == "Hunter(Clone)8" || v.name == "Soldier(Clone)6" || v.name == "Soldier(Clone)7" || v.name == "Soldier(Clone)8" || v.name == "BrigandSol(Clone)6" || v.name == "BrigandSol(Clone)7" || v.name == "BrigandSol(Clone)8" || v.name == "Captain(Clone)6" || v.name == "Captain(Clone)7" || v.name == "Captain(Clone)8")
                    {
                        lootImages = "LootImages/Shield";
                    }
                    else
                        lootImages = "LootImages/Armor";
                    lootItemList.Add(lootImages);
                }
                RemoveDeadBodies();
            }
            waveCount++;

        }
    }
    void RemoveDeadBodies()
    {
        enemyWaveCount = 0;
        float rand = Random.value;
        if (rand <= randomChance)
        {
            Debug.Log("checking chance");
            StartCoroutine(LootAnim());
        }
        else if (Globals.activeScene == Globals.CurrentScene.BarghestVillage || Globals.activeScene == Globals.CurrentScene.TheDeathWeight || Globals.activeScene == Globals.CurrentScene.TheDeathWeightDengeon || Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon || Globals.activeScene == Globals.CurrentScene.HuntingtonCastle || Globals.activeScene == Globals.CurrentScene.HuntingtonThroneRoom)
        {
            Debug.Log("inside special condition");
            StartCoroutine(LootAnim());
        }
        else
        {
            Debug.Log("remove bodies");
            StartCoroutine(BodiesRemoved());
        }
    }
    int randomNumber;
    IEnumerator LootAnim()
    {
        int i = 0;
        Debug.Log("loot item list :: " + lootItemList.Count);
        foreach (var v in deadEnemy)
        {
            if (v.tag == "Enemy")
            {
                GameObject animImage = v.GetComponent<EntityGroup>().lootImage;
                if (Globals.activeScene == Globals.CurrentScene.BarghestVillage && Globals.isBarghest && waveCount == 3)
                {
                    if (v.name == "Barghest(Black Hound)(Clone)7")
                        animImage.SetActive(true);
                }
                else if (Globals.activeScene == Globals.CurrentScene.TheDeathWeight && !Globals.isBarghest && v.name == "DeathWeight(Clone)3")
                {
                    Debug.Log("is not brghest........");
                    animImage.SetActive(false);
                    StartCoroutine(BodiesRemoved());
                }
                else
                    animImage.SetActive(true);
                //animImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(lootImages);
                animImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(lootItemList[i]);
                yield return new WaitForSeconds(0.6f);
                Vector3 toPos = new Vector3(12.3f, animImage.transform.localPosition.y, 0);
                Debug.Log("count::" + waveCount);
                iTween.MoveTo(animImage, iTween.Hash("position", toPos, "time", 0.9f, "islocal", true, "easeType", iTween.EaseType.linear, "looptype", iTween.LoopType.none, "oncomplete", "DisableImage", "oncompletetarget", gameObject));
                if (waveCount == 1)
                {
                    if (v.name == "Conscript(Clone)0" || v.name == "Conscript(Clone)1" || v.name == "Conscript(Clone)2" || v.name == "Militia(Clone)0" || v.name == "Militia(Clone)1" || v.name == "Militia(Clone)0" || v.name == "Militia(Clone)1" || v.name == "Militia(Clone)2" || v.name == "Hunter(Clone)0" || v.name == "Hunter(Clone)1" || v.name == "Hunter(Clone)2" || v.name == "Soldier(Clone)0" || v.name == "Soldier(Clone)1" || v.name == "Soldier(Clone)2" || v.name == "Hunter(Clone)0" || v.name == "Hunter(Clone)1" || v.name == "Hunter(Clone)2" || v.name == "Guard(Clone)0" || v.name == "Guard(Clone)1" || v.name == "Guard(Clone)2" || v.name == "Scout(Clone)0" || v.name == "Scout(Clone)1" || v.name == "Scout(Clone)2" || v.name == "BrigandArcher(Clone)0" || v.name == "BrigandArcher(Clone)1" || v.name == "BrigandArcher(Clone)2" || v.name == "BrigandSol(Clone)0" || v.name == "BrigandSol(Clone)1" || v.name == "BrigandSol(Clone)2" || v.name == "Brigand(Clone)0" || v.name == "Brigand(Clone)1" || v.name == "Brigand(Clone)2")
                        GoldStored(1, 10);
                    else if (v.name == "Sargent(Clone)")
                        GoldStored(10, 20);
                    else if (v.name == "Barmaid(Clone)0" || v.name == "Barmaid(Clone)1" || v.name == "Barmaid(Clone)2" || v.name == "GuardBarmaidDark(Clone)0" || v.name == "GuardBarmaidRed(Clone)1" || v.name == "GuardBarmaidBlonde(Clone)2")  // GuardBarmaidDark(Clone)0   GuardBarmaidRed(Clone)1  GuardBarmaidBlonde(Clone)2
                        GoldStored(10, 25);
                    else if (v.name == "bodyguard(Clone)0" || v.name == "bodyguard(Clone)1" || v.name == "bodyguard(Clone)2") //bodyguard(Clone)2
                        GoldStored(20, 50);
                    else if (v.name == "Captain(Clone)0" || v.name == "Captain(Clone)1" || v.name == "Captain(Clone)2")
                        GoldStored(25, 100);
                    else if (v.name == "BrigandVeteran(Clone)0" || v.name == "BrigandVeteran(Clone)1" || v.name == "BrigandVeteran(Clone)2")
                        Globals.shopMerchant.Gold += 250;
                    else if (v.name == "DireWolf(Clone)0" || v.name == "DireWolf(Clone)1" || v.name == "DireWolf(Clone)2" || v.name == "Hound(Clone)0" || v.name == "Hound(Clone)1" || v.name == "Hound(Clone)2" || v.name == "Panther(Clone)0" || v.name == "Panther(Clone)1" || v.name == "Panther(Clone)2" || v.name == "Rat(Clone)0" || v.name == "Rat(Clone)1" || v.name == "Rat(Clone)2")
                        Globals.shopMerchant.Meat += 1;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                }
                else if (waveCount == 2)
                {
                    if (v.name == "Conscript(Clone)0" || v.name == "Conscript(Clone)1" || v.name == "Conscript(Clone)2" || v.name == "Conscript(Clone)3" || v.name == "Conscript(Clone)4" || v.name == "Conscript(Clone)5" || v.name == "Militia(Clone)0" || v.name == "Militia(Clone)1")
                        Globals.shopMerchant.Spear += 1;
                    else if (v.name == "Militia(Clone)0" || v.name == "Militia(Clone)1" || v.name == "Militia(Clone)2" || v.name == "Militia(Clone)3" || v.name == "Militia(Clone)4" || v.name == "Militia(Clone)5" || v.name == "Brigand(Clone)3" || v.name == "Brigand(Clone)4" || v.name == "Brigand(Clone)5")
                        Globals.shopMerchant.ShortSword += 1;
                    else if (v.name == "Hunter(Clone)0" || v.name == "Hunter(Clone)1" || v.name == "Hunter(Clone)2" || v.name == "Hunter(Clone)3" || v.name == "Hunter(Clone)4" || v.name == "Hunter(Clone)5")
                        Globals.shopMerchant.ShortSword += 1;
                    else if (v.name == "Soldier(Clone)0" || v.name == "Soldier(Clone)1" || v.name == "Soldier(Clone)2" || v.name == "Soldier(Clone)3" || v.name == "Soldier(Clone)4" || v.name == "Soldier(Clone)5")
                        Globals.shopMerchant.ShortSword += 1;
                    else if (v.name == "Guard(Clone)0" || v.name == "Guard(Clone)1" || v.name == "Guard(Clone)2" || v.name == "Guard(Clone)3" || v.name == "Guard(Clone)4" || v.name == "Guard(Clone)5")
                        Globals.shopMerchant.ShortSword += 1;
                    else if (v.name == "Scout(Clone)0" || v.name == "Scout(Clone)1" || v.name == "Scout(Clone)2" || v.name == "Scout(Clone)3" || v.name == "Scout(Clone)4" || v.name == "Scout(Clone)5")
                        Globals.shopMerchant.ShortBow += 1;
                    else if (v.name == "BrigandArcher(Clone)0" || v.name == "BrigandArcher(Clone)1" || v.name == "BrigandArcher(Clone)2" || v.name == "BrigandArcher(Clone)3" || v.name == "BrigandArcher(Clone)4" || v.name == "BrigandArcher(Clone)5")
                        Globals.shopMerchant.CrossBow += 1;
                    else if (v.name == "BrigandSol(Clone)0" || v.name == "BrigandSol(Clone)1" || v.name == "BrigandSol(Clone)2" || v.name == "BrigandSol(Clone)3" || v.name == "BrigandSol(Clone)4" || v.name == "BrigandSol(Clone)5")
                        Globals.shopMerchant.ShortAxe += 1;
                    else if (v.name == "Sargent(Clone)" || v.name == "SargentAtArms(Clone)4" || v.name == "SargentAtArms(Clone)3") // add Guard(Clone)5   SargentAtArms(Clone)4  Guard(Clone)5
                        Globals.shopMerchant.LongSword += 1;
                    else if (v.name == "Barmaid(Clone)0" || v.name == "Barmaid(Clone)1" || v.name == "Barmaid(Clone)2" || v.name == "Barmaid(Clone)3" || v.name == "Barmaid(Clone)4" || v.name == "Barmaid(Clone)5")
                        Globals.shopMerchant.ShortSword += 1;
                    else if (v.name == "Captain(Clone)0" || v.name == "Captain(Clone)1" || v.name == "Captain(Clone)2" || v.name == "Captain(Clone)3" || v.name == "Captain(Clone)4" || v.name == "Captain(Clone)5")
                        Globals.shopMerchant.LongSword += 1;
                    else if (v.name == "BrigandVeteran(Clone)0" || v.name == "BrigandVeteran(Clone)1" || v.name == "BrigandVeteran(Clone)2" || v.name == "BrigandVeteran(Clone)3" || v.name == "BrigandVeteran(Clone)4" || v.name == "BrigandVeteran(Clone)5")
                        Globals.shopMerchant.DoubleHeadedAxe += 1;
                    else if (v.name == "bodyguard(Clone)3" || v.name == "bodyguard(Clone)4" || v.name == "bodyguard(Clone)5" || v.name == "bodyguard(Clone)6")
                        Globals.shopMerchant.LongSword += 1;
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                }
                else if (waveCount >= 3) // SargentAtArms(Clone)6
                {
                    if (v.name == "Conscript(Clone)6" || v.name == "Conscript(Clone)7" || v.name == "Conscript(Clone)8" || v.name == "Militia(Clone)6" || v.name == "Militia(Clone)7")
                        Globals.shopMerchant.ClothArmour += 1;
                    else if (v.name == "Militia(Clone)6" || v.name == "Militia(Clone)7" || v.name == "Militia(Clone)8")
                        Globals.shopMerchant.PaddedArmour += 1;
                    else if (v.name == "Hunter(Clone)6" || v.name == "Hunter(Clone)7" || v.name == "Hunter(Clone)8")
                        Globals.shopMerchant.WoodenBuckler += 1;
                    else if (v.name == "Soldier(Clone)6" || v.name == "Soldier(Clone)7" || v.name == "Soldier(Clone)8")
                        Globals.shopMerchant.WoodenSmallRounded += 1;
                    else if (v.name == "Guard(Clone)6" || v.name == "Guard(Clone)7" || v.name == "Guard(Clone)8" || v.name == "Guard(Clone)9" || v.name == "Guard(Clone)10" || v.name == "Guard(Clone)11")
                        Globals.shopMerchant.SplintmailArmor += 1;
                    else if (v.name == "Scout(Clone)6" || v.name == "Scout(Clone)7" || v.name == "Scout(Clone)8")
                        Globals.shopMerchant.ClothArmour += 1;
                    else if (v.name == "BrigandArcher(Clone)6" || v.name == "BrigandArcher(Clone)7" || v.name == "BrigandArcher(Clone)8") // Captain(Clone)7
                        Globals.shopMerchant.PaddedArmour += 1;
                    else if (v.name == "BrigandSol(Clone)6" || v.name == "BrigandSol(Clone)7" || v.name == "BrigandSol(Clone)8")
                        Globals.shopMerchant.WoodenSmallRounded += 1;
                    else if (v.name == "Sargent(Clone)" || v.name == "SargentAtArms(Clone)6" || v.name == "SargentAtArms(Clone)9" || v.name == "SargentAtArms(Clone)12")
                        Globals.shopMerchant.ChainArmour += 1;
                    else if (v.name == "Barmaid(Clone)6" || v.name == "Barmaid(Clone)7" || v.name == "Barmaid(Clone)8")
                        Globals.shopMerchant.ChainArmour += 1;
                    else if (v.name == "Captain(Clone)6" || v.name == "Captain(Clone)7" || v.name == "Captain(Clone)8")
                        Globals.shopMerchant.WoodenKiteShield += 1;
                    else if (v.name == "BrigandVeteran(Clone)6" || v.name == "BrigandVeteran(Clone)7" || v.name == "BrigandVeteran(Clone)8")
                        Globals.shopMerchant.BrigadineArmor += 1;
                    else if (v.name == "bodyguard(Clone)6" || v.name == "bodyguard(Clone)7" || v.name == "bodyguard(Clone)8" || v.name == "bodyguard(Clone)9" || v.name == "bodyguard(Clone)10")
                        Globals.shopMerchant.PaddedArmour += 1;
                    else if (v.name == "BrigandLeader(Clone)7")
                    {
                        GoldUpdate(250);
                    }
                    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                }
                //else if (waveCount >= 3)
                //{
                //    if (v.name == "Conscript(Clone)0" || v.name == "Conscript(Clone)1" || v.name == "Conscript(Clone)2" || v.name == "Militia(Clone)0" || v.name == "Militia(Clone)1")
                //        Globals.shopMerchant.ClothArmour += 1;
                //    else if (v.name == "Militia(Clone)0" || v.name == "Militia(Clone)1" || v.name == "Militia(Clone)2")
                //        Globals.shopMerchant.PaddedArmour += 1;
                //    else if (v.name == "Hunter(Clone)0" || v.name == "Hunter(Clone)1" || v.name == "Hunter(Clone)2")
                //        Globals.shopMerchant.WoodenBuckler += 1;
                //    else if (v.name == "Soldier(Clone)0" || v.name == "Soldier(Clone)1" || v.name == "Soldier(Clone)2")
                //        Globals.shopMerchant.WoodenSmallRounded += 1;
                //    else if (v.name == "Guard(Clone)0" || v.name == "Guard(Clone)1" || v.name == "Guard(Clone)2")
                //        Globals.shopMerchant.SplintmailArmor += 1;
                //    else if (v.name == "Scout(Clone)0" || v.name == "Scout(Clone)1" || v.name == "Scout(Clone)2")
                //        Globals.shopMerchant.ClothArmour += 1;
                //    else if (v.name == "BrigandArcher(Clone)0" || v.name == "BrigandArcher(Clone)1" || v.name == "BrigandArcher(Clone)2") // Captain(Clone)7
                //        Globals.shopMerchant.PaddedArmour += 1;
                //    else if (v.name == "BrigandSol(Clone)0" || v.name == "BrigandSol(Clone)1" || v.name == "BrigandSol(Clone)2")
                //        Globals.shopMerchant.WoodenSmallRounded += 1;
                //    else if (v.name == "Sargent(Clone)")
                //        Globals.shopMerchant.ChainArmour += 1;
                //    else if (v.name == "Barmaid(Clone)0" || v.name == "Barmaid(Clone)1" || v.name == "Barmaid(Clone)2")
                //        Globals.shopMerchant.ChainArmour += 1;
                //    else if (v.name == "Captain(Clone)0" || v.name == "Captain(Clone)1" || v.name == "Captain(Clone)2")
                //        Globals.shopMerchant.WoodenKiteShield += 1;
                //    else if (v.name == "BrigandVeteran(Clone)0" || v.name == "BrigandVeteran(Clone)1" || v.name == "BrigandVeteran(Clone)2")
                //        Globals.shopMerchant.BrigadineArmor += 1;
                //    else if (v.name == "bodyguard(Clone)6" || v.name == "bodyguard(Clone)7" || v.name == "bodyguard(Clone)8" || v.name == "bodyguard(Clone)9" || v.name == "bodyguard(Clone)10")
                //        Globals.shopMerchant.PaddedArmour += 1;
                //    db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                //}
                i++;
            }
        }
    }
    private void Update()
    {
        if (isUpdatingScore)
            ShowScoreEffect();
    }
    float tempScore, timeStarted;
    bool isUpdatingScore = false;
    void GoldStored(int startUnit, int randomUnit)
    {
        randomNumber = Random.Range(startUnit, randomUnit);
        Globals.shopMerchant.Gold += randomNumber;
        db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        isUpdatingScore = true;
        StartCoroutine(increaseSize());
    }
    IEnumerator increaseSize()
    {
        for (float i = goldText.fontSize; i <= 45; i += 1f)
        {
            goldText.fontSize = (int)i;
            yield return new WaitForSeconds(0.01f);
        }
        for (float i = goldText.fontSize; i >= 19; i -= 1f)
        {
            goldText.fontSize = (int)i;
            yield return new WaitForSeconds(0.01f);
        }

    }
    void GoldUpdate(int randomNumber)
    {
        Globals.shopMerchant.Gold += randomNumber;
        isUpdatingScore = true;
        StartCoroutine(increaseSize());
    }
    void ShowScoreEffect()
    {
        Debug.Log("here scrore increasing.......");
        int currentValue = int.Parse(goldText.text);
        goldText.text = ((int)Mathf.SmoothDamp(currentValue, Globals.shopMerchant.Gold, ref tempScore, 1)).ToString();
        if ((Time.time - timeStarted) >= 1)
        {
            goldText.text = Globals.shopMerchant.Gold.ToString();
            isUpdatingScore = false;
        }
    }
    void DisableImage()
    {
        foreach (var v in deadEnemy)
        {
            if (v.tag == "Enemy")
            {
                v.GetComponent<EntityGroup>().lootImage.SetActive(false);
            }
        }
        StartCoroutine(BodiesRemoved());
    }
    IEnumerator BodiesRemoved()
    {
        yield return new WaitForSeconds(1.2f);
        foreach (var v in deadEnemy)
        {
            if (v.tag == "Enemy")
            {
                v.gameObject.SetActive(false);
            }

        }
        foreach (var v in deadEnemy) // destroy game object enemy
        {
            if (v.tag == "Enemy")
            {
                DestroyImmediate(v.gameObject);
            }

        }
        deadEnemy.Clear();
        lootItemList.Clear();
        NextStep();
    }
    void NextStep()
    {
        Debug.Log("wave count here::" + waveCount);
        if (waveCount == 1)
        {
            if (Globals.activeScene == Globals.CurrentScene.SoldierCampsite && Globals.soldierCampsiteVisit == 1)
                StartCoroutine(ShowGameOver());
            else if (Globals.activeScene == Globals.CurrentScene.Tutorial || (Globals.activeScene == Globals.CurrentScene.Huntsville && (Globals.isArcher || Globals.isArcherF)))
                StartCoroutine(ShowGameOver());
            else if ((Globals.activeScene == Globals.CurrentScene.HuntingtonCastle) && Globals.secondFight)
                StartCoroutine(ShowGameOver());
            else if ((Globals.activeScene == Globals.CurrentScene.BarghestVillage || Globals.activeScene == Globals.CurrentScene.BarghestLairDengeon) && (!Globals.isBarghest))
                StartCoroutine(ShowGameOver());
            else if (Globals.activeScene == Globals.CurrentScene.HuntingtonVillage)
                StartCoroutine(ShowGameOver());
            else if ((Globals.activeScene == Globals.CurrentScene.MotteAndBaileyCastle) && (Globals.forthFight))
                GenerateEnemies(Globals.activeScene);
            else if ((Globals.activeScene == Globals.CurrentScene.MotteAndBaileyCastle) && (Globals.thirdFight))
                SpawnCaptain();

            else if ((Globals.activeScene == Globals.CurrentScene.BarghestVillage || Globals.activeScene == Globals.CurrentScene.BarghestLairDengeon) && Globals.isBarghest)
                GenerateEnemies(Globals.activeScene);
            else if (Globals.activeScene == Globals.CurrentScene.TheDeathWeight || Globals.activeScene == Globals.CurrentScene.TheDeathWeightDengeon)
                SpawnDeathWeight();
            else if (Globals.activeScene == Globals.CurrentScene.CellarInt)
                SpawnSargent();
            else if ((Globals.activeScene == Globals.CurrentScene.RandomAttack) && (Globals.carvan1 || Globals.caravan2 || Globals.caravan3) && Globals.atWaterCount > 5)
                GenerateEnemies(Globals.activeScene);
            else if (Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon && !Globals.secondFight)
            {
                if (!Globals.thirdFight)
                    StartCoroutine(ShowGameOver());
                else
                    GenerateEnemies(Globals.activeScene);
            }
            else if (Globals.activeScene == Globals.CurrentScene.monastery)
                GenerateEnemies(Globals.activeScene);
            else if (Globals.activeScene == Globals.CurrentScene.AtwaterVillage)
            {
                if (Globals.isSargent)
                {
                    SpawnSargent();
                    Globals.atwaterFinalFight = true;
                }
                else
                {
                    StartCoroutine(ShowGameOver());

                }

            }

            else
            {
                Globals.secondWave = true;
                GenerateEnemies(Globals.activeScene);
            }
        }
        else if (waveCount == 2)
        {
            if (Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon)
            {
                if (Globals.secondFight)
                    StartCoroutine(ShowGameOver());
                else if (Globals.thirdFight)
                    GenerateEnemies(Globals.activeScene);
            }
            else if (Globals.activeScene == Globals.CurrentScene.HuntingtonCastle && Globals.thirdFight)
                StartCoroutine(ShowGameOver());
            else if (Globals.activeScene == Globals.CurrentScene.MotteAndBaileyCastle && Globals.thirdFight)
                StartCoroutine(ShowGameOver());
            else if (Globals.activeScene == Globals.CurrentScene.MotteAndBaileyCastle && Globals.secondFight)
                StartCoroutine(ShowGameOver());
            //else if (Globals.activeScene == Globals.CurrentScene.HuntingtonThroneRoom && Globals.secondFight)
            //    StartCoroutine(ShowGameOver());
            else if (Globals.activeScene == Globals.CurrentScene.HuntingtonThroneRoom && Globals.secondFight)
                StartCoroutine(ShowGameOver());
            else if ((Globals.activeScene == Globals.CurrentScene.RandomAttack) && (Globals.carvan1 || Globals.caravan2 || Globals.caravan3))
                StartCoroutine(ShowGameOver());
            else if ((Globals.activeScene == Globals.CurrentScene.RandomAttack) && (Globals.petrol1 || Globals.petrol2 || Globals.petrol3) && Globals.atWaterCount > 5)
                SpawnSargent();
            else if ((Globals.activeScene == Globals.CurrentScene.TheDeathWeight || Globals.activeScene == Globals.CurrentScene.TheDeathWeightDengeon))
                StartCoroutine(ShowGameOver());
            else if ((Globals.activeScene == Globals.CurrentScene.BarghestVillage || Globals.activeScene == Globals.CurrentScene.BarghestLairDengeon) && Globals.isBarghest)
                GenerateEnemies(Globals.activeScene);
            else if (Globals.activeScene == Globals.CurrentScene.CellarTucker || Globals.activeScene == Globals.CurrentScene.monastery)
                GenerateEnemies(Globals.activeScene);
            else if (Globals.activeScene == Globals.CurrentScene.WagonCaravan)
                SpawnSargent();
            else if (Globals.activeScene == Globals.CurrentScene.BarghestLairDengeon || Globals.activeScene == Globals.CurrentScene.BarghestVillage || Globals.activeScene == Globals.CurrentScene.SoldierCampsite || Globals.activeScene == Globals.CurrentScene.CellarInt || Globals.activeScene == Globals.CurrentScene.HuntingtonCastle || Globals.activeScene == Globals.CurrentScene.CastleEscapeTunnel)
                StartCoroutine(ShowGameOver());
            else if ((Globals.activeScene == Globals.CurrentScene.Huntsville && (Globals.isSmith || Globals.isAcolyte || Globals.isSmithF || Globals.isAcolyteF)) && !Globals.secondFight && !Globals.thirdFight)
                StartCoroutine(ShowGameOver());
            else if (Globals.activeScene == Globals.CurrentScene.MotteAndBaileyCastle && !Globals.isPart1Battle)
                StartCoroutine(ShowGameOver());
            else if (Globals.activeScene == Globals.CurrentScene.MotteAndBaileyCastle && Globals.beforeMottey)
                GenerateEnemies(Globals.activeScene);
            else if (Globals.activeScene == Globals.CurrentScene.AtwaterVillage && Globals.storyCount == 0)
                StartCoroutine(ShowGameOver());
            else if (Globals.activeScene == Globals.CurrentScene.HuntingtonThroneRoom)
                GenerateEnemies(Globals.activeScene);
            else if ((Globals.activeScene == Globals.CurrentScene.RandomAttack) && (Globals.petrol2 || Globals.petrol3 || Globals.petrol1) && Globals.atWaterCount > 5)
                SpawnSargent();
            else if (Globals.activeScene == Globals.CurrentScene.monastery)
                GenerateEnemies(Globals.activeScene);
            else if (Globals.activeScene == Globals.CurrentScene.TheBrigand)
                GenerateEnemies(Globals.activeScene);
            else
            {
                if (!Globals.secondFight && !Globals.thirdFight)
                {
                    Globals.secondWave = false;
                    Globals.thirdWave = true;
                    GenerateEnemies(Globals.activeScene);
                }
                else if (Globals.secondFight || Globals.thirdFight)
                {
                    StartCoroutine(ShowGameOver());
                }
            }
        }
        else if (waveCount == 3)
        {
            if (Globals.activeScene == Globals.CurrentScene.SoldierCampsite || Globals.activeScene == Globals.CurrentScene.HuntingtonCastle || Globals.activeScene == Globals.CurrentScene.WagonCaravan || Globals.activeScene == Globals.CurrentScene.CellarTucker || Globals.activeScene == Globals.CurrentScene.CastleEscapeTunnel)
                StartCoroutine(ShowGameOver());
            else if (Globals.activeScene == Globals.CurrentScene.MotteAndBaileyCastle && Globals.secondFight)
                StartCoroutine(ShowGameOver());
            else if (Globals.activeScene == Globals.CurrentScene.TheDeathWeight || Globals.activeScene == Globals.CurrentScene.TheBrigand || Globals.activeScene == Globals.CurrentScene.TheDeathWeightDengeon || Globals.activeScene == Globals.CurrentScene.RandomAttack || Globals.activeScene == Globals.CurrentScene.BarghestVillage || Globals.activeScene == Globals.CurrentScene.BarghestLairDengeon)
                StartCoroutine(ShowGameOver());
            else if ((Globals.activeScene == Globals.CurrentScene.RandomAttack) && (Globals.carvan1 || Globals.caravan2 || Globals.caravan3) && Globals.atWaterCount < 5)
                StartCoroutine(ShowGameOver());
            else if (Globals.activeScene == Globals.CurrentScene.HuntingtonThroneRoom || (Globals.activeScene == Globals.CurrentScene.MotteAndBaileyCastle && !Globals.forthFight))
                GenerateEnemies(Globals.activeScene);
            else if (Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon || (Globals.activeScene == Globals.CurrentScene.MotteAndBaileyCastle && Globals.forthFight))
                StartCoroutine(ShowGameOver());

            else
                SpawnSargent();
        }
        else if (waveCount == 4)
        {
            if (Globals.activeScene == Globals.CurrentScene.TheBrigand || Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon)
                GenerateEnemies(Globals.activeScene);
            else if (Globals.activeScene == Globals.CurrentScene.MotteAndBaileyCastle && Globals.beforeMottey)
                SpawnSargent();
            else if (Globals.activeScene == Globals.CurrentScene.TheBrigand || Globals.activeScene == Globals.CurrentScene.monastery || Globals.activeScene == Globals.CurrentScene.HuntingtonThroneRoom)
                StartCoroutine(ShowGameOver());
            else
                StartCoroutine(ShowGameOver());
        }
        else if (waveCount == 5)
        {
            if (Globals.activeScene == Globals.CurrentScene.TheBrigand)
                GenerateEnemies(Globals.activeScene);
            else
                StartCoroutine(ShowGameOver());
        }
        else if (waveCount == 6)
        {
            if (Globals.activeScene == Globals.CurrentScene.MotteAndBaileyCastle)
                SpawnSargent();
            else
                StartCoroutine(ShowGameOver());
        }
        else if (waveCount == 7)
        {
            if (Globals.activeScene == Globals.CurrentScene.MotteAndBaileyCastle)
                StartCoroutine(ShowGameOver());
        }
        battleText.text = "Battle No - " + (waveCount + 1);
    }
    void SpawnCaptain()
    {
        PositionSet();
        foreach (PlayerConfiguration item in captainLibrary.PlayerCharacterLibrary)
        {
            PlayerItem enemy;
            pos1 = new Vector3((pos1.x + 2), character.transform.position.y, 0);
            enemy = Instantiate(item.playerItemPrefab, pos1, Quaternion.identity);
            enemy.transform.localScale = new Vector3(0.7f, 0.7f, 0);
            if (Globals.activeScene == Globals.CurrentScene.MotteAndBaileyCastle)
                BossEnemy(item, enemy, 400, 120, 10, 150, 125);
            else
                BossEnemy(item, enemy, 70, 120, 7, 130, 105);
            enemy.GetComponent<EntityGroup>().originalPos = enemy.transform.localPosition;
            enemy.InitializePlayerItem(item);
        }
        StartCoroutine(SwitchTurn());
    }
    void SpawnSargent()
    {

        PositionSet();
        foreach (PlayerConfiguration item in sargentLibrary.PlayerCharacterLibrary)
        {
            PlayerItem enemy;
            pos1 = new Vector3((pos1.x + 2), character.transform.position.y, 0);
            enemy = Instantiate(item.playerItemPrefab, pos1, Quaternion.identity);
            enemy.transform.localScale = new Vector3(0.7f, 0.7f, 0);
            if (Globals.activeScene == Globals.CurrentScene.WagonCaravan)
                BossEnemy(item, enemy, 50, 50, 3, 50, 55);
            else if (Globals.activeScene == Globals.CurrentScene.CellarInt || Globals.activeScene == Globals.CurrentScene.monastery)
                BossEnemy(item, enemy, 200, 50, 5, 30, 50); //h:50
            else if (Globals.activeScene == Globals.CurrentScene.RandomAttack)
                BossEnemy(item, enemy, 200, 50, 5, 20, 50); //a:30
            else if (Globals.activeScene == Globals.CurrentScene.MotteAndBaileyCastle)
                BossEnemy(item, enemy, 400, 50, 5, 70, 50); //a:30
            else
                BossEnemy(item, enemy, 50, 100, 5, 80, 75); //h:50
            enemy.GetComponent<EntityGroup>().originalPos = enemy.transform.localPosition;
            enemy.InitializePlayerItem(item);
        }
        StartCoroutine(SwitchTurn());
    }
    int deathWightNumber = 0;
    void SpawnDeathWeight()
    {
        Debug.Log("spawn death wight");
        Globals.deathWightNumber++;
        PositionSet();
        foreach (PlayerConfiguration item in deathWightLibrary.PlayerCharacterLibrary)
        {
            PlayerItem enemy;
            pos1 = new Vector3((pos1.x + 2), character.transform.position.y, 0);
            enemy = Instantiate(item.playerItemPrefab, pos1, Quaternion.identity);
            enemy.transform.localScale = new Vector3(0.7f, 0.7f, 0);
            BossEnemy(item, enemy, 340, 200, 10, 150, 20); //h: 340 a:200 preValue= 440 , 260
            enemy.GetComponent<EntityGroup>().originalPos = enemy.transform.localPosition;
            enemy.InitializePlayerItem(item);
        }
        StartCoroutine(SwitchTurn());
    }
    IEnumerator ShowGameOver()
    {
        battleText.gameObject.SetActive(false);
        ButtonSetting();
        StopCoroutine(SwitchTurn());
        StartCoroutine(DeathEffect());
        yield return new WaitForSeconds(1.2f);
        foreach (var v in playersInBattle)
        {
            v.GetComponent<EntityGroup>().namePopup.SetActive(false);
        }
        if (myTeam.Count == 0)
        {
            Globals.isMyTeam = true;
            GameOver.SetActive(true);
            Globals.conversationCount--;
            if (Globals.activeScene == Globals.CurrentScene.BarghestLairDengeon || Globals.activeScene == Globals.CurrentScene.BarghestVillage || Globals.activeScene == Globals.CurrentScene.TheDeathWeightDengeon || Globals.activeScene == Globals.CurrentScene.TheDeathWeight)
            {
                if (Globals.isBarghest)
                    Globals.isBarghest = false;
            }
            else if (Globals.activeScene == Globals.CurrentScene.CellarTucker)
            {
                Debug.Log("celler tucker again......");
                Globals.avatarState.TotalXp -= 1000;
                db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
                LevelCalculation.instance.CalculateXpPoints();
            }
            else if (Globals.activeScene == Globals.CurrentScene.MotteAndBaileyCastle)
            {
                if (Globals.secondFight)
                {
                    Globals.secondFight = false;
                    Globals.beforeMottey = true;
                }
                else if (Globals.thirdFight)
                {
                    Globals.thirdFight = false;
                    Globals.secondFight = true;
                }
                else if (Globals.forthFight)
                {
                    Globals.forthFight = false;
                    Globals.thirdFight = true;
                }
            }
            else if (Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon && Globals.thirdFight)
            {
                Globals.secondFight = true;
                Globals.thirdFight = false;
            }
            else if (Globals.activeScene == Globals.CurrentScene.TheBrigand)
                Globals.conversationCount = 0;
            else if (Globals.activeScene == Globals.CurrentScene.HuntingtonCastle || Globals.activeScene == Globals.CurrentScene.HuntingtonThroneRoom)
                Globals.secondFight = false;
            GameOver.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = "You have to play this story again";
        }
        else
        {
            Globals.isEnemyTeam = true;
            PointsOnWin(Globals.activeScene);
            Cross();
        }
    }
    void Cross()
    {
        //Globals.carvan1 = false;
        //Globals.caravan2 = false;
        //Globals.caravan3 = false;
        //Globals.petrol1 = false;
        //Globals.petrol2 = false;
        //Globals.petrol3 = false;
        Debug.Log("croosss::  .............222222222222");
        if (Globals.atWaterCount != 0)
            Globals.isSargent = false;
        if (Globals.isEnemyTeam)
        {
            if (Globals.activeScene == Globals.CurrentScene.MotteAndBaileyCastle && !Globals.isPart1Battle)
                Globals.isMotteyRetreat = true;
            Globals.isPart1Battle = true;

        }
        if (Globals.activeScene == Globals.CurrentScene.SoldierCampsite)
        {
            if (Globals.soldierCampsiteVisit == 0)
                SceneManager.LoadScene("Soldier Campsite");
            else
                SceneManager.LoadScene("Second Soldier Campsite");
        }
        else if (Globals.activeScene == Globals.CurrentScene.WagonCaravan)
            SceneManager.LoadScene("Wagon Carvan");
        else if (Globals.activeScene == Globals.CurrentScene.SecondSoldierCaravan)
            SceneManager.LoadScene("Second Soldier Campsite");
        else if (Globals.activeScene == Globals.CurrentScene.AtwaterVillage)
            SceneManager.LoadScene("Atwater Village");
        else if (Globals.activeScene == Globals.CurrentScene.Huntsville)
            SceneManager.LoadScene("Huntsville_Intro Scene");
        else if (Globals.activeScene == Globals.CurrentScene.monastery)
            SceneManager.LoadScene("Monastery_ext");
        else if (Globals.activeScene == Globals.CurrentScene.CellarInt)
            SceneManager.LoadScene("Monastery2ndFloor_int");
        else if (Globals.activeScene == Globals.CurrentScene.CellarTucker)
            SceneManager.LoadScene("Monastery1stFloor_int");
        else if (Globals.activeScene == Globals.CurrentScene.MotteAndBaileyCastle)
            SceneManager.LoadScene("Motte and Baley Castle");
        else if (Globals.activeScene == Globals.CurrentScene.BarghestLairDengeon || Globals.activeScene == Globals.CurrentScene.BarghestVillage)
        {
            if (!Globals.random)
                SceneManager.LoadScene("Barghest Lair-Dungeon");
            else
            {
                Globals.isWolf = false;
                Globals.isHound = false;
                SceneManager.LoadSceneAsync("Barghest Trail to Dungeon");
            }
        }
        else if (Globals.activeScene == Globals.CurrentScene.TheDeathWeightDengeon || Globals.activeScene == Globals.CurrentScene.TheDeathWeight)
        {
            Globals.conversationCount++;
            if (Globals.deathWightCount == 1)
            {
                Globals.iszombie = false;
                Globals.isHound = false;
                SceneManager.LoadScene("DeathWight Trail to Dungeon");
            }
            else if (Globals.deathWightCount == 2)
                SceneManager.LoadScene("Death WIght Lair");
        }
        else if (Globals.activeScene == Globals.CurrentScene.TheBrigand)
            SceneManager.LoadScene("Brigand Village");
        else if (Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon)
        {
            if (Globals.brigandCount == 1)
                SceneManager.LoadSceneAsync("Brigand Trail to Dungeon");
            else
                SceneManager.LoadSceneAsync("Brigand Lair");
        }
        else if (Globals.activeScene == Globals.CurrentScene.HuntingtonVillage)
            SceneManager.LoadScene("Huntington_Inn_1stFloor");
        else if (Globals.activeScene == Globals.CurrentScene.CastleEscapeTunnel)
            SceneManager.LoadScene("Huntington Castle Escape Tunnel");
        else if (Globals.activeScene == Globals.CurrentScene.HuntingtonCastle)
            SceneManager.LoadScene("Huntington Castle Interior");
        else if (Globals.activeScene == Globals.CurrentScene.HuntingtonThroneRoom)
            SceneManager.LoadScene("Huntigton Castle Throne Room");
        else if (Globals.activeScene == Globals.CurrentScene.RandomAttack)
        {
            if (Globals.activeRandom == Globals.CurrentRandom.caravans)
                SceneManager.LoadScene("Caravan");
            else if (Globals.activeRandom == Globals.CurrentRandom.petrols)
                SceneManager.LoadScene("Petrols");
        }
        else if (Globals.activeScene == Globals.CurrentScene.Tutorial)
            SceneManager.LoadScene("Huntsville_Well_Dungeon");
    }

    void PointsOnWin(Globals.CurrentScene scene)
    {
        Debug.Log("on game over.........");
        switch (scene)
        {
            case Globals.CurrentScene.SoldierCampsite:
                Globals.avatarState.TotalXp += 90;
                db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
                break;
            case Globals.CurrentScene.WagonCaravan:
                Globals.avatarState.TotalXp += 480;
                db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
                break;
            case Globals.CurrentScene.SecondSoldierCaravan:
                Globals.avatarState.TotalXp += 160;
                db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
                break;
            case Globals.CurrentScene.AtwaterVillage:
                if (Globals.atwaterFinalFight)
                {
                    Debug.Log("final fight atwater..........");
                    Globals.atwaterFinalFight = false;
                    Globals.avatarState.TotalXp += 4500;
                    db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
                }

                break;
            case Globals.CurrentScene.CellarInt:
                Globals.avatarState.TotalXp += 1560;
                db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
                break;
            case Globals.CurrentScene.CellarTucker:
                Globals.avatarState.TotalXp += 630;
                db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
                break;
            case Globals.CurrentScene.MotteAndBaileyCastle:
                if (Globals.secondFight)
                    Globals.avatarState.TotalXp += 2160;
                else if (Globals.thirdFight)
                    Globals.avatarState.TotalXp += 360;
                else
                    Globals.avatarState.TotalXp += 2050;
                db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
                break;
            case Globals.CurrentScene.BrigandLairDengeon:
                Globals.avatarState.TotalXp += 2540;
                db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
                break;
            case Globals.CurrentScene.HuntingtonVillage:
                Globals.avatarState.TotalXp += 3240;
                db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
                break;
            case Globals.CurrentScene.CastleEscapeTunnel:
                Globals.avatarState.TotalXp += 2160;
                db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
                break;
            case Globals.CurrentScene.HuntingtonCastle:
                break;
        }
        LevelCalculation.instance.CalculateXpPoints();
    }
    IEnumerator DeathEffect()
    {
        if (TargetObject.name == "DeathWeight(Clone)3")
        {
            if (Globals.isBarghest)
            {
                foreach (Transform child in TargetObject.transform)
                {
                    if (child.gameObject.activeInHierarchy && child.GetComponent<Animator>() != null)
                        child.GetComponent<Animator>().SetTrigger("Death");
                }
            }
            else
            {
                Globals.ActiveFaces(TargetObject.gameObject, false, false, false, false);
                TargetObject.GetComponent<EntityGroup>().disapp.SetActive(true);
                foreach (Transform child in TargetObject.transform)
                {
                    if (child.gameObject.activeInHierarchy && child.GetComponent<Animator>() != null)
                        child.GetComponent<Animator>().SetTrigger("Death");
                }
            }
        }
        else
        {
            foreach (Transform child in TargetObject.transform)
            {
                if (child.gameObject.activeInHierarchy && child.GetComponent<Animator>() != null)
                {
                    //child.GetComponent<Animator>().SetTrigger("Death");
                    if (TargetObject.transform.tag == "Player")
                    {
                        if (Globals.inventoryProtagnist.AttackWeapon == "shortBow" || Globals.inventoryProtagnist.AttackWeapon == "longBow")
                        {
                            Debug.Log("11111111111111111111");
                            child.GetComponent<Animator>().SetTrigger("Death");
                        }
                        else
                        {
                            Debug.Log("2222222222222");
                            if (TargetObject.name == "ArcherMale(Clone)" || TargetObject.name == "ArcherFemale(Clone)")
                                child.GetComponent<Animator>().SetTrigger("Death2");
                            else
                                child.GetComponent<Animator>().SetTrigger("Death");
                        }
                    }
                    else if (TargetObject.transform.tag == "Companion" && TargetObject.name != "Tucker(Clone)" && TargetObject.name != "JohnCompanion(Clone)")

                    {
                        if (Globals.inventoryMarium.WeaponAttack == "shortBow" || Globals.inventoryMarium.WeaponAttack == "longBow")
                        {
                            Debug.Log("11111111111111111111");
                            child.GetComponent<Animator>().SetTrigger("Death");
                        }
                        else
                        {
                            Debug.Log("2222222222222");
                            child.GetComponent<Animator>().SetTrigger("Death2");
                        }
                    }
                    else
                    {
                        Debug.Log("33333333333333");
                        child.GetComponent<Animator>().SetTrigger("Death");
                    }

                }

            }
        }
        if (TargetObject.name == "Rat(Clone)0" || TargetObject.name == "Rat(Clone)1" || TargetObject.name == "Rat(Clone)2")
            otherAudio.clip = Resources.Load("Sound/Battle/RatSound") as AudioClip;
        else if (TargetObject.name == "DireWolf(Clone)0" || TargetObject.name == "DireWolf(Clone)1" || TargetObject.name == "DireWolf(Clone)2" || TargetObject.name == "DireWolf(Clone)3" || TargetObject.name == "DireWolf(Clone)4" || TargetObject.name == "DireWolf(Clone)5" || TargetObject.name == "DireWolf(Clone)6" || TargetObject.name == "DireWolf(Clone)8" || TargetObject.name == "Panther(Clone)0" || TargetObject.name == "Panther(Clone)1" || TargetObject.name == "Panther(Clone)2")
            otherAudio.clip = Resources.Load("Sound/Battle/HoundSound") as AudioClip;
        else if (TargetObject.name == "Hound(Clone)0" || TargetObject.name == "Hound(Clone)1" || TargetObject.name == "Hound(Clone)2" || TargetObject.name == "Barghest(Black Hound)(Clone)7")
            otherAudio.clip = Resources.Load("Sound/Battle/HoundSound") as AudioClip;
        else if (TargetObject.name == "Barghest(Black Hound)(Clone)0" || TargetObject.name == "Barghest(Black Hound)(Clone)1" || TargetObject.name == "Barghest(Black Hound)(Clone)2")
            otherAudio.clip = Resources.Load("Sound/Battle/BarghestHound") as AudioClip;
        else if (TargetObject.name == "Skeleton2(Clone)0" || TargetObject.name == "Skeleton2(Clone)1" || TargetObject.name == "Skeleton2(Clone)2" || TargetObject.name == "Skeleton1(Clone)0" || TargetObject.name == "Skeleton1(Clone)1" || TargetObject.name == "Skeleton1(Clone)2" || TargetObject.name == "Skeleton0(Clone)0" || TargetObject.name == "Skeleton0(Clone)1" || TargetObject.name == "Skeleton0(Clone)2" || TargetObject.name == "Skeleton3(Clone)0" || TargetObject.name == "Skeleton3(Clone)1" || TargetObject.name == "Skeleton3(Clone)2")  //Skeleton3(Clone)0
        {
            Debug.Log("skeleton sound :: ");
            otherAudio.clip = Resources.Load("Sound/Battle/Zombie") as AudioClip;
        }

        else if (TargetObject.name == "Marium(Clone)" || TargetObject.name == "ArcherFemale(Clone)" || TargetObject.name == "PriestFemale(Clone)" || TargetObject.name == "WarriorFemale(Clone)")
            otherAudio.clip = Resources.Load("Sound/Battle/mariumDeathSound") as AudioClip;
        else
            otherAudio.clip = Resources.Load("Sound/Battle/SoundOfManBeingKilled") as AudioClip;
        if (Globals.avatarState.SoundLevel == 1)
            otherAudio.Play();
        yield return new WaitForSeconds(0.1f);
        //   Debug.Log(" death sound "+otherAudio);
        deathPoint.transform.localPosition = TargetObject.transform.localPosition;
        TargetObject.GetComponent<EntityGroup>().deathEffect.transform.SetParent(deathPoint.transform);
        deathPoint.transform.GetChild(deathCount).gameObject.SetActive(true);
        deathPoint.transform.GetChild(deathCount).localScale = new Vector3(1, 1, 1);
        TargetObject.GetComponent<EntityGroup>().namePopup.SetActive(false);
        if (TargetObject.transform.tag == "Enemy")
        {
            deadEnemy.Add(TargetObject);
            deathCount++;
        }

        if (TargetObject.transform.tag == "Player" || TargetObject.transform.tag == "Companion")
        {
            myTeam.Remove(TargetObject);
            tempList.Remove(TargetObject);
            playersInBattle.Remove(TargetObject);
            if (myTeam.Count == 0)
                StartCoroutine(ShowGameOver());
        }
        else if (TargetObject.transform.tag == "Enemy")
        {
            enemys.Remove(TargetObject);
            tempList.Remove(TargetObject);
            playersInBattle.Remove(TargetObject);
            if (enemys.Count == 0)
                StartCoroutine(ForPart1());

        }
        DamageCaluclate();
    }

    void RemoveEffect()
    {
        foreach (var v in playersInBattle)
        {
            v.GetComponent<EntityGroup>().selectionEffect.SetActive(false);
        }
        MyTeamSetting(false);
        ButtonSetting();
        StartCoroutine(BackwardMove());
    }
    IEnumerator BackwardMove()
    {

        yield return new WaitForSeconds(0.8f);

        if (!isDefence)
        {
            moveB = animPlayer.GetComponent<EntityGroup>().originalPos;
            iTween.MoveTo(animPlayer.gameObject, iTween.Hash("position", moveB, "time", 0.21f, "islocal", true, "easeType", iTween.EaseType.linear, "looptype", iTween.LoopType.none));
            yield return new WaitForSeconds(0.25f);
        }
        foreach (var v in playersInBattle)
        {
            v.GetComponent<EntityGroup>().namePopup.SetActive(true);
        }
        playerIndex++;
        if (!isDefence)
        {
            Debug.Log("backword move......... " + TargetObject.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount);
            if (TargetObject.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount <= 0)
                StartCoroutine(DeathEffect());
            else
                DamageCaluclate();
        }
        else
        {
            StartCoroutine(SwitchTurn());
            isDefence = false;
        }
        t = 1;
    }
    public void DefenceDone()
    {
        ProtagnistTeamSetting(false);
        BottomBarManage();
        SetUpBool(true, false);
        ButtonSetting();
        DefenceBool();
        DefenceComplete();
    }
    void DefenceBool()
    {
        if (tempList[OtherIndex].tag == "Player")
            Globals.protagnistDefence = true;
        else
        {
            if (tempList[OtherIndex].name == "JohnCompanion(Clone)")
                Globals.johnDefence = true;
            else if (tempList[OtherIndex].name == "Marium(Clone)")
                Globals.mariumDefence = true;
            else if (tempList[OtherIndex].name == "Tucker(Clone)")
                Globals.tuckerDefence = true;
        }
    }
    public void HealDone()
    {
        ProtagnistTeamSetting(false);
        BottomBarManage();
        SetUpBool(false, true);
        ButtonSetting();
        healAssets.SetActive(true);
        animPlayer.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount = 1;
    }
    bool cureused = false;
    public void HealFunctionality()
    {
        healAssets.SetActive(false);
        attackDone = true;
        CancelInvoke("CheckingForTimer");
        bar.GetComponent<Image>().fillAmount = 1;
        tempList[OtherIndex].GetComponent<EntityGroup>().increaseHealth = true;
        tempList[OtherIndex].GetComponent<EntityGroup>().selectionEffect.SetActive(false);
        if (Globals.usedCurePotion)
        {
            Debug.Log("use cure potion.............");
            tempList[OtherIndex].GetComponent<EntityGroup>().noAttack = 0;
            Globals.usedCurePotion = false;
            CancelInvoke("TimeBar");
            bar.GetComponent<Image>().fillAmount = 1;
            cureused = true;
            //  StopCoroutine(PetrifiedDelay());
        }
        tempList.Remove(tempList[OtherIndex]);
        ButtonSetting();
        StartCoroutine(SwitchTurn());
    }
    void SetUpBool(bool d, bool h)
    {
        isDefence = d;
        isHeal = h;
    }
    void DamageEffect()
    {
        DefenceFunctionality();
        isHeal = false;
        isDefence = false;
    }
    void DefenceAnim()
    {
        if (animPlayer.tag == "Player")
            protagnistDefence = true;
        else
        {
            if (animPlayer.name == "JohnCompanion(Clone)")
                johnDefence = true;
            else if (animPlayer.name == "Marium(Clone)")
                mariumDefence = true;
            else if (animPlayer.name == "Tucker(Clone)")
                tuckerDefence = true;
        }
        attackDone = true;
        healAssets.SetActive(false);
        CancelInvoke("CheckingForTimer");
        bar.GetComponent<Image>().fillAmount = 1;
        tempList[OtherIndex].GetComponent<EntityGroup>().noAttack = 0;
        tempList.Remove(tempList[OtherIndex]);
        StartCoroutine(SwitchTurn());
    }
    void AttackAnim()
    {
        if (!TargetObject.GetComponent<PlayerItem>().isMissed)
        {
            //if ((animPlayer.name == "Marium(Clone)" && (Globals.inventoryMarium.Dragger == 0 && Globals.inventoryMarium.ShortSword == 0 && Globals.inventoryMarium.ShortAxe == 0 && Globals.inventoryMarium.Warhammer == 0 && Globals.inventoryMarium.Spear == 0 && Globals.inventoryMarium.LongBow == 0)) || animPlayer.name == "Hunter(Clone)0" || animPlayer.name == "Hunter(Clone)1" || animPlayer.name == "Hunter(Clone)2" || animPlayer.name == "Hunter(Clone)3" || animPlayer.name == "Hunter(Clone)4" || animPlayer.name == "Hunter(Clone)5")
            //    StartCoroutine(DamageDelay());
            //else if (animPlayer.name == "Hunter(Clone)0" || animPlayer.name == "Hunter(Clone)1" || animPlayer.name == "Hunter(Clone)2" || animPlayer.name == "Hunter(Clone)3" || animPlayer.name == "Hunter(Clone)4" || animPlayer.name == "Hunter(Clone)5" || animPlayer.name == "Skeleton1(Clone)0" || animPlayer.name == "Skeleton1(Clone)1" || animPlayer.name == "Skeleton1(Clone)2" || animPlayer.name == "Skeleton1(Clone)3" || animPlayer.name == "Skeleton1(Clone)4" || animPlayer.name == "Skeleton1(Clone)5")
            //    StartCoroutine(DamageDelay());
            //else if ((animPlayer.name == "ArcherMale(Clone)" || animPlayer.name == "ArcherFemale(Clone)") && Globals.inventoryProtagnist.Dragger == 0 && Globals.inventoryProtagnist.ShortSword == 0 && Globals.inventoryProtagnist.ShortAxe == 0 && Globals.inventoryProtagnist.Club == 0 && Globals.inventoryProtagnist.LongSword == 0 && Globals.inventoryProtagnist.Mace == 0 && Globals.inventoryProtagnist.Warhammer == 0 && Globals.inventoryProtagnist.Spear == 0 && Globals.inventoryProtagnist.LongAxe == 0 && Globals.inventoryProtagnist.Flair == 0 && Globals.inventoryProtagnist.Maul == 0)
            //{
            //    StartCoroutine(DamageDelay());
            //}
            if (animPlayer.name == "Hunter(Clone)0" || animPlayer.name == "Hunter(Clone)1" || animPlayer.name == "Hunter(Clone)2" || animPlayer.name == "Hunter(Clone)3" || animPlayer.name == "Hunter(Clone)4" || animPlayer.name == "Hunter(Clone)5" || animPlayer.name == "Skeleton1(Clone)0" || animPlayer.name == "Skeleton1(Clone)1" || animPlayer.name == "Skeleton1(Clone)2" || animPlayer.name == "Skeleton1(Clone)3" || animPlayer.name == "Skeleton1(Clone)4" || animPlayer.name == "Skeleton1(Clone)5")
                StartCoroutine(DamageDelay());
            else
                Damages();
            if (TargetObject.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount != 0)
            {
                if (TargetObject.name == "Marium(Clone)")
                {
                    otherAudio2.clip = Resources.Load("Sound/Battle/SoundOfWomanBeingWounded") as AudioClip;
                }
                else
                    otherAudio2.clip = Resources.Load("Sound/Battle/SoundOfManBeingWounded") as AudioClip;
                if (Globals.avatarState.SoundLevel == 1)
                    otherAudio2.Play();
            }
        }
    }
    IEnumerator DamageDelay()
    {
        yield return new WaitForSeconds(2.5f);
        Damages();

    }
    void Damages()
    {
        foreach (Transform child in TargetObject.transform)
        {
            if (child.gameObject.activeInHierarchy && child.GetComponent<Animator>() != null)
            {
                if (TargetObject.name == "Marium(Clone)" && (Globals.inventoryMarium.WeaponAttack == "shortBow" || Globals.inventoryMarium.WeaponAttack == "longBow"))
                {
                    Debug.Log("damage 111111111111");
                    child.GetComponent<Animator>().SetTrigger("Damage2");
                }
                else
                {
                    Debug.Log("damage 2222222222");
                    child.GetComponent<Animator>().SetTrigger("Damage");
                }
            }

        }
    }

    void DefenceFunctionality()
    {
        if (TargetObject.tag == "Player")
        {
            if (protagnistDefence)
            {
                DefenceActive();
                protagnistDefence = false;
            }
            else
                AttackAnim();
        }
        else if (TargetObject.tag == "Companion")
        {
            if (TargetObject.name == "JohnCompanion(Clone)")
            {
                if (johnDefence)
                {
                    DefenceActive();
                    johnDefence = false;
                }
                else
                    AttackAnim();
            }
            else if (TargetObject.name == "Marium(Clone)")
            {
                if (mariumDefence)
                {
                    DefenceActive();
                    mariumDefence = false;
                }
                else
                    AttackAnim();
            }
            else if (TargetObject.name == "Tucker(Clone)")
            {
                if (tuckerDefence)
                {
                    DefenceActive();
                    tuckerDefence = false;
                }
                else
                    AttackAnim();
            }
        }
        else
            AttackAnim();

    }
    void DefenceActive()
    {
        if (TargetObject.tag == "Player")
        {
            if (Globals.inventoryProtagnist.WoodenBuckler == 0 && Globals.inventoryProtagnist.MetalBuckler == 0 && Globals.inventoryProtagnist.WoodenSmallRounded == 0 && Globals.inventoryProtagnist.MetalSmallRounded == 0 && Globals.inventoryProtagnist.WoodenMediumShield == 0 && Globals.inventoryProtagnist.MetalMediumShield == 0)
                DefenceWithOutShield();
            else
                DefenceWithDefence();
        }
        else if (TargetObject.name == "JohnCompanion(Clone)")
        {
            if (Globals.inventoryJohn.WoodenBuckler == 0 && Globals.inventoryJohn.metalBuckler == 0 && Globals.inventoryJohn.WoodenSmallRound == 0 && Globals.inventoryJohn.metalSmallRound == 0 && Globals.inventoryJohn.WoodenMedium == 0 && Globals.inventoryJohn.metalMedium == 0)
                DefenceWithOutShield();
            else
                DefenceWithDefence();
        }
        else if (TargetObject.name == "Marium(Clone)")
        {
            if (Globals.inventoryMarium.WoodenBuckler == 0 && Globals.inventoryMarium.MetalBuckler == 0 && Globals.inventoryMarium.woodenSmall == 0 && Globals.inventoryMarium.MetalSmall == 0)
                DefenceWithOutShield();
            else
                DefenceWithDefence();
        }
        else if (TargetObject.name == "Tucker(Clone)")
        {
            if (Globals.inventoryTucker.WoodenBuckler == 0 && Globals.inventoryTucker.MetalBuckler == 0 && Globals.inventoryTucker.WoodenSmall == 0 && Globals.inventoryTucker.MetalSmall == 0)
                DefenceWithOutShield();
            else
                DefenceWithDefence();
        }

        otherAudio2.clip = Resources.Load("Sound/Battle/Defence") as AudioClip;
        if (Globals.avatarState.SoundLevel == 1)
            otherAudio2.Play();
    }
    void DefenceWithOutShield()
    {
        foreach (Transform child in TargetObject.transform)
        {
            if (child.gameObject.activeInHierarchy && child.GetComponent<Animator>() != null)
                child.GetComponent<Animator>().SetTrigger("Defence");
        }
    }
    void DefenceWithDefence()
    {
        foreach (Transform child in TargetObject.transform)
        {
            if (child.gameObject.activeInHierarchy && child.GetComponent<Animator>() != null)
                child.GetComponent<Animator>().SetTrigger("Defence1");
        }
    }
    string attackEntity;
    void PlayAttackAnim()
    {
        if (Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon && Globals.thirdFight && waveCount == 2)
            Debug.Log("anim player in animation::" + animPlayer.name);
        if (animPlayer.name == "Marium(Clone)")
        {
            if (Globals.inventoryMarium.WeaponAttack == "Spear")
            {
                Debug.Log("here attack......spear");
                attackEntity = "Attack3";
            }
            else if ((Globals.inventoryMarium.Dragger == 0 && Globals.inventoryMarium.ShortSword == 0 && Globals.inventoryMarium.ShortAxe == 0 && Globals.inventoryMarium.ShortBow == 0 && Globals.inventoryMarium.Warhammer == 0 && Globals.inventoryMarium.Spear == 0 && Globals.inventoryMarium.LongBow == 0) || Globals.inventoryMarium.WeaponAttack == "shortBow" || Globals.inventoryMarium.WeaponAttack == "longBow")
            {
                Debug.Log("here attack......BOW");
                attackEntity = "Attack";
            }
            else
            {
                attackEntity = "Attack1";
                Debug.Log("here attack......else");
            }

        }
        else if (animPlayer.name == "ArcherMale(Clone)" || animPlayer.name == "ArcherFemale(Clone)")
        {
            if (Globals.inventoryProtagnist.AttackWeapon == "Spear")
            {
                Debug.Log("here attack......spear");
                attackEntity = "Attack3";
            }
            else if ((Globals.inventoryProtagnist.Dragger == 0 && Globals.inventoryProtagnist.ShortSword == 0 && Globals.inventoryProtagnist.ShortAxe == 0 && Globals.inventoryProtagnist.ShortBow == 0 && Globals.inventoryProtagnist.Warhammer == 0 && Globals.inventoryProtagnist.Spear == 0 && Globals.inventoryProtagnist.LongBow == 0 && Globals.inventoryProtagnist.Club == 0 && Globals.inventoryProtagnist.Spear == 0) || Globals.inventoryProtagnist.AttackWeapon == "shortBow" || Globals.inventoryProtagnist.AttackWeapon == "longBow")
            {
                Debug.Log("here attack......default");
                attackEntity = "Attack";
            }
            else
            {
                Debug.Log("here attack......else");
                attackEntity = "Attack1";
            }

        }
        else if (animPlayer.name == "WarriorMale(Clone)" || animPlayer.name == "WarriorFemale(Clone)")
        {
            if (Globals.inventoryProtagnist.AttackWeapon == "Spear")
            {
                Debug.Log("here attack......spear");
                attackEntity = "Attack1";
            }
            else
            {
                Debug.Log("here attack......normal");
                attackEntity = "Attack";
            }
        }
        else if (animPlayer.name == "JohnCompanion(Clone)")
        {
            if (Globals.inventoryJohn.WeaponAttack == "Spear")
            {
                Debug.Log("here attack......spear");
                attackEntity = "Attack1";
            }
            else
                attackEntity = "Attack";
        }
        else
            attackEntity = "Attack";
        foreach (Transform child in animPlayer.transform)
        {
            if (child.gameObject.activeInHierarchy && child.GetComponent<Animator>() != null)
            {
                if (Globals.aiSpeacialEffect && animPlayer.tag == "Enemy")
                    child.GetComponent<Animator>().SetTrigger("SpecialAttack");
                else
                    child.GetComponent<Animator>().SetTrigger(attackEntity);
            }
        }
        otherAudio.clip = Resources.Load("Sound/Battle/attack") as AudioClip;
        if (Globals.avatarState.SoundLevel == 1)
            otherAudio.Play();
        CancelInvoke("CheckingForTimer");
        bar.GetComponent<Image>().fillAmount = 1;
        if (animPlayer.tag != "Enemy")
            PanelsSetting(true, false, false);
        if (Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon && Globals.thirdFight && waveCount == 2)
            Debug.Log("before damage");
        GiveDamage();
        Debug.Log("anim player :: " + animPlayer.name);
        if ((animPlayer.name == "Marium(Clone)" && (Globals.inventoryMarium.WeaponAttack == "shortBow" || Globals.inventoryMarium.WeaponAttack == "longBow")) || ((animPlayer.name == "ArcherMale(Clone)" || animPlayer.name == "ArcherFemale(Clone)" || animPlayer.name == "WarriorMale(Clone)" || animPlayer.name == "WarriorFemale(Clone)" || animPlayer.name == "PriestMale(Clone)" || animPlayer.name == "PriestFemale(Clone)") && (Globals.inventoryProtagnist.AttackWeapon == "shortBow" || Globals.inventoryProtagnist.AttackWeapon == "longBow")) || (animPlayer.name == "JohnCompanion(Clone)" && (Globals.inventoryJohn.WeaponAttack == "shortBow" || Globals.inventoryJohn.WeaponAttack == "longBow")))
        {
            Debug.Log("bow attack delay");
        }
        else
            Invoke("RemoveEffect", 0.3f);
    }
    Vector3 moveF;
    bool notForward;
    bool petrifydelay = false;

    void MoveForward()
    {
        Debug.Log("move forward::" + Globals.aiSpeacialEffect);

        Globals.aiSpeacialEffect = false;
        if (isChoose)
            animPlayer = attacker;

        else if (!isChoose)
        {
            animPlayer = tempList[OtherIndex];

            if (Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon && Globals.thirdFight && waveCount == 2)
                Debug.Log("anim player::" + animPlayer + "  count::" + brigandCount);
        }
        Debug.Log("wave count in move forward :: " + waveCount);
        if (Globals.activeScene == Globals.CurrentScene.BarghestVillage && waveCount == 2)
        {
            barghestPetrifyCount = 0;
            //foreach (var v in myTeam)  // check turn count 
            //{

            //    if (v.GetComponent<EntityGroup>().noAttack > 0)
            //    {
            //        Debug.Log(" here............. no attack decrease.......");
            //        v.GetComponent<EntityGroup>().noAttack--;
            //    }
            //    else if(v.GetComponent<EntityGroup>().noAttack == 0)
            //    {

            //        barghestPetrifyCount++;
            //        Debug.Log(" petrify count ......."+ barghestPetrifyCount);
            //    }
            //}

            if (animPlayer.GetComponent<EntityGroup>().noAttack > 0)
            {
                Debug.Log(" here............. no attack decrease.......");
                animPlayer.GetComponent<EntityGroup>().noAttack--;
            }
            foreach (var v in myTeam)
            {
                if (v.GetComponent<EntityGroup>().noAttack == 0)
                {

                    barghestPetrifyCount++;
                    Debug.Log(" petrify count ......." + barghestPetrifyCount);
                }
            }

        }



        if (animPlayer.tag == "Enemy")
        {
            if (animPlayer.name == "Hunter(Clone)0" || animPlayer.name == "Hunter(Clone)1" || animPlayer.name == "Hunter(Clone)2" || animPlayer.name == "Hunter(Clone)3" || animPlayer.name == "Hunter(Clone)4" || animPlayer.name == "Hunter(Clone)5" || animPlayer.name == "Skeleton1(Clone)0" || animPlayer.name == "Skeleton1(Clone)1" || animPlayer.name == "Skeleton1(Clone)2" || animPlayer.name == "Skeleton1(Clone)3" || animPlayer.name == "Skeleton1(Clone)4" || animPlayer.name == "Skeleton1(Clone)5")
            {
                notForward = true;
                moveF = new Vector3(animPlayer.transform.localPosition.x, animPlayer.transform.localPosition.y, 0);
            }
            else
                moveF = new Vector3(animPlayer.transform.localPosition.x + 3f, animPlayer.transform.localPosition.y, 0);
            if (((animPlayer.name == "Barghest(Black Hound)(Clone)7" && barghestPetrifyCount == myTeam.Count) || animPlayer.name == "Skeleton3(Clone)0" || animPlayer.name == "Skeleton3(Clone)1" || animPlayer.name == "Skeleton3(Clone)2" || animPlayer.name == "Skeleton1(Clone)0" || animPlayer.name == "Skeleton1(Clone)1" || animPlayer.name == "Skeleton1(Clone)2") && animPlayer.GetComponent<EntityGroup>().specialAttack == 0)
            {

                Globals.aiSpeacialEffect = true;
                if (animPlayer.name == "Barghest(Black Hound)(Clone)7")
                {
                    Debug.Log("barghest..........");
                    barghestPetrifyCount = 0;
                    playble.playableAsset = Resources.Load("Playables/Battle Scene/Barghest_Dialog_Howl") as TimelineAsset;
                    playble.Play();
                }
            }

            else if (animPlayer.name == "DeathWeight(Clone)3" && animPlayer.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount < 0.5 && animPlayer.GetComponent<EntityGroup>().specialAttack == 0 && Globals.isBarghest)
            {
                Debug.Log("death wight");
                Globals.aiSpeacialEffect = true;
                playble.playableAsset = Resources.Load("Playables/Battle Scene/DeathWight_Dialog") as TimelineAsset;
                playble.Play();
            }
            else if ((animPlayer.name == "AbbotChesterEnemy(Clone)1" || animPlayer.name == "LordEdwardEnemy(Clone)10" || animPlayer.name == "LordEdwardReeve Variant(Clone)4" || animPlayer.name == "Panther(Clone)0" || animPlayer.name == "Panther(Clone)1" || animPlayer.name == "Panther(Clone)2" || animPlayer.name == "DireWolf(Clone)0" || animPlayer.name == "DireWolf(Clone)1" || animPlayer.name == "DireWolf(Clone)2" || animPlayer.name == "DireWolf(Clone)3" || animPlayer.name == "DireWolf(Clone)4" || animPlayer.name == "DireWolf(Clone)5" /*|| animPlayer.name == "Hound(Clone)0" || animPlayer.name == "Hound(Clone)1" || animPlayer.name == "Hound(Clone)2"*/ || animPlayer.name == "DeathWeight(Clone)3") && animPlayer.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount <= 0.6 && animPlayer.GetComponent<EntityGroup>().specialAttack == 0)
            {
                Globals.aiSpeacialEffect = true;
                if (animPlayer.name == "AbbotChesterEnemy(Clone)1")
                {
                    Debug.Log("abott chester......................");
                    playble.playableAsset = Resources.Load("Playables/Battle Scene/AbbottChester_Dialog") as TimelineAsset;
                    playble.Play();
                }
                else if (animPlayer.name == "LordEdwardEnemy(Clone)10")
                {
                    Debug.Log("loard edward enemy........");
                    playble.playableAsset = Resources.Load("Playables/Battle Scene/LordEdward_Dialog") as TimelineAsset;
                    playble.Play();
                }
                else if (animPlayer.name == "LordEdwardReeve Variant(Clone)4" && animPlayer.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount <= 0.5)  //LordEdwardEnemy(Clone)4
                {
                    Debug.Log("loard edward enemy.......variant.");
                    playble.playableAsset = Resources.Load("Playables/Battle Scene/LordEdward_ReAnimated_Dialog") as TimelineAsset;
                    playble.Play();
                    //animPlayer.GetComponent<EntityGroup>().darkMagic.SetActive(true);
                }
                else if (animPlayer.name == "DireWolf(Clone)0" || animPlayer.name == "DireWolf(Clone)1" || animPlayer.name == "DireWolf(Clone)2" || animPlayer.name == "DireWolf(Clone)3" || animPlayer.name == "DireWolf(Clone)4" || animPlayer.name == "DireWolf(Clone)5")
                {
                    Debug.Log("direwolf..........");
                    playble.playableAsset = Resources.Load("Playables/Battle Scene/Direwolf_Dialog") as TimelineAsset;
                    playble.Play();
                }
            }
            else if (animPlayer.name == "LordAlfredEnemy(Clone)7")
            {
                Globals.aiSpeacialEffect = true;
                playble.playableAsset = Resources.Load("Playables/Battle Scene/LordAlfred_Dialog") as TimelineAsset;
                playble.Play();
                animPlayer.GetComponent<EntityGroup>().powerStrike.SetActive(true);
                animPlayer.GetComponent<EntityGroup>().powerStrike.GetComponent<ParticleSystem>().Play();
            }
            iTween.MoveTo(animPlayer.gameObject, iTween.Hash("position", moveF, "time", 0.21f, "islocal", true, "easeType", iTween.EaseType.linear, "looptype", iTween.LoopType.none, "oncomplete", "PlayAttackAnim", "oncompletetarget", gameObject));
        }
        else if (animPlayer.tag == "Player" || animPlayer.tag == "Companion")
        {
            Debug.Log("active scene ::::::::: " + Globals.activeScene + "attack or no attack :: " + animPlayer.GetComponent<EntityGroup>().noAttack);
            if (animPlayer.GetComponent<EntityGroup>().noAttack > 0 && (Globals.activeScene == Globals.CurrentScene.BarghestVillage || Globals.activeScene == Globals.CurrentScene.BarghestLairDengeon || Globals.activeScene == Globals.CurrentScene.HuntingtonThroneRoom))
            {
                Debug.Log("inside this condition");
                // CancelInvoke();
                // animPlayer.GetComponent<EntityGroup>().noAttack = 0;
                playerIndex++;
                // tempList.Remove(tempList[OtherIndex]);
                // ButtonSetting();
                petrifydelay = true;
                InvokeRepeating("TimeBar", 0.05f, 0.05f);
                // InvokeRepeating("CheckingForTimer", 0.1f, 0.1f);
                Debug.Log("no attack false");
                tempList[OtherIndex].GetComponent<EntityGroup>().selectionEffect.SetActive(true);
                Attack.interactable = false;
                Attack.GetComponent<Animator>().enabled = false;
                if (Globals.activeScene != Globals.CurrentScene.BarghestVillage)
                    tempList[OtherIndex].GetComponent<EntityGroup>().noAttack = 0;

                if (Globals.shopMerchant.Ale >= 1 || Globals.shopMerchant.HealPotion >= 1 || Globals.shopMerchant.Meat >= 1 || Globals.shopMerchant.Food >= 1 || Globals.shopMerchant.Rum >= 1 || Globals.shopMerchant.CurePotion >= 1)
                {
                    Heal.interactable = true;
                    Heal.GetComponent<Animator>().enabled = true;
                }
                else
                    Heal.interactable = false;

                StartCoroutine(PetrifiedDelay());  // petrify delay for barghest
                animPlayer.GetComponent<EntityGroup>().namePopup.SetActive(false);
                return;
            }
            else
            {
                CancelInvoke();
                ProtagnistTeamSetting(false);
                if (animPlayer.name == "Marium(Clone)")
                {
                    moveF = new Vector3(animPlayer.transform.localPosition.x - 3f, animPlayer.transform.localPosition.y, 0);
                    if (Globals.inventoryMarium.Dragger == 0 && Globals.inventoryMarium.ShortSword == 0 && Globals.inventoryMarium.ShortAxe == 0 && Globals.inventoryMarium.ShortBow == 0 && Globals.inventoryMarium.Warhammer == 0 && Globals.inventoryMarium.Spear == 0 && Globals.inventoryMarium.LongBow == 0)
                        moveF = new Vector3(animPlayer.transform.localPosition.x, animPlayer.transform.localPosition.y, 0);
                    else if (Globals.inventoryMarium.WeaponAttack == "shortBow" || Globals.inventoryMarium.WeaponAttack == "longBow")
                    {
                        Debug.Log("here................marium");
                        moveF = new Vector3(animPlayer.transform.localPosition.x, animPlayer.transform.localPosition.y, 0);
                    }
                    else
                        moveF = new Vector3(animPlayer.transform.localPosition.x - 3f, animPlayer.transform.localPosition.y, 0);

                }
                else if (animPlayer.name == "ArcherMale(Clone)" || animPlayer.name == "ArcherFemale(Clone)")
                {
                    if (Globals.inventoryProtagnist.AttackWeapon == "Spear")
                    {
                        moveF = new Vector3(animPlayer.transform.localPosition.x - 3f, animPlayer.transform.localPosition.y, 0);
                    }
                    else if ((Globals.inventoryProtagnist.Dragger == 0 && Globals.inventoryProtagnist.ShortSword == 0 && Globals.inventoryProtagnist.ShortAxe == 0 && Globals.inventoryProtagnist.ShortBow == 0 && Globals.inventoryProtagnist.Warhammer == 0 && Globals.inventoryProtagnist.Spear == 0 && Globals.inventoryProtagnist.LongBow == 0 && Globals.inventoryProtagnist.Warhammer == 0 && Globals.inventoryProtagnist.Club == 0 && Globals.inventoryProtagnist.LongAxe == 0 && Globals.inventoryProtagnist.Flair == 0 && Globals.inventoryProtagnist.Maul == 0) || Globals.inventoryProtagnist.AttackWeapon == "shortBow" || Globals.inventoryProtagnist.AttackWeapon == "longBow")
                        moveF = new Vector3(animPlayer.transform.localPosition.x, animPlayer.transform.localPosition.y, 0);
                    else
                        moveF = new Vector3(animPlayer.transform.localPosition.x - 3f, animPlayer.transform.localPosition.y, 0);
                }
                else
                    moveF = new Vector3(animPlayer.transform.localPosition.x - 3f, animPlayer.transform.localPosition.y, 0);
                // animPlayer.GetComponent<EntityGroup>().prespFaceL.GetComponent<MeshRenderer>().sortingOrder = 1;
                if (!isChoose)
                {
                    Debug.Log("not choose...........");
                    InvokeRepeating("TimeBar", 0.05f, 0.05f);
                    InvokeRepeating("CheckingForTimer", 0.1f, 0.1f);
                }
                else
                {
                    Debug.Log("choose...........");
                    iTween.MoveTo(animPlayer.gameObject, iTween.Hash("position", moveF, "time", 0.21f, "islocal", true, "easeType", iTween.EaseType.linear, "looptype", iTween.LoopType.none, "oncomplete", "PlayAttackAnim", "oncompletetarget", gameObject));

                }
            }
        }
        animPlayer.GetComponent<EntityGroup>().namePopup.SetActive(false);
        MyTeamSetUp();
    }
    IEnumerator PetrifiedDelay()
    {
        yield return new WaitForSeconds(7f);
        if (!cureused)
        {
            Debug.Log("petrify delay..........");
            tempList[OtherIndex].GetComponent<EntityGroup>().selectionEffect.SetActive(false);
            tempList.Remove(tempList[OtherIndex]);
            CancelInvoke("TimeBar");
            CancelInvoke("CheckingForTimer");
            bar.GetComponent<Image>().fillAmount = 1;
            ButtonSetting();
            StartCoroutine(SwitchTurn());
        }
        else
        {
            Debug.Log("petrify delay.......... cure used");
            cureused = false;
        }

    }
    void CheckingForTimer()
    {
        if (bar.GetComponent<Image>().fillAmount <= 0)
            OnCompleteTimer();
    }
    void OnCompleteTimer()
    {

        attackEntity = "";
        CancelInvoke("TimeBar");
        CancelInvoke("CheckingForTimer");
        ButtonActive();
        //if (petrifydelay)
        //{
        //    petrifydelay = false;
        //    ButtonSetting();
        //    StartCoroutine(SwitchTurn());
        //}
        if (attackEntity == "Attack")
            iTween.MoveTo(animPlayer.gameObject, iTween.Hash("position", moveF, "time", 0.21f, "islocal", true, "easeType", iTween.EaseType.linear, "looptype", iTween.LoopType.none, "oncomplete", "PlayAttackAnim", "oncompletetarget", gameObject));
        else if (attackEntity == "Defence")
        {
            isDefence = true;
            DefenceAnim();
            // iTween.MoveTo(animPlayer.gameObject, iTween.Hash("position", moveF, "time", 0.21f, "islocal", true, "easeType", iTween.EaseType.linear, "looptype", iTween.LoopType.none, "oncomplete", "DefenceAnim", "oncompletetarget", gameObject));
        }
        ProtagnistTeamSetting(false);
    }
    void CommonPart()
    {
        if (Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon && Globals.thirdFight)
            Debug.Log("start common part");
        ProtagnistTeamSetting(false);
        CancelInvoke("TimeBar");
        CancelInvoke("CheckingForTimer");
        BottomBarManage();
        ButtonSetting();
    }
    public void LightningBolt()
    {
        isLightning = true;
        StartCoroutine(LighteningEffect());
        AttackDone();
    }
    IEnumerator LighteningEffect()
    {
        yield return new WaitForSeconds(0.6f);
        tempList[OtherIndex].GetComponent<EntityGroup>().lightEffect.SetActive(true);
    }
    public void DeadEyeShot()
    {
        isdeadEye = true;
        AttackDone();
    }
    public void CriticalStrike()
    {
        iscritical = true;
        StartCoroutine(CriticalEffect());
        AttackDone();
    }
    IEnumerator CriticalEffect()
    {
        yield return new WaitForSeconds(0.6f);
        tempList[OtherIndex].GetComponent<EntityGroup>().criticalEff.SetActive(true);
    }
    public void Bandage()
    {
        PanelsSetting(false, false, true);
    }
    public void ProtagnistBandage()
    {
        character.GetComponent<EntityGroup>().healEff.SetActive(true);
        character.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount = 1;
        character.GetComponent<EntityGroup>().increaseHealth = true;
        BandageCommon();
    }
    public void JohnBandage(string name)
    {
        foreach (var v in myTeam)
        {
            if (v.name == name)
            {
                Debug.Log("name::" + v.name);
                v.GetComponent<EntityGroup>().healEff.SetActive(true);
                v.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount = 1;
                v.GetComponent<EntityGroup>().increaseHealth = true;
            }
        }
        BandageCommon();
    }
    void BandageCommon()
    {
        attackDone = true;
        DisableBandage(false, false, false, false);
        tempList[OtherIndex].GetComponent<EntityGroup>().bandageProperty = 1;
        tempList.Remove(tempList[OtherIndex]);
        ButtonSetting();
        StartCoroutine(SwitchTurn());
        AttackBack();
    }
    void DisableBandage(bool ch, bool j, bool m, bool t)
    {
        Globals.bandageHandler.protagnist.SetActive(ch);
        Globals.bandageHandler.john.SetActive(j);
        Globals.bandageHandler.marium.SetActive(m);
        Globals.bandageHandler.tucker.SetActive(t);
    }
    public void StealthAttack()
    {
        isStealth = true;
        AttackDone();
    }
    public void AttackOption()
    {
        if (healAssets.activeInHierarchy)
            healAssets.SetActive(false);
        CommonPart();
        PanelsSetting(false, true, false);
        Globals.specialAttack.SettingOfButtons();

    }
    public void AttackBack()
    {
        PanelsSetting(true, false, false);
        if (!attackDone)
            ButtonActive();
    }
    void PanelsSetting(bool att, bool spe, bool ban)
    {
        attackPanel.SetActive(att);
        specialAttackPanel.SetActive(spe);
        bandagePanel.SetActive(ban);
    }
    public void AttackDone()
    {
        if (Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon && Globals.thirdFight)
            Debug.Log("attack done::" + animPlayer.name + enemys.Count);
        CommonPart();
        if (Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon && Globals.thirdFight)
            Debug.Log("after common part");
        iTween.MoveTo(animPlayer.gameObject, iTween.Hash("position", moveF, "time", 0.21f, "islocal", true, "easeType", iTween.EaseType.linear, "looptype", iTween.LoopType.none, "oncomplete", "PlayAttackAnim", "oncompletetarget", gameObject));
        if (Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon && Globals.thirdFight)
            Debug.Log("after attack anim");
        attackDone = true;
        if (Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon && Globals.thirdFight)
        {
            if (Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon && Globals.thirdFight && waveCount == 2)
                Debug.Log("end" + enemys.Count);
            foreach (var v in enemys)
            {
                Debug.Log("name::" + v.name);
            }
        }
    }
    void DefenceComplete()
    {
        Debug.Log("complete defence");
        ProtagnistTeamSetting(false);
        CancelInvoke("TimeBar");
        CancelInvoke("CheckingForTimer");
        BottomBarManage();
        DefenceAnim();
        ButtonSetting();
    }

    void BottomBarManage()
    {
        Attack.GetComponent<Animator>().enabled = false;
        Defence.GetComponent<Animator>().enabled = false;
        Heal.GetComponent<Animator>().enabled = false;
        Attack.transform.localScale = new Vector3(1, 1, 1);
        Defence.transform.localScale = new Vector3(1, 1, 1);
        Heal.transform.localScale = new Vector3(1, 1, 1);
        if (Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon && Globals.thirdFight)
            Debug.Log("end bottom bar");
    }
    void TimeBar()
    {
        bar.GetComponent<Image>().fillAmount = 1;
        bar.GetComponent<Image>().fillAmount = Mathf.Lerp(0, 1, t);
        t -= Time.deltaTime * lerpSpeed;
    }
    void ButtonSetting()
    {
        Debug.Log("here.......??????????");
        Attack.interactable = false;
        Defence.interactable = false;
        Heal.interactable = false;
        Attack.GetComponent<Animator>().enabled = false;
        Defence.GetComponent<Animator>().enabled = false;
        Heal.GetComponent<Animator>().enabled = false;
        Attack.transform.localScale = new Vector3(1, 1, 1);
        Defence.transform.localScale = new Vector3(1, 1, 1);
        Heal.transform.localScale = new Vector3(1, 1, 1);
        if (Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon && Globals.thirdFight)
            Debug.Log("end button setting");
    }
}
