using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1); // Þegar smellt er á Spila þá er fært spilara á leik senuna þar sem hann getur spilað leikinn.
    }
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit(); // Þegar smellt er á hætta þá er forriti lokað.
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0); // Þegar smellt er á spila aftur þá er fært spilara aftur á upphafskjá.
    }
}
