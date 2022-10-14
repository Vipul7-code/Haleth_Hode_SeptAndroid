using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;
public class HuntingtonController : MonoBehaviour
{
    public PlayableDirector playble;
    public GameObject dialogBox;
    int dialogCount,specialCount;
    PlayableDirector activePlayble;
    [SerializeField]
  public  GameObject popUp,creditPanel;
    [SerializeField]
    AudioSource audio;
    [SerializeField]
    GameObject guard2;
    // Start is called before the first frame update
    private void Start()
    {
        Globals.huntingtonContt = this;
        Debug.Log("here.. "+ Globals.leavingThrone+ " "+ Globals.huntingtonVill.clickCount+ " "+Globals.drunkenGuy+ " " + Globals.isGuard + " " + Globals.isSpecial+ " " + Globals.completeEfforts);
        if(Globals.leavingThrone)
        {
            audio.clip= Resources.Load("Sound/TownAttack/Peaceful Town Music mix") as AudioClip;
            audio.Play();
        }
    }
   public void PlaybleFile()
    {
        Debug.Log("click count::" + Globals.huntingtonVill.clickCount);
        if (Globals.huntingtonVill.clickCount == 2)
        {
            Globals.drunkenGuy = true;
            if (Globals.avatarState.AvatarName == "WarriorMale")
                playble.playableAsset = Resources.Load("Playables/Huntington Village/Smith/Smith_M_Huntington_Town_DrunkGuard_CutScene") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "WarriorFemale")
                playble.playableAsset = Resources.Load("Playables/Huntington Village/Smith/Smith_F_Huntington_Town_DrunkGuard_CutScene") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "ArcherMale")
                playble.playableAsset = Resources.Load("Playables/Huntington Village/Archer/Archer_M_Huntington_Town_DrunkGuard_CutScene") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "ArcherFemale")
                playble.playableAsset = Resources.Load("Playables/Huntington Village/Archer/Archer_F_Huntington_Town_DrunkGuard_CutScene") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "PriestMale")
                playble.playableAsset = Resources.Load("Playables/Huntington Village/Acolyte/Acolyte_M_Huntington_Town_DrunkGuard_CutScene") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "PriestFemale")
                playble.playableAsset = Resources.Load("Playables/Huntington Village/Acolyte/Acolyte_F_Huntington_Town_DrunkGuard_CutScene") as TimelineAsset;
            playble.Play();
        }
        else if (Globals.huntingtonVill.clickCount == 3)
        {
            if (Globals.avatarState.AvatarName == "WarriorMale")
                playble.playableAsset = Resources.Load("Playables/Huntington Village/Smith/Smith_M_Huntington_Town_GuardAtGate_CutScene") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "WarriorFemale")
                playble.playableAsset = Resources.Load("Playables/Huntington Village/Smith/Smith_F_Huntington_Town_GuardAtGate_CutScene") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "ArcherMale")
                playble.playableAsset = Resources.Load("Playables/Huntington Village/Archer/Archer_M_Huntington_Town_GuardAtGate_CutScene") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "ArcherFemale")
                playble.playableAsset = Resources.Load("Playables/Huntington Village/Archer/Archer_F_Huntington_Town_GuardAtGate_CutScene") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "PriestMale")
                playble.playableAsset = Resources.Load("Playables/Huntington Village/Acolyte/Acolyte_M_Huntington_Town_GuardAtGate_CutScene") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "PriestFemale")
                playble.playableAsset = Resources.Load("Playables/Huntington Village/Acolyte/Acolyte_F_Huntington_Town_GuardAtGate_CutScene") as TimelineAsset;
            playble.Play();
        }
        else if (Globals.huntingtonVill.clickCount == 5)
        {
            
            guard2.GetComponent<MeshRenderer>().sortingOrder = -1;
            if (Globals.avatarState.AvatarName == "WarriorMale")
                playble.playableAsset = Resources.Load("Playables/Huntington Village/Smith/Smith_M_Huntington_Town_GrapplingHook_CutScene") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "WarriorFemale")
                playble.playableAsset = Resources.Load("Playables/Huntington Village/Smith/Smith_F_Huntington_Town_GrapplingHook_CutScene") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "ArcherMale")
                playble.playableAsset = Resources.Load("Playables/Huntington Village/Archer/Archer_M_Huntington_Town_GrapplingHook_CutScene") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "ArcherFemale")
                playble.playableAsset = Resources.Load("Playables/Huntington Village/Archer/Archer_F_Huntington_Town_GrapplingHook_CutScene") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "PriestMale")
                playble.playableAsset = Resources.Load("Playables/Huntington Village/Acolyte/Acolyte_M_Huntington_Town_GrapplingHook_CutScene") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "PriestFemale")
                playble.playableAsset = Resources.Load("Playables/Huntington Village/Acolyte/Acolyte_F_Huntington_Town_GrapplingHook_CutScene") as TimelineAsset;
            playble.Play();
        }
    }
    public void PauseClip()
    {
        dialogBox.GetComponent<Button>().enabled = true;
        playble.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }
    public void OnBattle()
    {
        Debug.Log("here battle");
        Globals.isGuard = true;
    }
    public void PlayFirstClip()
    {
        playble.playableGraph.GetRootPlayable(0).SetSpeed(1);
        if (!Globals.isSpecial)
            dialogCount++;
        else
            specialCount++;
        dialogBox.GetComponent<Button>().enabled = false;
        if (Globals.huntingtonVill.clickCount == 3)
        {
            if (Globals.isSpecial)
            {
                if (specialCount == 6)
                {
                    Globals.huntingtonVill.playble2.SetActive(false);
                    Globals.huntingtonVill.playble1.transform.GetChild(0).GetComponent<PlayableDirector>().playableGraph.GetRootPlayable(0).SetSpeed(1);
                    Globals.isSpecial = false;
                    Globals.first = true;
                }
            }
        }
    }
    public void OnSoundChange()
    {
        Globals.huntingtonVill.playble1.transform.GetChild(0).GetComponent<PlayableDirector>().playableGraph.GetRootPlayable(0).SetSpeed(0);
        popUp.SetActive(true);
    }
    public void SecondPlayble()
    {
        Globals.huntingtonVill.playble2.SetActive(true);
        Globals.huntingtonVill.playble2.transform.GetChild(0).GetComponent<PlayableDirector>().playableAsset = Resources.Load(Globals.playbleAssets) as TimelineAsset;
        Globals.huntingtonVill.playble2.transform.GetChild(0).GetComponent<PlayableDirector>().Play();
        Globals.isSpecial = true;
    }
    void Common()
    {
        Globals.ActiveControls(Globals.huntingtonVill.character, true);
        Globals.completeEfforts = true;
    }
    public void DialogPart()
    {
        Globals.huntingtonVill.clickCount = 1;
        if (Globals.avatarState.AvatarName == "WarriorMale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Smith/Smith_M_Huntington_Caved-In_Tunnel_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "WarriorFemale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Smith/Smith_F_Huntington_Caved-In_Tunnel_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherMale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Archer/Archer_M_Huntington_Caved-In_Tunnel_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherFemale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Archer/Archer_F_Huntington_Caved-In_Tunnel_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestMale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Acolyte/Acolyte_M_Huntington_Caved-In_Tunnel_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestFemale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Acolyte/Acolyte_F_Huntington_Caved-In_Tunnel_Dialog") as TimelineAsset;
        playble.Play();
        Globals.ActiveControls(Globals.huntingtonVill.character, false);
    }
    public void AfterCutsceneDialog()
    {
        if (Globals.avatarState.AvatarName == "WarriorMale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Smith/Smith_M_Huntington_AfterChase_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "WarriorFemale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Smith/Smith_F_Huntington_AfterChase_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherMale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Archer/Archer_M_Huntington_AfterChase_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherFemale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Archer/Archer_F_Huntington_AfterChase_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestMale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Acolyte/Acolyte_M_Huntington_AfterChase_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestFemale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Acolyte/Acolyte_F_Huntington_AfterChase_Dialog") as TimelineAsset;
        playble.Play();
        Globals.ActiveControls(Globals.huntingtonVill.character, false);
    }
    public void OnComplete()
    {
        Debug.Log("on complete");
        if (Globals.leavingThrone)
        {
            if (Globals.huntingtonVill.clickCount == 2)
            {
                creditPanel.SetActive(true);
                audio.clip = Resources.Load("Sound/ThemeSong/Haleth Hode Main Theme final mix#.3") as AudioClip;
                audio.Play();
            }
            else
            {
                Globals.huntingtonVill.clickCount = 2;
                Globals.huntingtonVill.PlaybleSetting(false, false, false, true);
                if (Globals.avatarState.AvatarName == "WarriorMale")
                Globals.huntingtonVill.playble4.transform.GetChild(0).GetComponent< PlayableDirector>().playableAsset = Resources.Load("Playables/Huntington Village/Smith/Smith_M_Huntington_Courtyard_End_CutScene") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "WarriorFemale")
                    Globals.huntingtonVill.playble4.transform.GetChild(0).GetComponent<PlayableDirector>().playableAsset = Resources.Load("Playables/Huntington Village/Smith/Smith_F_Huntington_Courtyard_End_CutScene") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherMale")
                    Globals.huntingtonVill.playble4.transform.GetChild(0).GetComponent<PlayableDirector>().playableAsset = Resources.Load("Playables/Huntington Village/Archer/Archer_M_Huntington_Courtyard_End_CutScene") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherFemale")
                    Globals.huntingtonVill.playble4.transform.GetChild(0).GetComponent<PlayableDirector>().playableAsset = Resources.Load("Playables/Huntington Village/Archer/Archer_F_Huntington_Courtyard_End_CutScene") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestMale")
                    Globals.huntingtonVill.playble4.transform.GetChild(0).GetComponent<PlayableDirector>().playableAsset = Resources.Load("Playables/Huntington Village/Acolyte/Acolyte_M_Huntington_Courtyard_End_CutScene") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestFemale")
                    Globals.huntingtonVill.playble4.transform.GetChild(0).GetComponent<PlayableDirector>().playableAsset = Resources.Load("Playables/Huntington Village/Acolyte/Acolyte_F_Huntington_Courtyard_End_CutScene") as TimelineAsset;
                Globals.huntingtonVill.playble4.transform.GetChild(0).GetComponent<PlayableDirector>().Play();

            }
        }
        else
        {
            if (Globals.huntingtonVill.clickCount == 1)
            {
                Globals.huntingtonVill.conversationBox1.SetActive(false);
                Common();
            }
            else if (Globals.huntingtonVill.clickCount == 2)
                Globals.huntingtonVill.OnCompleteCutScene();
            else if (Globals.huntingtonVill.clickCount == 3)
            {
                Debug.Log("hereeee");
                Globals.huntingtonVill.OnCompleteThirdScene();
            }
            else if (Globals.huntingtonVill.clickCount == 4)
            {
                Globals.huntingtonVill.conversationBox2.SetActive(false);
                DisablePartyMember();
                Common();
            }
            else if (Globals.huntingtonVill.clickCount == 5)
            {
                Globals.grappingHook = true;
                Globals.huntingtonVill.OnCompleteFifthScene();
            }
            Globals.first = false;
            Globals.isGuard = false;
        }
    }
    void DisablePartyMember()
    {
        Globals.huntingtonVill.marium.SetActive(false);
        Globals.huntingtonVill.john.SetActive(false);
        Globals.huntingtonVill.tucker.SetActive(false);
    }
}
