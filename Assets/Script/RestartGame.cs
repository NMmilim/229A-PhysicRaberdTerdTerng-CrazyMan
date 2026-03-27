using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RestartGame : MonoBehaviour
{
    public float restartDelay = 1f;
    private bool isRestarting = false;
    public TMP_Text restartText;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isRestarting)
        {
            Restart();
        }
    }

    public void Restart()
    {
        if (!isRestarting)
        {
            StartCoroutine(RestartRoutine());
        }
    }

    IEnumerator RestartRoutine()
    {
        isRestarting = true;

        if (restartText != null)
            restartText.gameObject.SetActive(true);

        float timer = restartDelay;

        while (timer > 0)
        {
            restartText.text = "Restarting in " + timer.ToString("0");
            timer -= 1f;
            yield return new WaitForSeconds(1f);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}