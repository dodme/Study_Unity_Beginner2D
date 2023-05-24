using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Single - Ton
/// 게임 시스템 관리를 위해
// 디자인패턴
/// 개발을 하기 좋은 구조
public class GameManager : MonoBehaviour
{
    // 정적 객체
    // 없다면 만들어주는 기능
    // 옵저버패턴
    /// 부모에서 자식으로 가지 못하는 것을 부모쪽에 새로운 객체를 만들어서 상속처럼 보이게 하는것

    // animator 컴포넌트
    /// 애니메이션을 재생시켜주는 재생기
    
    static GameManager _instance;
    public static GameManager Instance { get { Init(); return _instance; } }

    InputManager _input = new InputManager();
    SceneManagerEx _scene = new SceneManagerEx();

    public static InputManager Input { get => Instance._input; }
    public static SceneManagerEx Scene { get => Instance._scene; }

    private void Awake()
    {
        Init();
        _input.Init();
        _scene.Init();
    }

    private void Update()
    {
        _input.OnUpdate();
        _scene.OnUpdate();
    }

    public static void Init()
    {
        if (_instance == null)
        {
            GameObject go = GameObject.Find("@GameManager");

            if (go == null)
            {
                go = new GameObject { name = "@GameManager" };
                go.AddComponent<GameManager>();
            }

            _instance = go.GetComponent<GameManager>();
            DontDestroyOnLoad(go);
        }
    }

    private void OnDestroy()
    {
        _input.Clear();
        _scene.Clear();
    }
}
