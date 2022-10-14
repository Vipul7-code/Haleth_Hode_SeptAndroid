using UnityEngine;

public class LegendMazeCameraFollow : MonoBehaviour
{
    [SerializeField]
    private float distanceAway;
    [SerializeField]
    private float distanceUp;
    [SerializeField]
    private float smooth;

    private Transform follow;
    private Vector3 targetPosition;

     GameObject playerReference;
    public static GameObject treasureBox;

    private void Start()
    {
       
    }

    private void LateUpdate()
    {
      //  if (CharacterControllerMe.treasureOpened == true)
        {
          //  follow = treasureBox.transform.GetChild(2);
        }
        if (Globals.isGameStart)
        {
            Debug.Log("active player" + Globals.activePlayer.name);
            playerReference = Globals.activePlayer;
            follow = playerReference.transform;
            //  CharacterControllerMe.treasureOpened = false;
            //  follow = playerReference.transform.GetChild(2);
            transform.LookAt(follow);
            if (follow != null)
            {
                targetPosition = follow.position + transform.up * distanceUp - follow.forward * distanceAway;
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
                transform.LookAt(follow);
            }
        }
    }
}