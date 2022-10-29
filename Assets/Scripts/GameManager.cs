using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject m_pipe;
    public float m_firstPipeAt = 15.0f;
    public float m_horizSpacing = 8.0f;
    public float m_vertSpacing = 4.0f;
    public float m_maxY = 4.0f;
    public GameObject m_pauseMenu;

    Vector3 m_lastPos;
    Vector3 m_lastCamPos;
    PlayerBird m_player;
    bool m_isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        m_lastCamPos = Camera.main.transform.position;
        m_lastPos = m_lastCamPos;
        m_lastCamPos.x -= m_horizSpacing;   // spawn the first pipe now
        m_lastPos.x += m_firstPipeAt;       // the first pipe is this far forward from the starting position

        // grab the player for future reference
        m_player = FindObjectOfType<PlayerBird>();

        SetPause(false);
    }

    // Update is called once per frame
    void Update()
    {
        {   // TODO make pipes

            Vector3 pos = Camera.main.transform.position;
            if (pos.x - m_lastCamPos.x >= m_horizSpacing){
                m_lastCamPos = pos;
                GameObject new_pipe = Instantiate(m_pipe);
                Vector3 temp = m_lastPos;
                temp.y = Random.Range(-m_vertSpacing,m_vertSpacing);
                new_pipe.transform.position= temp;
                m_horizSpacing = 8.0f - Time.timeSinceLevelLoad * 0.1f;
                if (m_horizSpacing <= 4.0f){
                    m_horizSpacing = 4.0f;
                }
                m_lastPos.x += m_horizSpacing;
            }
        }

        if (null == m_player)
        {   // Player Died
            StartCoroutine(GameOver());
        }

        // keys
        if (Input.GetKeyDown(KeyCode.Escape))
        {   // this doubles as the option key in the android navigation bar
            SetPause(!m_isPaused);
        }
    }

    IEnumerator GameOver()
    {
        // wait 3 seconds
        yield return new WaitForSecondsRealtime(3.0f);
        // and reload the scene
        SceneManager.LoadScene(0);
    }

    public void SetPause(bool setPause)
    {
        if (setPause)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
        m_pauseMenu.SetActive(setPause);
        m_isPaused = setPause;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
