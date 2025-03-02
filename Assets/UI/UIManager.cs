using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void MapPressed()
    {
        SceneManager.LoadScene("Map");
    }
}
