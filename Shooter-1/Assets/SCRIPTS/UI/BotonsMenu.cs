using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonsMenu : MonoBehaviour
{
   public void StartGame()
   {
       SceneManager.LoadScene("GameScene");
    }
}
