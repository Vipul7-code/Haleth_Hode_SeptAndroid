using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System;
using HelthHolde;

public class Globals
{
    public static LevelManager levelManager;
    public static SoundSetting soundSetting;
    public static UIManager uiManager;
    public static UIHandler uiHandler;
    public static ChurchHandler churchHandler;
    public static GameCenterHandler gameCenterHandler;
    public static CharacterSlot characterSlot;
    public static bool isHome,isChurch,isGameStart,isShop,isBattle,isSavePoint,isPart1Battle,secondWave,backToVill,isShrine,ishault,shopCounter,innSecond,isHound,isWolf,isBarghest,isskelton,iszombie,isFirstTut;
    public static BattleManager battleManager;
    public static int conversationCount;
    public static string converstionMessage,slot,attachment,weaponName,skinName,selectedInventoryCharacter;
    public static Player playerState = new Player();
    public static string accessToken;
    public static PlayerItemLibrary lastActiveLib;
    public static string id;
    public static Sprite imageUse;
    public static GameObject collideObject;
    public static CaravanAndPetrols randomHandler;
    public static SelectedAvatar avatarState = new SelectedAvatar();
    public static MerchantShop shopMerchant = new MerchantShop();
    public static JohnInventory inventoryJohn = new JohnInventory();
    public static MariumInventory inventoryMarium = new MariumInventory();
    public static TuckerInventory inventoryTucker = new TuckerInventory();
    public static ProtagnistInventory inventoryProtagnist = new ProtagnistInventory();
    public static LoadGame loadGame = new LoadGame();
    public static userData user = new userData();
    public static Inventory inventory = new Inventory();
    public static GameObject activePlayer;
    public static List<Inventory> inventoryData=new List<Inventory>();
    public static List<MerchantShop> merchantData = new List<MerchantShop>();
    public static List<JohnInventory> johnData = new List<JohnInventory>();
    public static List<MariumInventory> mariumData = new List<MariumInventory>();
    public static List<TuckerInventory> tuckerData = new List<TuckerInventory>();
    public static List<ProtagnistInventory> protagnistData = new List<ProtagnistInventory>();
    public static string activePart;
    public static GameController gameController;
    public static ChatInteraction chat;
    public static AtWaterVillage atwater;
    public static HunsvilleChurch hunsChurch;
    public static MonestryExt monestryExt;
    public static MonestryCellarInt monestryCellar;
    public static MonestryTuckerJoin monestryTuckerJoin;
    public static MotteAndBaileyCastle motteBaileyCastle;
    public static HunsvilleExt hunsExt;
    public static InventoryHandler inventoryHandler;
    public static BarghestVill barghestVill;
    public static TutorialPart tutorialPart;
    public static BargestLierDengeon barghestDengeon;
    public static DeathWeightVill deathWeight;
    public static DeathWeightDengeon deathWeightDengeon;
    public static BrigandVill brigandVill;
    public static BrigandLierDengeon brigandLierDengeon;
    public static HuntigtonVill huntingtonVill;
    public static CastleEscapeTunnel castleTunnel;
    public static HuntingtonCastle huntingtonCastle;
    public static BarghestShop barghestShop;
    public static Controller controller;
    public static SpecialAttckHandler specialAttack;
    public static BandageHadler bandageHandler;
    public static HuntingtonChurchManager huntingtonChurch;
    public static InnController innController;
    public static HuntingtonController huntingtonContt;
    public static StatisticsRecords staticRecord;
    public static int waveCount,brigandCount;
    public static GameObject player,companion, AI1, AI2, AI3, AI4, AI5;
    public static DialogData library;
    public static MessageData message;
    public static QuestHandler questHandler;
    public static ArmorImplementation armourImplimentation;
    public static List<String> barghestAnim = new List<String>();
    public static string lastRandom,sideName;
    public static List<string> selectedCompanions = new List<string>();
    public static GameObject[] positionUpdate;
    public static string objectiveScene,previousScene,playbleAssets;
    public static Objective currentObjective1;
    public static bool carvan1, caravan2, caravan3, petrol1, petrol2, petrol3,isAnim,isGuard,isbones,isRottenFlesh,isCompleteVid,isMotteyRetreat;
    public static string currentObjective,previosObjective;
    public static CurrentScene activeScene;
    public static CurrentRandom activeRandom;
    public static Vector3 lastPos,enterPos;
    public static bool secondVisitMon,monFrontDoor,beforeMottey,isShieldWall, isShopDialog, random,thirdWave,secondFight, thirdFight,forthFight,fifthFight, enterChurch,enterInn,enterShop,enterBarghestShop,completeEfforts, PlayNow,comBack,isMyTeam,isEnemyTeam, isFirstCompleteStory, isChurchComplete,leavingThrone,leavingsecondInn,leavingDengeon,enterMayor,enterFarmhouse,enterBlackSmith,isSmith,isArcher, isAcolyte,isSmithF,isArcherF,isAcolyteF,completeIntro,isArmorer, isSargent,sargentKill,isSpecial,first,drunkenGuy,grappingHook,isUiPause;
    public static int  soldierCampsiteVisit,secondVisit,againVisit, playerShied, playerHelmet, playerArmour,deathWightCount,storyCount,atWaterCount,caravnCount,petrolCount,indexCount,noOfCompanions,InnVisit,mayorVisit,merchantVisit,dengeonDoor,dengeonTreasure,innDialog;
    public static bool JohnAI, mariumAI, tuckerAI,sneakMon,isRandomAttack,isVeteran,isArcherBrigand,isCompleteGame,isT1,isT2,isT3,protagnistDefence,johnDefence,mariumDefence,tuckerDefence,aiSpeacialEffect,afterPromotion, isTunnel,isLightening,isSecondInn,isExploringTunnel,isRetreate;
    public static string johnAction, mariumAction, tuckerAction;
    public static List<string> caravanName = new List<string>();
    public static List<string> petrolsName = new List<string>();
    public static string characterName = "Robyn";
    public static int deathWightNumber = 0;
    public static Vector3 lastbrigandpos, lastposDeathWight;
    public static bool mbStart = false;
    public static PlayerController playerController;
    public static bool backToMenu = false;
    public static GameObject globalChar = null;
    public static bool healthDrainAttack = false;
    public static bool atwaterFinalFight = false;
    public static bool againCellerTucker = false;
    public static bool loadSaveGame = false;
    public static bool usedCurePotion = false;
    public static bool loadData = false;


