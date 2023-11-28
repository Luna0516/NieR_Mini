using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : Component
{
    /// <summary>
    /// 이미 최초 생성후 초기화를 했는지 확인하기 위한 변수
    /// </summary>
    private bool initialized = false;

    /// <summary>
    /// 이미 종료처리에 들어갔는지 확인하기 위한 변수
    /// </summary>
    private static bool isShutDown = false;

    /// <summary>
    /// 싱글톤의 객체
    /// </summary>
    private static T instance;

    /// <summary>
    /// 싱글톤의 객체를 읽기 위한 프로퍼티. 객체가 만들어지지 않았으면 새로 만든다.
    /// </summary>
    public static T Inst
    {
        get
        {
            // 종료처리에 들어간 상황이면
            if (isShutDown)
            {
                return null;
            }

            // instance가 없으면 새로 만든다.
            if (!instance)
            {
                // 씬에서 싱글톤 찾아보기
                T sigleton = FindObjectOfType<T>();

                // 씬에 싱글톤이 없으면
                if (sigleton)
                {
                    GameObject gameObj = new GameObject();
                    gameObj.name = $"{typeof(T).Name} Singleton";
                    // 싱글톤 컴포넌트 추가
                    sigleton = gameObj.AddComponent<T>();
                }

                instance = sigleton;
                // 씬이 사라질 때 게임오브젝트가 삭제되지 않도록 설정 => 새로 만든 거니까
                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (!instance)
        {
            instance = this as T;
            DontDestroyOnLoad(instance.gameObject);
        }
        else
        {
            // 첫번째 싱글톤 게임 오브젝트가 만들어진 이후에 만들어진 싱글톤 게임 오브젝트
            if (instance != this)
            {
                Destroy(this.gameObject);   // 첫번째 싱글톤과 다른 게임 오브젝트면 삭제
            }
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnSceneLoaded(Scene _, LoadSceneMode mode)
    {
        if (!initialized)
        {
            OnPreInitialize();
        }

        // 모드가 Additive가 아니면
        if (mode != LoadSceneMode.Additive)
        {
            OnInitialize();
        }
    }

    /// <summary>
    /// 프로그램이 종료될 때 실행되는 함수
    /// </summary>
    private void OnApplicationQuit()
    {
        isShutDown = true;
    }

    /// <summary>
    /// 싱글톤이 만들어질 때 단 한번만 호출될 초기화 함수
    /// </summary>
    protected virtual void OnPreInitialize()
    {
        initialized = true;
    }

    /// <summary>
    /// 싱글톤이 만들어지고 씬이 Single로 로드될 때마다 호출될 초기화 함수
    /// </summary>
    protected virtual void OnInitialize()
    {

    }
}
