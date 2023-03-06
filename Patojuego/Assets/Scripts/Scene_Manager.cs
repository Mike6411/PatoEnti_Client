using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    public static Scene_Manager sceneManager;

    private void Awake()
    {
        if (sceneManager != null && sceneManager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            sceneManager = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadLoginRegisterScene()
    {
        SceneManager.LoadScene("loginregister_screen", LoadSceneMode.Additive);
    }

    public void LoadMatchMakingScene()
    {
        SceneManager.LoadScene("matchmaking_screen", LoadSceneMode.Additive);
    }

}