    public enum CurrentRandom { None,caravans,petrols};
    public enum CurrentScene { None, SoldierCampsite, WagonCaravan, SecondSoldierCaravan, Huntsville, AtwaterVillage,RandomAttack, SacredPlace, SacredPlaceDengeon, MonkCampsite, monastery, CellarInt, CellarTucker, MotteAndBaileyCastle, BarghestVillage, BarghestLairDengeon, TheDeathWeight, TheDeathWeightDengeon, TheBrigand, BrigandLairDengeon, HuntingtonVillage, CastleEscapeTunnel, HuntingtonCastle,HuntingtonThroneRoom, HuntingtonCastleCourtyard, Tutorial, monasterywave };
public enum Objective { None, SoldierCampsite, WagonCaravan, SecondSoldierCaravan, Huntsville, AtwaterVillage, SacredPlace, SacredPlaceDengeon, MonkCampsite, monastery, CellarInt, CellarTucker,MotteAndBaileyCastle,BarghestVillage,BarghestLairDengeon,TheDeathWeight,TheDeathWeightDengeon,TheBrigand,BrigandLairDengeon,HuntingtonVillage,CastleEscapeTunnel,HuntingtonCastle, HuntingtonThroneRoom, HuntingtonCastleCourtyard,Tutorail };
    public class Player
    {
        [PrimaryKey]
        public int Key { get; set; }
        public int Friend { get; set; }
        public string GameCenterName { get; set; }
        public string GoogleName { get; set; }
        public string GoogleId { get; set; }
        public string GoogleEmail { get; set; }
        public string AccessToken { get; set; }
        public string GameCenterId { get; set; }
       public string companion1 { get; set; }
        public string companion2 { get; set; }
        public string companion3 { get; set; }
        public override string ToString()
        {
            return string.Format("[Player: Key={0}, Friend={1},GameCenterName={2},GoogleName={3},GoogleId={4},GoogleEmail={5},AccessToken={6},GameCenterId={7},companion1={8},companion2={9},companion3={10}]", Key, Friend, GameCenterName, GoogleName, GoogleId, GoogleEmail, AccessToken, GameCenterId,companion1,companion2,companion3);
        }

        public Player()
        {
            Key = 1;
        }
    }
   
    public enum Gender { Male, Female }
    public enum PlayerType { Player, AI ,Companion,prison}
    [Serializable]
    public class DialogDataItem
    {
        public CharacterItem playerData;
        public string dialogData;
        public PlayerType playerType;
        public Sprite pic;
    }
    public class MessageData
    {
        public DialogDataItem dialogData;
        public int dialogsCount;
    }
    public class SelectedAvatar
    {
        [PrimaryKey]
        public int Key { get; set;}
        public string AvatarName{ get; set;}
        public string AvatarType { get; set; }
       public int Sword { get; set; }
        public int Bow { get; set; }
        public int OtherWeapon { get; set; }
        public int Armour1 { get; set; }
        public int Armour2 { get; set; }
        public int Armour3 { get; set; }
        public string WeaponType { get; set;}
        public string SaveState { get; set;}
        public string SaveObjective { get; set; }
        public int secondVisit { get; set; }
        public int WeaponId { get; set; }
        public int ArmourId { get; set; }
        public int TotalXp { get; set;}
        public int Level { get; set;}
        public int IntroValue { get; set; }
        public int SoundLevel { get; set; }
        public int ControlLevel { get; set;}
        public int Smith { get; set; }
        public int Archer { get; set; }
        public int Priest { get; set; }
        public int SmithF { get; set; }
        public int ArcherF { get; set; }
        public int PriestF { get; set; }
        public int atwaterCount { get; set; }
        public int petrol { get; set; }
        public int carvan { get; set; }
        public int petrol1 { get; set; }
        public int petrol2 { get; set; }
        public int petrol3 { get; set; }
        public int carvan1 { get; set; }
        public int carvan2 { get; set; }
        public int carvan3 { get; set; }  // companionCount
        public int companionCount { get; set; }
        public float lastposx { get; set; }
        public float lastposy { get; set; }
        public override string ToString()
        {
            return string.Format("[SelectedAvatar: Key={0}, AvatarName={1},AvatarType={2},Sword={3},Bow={4},OtherWeapon={5},Armour1={6},Armour2={7},Armour3={8},WeaponType={9},SaveState={10},SaveObjective={11},WeaponId={12},ArmourId={13},TotalXp={14},Level={15},IntroValue={16},SoundLevel={17},ControlLevel={18},Smith={19},Archer={20},Priest={21},SmithF={22},ArcherF={23},PriestF={24},secondVisit={25}, atwaterCount={26}, petrol={27}, carvan={28}, petrol1={29},petrol2={30},petrol3={31}, carvan1={32},carvan2={33},carvan3={34}, companionCount={35}, lastposx={36}, lastposy={37}", Key, AvatarName,AvatarType,Sword,Bow,OtherWeapon,Armour1,Armour2,Armour3,WeaponType,SaveState,SaveObjective,secondVisit,WeaponId,ArmourId, TotalXp, Level,IntroValue,SoundLevel,ControlLevel,Smith,Archer,Priest,SmithF,ArcherF,PriestF, atwaterCount, petrol,carvan, petrol1,petrol2,petrol3, carvan1,carvan2,caravan3, companionCount,lastposx,lastposy);
        }
        public SelectedAvatar()
        {
            Key = 1;
        }
    }
    public class MerchantShop
    {
        [PrimaryKey]
        public int Id { get; set; }
        public int Dragger { get; set; }
        public int ShortSword { get; set; }
        public int ShortAxe { get; set; }
        public int Club { get; set; }
        public int ShortBow { get; set; }
        public int LongSword { get; set; }
        public int LongBow { get; set; }
        public int Mace { get; set; }
        public int Warhammer { get; set; }
        public int Spear { get; set; }
        public int LongAxe { get; set; }
        public int DoubleHeadedAxe { get; set; }
        public int Flair { get; set; }
        public int Maul { get; set; }
        public int CompositeBow { get; set; }
        public int CrossBow { get; set; }
        public int WoodenBuckler { get; set;}
        public int WoodenSmallRounded { get; set;}
        public int MetalBuckler { get; set;}
        public int MetalSmallRounded { get; set; }
        public int WoodenMediumShield { get; set; }
        public int MetalMediumShield { get; set; }
        public int WoodenKiteShield { get; set; }
        public int WoodenTowerShield { get; set; }
        public int MetalKiteShield { get; set; }
        public int ClothArmour { get; set; }
        public int PaddedArmour { get; set; }
        public int LeatherCap { get; set; }
        public int HideArmour { get; set; }

