using HelthHolde;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCalculator : MonoBehaviour
{
    public static AttackCalculator instance;

    AttackEntitiesLibrary attackEntitiesLibrary;
    [SerializeField]
    AttackEntitiesLibrary smithAttackLibrary,scoutAttackLibrary,priestAttackLibrary,johnAttackLibrary,mariumAttackLibrary,tuckerAttackLibrary;
    [SerializeField]
    DefenceEntitiesLibrary smithHelmetLibrary,scoutHelmetLibrary,priestHelemtLibrary,johnHelmetLibrary,mariumHelmetLibrary,tuckerHelmetLibrary;
    [SerializeField]
    DefenceEntitiesLibrary smithArmorLibrary,priestArmourLibrary,scoutArmourLibrary,johnArmourLibrary,mariumArmourLibrary,tuckerArmourLibrary;
    [SerializeField]
    DefenceEntitiesLibrary smithShieldLibrary,scoutShieldLibrary,priestShieldLibrary,johnShieldLibrary,mariumShieldLibrary,tuckerShieldLibrary;
    [HideInInspector]
    public DefenceEntitiesLibrary helmetLibrary;
    [HideInInspector]
    public DefenceEntitiesLibrary armorLibrary;
    [HideInInspector]
    public DefenceEntitiesLibrary shieldLibrary;
    [HideInInspector]
    public AttackEntitiesLibrary weaponLibrary;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void Start()
    {
        if (Globals.avatarState.AvatarName == "WarriorMale" || Globals.avatarState.AvatarName == "WarriorFemale")
        {
            attackEntitiesLibrary = smithAttackLibrary;
            weaponLibrary = smithAttackLibrary;
            helmetLibrary = smithHelmetLibrary;
            armorLibrary = smithArmorLibrary;
            shieldLibrary = smithShieldLibrary;
        }
        else if (Globals.avatarState.AvatarName == "ArcherMale" || Globals.avatarState.AvatarName == "ArcherFemale")
        {
            attackEntitiesLibrary = scoutAttackLibrary;
            weaponLibrary = scoutAttackLibrary;
            helmetLibrary = scoutHelmetLibrary;
            armorLibrary = scoutArmourLibrary;
            shieldLibrary = scoutShieldLibrary;
        }
        else if (Globals.avatarState.AvatarName == "PriestMale" || Globals.avatarState.AvatarName == "PriestFemale")
        {
            attackEntitiesLibrary = priestAttackLibrary;
            weaponLibrary = priestAttackLibrary;
            helmetLibrary = priestHelemtLibrary;
            armorLibrary = priestArmourLibrary;
            shieldLibrary = priestShieldLibrary;
        }
        // else if()
    }
   public void AttckEntity(PlayerItem _player)
    {
        if (_player.name == "WarriorMale(Clone)" || _player.name == "WarriorFemale(Clone)")
        {
            attackEntitiesLibrary = smithAttackLibrary;
            weaponLibrary = smithAttackLibrary;
            helmetLibrary = smithHelmetLibrary;
            armorLibrary = smithArmorLibrary;
            shieldLibrary = smithShieldLibrary;
        }
        else if (_player.name == "ScoutMale(Clone)" || _player.name == "ArcherFemale(Clone)")
        {
            attackEntitiesLibrary = scoutAttackLibrary;
            weaponLibrary = scoutAttackLibrary;
            helmetLibrary = scoutHelmetLibrary;
            armorLibrary = scoutArmourLibrary;
            shieldLibrary = scoutShieldLibrary;
        }
        else if (_player.name == "PriestMale(Clone)" || _player.name == "PriestFemale(Clone)")
        {
            attackEntitiesLibrary = priestAttackLibrary;
            weaponLibrary = priestAttackLibrary;
            helmetLibrary = priestHelemtLibrary;
            armorLibrary = priestArmourLibrary;
            shieldLibrary = priestShieldLibrary;
        }
        else if(_player.name== "JohnCompanion(Clone)")
        {
            attackEntitiesLibrary = johnAttackLibrary;
            weaponLibrary = johnAttackLibrary;
            helmetLibrary = johnHelmetLibrary;
            armorLibrary = johnArmourLibrary;
            shieldLibrary = johnShieldLibrary;
        }
        else if (_player.name == "Marium(Clone)")
        {
            attackEntitiesLibrary = mariumAttackLibrary;
            weaponLibrary = mariumAttackLibrary;
            helmetLibrary = mariumHelmetLibrary;
            armorLibrary = mariumArmourLibrary;
            shieldLibrary = mariumShieldLibrary;
        }
        else if (_player.name == "Tucker(Clone)")
        {
            attackEntitiesLibrary = tuckerAttackLibrary;
            weaponLibrary = tuckerAttackLibrary;
            helmetLibrary = tuckerHelmetLibrary;
            armorLibrary = tuckerArmourLibrary;
            shieldLibrary = tuckerShieldLibrary;
        }
    }

    public float GetTotalWeaponDamage(PlayerItem player)
    {
        Debug.Log("player attack value :: " + player.attack + "player level :: " + player.level);
        float value = (player.attack * player.level)+attackEntitiesLibrary.AttackEntityLibrary[player.weaponId].BaseWeaponAttack;
        return value;
    }
   
    public float GetTotalWeapomDamageForAI(PlayerItem AI)
    {
        Debug.Log("AI attack value :: "+AI.attack+" level :: "+AI.level);
       float value1 = (AI.attack + AI.level) / 2;
        return value1;
    }
    public float GetPlayerDefenceValue(PlayerItem player)
    {
        Debug.Log("player armor id :: " + player.armourId+" helmet id :: "+player.helmetId+ "shiled id :: "+ player.shieldId);
        float value =helmetLibrary.DefenceEntityLibrary[player.helmetId].defence+ armorLibrary.DefenceEntityLibrary[player.armourId].defence + +shieldLibrary.DefenceEntityLibrary[player.shieldId].defence;
        return value;
    }
    public float GetAIDefenceValue(PlayerItem AI)
    {
        Debug.Log("defence value AI :: " + AI.defence);
        float value = AI.defence;  ///2
        return value;
    }

    public void AttckEntityForInventory()
    {
        if (Globals.selectedInventoryCharacter == "WarriorMale" || Globals.selectedInventoryCharacter == "WarriorFemale")
        {
            attackEntitiesLibrary = smithAttackLibrary;
            weaponLibrary = smithAttackLibrary;
            helmetLibrary = smithHelmetLibrary;
            armorLibrary = smithArmorLibrary;
            shieldLibrary = smithShieldLibrary;
        }
        else if (Globals.selectedInventoryCharacter == "ArcherMale" || Globals.selectedInventoryCharacter == "ArcherFemale")
        {
            attackEntitiesLibrary = scoutAttackLibrary;
            weaponLibrary = scoutAttackLibrary;
            helmetLibrary = scoutHelmetLibrary;
            armorLibrary = scoutArmourLibrary;
            shieldLibrary = scoutShieldLibrary;
        }
        else if (Globals.selectedInventoryCharacter == "PriestMale" || Globals.selectedInventoryCharacter == "PriestFemale")
        {
            attackEntitiesLibrary = priestAttackLibrary;
            weaponLibrary = priestAttackLibrary;
            helmetLibrary = priestHelemtLibrary;
            armorLibrary = priestArmourLibrary;
            shieldLibrary = priestShieldLibrary;
        }
        else if (Globals.selectedInventoryCharacter == "John")
        {
            attackEntitiesLibrary = johnAttackLibrary;
            weaponLibrary = johnAttackLibrary;
            helmetLibrary = johnHelmetLibrary;
            armorLibrary = johnArmourLibrary;
            shieldLibrary = johnShieldLibrary;
        }
        else if (Globals.selectedInventoryCharacter == "Marium")
        {
            Debug.Log("marium selected ");
            attackEntitiesLibrary = mariumAttackLibrary;
            weaponLibrary = mariumAttackLibrary;
            helmetLibrary = mariumHelmetLibrary;
            armorLibrary = mariumArmourLibrary;
            shieldLibrary = mariumShieldLibrary;
        }
        else if (Globals.selectedInventoryCharacter == "Tucker")
        {
            attackEntitiesLibrary = tuckerAttackLibrary;
            weaponLibrary = tuckerAttackLibrary;
            helmetLibrary = tuckerHelmetLibrary;
            armorLibrary = tuckerArmourLibrary;
            shieldLibrary = tuckerShieldLibrary;
        }
    }

}
