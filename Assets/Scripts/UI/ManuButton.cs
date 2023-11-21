using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManuButton : MonoBehaviour
{
    /// <summary>
    /// 버튼의 종류 enum
    /// </summary>
    public enum ButtonType
    {
        None,
        Start,
        Manual,
        Exit,
        Close,
        Return
    }

    /// <summary>
    /// 이 버튼의 종류
    /// </summary>
    public ButtonType type;

    /// <summary>
    /// 다음 넘어갈 씬의 이름
    /// </summary>
    public string nextSceneName = "Map";

    /// <summary>
    /// 메뉴얼 게임 오브젝트
    /// </summary>
    private GameObject manualManu;

    private void Awake()
    {
        Button button = GetComponentInChildren<Button>();

        switch (type)
        {
            case ButtonType.Start:
                button.onClick.AddListener(OnStart);
                break;
            case ButtonType.Manual:
                button.onClick.AddListener(OnManual);
                manualManu = transform.GetChild(1).gameObject;
                break;
            case ButtonType.Exit:
                button.onClick.AddListener(OnExit);
                break;
            case ButtonType.Close:
                button.onClick.AddListener(OnClose);
                gameObject.SetActive(false);
                break;
            case ButtonType.Return:
                button.onClick.AddListener(OnReturn);
                break;
            case ButtonType.None:
            default:
                break;
        }
    }

    /// <summary>
    /// 게임 시작을 위한 함수
    /// </summary>
    private void OnStart()
    {
        StartCoroutine(LoadScene(nextSceneName));

        GameManager.Inst.State = GameManager.GameState.Play;
    }

    /// <summary>
    /// 메뉴얼 창을 여는 함수
    /// </summary>
    private void OnManual()
    {
        if (manualManu)
        {
            manualManu.SetActive(true);
        }
    }

    /// <summary>
    /// 게임 종료 함수
    /// </summary>
    private void OnExit()
    {
        Application.Quit();
    }

    /// <summary>
    /// 버튼이 있는 상위 오브젝트를 비활성화 하는 함수
    /// </summary>
    private void OnClose()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 처음 씬으로 돌아가는 함수
    /// </summary>
    private void OnReturn()
    {
        StartCoroutine(LoadScene(nextSceneName));

        GameManager.Inst.State = GameManager.GameState.Ready;
    }

    /// <summary>
    /// 씬 넘어가는 코루틴
    /// </summary>
    private IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);

        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            yield return null;
        }

        async.allowSceneActivation = true;
    }
}