        public int LeatherArmour { get; set; }
        public int KettleHat { get; set; }
        public int BrigadineArmor { get; set; }
        public int ScaleArmour { get; set; }
        public int ChainArmour { get; set; }
        public int NesalHelmet { get; set; }
        public int Aventail { get; set; }
        public int MailCoif { get; set; }
        public int BreastPlateArmour { get; set; }
        public int RingMailArmour { get; set; }
        public int SplintmailArmor { get; set; }
        public int BandedMailArmour { get; set; }
        public int Ale { get; set; }
        public int CurePotion { get; set; }
        public int Food { get; set; }
        public int Meat { get; set; }
        public int HealPotion { get; set; }
        public int Rum { get; set; }
        public int Gold { get; set; }
        public string LastAvatar { get; set; }
        public int MagicSword { get; set; }
        public int DeathWightCloak { get; set; }
        public int SoulGem { get; set; }
        public int SoulEyeGems { get; set; }
        public int BarghestHeart { get; set; }
        public int Bones { get; set; }
        public int RottenFlesh { get; set; }
        public int DeathWightMace { get; set; }
        public override string ToString()
        {
            return string.Format("[MerchantShop: Id={0}, Dragger={1},ShortSword={2},ShortAxe={3},Club={4},ShortBow={5},LongSword={6},LongBow={7},Mace={8},Warhammer={9},Spear={10},LongAxe={11},DoubleHeadedAxe={12},Flair={13},Maul={14},CompositeBow={15},CrossBow={16},WoodenBuckler={17},WoodenSmallRounded={18},MetalBuckler={19},MetalSmallRounded={20},WoodenMediumShield={21},MetalMediumShield={22},WoodenKiteShield={23},WoodenTowerShield={24},MetalKiteShield={25},ClothArmour={26},PaddedArmour={27},LeatherCap={28},HideArmour={29},LeatherArmour={30},KettleHat={31},BrigadineArmor={32},ScaleArmour={33},ChainArmour={34},NesalHelmet={35},Aventail={36},MailCoif={37},BreastPlateArmour={38},RingMailArmour={39},SplintmailArmor={40},BandedMailArmour={41},Ale={42},CurePotion={43},Food={44},Meat={45},HealPotion={46},Rum={47},Gold={48},LastAvatar={49},MagicSword={50},DeathWightCloak={51},SoulGem={52},SoulEyeGems={53},BarghestHeart={54},Bones={55},RottenFlesh={56}, DeathWightMace={57}", id, Dragger, ShortSword,ShortBow,ShortAxe,Club,LongSword,LongBow,Mace,Warhammer,Spear,LongAxe,DoubleHeadedAxe,Flair,Maul,CompositeBow,CrossBow,WoodenBuckler, WoodenSmallRounded, MetalBuckler, MetalSmallRounded, WoodenMediumShield, MetalMediumShield, WoodenKiteShield, WoodenTowerShield, MetalKiteShield, ClothArmour, PaddedArmour, LeatherCap, HideArmour, LeatherArmour, KettleHat, BrigadineArmor, ScaleArmour, ChainArmour, NesalHelmet, Aventail, MailCoif, BreastPlateArmour, RingMailArmour, SplintmailArmor, BandedMailArmour, Ale, CurePotion, Food, Meat, HealPotion, Rum,Gold,LastAvatar, MagicSword,DeathWightCloak,SoulGem,SoulEyeGems, BarghestHeart,Bones,RottenFlesh, DeathWightMace) ;
        }
        public MerchantShop()
        {
            Id = 1;
        }
    }
    public class JohnInventory
    {
        [PrimaryKey]
        public int Key { get; set; }
        public int Dragger { get; set; }
        public int ShortSword { get; set; }
        public int ShortAxe { get; set; }
        public int LongSword { get; set; }
        public int Mace { get; set; }
        public int Warhammer { get; set; }
        public int Spear { get; set; }
        public int LongAxe { get; set; }
        public int WoodenBuckler { get; set; }
        public int WoodenSmallRound { get; set; }
        public int metalBuckler { get; set; }
        public int metalSmallRound { get; set; }
        public int WoodenMedium { get; set; }
        public int metalMedium { get; set; }
        public int ClothArmour { get; set; }
        public int PaddedArmour { get; set; }
        public int LeatherCap { get; set; }
        public int HideArmour { get; set; }

        public int LeatherArmour { get; set; }
        public int KettleHat { get; set; }
        public int BrigadineArmour { get; set; }
        public int ScaleArmour { get; set; }
        public int ChainArmour { get; set; }
        public int NasalHelmet { get; set; }
        public int Avaintail { get; set; }
        public int MailCoif { get; set; }
        public int Ale { get; set; }
        public int Meat { get; set; }
        public int Food { get; set; }
        public int HealPotion { get; set; }
        public int CurePotion { get; set; }
        public int Rum { get; set; }
        public string WeaponAttack { get; set; }
        public string Armour { get; set; }
        public string Helmet { get; set; }
        public string Shield { get; set; }
        public override string ToString()
        {
            return string.Format("[JohnInventory: Key={0}, Dragger={1},ShortSword={2},ShortAxe={3},LongSword={4},Mace={5},Warhammer={6},Spear={7},LongAxe={8},WoodenBuckler={9},WoodenSmallRounded={10},MetalBuckler={11},MetalSmallRounded={12},WoodenMediumShield={13},MetalMediumShield={14},ClothArmour={15},PaddedArmour={16},LeatherCap={17},HideArmour={18},LeatherArmour={19},KettleHat={20},BrigadineArmor={21},ScaleArmour={22},ChainArmour={23},NesalHelmet={24},Aventail={25},MailCoif={26},Ale={27},Meat={28},Food={29},HealPotion={30},CurePotion={31},Rum={32},WeaponAttack={33},Armour={34},Helmet={35}, Shield={36}", id, Dragger, ShortSword, ShortAxe, LongSword, Mace, Warhammer, Spear, LongAxe, WoodenBuckler, WoodenSmallRound, metalBuckler, metalSmallRound, WoodenMedium, metalMedium, ClothArmour, PaddedArmour, LeatherCap, HideArmour, LeatherArmour, KettleHat, BrigadineArmour, ScaleArmour, ChainArmour, NasalHelmet, Avaintail, MailCoif,Ale,Meat,Food,HealPotion,CurePotion,Rum, WeaponAttack, Armour, Helmet, Shield);
        }
        public JohnInventory()
        {
            Key = 1;
        }
    }
    public class MariumInventory
    {
        [PrimaryKey]
        public int Key { get; set; }
        public int Dragger { get; set; }
        public int ShortSword { get; set; }
        public int ShortAxe { get; set; }
        public int ShortBow { get; set; }
        public int Warhammer { get; set; }
        public int Spear { get; set; }
        public int LongBow { get; set; }
        public int WoodenBuckler { get; set; }
        public int woodenSmall { get; set; }
        public int MetalBuckler { get; set; }
        public int MetalSmall { get; set; }
        public int ClothArmour { get; set; }
        public int PaddedArmour { get; set; }
        public int LeatherCap { get; set; }
        public int HideArmour { get; set; }

