using UnityEngine;
using System.Collections;

public class SawController : MonoBehaviour
{
    private PlayerScore playerScore;
    private PlayerInfo playerInfo;
    private SawGoodGame gameController;

    private float topY;

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
        if (positive && playerInfo.GetAxis("Triggers") > 0
            || !positive && playerInfo.GetAxis("Triggers") < 0)
        {
            sawOnce();
        }

        //set y pos relative to score
        Vector3 pos = transform.position;
        pos.y = topY * (gameController.TargetScore - playerScore.Score) / gameController.TargetScore;
        transform.position = pos;
    }

    private void sawOnce()
    {
        //target other trigger
        positive = !positive;

        //update score
        playerScore.Score++;
    }
}
