using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Single - Ton
/// ���� �ý��� ������ ����
// ����������
/// ������ �ϱ� ���� ����
public class GameManager : MonoBehaviour
{
    // ���� ��ü
    // ���ٸ� ������ִ� ���
    // ����������
    /// �θ𿡼� �ڽ����� ���� ���ϴ� ���� �θ��ʿ� ���ο� ��ü�� ���� ���ó�� ���̰� �ϴ°�

    // animator ������Ʈ
    /// �ִϸ��̼��� ��������ִ� �����
    
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