        public int LeatherArmour { get; set; }
        public int KettleHat { get; set; }
        public int BrigadineArmour { get; set; }
        public int NasalHelmet { get; set; }
        public int Ale { get; set; }
        public int Meat { get; set; }
        public int Food { get; set; }
        public int HealPotion { get; set; }
        public int CurePotion { get; set; }
        public int Rum { get; set; }
        public string WeaponAttack { get; set; }
        public string Armour { get; set; }
        public string Helmet { get; set; }
        public string Shield { get; set; }
        public override string ToString()
        {
            return string.Format("[MariumInventory: Key={0}, Dragger={1},ShortSword={2},ShortAxe={3},ShortBow={4},Warhammer={5},Spear={6},LongBow={7},WoodenBuckler={8},woodenSmall={9},MetalBuckler={10},MetalSmall={11},ClothArmour={12},PaddedArmour={13},LeatherCap={14},HideArmour={15},PaddedArmour={16},LeatherCap={17},HideArmour={18},LeatherArmour={19},KettleHat={20},BrigadineArmour={21},NasalHelmet={22},Ale={23},Meat={24},Food={25},HealPotion={26},CurePotion={27},Rum={28},WeaponAttack={29},Armour={30},Helmet={31}, Shield={32}", id, Dragger, ShortSword, ShortAxe, ShortBow, Warhammer, Spear, LongBow, WoodenBuckler, MetalBuckler, MetalBuckler, woodenSmall, MetalSmall, ClothArmour, PaddedArmour, LeatherCap, HideArmour, LeatherArmour, KettleHat, BrigadineArmour, NasalHelmet,Ale,Meat,Food,HealPotion,CurePotion,Rum,WeaponAttack, Armour, Helmet, Shield);
        }
        public MariumInventory()
        {
            Key = 1;
        }
    }
    public class TuckerInventory
    {
        [PrimaryKey]
        public int Key { get; set; }
        public int Dragger { get; set; }
        public int Mace { get; set; }
        public int Warhammer { get; set; }
        public int Flair { get; set; }
        public int Maul { get; set; }
        public int WoodenBuckler { get; set; }
        public int WoodenSmall { get; set; }
        public int MetalBuckler { get; set; }
        public int MetalSmall { get; set; }
        public int MetalMedium { get; set; }
        public int ClothArmour { get; set; }
        public int PaddedArmour { get; set; }
        public int HideArmour { get; set; }
        public int LeatherArmour { get; set; }
        public int LeatherCap { get; set; }
        public int KettleHat { get; set; }
        public int Ale { get; set; }
        public int Meat { get; set; }
        public int Food { get; set; }
        public int CurePotion { get; set; }
        public int HealPotion { get; set; }
        public int Rum { get; set; }
        public string WeaponAttack { get; set; }
        public string Armour { get; set; }
        public string Helmet { get; set; }
        public string Shield { get; set; }
        public override string ToString()
        {
            return string.Format("[TuckerInventory: Key={0}, Dragger={1},Mace={2},Warhammer={3},Flair={4},Maul={5},WoodenBuckler={6},WoodenSmall={7},MetalBuckler={8},MetalSmall={9},ClothArmour={10},PaddedArmour={11},HideArmour={12},LeatherArmour={13},LeatherCap={14},HideArmour={15},PaddedArmour={16},LeatherCap={17},KettleHat={18},Ale={19},Meat={20},Food={21},CurePotion={22},HealPotion={23},Rum={24},WeaponAttack={25},Armour={26},Helmet={27}, Shield={28}", id, Dragger, Mace, Warhammer, Flair, Maul, WoodenBuckler, WoodenSmall, MetalBuckler, MetalSmall, MetalMedium, ClothArmour, PaddedArmour, HideArmour, LeatherArmour,LeatherCap ,KettleHat,Ale,Meat,Food,CurePotion,HealPotion,Rum, WeaponAttack, Armour, Helmet, Shield);
        }
        public TuckerInventory()
        {
            Key = 1;
        }
    }
    public class ProtagnistInventory
    {
        [PrimaryKey]
        public int Key { get; set; }
        public int Dragger { get; set; }
        public int ShortSword { get; set; }
        public int ShortAxe { get; set; }
        public int Club { get; set; }
        public int ShortBow { get; set; }
        public int LongSword { get; set; }
        public int LongBow { get; set; }
        public int Mace { get; set; }
        public int Warhammer { get; set; }
        public int Spear { get; set; }
        public int LongAxe { get; set; }
        public int DoubleHeadedAxe { get; set; }
        public int Flair { get; set; }
        public int Maul { get; set; }
        public int CompositeBow { get; set; }
        public int CrossBow { get; set; }
        public int WoodenBuckler { get; set; }
        public int WoodenSmallRounded { get; set; }
        public int MetalBuckler { get; set; }
        public int MetalSmallRounded { get; set; }
        public int WoodenMediumShield { get; set; }
        public int MetalMediumShield { get; set; }
        public int WoodenKiteShield { get; set; }
        public int WoodenTowerShield { get; set; }
        public int MetalKiteShield { get; set; }
        public int ClothArmour { get; set; }
        public int PaddedArmour { get; set; }
        public int LeatherCap { get; set; }
        public int HideArmour { get; set; }
        public int LeatherArmour { get; set; }
        public int KettleHat { get; set; }
        public int BrigadineArmor { get; set; }
        public int ScaleArmour { get; set; }
        public int ChainArmour { get; set; }
        public int NesalHelmet { get; set; }
        public int Aventail { get; set; }
        public int MailCoif { get; set; }
        public int BreastPlateArmour { get; set; }
        public int RingMailArmour { get; set; }
        public int SplintmailArmor { get; set; }
        public int BandedMailArmour { get; set; }
        public int Ale { get; set; }
        public int Meat { get; set; }
        public int Food { get; set; }
        public int CurePotion { get; set; }
        public int HealPotion { get; set; }
        public int Rum { get; set; }
        public string AttackWeapon { get; set; }
        public string Armour { get; set; }
        public string Helmet { get; set; }
        public int magicSword { get; set; }
        public string Shield { get; set; }
        public override string ToString()
        {
            return string.Format("[ProtagnistInventory: Key={0},Dragger={1},ShortSword={2},ShortAxe={3},Club={4},ShortBow={5},LongSword={6},LongBow={7},Mace={8},Warhammer={9},Spear={10},LongAxe={11},DoubleHeadedAxe={12},Flair={13},Maul={14},CompositeBow={15},CrossBow={16},WoodenBuckler={17},WoodenSmallRounded={18},MetalBuckler={19},MetalSmallRounded={20},WoodenMediumShield={21},MetalMediumShield={22},WoodenKiteShield={23},WoodenTowerShield={24},MetalKiteShield={25},ClothArmour={26},PaddedArmour={27},LeatherCap={28},HideArmour={29},LeatherArmour={30},KettleHat={31},BrigadineArmor={32},ScaleArmour={33},ChainArmour={34},NesalHelmet={35},Aventail={36},MailCoif={37},BreastPlateArmour={38},RingMailArmour={39},SplintmailArmor={40},BandedMailArmour={41},Ale={41},Meat={42},Food={43},CurePotion={44},HealPotion={45},Rum={46},AttackWeapon={47},Armour={48},Helmet={49},magicSword={50}, Shield={51}]", Key,Dragger,ShortSword,ShortAxe,Club,ShortBow,LongSword,LongBow,Mace,Warhammer,Spear,LongAxe,DoubleHeadedAxe,Flair,Maul,CompositeBow,CrossBow,WoodenBuckler,WoodenSmallRounded,MetalBuckler,MetalSmallRounded,WoodenMediumShield,MetalMediumShield,WoodenKiteShield,WoodenTowerShield,MetalKiteShield,ClothArmour,PaddedArmour,LeatherCap,HideArmour,LeatherArmour,KettleHat,BrigadineArmor,ScaleArmour,ChainArmour,NesalHelmet,Aventail,MailCoif,BreastPlateArmour,RingMailArmour,SplintmailArmor,BandedMailArmour,Ale,Meat,Food,CurePotion,HealPotion,Rum, AttackWeapon, Armour, Helmet,magicSword,Shield);
        }
        public ProtagnistInventory()
        {
            Key = 1;
        }
    }
    public class LoadGame
    {
        [PrimaryKey]
        public int Key { get; set; }
        public int Xp1 { get; set; }
        public string SaveState1 { get; set; }
        public string SaveObjective1 { get; set; }
        public int secondVisit1 { get; set; }
        public int Xp2 { get; set; }
        public string SaveState2 { get; set; }
        public string SaveObjective2 { get; set; }
        public int secondVisit2 { get; set; }
        public int Xp3 { get; set; }
        public string SaveState3 { get; set; }
        public string SaveObjective3 { get; set; }
        public int secondVisit3 { get; set; }
        public int Xp4 { get; set; }
        public string SaveState4 { get; set; }
        public string SaveObjective4 { get; set; }
        public int secondVisit4 { get; set; }
        public int Xp5 {get; set;}
        public string SaveState5 { get; set; }
        public string SaveObjective5 { get; set; }
        public int secondVisit5 { get; set; }
        public int Xp6 { get; set; }
        public string SaveState6 { get; set; }
        public string SaveObjective6 { get; set; }
        public int secondVisit6 { get; set; }
        public string savePlayer1 { get; set; }
        public string savePlayer2 { get; set; }
        public string savePlayer3 { get; set; }
        public string savePlayer4 { get; set; }
        public string savePlayer5 { get; set; }
        public string savePlayer6 { get; set; }
        public int companionCount1 { get; set; }
        public int companionCount2 { get; set; }
        public int companionCount3 { get; set; }
        public int companionCount4 { get; set; }
        public int companionCount5 { get; set; }
        public int companionCount6 { get; set; }
        public int gold1 { get; set; }
        public int gold2 { get; set; }
        public int gold3 { get; set; }
        public int gold4 { get; set; }
        public int gold5 { get; set; }
        public int gold6 { get; set; }
        public float lastpos1x { get; set; }
        public float lastpos1y { get; set; }
        public float lastpos2x { get; set; }
        public float lastpos2y { get; set; }
        public float lastpos3x { get; set; }
        public float lastpos3y { get; set; }
        public float lastpos4x { get; set; }
        public float lastpos4y { get; set; }
        public float lastpos5x { get; set; }
        public float lastpos5y { get; set; }
        public float lastpos6x { get; set; }
        public float lastpos6y { get; set; }
        public int atwater1 { get; set; }
        public int atwater2 { get; set; }
        public int atwater3 { get; set; }
        public int atwater4 { get; set; }
        public int atwater5 { get; set; }
        public int atwater6 { get; set; }

