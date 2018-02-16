using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class MarioSpawner : MonoBehaviour
{
    public GameObject prefab;
    [Range(0.85f, 10.0f)]
    public float distance = 2.0f;
    [Range(1, 127)]
    public int maxCount = 1;
    private GestureRecognizer gestureRecognizer;

    // Use this for initialization
    void Start()
    {
        gestureRecognizer = new GestureRecognizer();
        gestureRecognizer.TappedEvent += (InteractionSourceKind source, int tapCount, Ray headRay) =>
        {
            SpawnMario(headRay);
        };
        gestureRecognizer.StartCapturingGestures();
    }

    // Update is called once per frame
    void Update()
    {
        // マウスクリック時
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            SpawnMario(ray);
        }
        else if (Input.GetKeyDown(KeyCode.JoystickButton11))
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            SpawnMario(ray);
        }
    }

    // マウスクリック方向にレイキャストしてオブジェクトと衝突するかを調べる。
    // 衝突した場合はその位置より少し手前にマリオを出現。
    // 衝突しなかった場合は distance で設定した距離にマリオを出現。
    private void SpawnMario(Ray ray)
    {
        RaycastHit hit;
        Vector3 position;

        if (Physics.Raycast(ray, out hit, distance))
        {
            position = hit.point * 0.95f;
        }
        else
        {
            position = ray.GetPoint(distance);
        }

        // 以前作ったやつを削除
        int count = maxCount;
        foreach (Transform mario in transform)
        {
            if (count == 1)
            {
                Destroy(mario.gameObject);
            }
            else
            {
                count--;
            }
        }

        // 生成
        Instantiate(prefab, position, new Quaternion(), transform);
    }
}
