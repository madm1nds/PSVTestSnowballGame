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
    private const float indent = 0f;
    private const float spaceBetweenChars = 10f;
    private const int initialPoint = 0;
    /// <summary>
    /// Обновление оличества очков.
    /// </summary>
    /// <param name="points">Количество прибавляемых очков.</param>
    public void RefreshPoints(int points)
    {
        scorePlayer += points;
        stringScorePlayer = System.Convert.ToString(scorePlayer) + "/" + System.Convert.ToString(Vault.instance.settings.pointsForVictory);
        textPictureConverter.SetImageNumber(scoreSet, stringScorePlayer, indent, spaceBetweenChars, AlignmentTextPicture.Center);
    }
    void Start()
    {
        if (instance is null)
        {
            instance = gameObject.transform.GetComponent<ScoreSetController>();
        }
        RefreshPoints(initialPoint);
    }
}