        public override string ToString()
        {
            return string.Format("[LoadGame: Key={0},Xp1={1},SaveState1={2},SaveObjective1={3},Xp2={4},SaveState2={5},SaveObjective2={6},Xp3={7},SaveState3={8},SaveObjective3={9},Xp4={10},SaveState4={11},SaveObjective4={12},Xp5={13},SaveState5={14},SaveObjective5={15},Xp6={16},SaveState6={17},SaveObjective6={18},savePlayer1={19},savePlayer2={20},savePlayer3={21},savePlayer4={22},savePlayer5={23},savePlayer6={24},secondVisit1={25},secondVisit2={26},secondVisit3={27},secondVisit4={28},secondVisit5={29},secondVisit6={30},companionCount1={31},companionCount2={32},companionCount3={33},companionCount4={35},companionCount5={36},companionCount6={37},gold1={38},gold2={39},gold3={40},gold4={41},gold5={42},gold6={43},lastpos1x={44},lastpos1y={45},lastpos2x={46},lastpos2y={47}, lastpos3x={48},lastpos3y={49},lastpos4x={50},lastpos4y={51},lastpos5x={52},lastpos5y={53},lastpos6x={54},lastpos6y={55},atwater1={56},atwater2={57},atwater3={58},atwater4={59},atwater5={60},atwater6={61}", Key,Xp1,SaveState1,SaveObjective1,Xp2,SaveState2,SaveObjective2,Xp3,SaveState3,SaveObjective3,Xp4,SaveState4,SaveObjective4,Xp5,SaveState5,SaveObjective5,Xp6,SaveState6,SaveObjective6,savePlayer1,savePlayer2,savePlayer3,savePlayer4,savePlayer5,savePlayer6,secondVisit1,secondVisit2,secondVisit3,secondVisit4,secondVisit5,secondVisit6, companionCount1, companionCount2, companionCount3, companionCount4, companionCount5, companionCount6, gold1,gold2,gold3,gold4,gold5,gold6,lastpos1x,lastpos1y,lastpos2x,lastpos2y,lastpos3x,lastpos3y,lastpos4x,lastpos4y,lastpos5x,lastpos5y,lastpos6x,lastpos6y,atwater1,atwater2,atwater3,atwater4,atwater5,atwater6);
        }
        public LoadGame()
        {
            Key = 1;
        }
    }

    public class Inventory
    {
        [PrimaryKey]
        public int Key { get; set;}
        public string itemName { get; set;}
        public int value { get; set; }
        public override string ToString()
        {
            return string.Format("Inventory: Key={0},itemName={1},value={2}", Key, itemName, value);
        }
    }

    public class userData
    {
        public string uniquId;
        public string uniquname;
        public string uniquemail;
        public string Name;
        public string Id;
        public string armour1;
        public string armour2;
        public string armour3;
        public string weapon1;
        public string weapon2;
        public string weapon3;
        public userData()
        {
            uniquId = GameCenterHandler.uniquiId;
            uniquname = GameCenterHandler.uniquiName;
            uniquemail = GameCenterHandler.email;
            Name = GameCenterHandler.gameCenterName;
            Id = GameCenterHandler.gameCenterId;
            armour1 = GameCenterHandler.armour1;
            armour2 = GameCenterHandler.armour2;
            armour3 = GameCenterHandler.armour3;
            weapon1 = GameCenterHandler.weapon1;
            weapon2 = GameCenterHandler.weapon2;
            weapon3 = GameCenterHandler.weapon3;
        }
    }
  public static void ActiveFaces(GameObject player,bool front,bool back, bool left, bool right)
    {
        //if (player.name == "MoveableSoldiers" && activeScene!=CurrentScene.MotteAndBaileyCastle)
        //{
        //    player.GetComponent<NPCMovement>().enabled = false;
        //    player.transform.parent = null;
        //}
        player.GetComponent<EntityGroup>().frontFaces.SetActive(front);
        player.GetComponent<EntityGroup>().backFace.SetActive(back);
        player.GetComponent<EntityGroup>().prespFaceL.SetActive(left);
        player.GetComponent<EntityGroup>().prespFaceR.SetActive(right);
    }
    public static void ActiveControls(GameObject player, bool set)
    {
        if(player == null)
        {
            Debug.Log("player null");
        }
      
       // Debug.Log("merchant555555555" + player.GetComponent<EntityGroup>().controlPanel.name + " bool "+ set);
        player.GetComponent<EntityGroup>().controlPanel.SetActive(set);
     //   Debug.Log("merchant22222222");
        if (avatarState.ControlLevel==0)
        {
          //  Debug.Log("merchant333333333");
            player.GetComponent<PlayerController>().leftControl.SetActive(false);
            player.GetComponent<PlayerController>().rightControl.SetActive(true);
        }
        else
        {
          //  Debug.Log("merchant44444444");
            player.GetComponent<PlayerController>().leftControl.SetActive(true);
            player.GetComponent<PlayerController>().rightControl.SetActive(false);
        }
    }
    public static void UpdateLibrary(GameObject player, DialogData library)
    {
        player.GetComponent<ChatInteraction>().dialogLibrary = library;
    }

