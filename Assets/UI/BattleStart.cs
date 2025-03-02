using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleStart : MonoBehaviour
{
    public void BattlePressed()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
