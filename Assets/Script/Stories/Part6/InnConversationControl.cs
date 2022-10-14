using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class InnConversationControl : MonoBehaviour
{
    public PlayableDirector playble;
    public GameObject dialogBox;
    int dialogCount;
    // Start is called before the first frame update
    void Start()
    {
        if(Globals.isPart1Battle && Globals.activeScene==Globals.CurrentScene.HuntingtonThroneRoom)
        {
            if (Globals.secondFight)
            {
                if (Globals.avatarState.AvatarName == "WarriorMale")
                    playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Smith/Smith_M_Huntington_ThronRoom_BloackSmoke_FinalEncounter_CutScene_02") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "WarriorFemale")
                    playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Smith/Smith_F_Huntington_ThronRoom_BloackSmoke_FinalEncounter_CutScene_02") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherMale")
                    playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Archer/Archer_M_Huntington_ThronRoom_BloackSmoke_FinalEncounter_CutScene_02") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherFemale")
                    playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Archer/Archer_F_Huntington_ThronRoom_BloackSmoke_FinalEncounter_CutScene_02") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestMale")
                    playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Acolyte/Acolyte_M_Huntington_ThronRoom_BloackSmoke_FinalEncounter_CutScene_02") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestFemale")
                    playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Acolyte/Acolyte_F_Huntington_ThronRoom_BloackSmoke_FinalEncounter_CutScene_02") as TimelineAsset;
            }
            else
            {
                if (Globals.avatarState.AvatarName == "WarriorMale")
                    playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Smith/Smith_M_Huntington_ThronRoom_BloackSmoke_FinalEncounter_CutScene_01") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "WarriorFemale")
                    playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Smith/Smith_F_Huntington_ThronRoom_BloackSmoke_FinalEncounter_CutScene_01") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherMale")
                    playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Archer/Archer_M_Huntington_ThronRoom_BloackSmoke_FinalEncounter_CutScene_01") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherFemale")
                    playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Archer/Archer_F_Huntington_ThronRoom_BloackSmoke_FinalEncounter_CutScene_01") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestMale")
                    playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Acolyte/Acolyte_M_Huntington_ThronRoom_BloackSmoke_FinalEncounter_CutScene_01") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestFemale")
                    playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Acolyte/Acolyte_F_Huntington_ThronRoom_BloackSmoke_FinalEncounter_CutScene_01") as TimelineAsset;
            }
            playble.Play();
        }
        else if(!Globals.isPart1Battle && Globals.activeScene==Globals.CurrentScene.HuntingtonCastle)
        {
            if (Globals.avatarState.AvatarName == "WarriorMale")
                playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Smith/Smith_M_Huntington_CastleIntirior_GeneralDialogs_CutScene_01") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "WarriorFemale")
                playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Smith/Smith_F_Huntington_CastleIntirior_GeneralDialogs_CutScene_01") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "ArcherMale")
                playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Archer/Archer_M_Huntington_CastleIntirior_GeneralDialogs_CutScene_01") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "ArcherFemale")
                playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Archer/Archer_F_Huntington_CastleIntirior_GeneralDialogs_CutScene_01") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "PriestMale")
                playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Acolyte/Acolyte_M_Huntington_CastleIntirior_GeneralDialogs_CutScene_01") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "PriestFemale")
                playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Acolyte/Acolyte_F_Huntington_CastleIntirior_GeneralDialogs_CutScene_01") as TimelineAsset;
            playble.Play();
        }
        
    }
    public void PauseClip()
    {
        dialogBox.GetComponent<Button>().enabled = true;
        playble.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }
    public void PlayFirstClip()
    {
        playble.playableGraph.GetRootPlayable(0).SetSpeed(1);
        dialogCount++;
        dialogBox.GetComponent<Button>().enabled = false;

    }
  
    public void OnCompleteVideo()
    {
        if (Globals.activeScene == Globals.CurrentScene.HuntingtonCastle)
            StartCoroutine(startBattle());
        else if(Globals.activeScene==Globals.CurrentScene.HuntingtonThroneRoom)
        {
            if (!Globals.secondFight)
            {
                Globals.secondFight = true;
                StartCoroutine(startBattle());
            }
            else
                FindObjectOfType<ThronController>().PlaybleSetting(true, false);
        }
        else
           Globals.innController.OnCompleteFirstCutscene();
    }
    IEnumerator startBattle()
    {
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene("Battle Scene_Castle");
        //if(Globals.activeScene==Globals.CurrentScene.HuntingtonCastle || Globals.activeScene==Globals.CurrentScene.HuntingtonThroneRoom)

        ////else
        ////   SceneManager.LoadScene("BattleScene");
    }

}