    public static void UpdateDefaultEquipment()
    {
        Debug.Log("here................"+noOfCompanions);
        if(Globals.noOfCompanions == 1)  // john joins
        {
            Globals.shopMerchant.ShortSword += 1;
            Globals.shopMerchant.WoodenSmallRounded += 1;
            Globals.shopMerchant.PaddedArmour += 1;

            Globals.inventoryJohn.WeaponAttack = "ShortSword";
            Globals.inventoryJohn.ShortSword = 1;
            Globals.inventoryJohn.WoodenSmallRound = 1;
            Globals.inventoryJohn.Shield = "WoodenRound";
            Globals.inventoryJohn.PaddedArmour = 1;
            Globals.inventoryJohn.Armour = "padded";
        }
        if (Globals.noOfCompanions == 2)  // marium joins
        {
            Globals.shopMerchant.ShortBow += 1;
            Globals.shopMerchant.PaddedArmour += 1;

            Globals.inventoryMarium.ShortBow += 1;
            Globals.inventoryMarium.PaddedArmour = 1;
            Globals.inventoryMarium.WeaponAttack = "shortBow";
            Globals.inventoryMarium.Armour = "padded";
        }
        if (Globals.noOfCompanions == 3)  // tucker joins
        {
            Globals.shopMerchant.Mace += 1;
            Globals.shopMerchant.PaddedArmour += 1;

            Globals.inventoryTucker.Mace += 1;
            Globals.inventoryTucker.PaddedArmour = 1;
            Globals.inventoryTucker.WeaponAttack = "Mace";
            Globals.inventoryTucker.Armour = "padded";
        }

    }

    public class SaveGame
    {
        [PrimaryKey]
        public int Id { get; set; }
        public int Dragger { get; set; }
        public int ShortSword { get; set; }
        public int ShortAxe { get; set; }
        public int Club { get; set; }
        public int ShortBow { get; set; }
        public int LongSword { get; set; }
        public int LongBow { get; set; }
        public int Mace { get; set; }
        public int Warhammer { get; set; }
        public int Spear { get; set; }
        public int LongAxe { get; set; }
        public int DoubleHeadedAxe { get; set; }
        public int Flair { get; set; }
        public int Maul { get; set; }
        public int CompositeBow { get; set; }
        public int CrossBow { get; set; }
        public int WoodenBuckler { get; set; }
        public int WoodenSmallRounded { get; set; }
        public int MetalBuckler { get; set; }
        public int MetalSmallRounded { get; set; }
        public int WoodenMediumShield { get; set; }
        public int MetalMediumShield { get; set; }
        public int WoodenKiteShield { get; set; }
        public int WoodenTowerShield { get; set; }
        public int MetalKiteShield { get; set; }
        public int ClothArmour { get; set; }
        public int PaddedArmour { get; set; }
        public int LeatherCap { get; set; }
        public int HideArmour { get; set; }

