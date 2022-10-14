using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;
public class NPCMovement : MonoBehaviour
{
    public AIPath aiPath;
    EntityGroup entityGroup;
   public AIDestinationSetter aidestination;
  public  GameObject npc;
    float speed =0.2f;
    string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        entityGroup = GetComponent<EntityGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (aiPath.velocity.x > 0.05f)
            entityGroup.UpdateFace("right");
        else if (aiPath.velocity.x < 0.05f)
            entityGroup.UpdateFace("left");
       else if (aiPath.velocity.y > 0.05f)
            entityGroup.UpdateFace("back");
        else if (aiPath.velocity.y < 0.05f)
            entityGroup.UpdateFace("front");
        if(Globals.levelManager.character!=null)
            Physics2D.IgnoreCollision(npc.GetComponent<Collider2D>(), Globals.levelManager.character.GetComponent<Collider2D>());
        if (!Globals.PlayNow)
        {
            aidestination.isStart = true;
            OnAnimatorMove(0.2f);
            if (aiPath.reachedEndOfPath)
            {
                if (Globals.activeScene == Globals.CurrentScene.AtwaterVillage)
                    aidestination.target = Globals.atwater.targetPos[Random.Range(0, 3)].transform;
                else if (Globals.activeScene == Globals.CurrentScene.BarghestVillage)
                    aidestination.target = Globals.barghestDengeon.targetPos[Random.Range(0, 3)].transform;
                else if (Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon)
                    aidestination.target = Globals.brigandLierDengeon.targetPos[Random.Range(0, 4)].transform;
                else if (sceneName == "Huntsville_Inn_1stFloor")
                    aidestination.target = Globals.gameController.targetPos[Random.Range(0, 4)];
            }
        }
        else
            aidestination.isStart = false;
       
           
    }
    public void OnAnimatorMove(float speed)
    {
        foreach (Transform child in this.transform)
        {
            if (child.gameObject.activeInHierarchy && child.GetComponent<Animator>() != null)
                child.GetComponent<Animator>().SetFloat("Speed", speed);
        }
    }
}
