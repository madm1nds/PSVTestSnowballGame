using UnityEngine;
/// <summary>
/// Класс обновляющий счёт игрока.
/// instance - общедоступная ссылка на самого себя.
/// scorePlayer - количество очков игрока в числовом формате
/// stringScorePlayer -  количество очков игрока в строковом формате
/// scoreSet -  оьъект содержащий обновляемые спрайты
/// </summary>
public class ScoreSetController : MonoBehaviour
{
    public static ScoreSetController instance;
    public static int scorePlayer;
    private static string stringScorePlayer;
    [SerializeField]
    private TextPictureConverter textPictureConverter;
    [SerializeField]
    private GameObject scoreSet;
    /// <summary>
    /// Обновление оличества очков.
    /// </summary>
    /// <param name="points">Количество прибавляемых очков.</param>
    public void RefreshPoints(int points)
    {
        scorePlayer += points;
        stringScorePlayer = System.Convert.ToString(scorePlayer) + "/" + System.Convert.ToString(Vault.instance.settings.pointsForVictory);
        textPictureConverter.SetImageNumber(scoreSet, stringScorePlayer, 0, 10, AlignmentTextPicture.Center);
    }
    void Start()
    {
        if (instance is null)
        {
            instance = gameObject.transform.GetComponent<ScoreSetController>();
        }
        RefreshPoints(0);
    }
}