        public int LeatherArmour { get; set; }
        public int KettleHat { get; set; }
        public int BrigadineArmor { get; set; }
        public int ScaleArmour { get; set; }
        public int ChainArmour { get; set; }
        public int NesalHelmet { get; set; }
        public int Aventail { get; set; }
        public int MailCoif { get; set; }
        public int BreastPlateArmour { get; set; }
        public int RingMailArmour { get; set; }
        public int SplintmailArmor { get; set; }
        public int BandedMailArmour { get; set; }
        public int Ale { get; set; }
        public int CurePotion { get; set; }
        public int Food { get; set; }
        public int Meat { get; set; }
        public int HealPotion { get; set; }
        public int Rum { get; set; }
        public int Gold { get; set; }
        public string LastAvatar { get; set; }
        public int MagicSword { get; set; }
        public int DeathWightCloak { get; set; }
        public int SoulGem { get; set; }
        public int SoulEyeGems { get; set; }
        public int BarghestHeart { get; set; }
        public int Bones { get; set; }
        public int RottenFlesh { get; set; }
        public int DeathWightMace { get; set; }
        public string ProtagonistWeapon { get; set; }
        public string ProtagonistArmor { get; set; }
        public string ProtagonistShield { get; set; }
        public string ProtagonistHelmet { get; set; }
        public string JohnWeapon { get; set; }
        public string JohnArmor { get; set; }
        public string JohnShield { get; set; }
        public string JohnHelmet { get; set; }
        public string MariumWeapon { get; set; }
        public string MariumArmor { get; set; }
        public string MariumShield { get; set; }
        public string MariumHelmet { get; set; }
        public string TuckerWeapon { get; set; }
        public string TuckerArmor { get; set; }
        public string TuckerShield { get; set; }
        public string TuckerHelmet { get; set; }
        public override string ToString()
        {
            return string.Format("[Id={0}, Dragger={1},ShortSword={2},ShortAxe={3},Club={4},ShortBow={5},LongSword={6},LongBow={7},Mace={8},Warhammer={9},Spear={10},LongAxe={11},DoubleHeadedAxe={12},Flair={13},Maul={14},CompositeBow={15},CrossBow={16},WoodenBuckler={17},WoodenSmallRounded={18},MetalBuckler={19},MetalSmallRounded={20},WoodenMediumShield={21},MetalMediumShield={22},WoodenKiteShield={23},WoodenTowerShield={24},MetalKiteShield={25},ClothArmour={26},PaddedArmour={27},LeatherCap={28},HideArmour={29},LeatherArmour={30},KettleHat={31},BrigadineArmor={32},ScaleArmour={33},ChainArmour={34},NesalHelmet={35},Aventail={36},MailCoif={37},BreastPlateArmour={38},RingMailArmour={39},SplintmailArmor={40},BandedMailArmour={41},Ale={42},CurePotion={43},Food={44},Meat={45},HealPotion={46},Rum={47},Gold={48},LastAvatar={49},MagicSword={50},DeathWightCloak={51},SoulGem={52},SoulEyeGems={53},BarghestHeart={54},Bones={55},RottenFlesh={56}, DeathWightMace={57}, ProtagonistWeapon={58}, ProtagonistArmor={59}, ProtagonistShield={60},ProtagonistHelmet={61},JohnWeapon={62},JohnArmor={63},JohnShield={64},JohnHelmet={65},MariumWeapon={66},MariumArmor={67},MariumShield={68},MariumHelmet={69},TuckerWeapon={70},TuckerArmor={71},TuckerShield={72},TuckerHelmet={73}", id, Dragger, ShortSword, ShortBow, ShortAxe, Club, LongSword, LongBow, Mace, Warhammer, Spear, LongAxe, DoubleHeadedAxe, Flair, Maul, CompositeBow, CrossBow, WoodenBuckler, WoodenSmallRounded, MetalBuckler, MetalSmallRounded, WoodenMediumShield, MetalMediumShield, WoodenKiteShield, WoodenTowerShield, MetalKiteShield, ClothArmour, PaddedArmour, LeatherCap, HideArmour, LeatherArmour, KettleHat, BrigadineArmor, ScaleArmour, ChainArmour, NesalHelmet, Aventail, MailCoif, BreastPlateArmour, RingMailArmour, SplintmailArmor, BandedMailArmour, Ale, CurePotion, Food, Meat, HealPotion, Rum, Gold, LastAvatar, MagicSword, DeathWightCloak, SoulGem, SoulEyeGems, BarghestHeart, Bones, RottenFlesh, DeathWightMace,ProtagonistWeapon,ProtagonistArmor,ProtagonistShield,ProtagonistHelmet,JohnWeapon,JohnArmor,JohnShield,JohnHelmet,MariumWeapon,MariumArmor,MariumShield,MariumHelmet,TuckerWeapon,TuckerArmor,TuckerShield,TuckerHelmet);
        }
        public SaveGame()
        {
            if (!loadData)
            {
                Id = 1;
                Dragger = Globals.shopMerchant.Dragger;
                ShortSword = Globals.shopMerchant.ShortSword;
                ShortAxe = Globals.shopMerchant.ShortAxe;
                Club = Globals.shopMerchant.Club;
                ShortBow = Globals.shopMerchant.ShortBow;
                LongSword = Globals.shopMerchant.LongSword;
                LongBow = Globals.shopMerchant.LongBow;
                Mace = Globals.shopMerchant.Mace;
                Warhammer = Globals.shopMerchant.Warhammer;
                Spear = Globals.shopMerchant.Spear;
                LongAxe = Globals.shopMerchant.LongAxe;
                DoubleHeadedAxe = Globals.shopMerchant.DoubleHeadedAxe;
                Flair = Globals.shopMerchant.Flair;
                Maul = Globals.shopMerchant.Maul;
                CompositeBow = Globals.shopMerchant.CompositeBow;
                CrossBow = Globals.shopMerchant.CrossBow;
                WoodenBuckler = Globals.shopMerchant.WoodenBuckler;
                WoodenSmallRounded = Globals.shopMerchant.WoodenSmallRounded;
                MetalBuckler = Globals.shopMerchant.MetalBuckler;
                MetalSmallRounded = Globals.shopMerchant.MetalSmallRounded;
                WoodenMediumShield = Globals.shopMerchant.MetalMediumShield;
                MetalMediumShield = Globals.shopMerchant.MetalMediumShield;
                WoodenKiteShield = Globals.shopMerchant.WoodenKiteShield;
                WoodenTowerShield = Globals.shopMerchant.WoodenTowerShield;
                MetalKiteShield = Globals.shopMerchant.MetalKiteShield;
                PaddedArmour = Globals.shopMerchant.PaddedArmour;
                LeatherCap = Globals.shopMerchant.LeatherCap;
                HideArmour = Globals.shopMerchant.HideArmour;
                LeatherArmour = Globals.shopMerchant.LeatherArmour;
                KettleHat = Globals.shopMerchant.KettleHat;
                BrigadineArmor = Globals.shopMerchant.BrigadineArmor;
                ScaleArmour = Globals.shopMerchant.ScaleArmour;
                ChainArmour = Globals.shopMerchant.ChainArmour;
                NesalHelmet = Globals.shopMerchant.NesalHelmet;
                Aventail = Globals.shopMerchant.Aventail;
                MailCoif = Globals.shopMerchant.MailCoif;
                Ale = Globals.shopMerchant.Ale;
                CurePotion = Globals.shopMerchant.CurePotion;
                Food = Globals.shopMerchant.Food;
                Meat = Globals.shopMerchant.Meat;
                HealPotion = Globals.shopMerchant.HealPotion;
                Rum = Globals.shopMerchant.Rum;
                MagicSword = Globals.shopMerchant.MagicSword;
                DeathWightCloak = Globals.shopMerchant.DeathWightCloak;
                SoulGem = Globals.shopMerchant.SoulGem;
                SoulEyeGems = Globals.shopMerchant.SoulEyeGems;
                BarghestHeart = Globals.shopMerchant.BarghestHeart;
                Bones = Globals.shopMerchant.Bones;
                RottenFlesh = Globals.shopMerchant.RottenFlesh;
                ProtagonistWeapon = Globals.inventoryProtagnist.AttackWeapon;
                ProtagonistArmor = Globals.inventoryProtagnist.Armour;
                ProtagonistHelmet = Globals.inventoryProtagnist.Helmet;
                ProtagonistShield = Globals.inventoryProtagnist.Shield;
                MariumWeapon = Globals.inventoryMarium.WeaponAttack;
                MariumArmor = Globals.inventoryMarium.Armour;
                MariumHelmet = Globals.inventoryMarium.Helmet;
                MariumShield = Globals.inventoryMarium.Shield;
                TuckerWeapon = Globals.inventoryTucker.WeaponAttack;
                TuckerArmor = Globals.inventoryTucker.Armour;
                TuckerShield = Globals.inventoryTucker.Shield;
                TuckerHelmet = Globals.inventoryTucker.Helmet;
                JohnWeapon = Globals.inventoryJohn.WeaponAttack;
                JohnArmor = Globals.inventoryJohn.Armour;
                JohnHelmet = Globals.inventoryJohn.Helmet;
                JohnShield = Globals.inventoryJohn.Shield;

                Debug.Log("here.....................update");
            }
            else
            {
                Debug.Log("load data true");
                loadData = false;
            }
        }
    }
    public class SaveGame1 : SaveGame
    {
        public SaveGame1() : base()
        {
            Debug.Log("game 1 constructor");
        }

    }
    public class SaveGame2 : SaveGame
    {
        public SaveGame2() : base()
        {
            Debug.Log("game 2 constructor");
        }
        //[PrimaryKey]
        //public int Id { get; set; }
        //public int Dragger { get; set; }
        //public int ShortSword { get; set; }
        //public int ShortAxe { get; set; }
        //public int Club { get; set; }
        //public int ShortBow { get; set; }
        //public int LongSword { get; set; }
        //public int LongBow { get; set; }
        //public int Mace { get; set; }
        //public int Warhammer { get; set; }
        //public int Spear { get; set; }
        //public int LongAxe { get; set; }
        //public int DoubleHeadedAxe { get; set; }
        //public int Flair { get; set; }
        //public int Maul { get; set; }
        //public int CompositeBow { get; set; }
        //public int CrossBow { get; set; }
        //public int WoodenBuckler { get; set; }
        //public int WoodenSmallRounded { get; set; }
        //public int MetalBuckler { get; set; }
        //public int MetalSmallRounded { get; set; }
        //public int WoodenMediumShield { get; set; }
        //public int MetalMediumShield { get; set; }
        //public int WoodenKiteShield { get; set; }
        //public int WoodenTowerShield { get; set; }
        //public int MetalKiteShield { get; set; }
        //public int ClothArmour { get; set; }
        //public int PaddedArmour { get; set; }
        //public int LeatherCap { get; set; }
        //public int HideArmour { get; set; }

