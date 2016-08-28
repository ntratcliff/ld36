using UnityEngine;
using System.Collections;

public class SawController : MonoBehaviour
{
    [Tooltip("How far the saw moves from center with each trigger press")]
    public float SawOffset;
    public float SawMinY;

    public float SlerpSpeed;

    private PlayerScore playerScore;
    private PlayerInfo playerInfo;
    private SawGoodGame gameController;

    private float topY;
    private float targetX;

    private bool positive = true;

    // Use this for initialization
    void Start()
    {
        playerScore = GetComponentInParent<PlayerScore>();
        playerInfo = GetComponentInParent<PlayerInfo>();
        gameController = FindObjectOfType<SawGoodGame>();

        topY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameController.TargetScore > playerScore.Score)
        {
            float triggers = playerInfo.GetAxis("Triggers");
            if (positive && triggers > 0
                || !positive && triggers < 0)
            {
                sawOnce();
            }

            Vector3 localPos = transform.localPosition;

            //set x pos relative to depressed trigger
            if (triggers != 0)
                localPos.x = -Mathf.Sign(triggers) * SawOffset;

            //set y pos relative to score
            localPos.y = (topY - SawMinY) * (gameController.TargetScore - playerScore.Score) / gameController.TargetScore + SawMinY;

            //slerp position
            transform.localPosition = Vector3.Slerp(
                transform.localPosition,
                localPos,
                Time.deltaTime * SlerpSpeed);
        }   
    }

    private void sawOnce()
    {
        //target other trigger
        positive = !positive;

        //update score
        if(playerScore.Score < gameController.TargetScore)
            playerScore.Score++;
    }
}
