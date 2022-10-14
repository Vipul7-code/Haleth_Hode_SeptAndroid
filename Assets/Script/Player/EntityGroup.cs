using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EntityGroup : MonoBehaviour
{
    [SerializeField]
    public GameObject frontFaces, backFace, prespFaceL, prespFaceR, controlPanel, selectionEffect,  namePopup,bar, effect,deathEffect,lootImage,smoke,helenaRope1, helenaRope2,darkMagic,disapp,lightEffect,healEff,criticalEff,powerStrike;
    [SerializeField]
    public GameObject[] allSides;
    [SerializeField]
    PlayerItem charactr;
    GameObject player;
    [HideInInspector]
    public int enemyIndex,companionIndex, attackPower;
    [HideInInspector]
    public int HealthBar;
    Scene currentScene;
    public Vector3 originalPos;
    [HideInInspector]
    public bool increaseHealth;
    [SerializeField]
    public int indexOfPlayer,specialAttack,noAttack,lighteningBolt,criticalStrike,deadEye,bandageProperty,alchemy, swordSlash;
    public int lighteningBoltDeathWight;
    public bool darkMagicReanimated = false;
   
    public GameObject arrow, parent;
    // Update is called once per frame
    private void Start()
    {
         currentScene = SceneManager.GetActiveScene();
    }
    public void UpdateFace(string face)
    {
        switch (face)
        {
            case "front":
                {
                    if (player != null)
                        frontFaces.transform.position = player.transform.position;
                    ControlFaces(true, false, false, false);
                    player = frontFaces;
                    if (currentScene.name == "World Map")
                    {
                        if(Globals.previosObjective== "Huntsville" || Globals.previosObjective== "MotteAndBaileyCastle")
                           StartCoroutine(ActiveCollider());
                        FindObjectOfType<FogOfwar>().m_radius = 5;
                    }
                    break;
                }

            case "back":
                {
                    if (player != null)
                        backFace.transform.position = player.transform.position;
                    ControlFaces(false, true, false, false);
                    player = backFace;
                    if (currentScene.name == "World Map")
                    {
                        if (Globals.previosObjective == "Huntsville" || Globals.previosObjective == "MotteAndBaileyCastle")
                            StartCoroutine(ActiveCollider());
                        FindObjectOfType<FogOfwar>().m_radius = 5;
                    }
                    break;
                }

            case "left":
                {
                    if (player != null)
                        prespFaceL.transform.position = player.transform.position;
                    ControlFaces(false, false, true, false);
                    player = prespFaceL;
                    if (currentScene.name == "World Map")
                    {
                        if (Globals.previosObjective == "Huntsville" || Globals.previosObjective == "MotteAndBaileyCastle")
                            StartCoroutine(ActiveCollider());
                        FindObjectOfType<FogOfwar>().m_radius = 5;
                    }
                    break;
                }

            case "right":
                {
                    if (player != null)
                        prespFaceR.transform.position = player.transform.position;
                    ControlFaces(false, false, false, true);
                    player = prespFaceR;
                    if (currentScene.name == "World Map")
                    {
                        if (Globals.previosObjective == "Huntsville" || Globals.previosObjective == "MotteAndBaileyCastle")
                            StartCoroutine(ActiveCollider());
                        FindObjectOfType<FogOfwar>().m_radius = 5;
                    }
                    break;
                }
        }
    }
    IEnumerator ActiveCollider()
    {
        yield return new WaitForSeconds(0.9f);
        //foreach(var v in Globals.uiManager.colliders)
        //{
        //    v.SetActive(true);
        //}
        for(int i = 3; i < Globals.uiManager.colliders.Length;i++)
        {
            Globals.uiManager.colliders[i].gameObject.SetActive(true);
        }
    }
    void ControlFaces(bool front,bool back, bool left, bool right)
    {
        frontFaces.SetActive(front);
        backFace.SetActive(back);
        prespFaceL.SetActive(left);
        prespFaceR.SetActive(right);
    }
    void OnMouseDown()
    {
        if (currentScene.name == "BattleScene")
        {
            if (charactr.tag == "Player")
            {
                selectionEffect.SetActive(true);
                foreach (var v in Globals.battleManager.companionList)
                {
                    v.GetComponent<BoxCollider2D>().enabled = false;
                }
                Globals.battleManager.isChoose = true;
                Globals.battleManager.attacker = Globals.battleManager.character;
                Globals.battleManager.attackerName = "P";
                Globals.battleManager.ClickEffect();
            }
            else if (charactr.tag == "Companion")
            {
                selectionEffect.SetActive(true);
                Globals.battleManager.character.GetComponent<BoxCollider2D>().enabled = false;
                Globals.battleManager.isChoose = true;
                companionIndex = Globals.battleManager.companionList.IndexOf(charactr);
                Globals.battleManager.attacker = Globals.battleManager.companionList[companionIndex];
                Globals.battleManager.attackerName = "C";
                Globals.battleManager.ClickEffect();
            }
            else if (charactr.tag == "Enemy")
            {
                enemyIndex = Globals.battleManager.enemys.IndexOf(charactr);
                EntityGroup entity = Globals.battleManager.enemys[enemyIndex].GetComponent<EntityGroup>();
                if (!entity.selectionEffect.activeInHierarchy)
                    entity.selectionEffect.SetActive(true);
                Globals.battleManager.enemyCount = enemyIndex;
                Globals.battleManager.GiveAttack(Globals.battleManager.attackerName);

            }
        }
    }
    void Effect()
    {
        int indexofplayer =Globals.battleManager.playerIndex;//-1
       Globals.battleManager.playersInBattle[indexofplayer].GetComponent<EntityGroup>().effect.SetActive(false);
      Globals.battleManager.playersInBattle[indexofplayer].GetComponent<EntityGroup>().effect.transform.localPosition =Globals.battleManager.effectOriginalPos;
    }
}
