using UnityEngine;


public class ScoreSetController : MonoBehaviour
{
    private static int scorePlayer;
    private static string stringScorePlayer;
    [SerializeField]
    private TextPictureConverter textPictureConverter;
    [SerializeField]
    private GameObject scoreSet;
    [SerializeField]
    private RequiredPoints requiredPoints; 
    public void RefreshPoints(int points)
    {
        scorePlayer += points;
        stringScorePlayer = System.Convert.ToString(scorePlayer) + "/" + System.Convert.ToString(requiredPoints.points);
        textPictureConverter.SetImageNumber(scoreSet, stringScorePlayer, 0, 10, AlignmentTextPicture.Center);
    }
    void Start()
    {
        RefreshPoints(0);
    }
}
