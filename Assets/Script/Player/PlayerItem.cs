using HelthHolde;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerItem : MonoBehaviour
{
    [HideInInspector]
    public int attack, shield, armour, helmet, weaponId, helmetId, armourId, shieldId;
    [HideInInspector]
    public float result, health, totalHealth, armorDefence;
    [HideInInspector]
    public int level, defence, damageValue;
    [HideInInspector]
    public float playerDefenceValue, totalWeaponDamage;
    public TypePlayer Type = TypePlayer.Player;
    public enum TypePlayer { Player, AI, Companion }
    Scene currentScene;
    [HideInInspector]
    public bool isMissed;
    // Start is called before the first frame update
    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        totalHealth = 1;
    }
    public void InitializePlayerItem(PlayerConfiguration playerInfo)
    {
        if (playerInfo.Type == PlayerConfiguration.PlayerType.Player || playerInfo.Type == PlayerConfiguration.PlayerType.Companion)
        {
            Debug.Log("player info :: " + playerInfo.health + " " + Globals.inventoryProtagnist.Spear);
            health = playerInfo.health * Globals.avatarState.Level;
            //attack = playerInfo.damageValue;
            defence = playerInfo.defence;
            if (playerInfo.Type == PlayerConfiguration.PlayerType.Player)
            {
                Debug.Log("inside...... " + playerInfo.health);
                if (Globals.inventoryProtagnist.WoodenBuckler == 1)
                    playerInfo.shield = 1;
                else if (Globals.inventoryProtagnist.WoodenSmallRounded == 1)
                    playerInfo.shield = 2;
                else if (Globals.inventoryProtagnist.WoodenMediumShield == 1)
                    playerInfo.shield = 3;
                else if (Globals.inventoryProtagnist.MetalBuckler == 1)
                    playerInfo.shield = 4;
                else if (Globals.inventoryProtagnist.MetalSmallRounded == 1)
                    playerInfo.shield = 5;
                else if (Globals.inventoryProtagnist.MetalMediumShield == 1)
                    playerInfo.shield = 6;
                else
                    playerInfo.shield = 0;

                if (Globals.inventoryProtagnist.PaddedArmour == 1)
                    playerInfo.armour = 1;
                else if (Globals.inventoryProtagnist.LeatherArmour == 1)
                    playerInfo.armour = 2;
                else if (Globals.inventoryProtagnist.HideArmour == 1)
                    playerInfo.armour = 3;
                else if (Globals.inventoryProtagnist.BrigadineArmor == 1)
                {
                    playerInfo.armour = 4;
                }

                else if (Globals.inventoryProtagnist.ScaleArmour == 1)
                    playerInfo.armour = 5;
                else if (Globals.inventoryProtagnist.ChainArmour == 1)
                {
                    if (playerInfo.characterName != "Priest")
                        playerInfo.armour = 6;
                }
                else
                    playerInfo.armour = 0;

                if (Globals.inventoryProtagnist.LeatherCap == 1)
                    playerInfo.helmet = 1;
                else if (Globals.inventoryProtagnist.KettleHat == 1)
                    playerInfo.helmet = 2;
                else if (Globals.inventoryProtagnist.NesalHelmet == 1)
                    playerInfo.helmet = 3;
                else if (Globals.inventoryProtagnist.Aventail == 1)
                    playerInfo.helmet = 4;
                else if (Globals.inventoryProtagnist.MailCoif == 1)
                    playerInfo.helmet = 5;
                else
                    playerInfo.helmet = 0;

                if (Globals.inventoryProtagnist.ShortSword == 1)
                    playerInfo.weapon = 0;
                else if (Globals.inventoryProtagnist.ShortAxe == 1)
                    playerInfo.weapon = 1;
                else if (Globals.inventoryProtagnist.LongSword == 1)
                    playerInfo.weapon = 2;
                else if (Globals.inventoryProtagnist.LongAxe == 1)
                    playerInfo.weapon = 3;
                else if (Globals.inventoryProtagnist.ShortBow == 1)
                    playerInfo.weapon = 4;
                else if (Globals.inventoryProtagnist.LongBow == 1)
                    playerInfo.weapon = 5;
                else if (Globals.inventoryProtagnist.Warhammer == 1)
                    playerInfo.weapon = 6;
                else if (Globals.inventoryProtagnist.Spear == 1)
                    playerInfo.weapon = 7;
                else if (Globals.inventoryProtagnist.Club == 1)
                    playerInfo.weapon = 8;
                else if (Globals.inventoryProtagnist.Mace == 1)
                    playerInfo.weapon = 9;
                else if (Globals.inventoryProtagnist.Flair == 1)
                    playerInfo.weapon = 10;
                else if (Globals.inventoryProtagnist.Maul == 1)
                    playerInfo.weapon = 11;
                else
                    playerInfo.weapon = 0;

            }
            else
            {
                if (playerInfo.characterName == "John")
                {
                    if (Globals.inventoryJohn.Dragger == 1)
                        playerInfo.weapon = 0;
                    else if (Globals.inventoryJohn.ShortSword == 1)
                        playerInfo.weapon = 1;
                    else if (Globals.inventoryJohn.ShortAxe == 1)
                        playerInfo.weapon = 2;
                    else if (Globals.inventoryJohn.Warhammer == 1)
                        playerInfo.weapon = 3;
                    else if (Globals.inventoryJohn.LongSword == 1)
                        playerInfo.weapon = 4;
                    else if (Globals.inventoryJohn.LongAxe == 1)
                        playerInfo.weapon = 5;
                    else if (Globals.inventoryJohn.Mace == 1)
                        playerInfo.weapon = 6;
                    else if (Globals.inventoryJohn.Spear == 1)
                        playerInfo.weapon = 7;

                    if (Globals.inventoryJohn.LeatherCap == 1)
                        playerInfo.helmet = 1;
                    else if (Globals.inventoryJohn.KettleHat == 1)
                        playerInfo.helmet = 2;
                    else if (Globals.inventoryJohn.NasalHelmet == 1)
                        playerInfo.helmet = 3;
                    else if (Globals.inventoryJohn.Avaintail == 1)
                        playerInfo.helmet = 4;
                    else if (Globals.inventoryJohn.MailCoif == 1)
                        playerInfo.helmet = 5;
                    else
                        playerInfo.helmet = 0;

                    if (Globals.inventoryJohn.WoodenBuckler == 1)
                        playerInfo.shield = 1;
                    else if (Globals.inventoryJohn.WoodenSmallRound == 1)
                        playerInfo.shield = 2;
                    else if (Globals.inventoryJohn.WoodenMedium == 1)
                        playerInfo.shield = 3;
                    else if (Globals.inventoryJohn.metalBuckler == 1)
                        playerInfo.shield = 4;
                    else if (Globals.inventoryJohn.metalSmallRound == 1)
                        playerInfo.shield = 5;
                    else if (Globals.inventoryJohn.metalMedium == 1)
                        playerInfo.shield = 6;
                    else
                        playerInfo.shield = 0;

                    if (Globals.inventoryJohn.PaddedArmour == 1)
                        playerInfo.armour = 1;
                    else if (Globals.inventoryJohn.LeatherArmour == 1)
                        playerInfo.armour = 2;
                    else if (Globals.inventoryJohn.HideArmour == 1)
                        playerInfo.armour = 3;
                    else if (Globals.inventoryJohn.BrigadineArmour == 1)
                        playerInfo.armour = 4;
                    else if (Globals.inventoryJohn.ScaleArmour == 1)
                        playerInfo.armour = 5;
                    else if (Globals.inventoryJohn.ChainArmour == 1)
                        playerInfo.armour = 6;
                    else
                        playerInfo.armour = 0;
                }
                if (playerInfo.characterName == "Marium")
                {
                    if (Globals.inventoryMarium.Dragger == 1)
                        playerInfo.weapon = 0;
                    else if (Globals.inventoryMarium.ShortSword == 1)
                        playerInfo.weapon = 1;
                    else if (Globals.inventoryMarium.ShortAxe == 1)
                        playerInfo.weapon = 2;
                    else if (Globals.inventoryMarium.Warhammer == 1)
                        playerInfo.weapon = 3;
                    else if (Globals.inventoryMarium.ShortBow == 1)
                        playerInfo.weapon = 4;
                    else if (Globals.inventoryMarium.LongBow == 1)
                        playerInfo.weapon = 5;
                    else if (Globals.inventoryMarium.Spear == 1)
                        playerInfo.weapon = 6;

                    if (Globals.inventoryMarium.LeatherCap == 1)
                        playerInfo.helmet = 1;
                    else if (Globals.inventoryMarium.KettleHat == 1)
                        playerInfo.helmet = 2;
                    else if (Globals.inventoryMarium.NasalHelmet == 1)
                        playerInfo.helmet = 3;
                    else
                        playerInfo.helmet = 0;

                    if (Globals.inventoryMarium.WoodenBuckler == 1)
                        playerInfo.shield = 1;
                    else if (Globals.inventoryMarium.woodenSmall == 1)
                        playerInfo.shield = 2;
                    else if (Globals.inventoryMarium.MetalBuckler == 1)
                        playerInfo.shield = 3;
                    else if (Globals.inventoryMarium.MetalSmall == 1)
                        playerInfo.shield = 4;
                    else
                        playerInfo.shield = 0;

                    if (Globals.inventoryMarium.PaddedArmour == 1)
                        playerInfo.armour = 1;
                    else if (Globals.inventoryMarium.LeatherArmour == 1)
                        playerInfo.armour = 2;
                    else if (Globals.inventoryMarium.HideArmour == 1)
                        playerInfo.armour = 3;
                    else if (Globals.inventoryMarium.BrigadineArmour == 1)
                        playerInfo.armour = 4;
                    else
                        playerInfo.armour = 0;
                }
                if (playerInfo.characterName == "Tucker")
                {
                    if (Globals.inventoryTucker.Dragger == 1)
                        playerInfo.weapon = 0;
                    else if (Globals.inventoryTucker.Mace == 1)
                        playerInfo.weapon = 1;
                    else if (Globals.inventoryTucker.Warhammer == 1)
                        playerInfo.weapon = 2;
                    else if (Globals.inventoryTucker.Flair == 1)
                        playerInfo.weapon = 3;
                    else if (Globals.inventoryTucker.Maul == 1)
                        playerInfo.weapon = 4;

                    if (Globals.inventoryTucker.LeatherCap == 1)
                        playerInfo.helmet = 1;
                    else if (Globals.inventoryTucker.KettleHat == 1)
                        playerInfo.helmet = 2;
                    else
                        playerInfo.helmet = 0;

                    if (Globals.inventoryTucker.WoodenBuckler == 1)
                        playerInfo.shield = 1;
                    else if (Globals.inventoryTucker.WoodenSmall == 1)
                        playerInfo.shield = 2;
                    else if (Globals.inventoryTucker.MetalBuckler == 1)
                        playerInfo.shield = 3;
                    else if (Globals.inventoryTucker.MetalSmall == 1)
                        playerInfo.shield = 4;
                    else if (Globals.inventoryTucker.MetalMedium == 1)
                        playerInfo.shield = 5;
                    else
                        playerInfo.shield = 0;

                    if (Globals.inventoryTucker.PaddedArmour == 1)
                        playerInfo.armour = 1;
                    else if (Globals.inventoryTucker.LeatherArmour == 1)
                        playerInfo.armour = 2;
                    else if (Globals.inventoryTucker.HideArmour == 1)
                        playerInfo.armour = 3;
                    else
                        playerInfo.armour = 0;
                }
            }
            attack = AttackCalculator.instance.weaponLibrary.AttackEntityLibrary[playerInfo.weapon].BaseWeaponDamage;
            shield = AttackCalculator.instance.shieldLibrary.DefenceEntityLibrary[playerInfo.shield].defence;
            helmet = AttackCalculator.instance.helmetLibrary.DefenceEntityLibrary[playerInfo.helmet].defence;
            armour = AttackCalculator.instance.armorLibrary.DefenceEntityLibrary[playerInfo.helmet].defence;
            weaponId = playerInfo.weapon;
            armourId = playerInfo.armour;
            shieldId = playerInfo.shield;
            helmetId = playerInfo.helmet;
            GiveValue();
            Debug.Log(" weapon id player :: " + playerInfo.weaponId);
        }
        else
        {
            Debug.Log("AI info :: " + playerInfo.health + " weapon id ");
            weaponId = playerInfo.weaponId;
        }


    }

    void GiveValue()
    {
        level = Globals.avatarState.Level;
        Globals.playerShied = shield;
        Globals.playerHelmet = helmet;
        Globals.playerArmour = armour;
    }
    public bool TakeDamage(PlayerItem player, PlayerItem Enemy)
    {
        result = 0;
        isMissed = false;
        if (player.Type == TypePlayer.Player || player.Type == TypePlayer.Companion)
        {

            AttackCalculator.instance.AttckEntity(player);
            totalWeaponDamage = AttackCalculator.instance.GetTotalWeaponDamage(player);
            if (Enemy.defence >= totalWeaponDamage)
                totalWeaponDamage = Enemy.defence - totalWeaponDamage;
            else
                totalWeaponDamage = totalWeaponDamage - Enemy.defence;
            health = Enemy.health;
        }
        else
        {

            totalWeaponDamage = AttackCalculator.instance.GetTotalWeapomDamageForAI(player);
            if (Enemy.GetComponent<EntityGroup>().increaseHealth)
            {
                health = 315;
                Enemy.GetComponent<EntityGroup>().increaseHealth = false;
            }
        }
        totalHealth = health;
        if (Globals.isAcolyte || Globals.isSmith || Globals.isArcher || Globals.isSmithF || Globals.isAcolyteF || Globals.isArcherF)
        {
            //  Debug.Log("type::" + Enemy.Type);
            if (player.Type == TypePlayer.Player)
                IntroEnemyHealth();
            else
                IntroPlayerHealth();
        }
        else
        {
            if (totalWeaponDamage >= Enemy.defence)
                HealthDeduction(Enemy, player);
            else
            {
                if (Enemy.Type == TypePlayer.Player || Enemy.Type == TypePlayer.Companion)
                    playerDefenceValue = AttackCalculator.instance.GetPlayerDefenceValue(Enemy);
                //else //added charu
                //    playerDefenceValue = AttackCalculator.instance.GetAIDefenceValue(Enemy);
                float diff = playerDefenceValue - totalWeaponDamage;
                bool attackBool = false;
                float diffPercent;
                int probablilty = 0;
                if (diff <= 0)
                    attackBool = true;
                else
                {
                    Debug.Log("total weapon damage : " + totalWeaponDamage + " player defence value : " + playerDefenceValue);
                    diffPercent = (totalWeaponDamage / playerDefenceValue) * 100;
                    Debug.Log("diff per : " + diffPercent);
                    if (diffPercent < 50)
                        probablilty = 40;
                    else if (diffPercent >= 50 && diffPercent < 100)
                        probablilty = 25;
                    else if (diffPercent >= 100)
                        probablilty = 10;
                    float randomNumber = UnityEngine.Random.Range(0, 100);
                    if (randomNumber < probablilty)
                        attackBool = true;
                }
                if (attackBool)
                    HealthDeduction(Enemy, player);
                else
                {
                    Debug.Log("missed...............");
                    isMissed = true;
                    Globals.battleManager.missAttack.SetActive(true);
                }
            }
        }
        // Debug.Log("result:: " + result);
        if (health < 0)
            return false;
        else
            return true;

    }
    void IntroPlayerHealth()
    {
        result = 0;
    }
    void IntroEnemyHealth()
    {
        result = 1;
    }

    void HealthDeduction(PlayerItem enemy, PlayerItem player)
    {
        Debug.Log("total health : " + totalHealth + " health :: " + health + " total weapon damage :: " + totalWeaponDamage);
        if (enemy.tag == "Enemy" && enemy.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount <= 0.3f)
        {
            Debug.Log("enemy armour defence........" + enemy.armorDefence);
            health += enemy.armorDefence;
            if (totalHealth >= health)//totalHealth > health
            {
                totalHealth -= (int)totalWeaponDamage;
                result = (totalHealth - health) / totalHealth;

            }
            else
            {
                Debug.Log("enemy armour defence........ health " + health);
                health -= (int)totalWeaponDamage;
                result = (health - totalHealth) / health;
            }
        }
        else
        {
            if (Globals.protagnistDefence || Globals.johnDefence || Globals.mariumDefence || Globals.tuckerDefence)
                CommonPart();
            health -= (int)totalWeaponDamage;
            result = (totalHealth - health) / totalHealth;
        }
        SpecialAttack(player, enemy);
    }
    void SpecialAttack(PlayerItem player, PlayerItem enemy)
    {
        if ((player.name == "DireWolf(Clone)0" || player.name == "DireWolf(Clone)1" || player.name == "DireWolf(Clone)2" /*|| player.name== "Hound(Clone)0" || player.name == "Hound(Clone)1" || player.name == "Hound(Clone)2"*/ || player.name == "DireWolf(Clone)3" || player.name == "DireWolf(Clone)4" || player.name == "DireWolf(Clone)5") && Globals.aiSpeacialEffect && player.GetComponent<EntityGroup>().specialAttack == 0)
        {
            Debug.Log("in special attack...... direwolf");

            enemy.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount = enemy.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount / 2;///2
            foreach (var v in Globals.battleManager.enemys)
            {
                v.GetComponent<EntityGroup>().specialAttack = 1;
            }
            //  player.GetComponent<EntityGroup>().specialAttack = 1;
            Globals.aiSpeacialEffect = false;
        }
        else if ((player.name == "Panther(Clone)0" || player.name == "Panther(Clone)1" || player.name == "Panther(Clone)2") /*&& player.GetComponent<EntityGroup>().specialAttack == 0*/ && Globals.aiSpeacialEffect)
        {
            //  enemy.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount = 0;
            //  player.GetComponent<EntityGroup>().specialAttack = 1;
            Globals.aiSpeacialEffect = false;
        }
        else if ((enemy.name == "Skeleton1(Clone)0" || enemy.name == "Skeleton1(Clone)1" || enemy.name == "Skeleton1(Clone)2") && enemy.GetComponent<EntityGroup>().specialAttack == 0)
        {
            Debug.Log("here skeleton :: 11111111111");
            result = result / 2;
            enemy.GetComponent<EntityGroup>().specialAttack = 1;
        }
        else if ((player.name == "Skeleton3(Clone)0" || player.name == "Skeleton3(Clone)1" || player.name == "Skeleton3(Clone)2") && (player.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount <= 0.75f) && player.GetComponent<EntityGroup>().specialAttack == 0)
        {
            player.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount += result;
            player.GetComponent<EntityGroup>().specialAttack = 1;

            Debug.Log("here skeleton :: 33333333");
        }
        else if (player.name == "Barghest(Black Hound)(Clone)7" && Globals.aiSpeacialEffect)
        {
            Debug.Log("inside barghest.........");
            enemy.GetComponent<EntityGroup>().noAttack = 4; //1
                                                            //   player.GetComponent<EntityGroup>().specialAttack = 1;
            Globals.aiSpeacialEffect = false;
            player.GetComponent<EntityGroup>().effect.SetActive(true);
            player.GetComponent<EntityGroup>().effect.GetComponent<ParticleSystem>().Play();
            result = 0;
        }
        else if (player.name == "DeathWeight(Clone)3" && Globals.aiSpeacialEffect && Globals.isBarghest)
        {
            Debug.Log("inside death weight :: " + enemy.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount);
            if (enemy.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount <= 0.2)
            {
                enemy.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount = 0f;
            }
            else
                enemy.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount = 0.2f;

            player.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount += player.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount * 35 / 100;//20
            player.GetComponent<EntityGroup>().specialAttack = 1;


            Globals.aiSpeacialEffect = false;
            player.health += player.health * 35 / 100;//20
            Debug.Log("inside death weight after :: " + player.health);
            Globals.healthDrainAttack = true;
        }
        else if (player.name == "AbbotChesterEnemy(Clone)1" && Globals.aiSpeacialEffect && player.GetComponent<EntityGroup>().specialAttack == 0)
        {
            Debug.Log("result :: " + result);
            result /= 2;
            Debug.Log("result :: after " + result);

            player.GetComponent<EntityGroup>().specialAttack = 1;
            Globals.aiSpeacialEffect = false;


            player.GetComponent<EntityGroup>().alchemy = 1;
            player.GetComponent<EntityGroup>().effect.SetActive(true);
            player.GetComponent<EntityGroup>().effect.GetComponent<ParticleSystem>().Play();
        }
        else if (player.name == "LordAlfredEnemy(Clone)7" && Globals.aiSpeacialEffect)
        {
            Debug.Log("special effect..........." + result);
            float per = result * 50 / 100;
            result += per;
            player.GetComponent<EntityGroup>().specialAttack = 1;
            Globals.aiSpeacialEffect = false;
            Debug.Log("special effect...........after" + result);
        }
        else if ((player.name == "LordEdwardEnemy(Clone)10" || player.name == "LordEdwardReeve Variant(Clone)4") && Globals.aiSpeacialEffect)
        {
            foreach (var v in Globals.battleManager.myTeam)
            {
                if (Globals.secondFight)
                {
                    Debug.Log("no attack lord edward.......");
                    v.GetComponent<EntityGroup>().noAttack = 1;
                    if (player.name == "LordEdwardReeve Variant(Clone)4")
                    {
                        Debug.Log("no attack lord edward.......  inside");
                        player.GetComponent<EntityGroup>().darkMagic.SetActive(true);
                        player.GetComponent<EntityGroup>().darkMagic.GetComponent<ParticleSystem>().Play();
                        player.GetComponent<EntityGroup>().darkMagicReanimated = true;
                        result = 0f;
                    }
                    //else
                    //{
                    //    player.GetComponent<EntityGroup>().effect.SetActive(true);
                    //    player.GetComponent<EntityGroup>().effect.GetComponent<ParticleSystem>().Play();
                    //}

                }

                else
                {
                    Debug.Log("else lord edward.......");
                    player.GetComponent<EntityGroup>().swordSlash = 1;
                    result = 0.65f;
                    player.GetComponent<EntityGroup>().effect.SetActive(true);
                    player.GetComponent<EntityGroup>().effect.GetComponent<ParticleSystem>().Play();

                    //v.GetComponent<EntityGroup>().bar.GetComponent<Image>().fillAmount = 0;  //charu
                    //Globals.battleManager.removeList.Add(v);
                }
            }
            if (!Globals.secondFight)
            {
                foreach (var v in Globals.battleManager.removeList)
                {
                    Globals.battleManager.tempList.Remove(v);
                    Globals.battleManager.myTeam.Remove(v);
                    Globals.battleManager.playersInBattle.Remove(v);
                }
            }
            player.GetComponent<EntityGroup>().specialAttack = 1;
            Globals.aiSpeacialEffect = false;
        }
    }
    void CommonPart()
    {
        if (shield > 0)
            health += shield;
        else if (helmet > 0)
            health += helmet;
        else if (armour > 0)
            health += armour;
        Globals.protagnistDefence = false;
        Globals.johnDefence = false;
        Globals.mariumDefence = false;
        Globals.tuckerDefence = false;
    }
}
