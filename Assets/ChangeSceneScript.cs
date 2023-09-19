using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;

public class ChangeSceneScript: MonoBehaviour
{
    public async void SetScene(int number)
    {
        await WaitOneSecondAsync();

        SceneManager.LoadScene(number);
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }

    private async Task WaitOneSecondAsync()
    {
        await Task.Delay(TimeSpan.FromSeconds(1));
        Debug.Log("Finished waiting.");
    }
}