        //public int LeatherArmour { get; set; }
        //public int KettleHat { get; set; }
        //public int BrigadineArmor { get; set; }
        //public int ScaleArmour { get; set; }
        //public int ChainArmour { get; set; }
        //public int NesalHelmet { get; set; }
        //public int Aventail { get; set; }
        //public int MailCoif { get; set; }
        //public int BreastPlateArmour { get; set; }
        //public int RingMailArmour { get; set; }
        //public int SplintmailArmor { get; set; }
        //public int BandedMailArmour { get; set; }
        //public int Ale { get; set; }
        //public int CurePotion { get; set; }
        //public int Food { get; set; }
        //public int Meat { get; set; }
        //public int HealPotion { get; set; }
        //public int Rum { get; set; }
        //public int Gold { get; set; }
        //public string LastAvatar { get; set; }
        //public int MagicSword { get; set; }
        //public int DeathWightCloak { get; set; }
        //public int SoulGem { get; set; }
        //public int SoulEyeGems { get; set; }
        //public int BarghestHeart { get; set; }
        //public int Bones { get; set; }
        //public int RottenFlesh { get; set; }
        //public int DeathWightMace { get; set; }
        //public string ProtagonistWeapon { get; set; }
        //public string ProtagonistArmor { get; set; }
        //public string ProtagonistShield { get; set; }
        //public string ProtagonistHelmet { get; set; }
        //public string JohnWeapon { get; set; }
        //public string JohnArmor { get; set; }
        //public string JohnShield { get; set; }
        //public string JohnHelmet { get; set; }
        //public string MariumWeapon { get; set; }
        //public string MariumArmor { get; set; }
        //public string MariumShield { get; set; }
        //public string MariumHelmet { get; set; }
        //public string TuckerWeapon { get; set; }
        //public string TuckerArmor { get; set; }
        //public string TuckerShield { get; set; }
        //public string TuckerHelmet { get; set; }
        //public override string ToString()
        //{
        //    return string.Format("[SaveGame2: Id={0}, Dragger={1},ShortSword={2},ShortAxe={3},Club={4},ShortBow={5},LongSword={6},LongBow={7},Mace={8},Warhammer={9},Spear={10},LongAxe={11},DoubleHeadedAxe={12},Flair={13},Maul={14},CompositeBow={15},CrossBow={16},WoodenBuckler={17},WoodenSmallRounded={18},MetalBuckler={19},MetalSmallRounded={20},WoodenMediumShield={21},MetalMediumShield={22},WoodenKiteShield={23},WoodenTowerShield={24},MetalKiteShield={25},ClothArmour={26},PaddedArmour={27},LeatherCap={28},HideArmour={29},LeatherArmour={30},KettleHat={31},BrigadineArmor={32},ScaleArmour={33},ChainArmour={34},NesalHelmet={35},Aventail={36},MailCoif={37},BreastPlateArmour={38},RingMailArmour={39},SplintmailArmor={40},BandedMailArmour={41},Ale={42},CurePotion={43},Food={44},Meat={45},HealPotion={46},Rum={47},Gold={48},LastAvatar={49},MagicSword={50},DeathWightCloak={51},SoulGem={52},SoulEyeGems={53},BarghestHeart={54},Bones={55},RottenFlesh={56}, DeathWightMace={57}, ProtagonistWeapon={58}, ProtagonistArmor={59}, ProtagonistShield={60},ProtagonistHelmet={61},JohnWeapon={62},JohnArmor={63},JohnShield={64},JohnHelmet={65},MariumWeapon={66},MariumArmor={67},MariumShield={68},MariumHelmet={69},TuckerWeapon={70},TuckerArmor={71},TuckerShield={72},TuckerHelmet={73}", id, Dragger, ShortSword, ShortBow, ShortAxe, Club, LongSword, LongBow, Mace, Warhammer, Spear, LongAxe, DoubleHeadedAxe, Flair, Maul, CompositeBow, CrossBow, WoodenBuckler, WoodenSmallRounded, MetalBuckler, MetalSmallRounded, WoodenMediumShield, MetalMediumShield, WoodenKiteShield, WoodenTowerShield, MetalKiteShield, ClothArmour, PaddedArmour, LeatherCap, HideArmour, LeatherArmour, KettleHat, BrigadineArmor, ScaleArmour, ChainArmour, NesalHelmet, Aventail, MailCoif, BreastPlateArmour, RingMailArmour, SplintmailArmor, BandedMailArmour, Ale, CurePotion, Food, Meat, HealPotion, Rum, Gold, LastAvatar, MagicSword, DeathWightCloak, SoulGem, SoulEyeGems, BarghestHeart, Bones, RottenFlesh, DeathWightMace, ProtagonistWeapon, ProtagonistArmor, ProtagonistShield, ProtagonistHelmet, JohnWeapon, JohnArmor, JohnShield, JohnHelmet, MariumWeapon, MariumArmor, MariumShield, MariumHelmet, TuckerWeapon, TuckerArmor, TuckerShield, TuckerHelmet);
        //}
        //public SaveGame2()
        //{
        //    Id = 1;
        //}
    }
    public class SaveGame3 : SaveGame
    {
        public SaveGame3() : base()
        {
            Debug.Log("game 3 constructor");
        }

    }
    public class SaveGame4 : SaveGame
    {
        public SaveGame4() : base()
        {
            Debug.Log("game 4 constructor");
        }

    }
    public class SaveGame5 : SaveGame
    {
        public SaveGame5() : base()
        {
            Debug.Log("game 5 constructor");
        }

    }
    public class SaveGame6 : SaveGame
    {
        public SaveGame6() : base()
        {
            Debug.Log("game 6 constructor");
        }

    }
    public class ContinueGame : SaveGame
    {
        public ContinueGame() : base()
        {
            Debug.Log("continue game constructor");
        }

    }
}




