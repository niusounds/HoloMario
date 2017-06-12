using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Tooltip("Set in editor. Must not be null.")]
    public GameObject spatialUnderstanding;
    [Tooltip("Set in editor. Must not be null.")]
    public GameObject marioSpawner;
    private MarioSpawner marioSpawnerComponent;
    void Start()
    {
        marioSpawnerComponent = marioSpawner.GetComponent<MarioSpawner>();
    }
    void Update()
    {
        // Spatial Mappingの表示の切り替え
        if (Input.GetKeyDown(KeyCode.JoystickButton10))
        {
            spatialUnderstanding.SetActive(!spatialUnderstanding.activeSelf);
        }
        else if (Input.GetKeyDown(KeyCode.JoystickButton6))
        {
            marioSpawnerComponent.maxCount = Mathf.Max(1, marioSpawnerComponent.maxCount - 1);
        }
        else if (Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            marioSpawnerComponent.maxCount = Mathf.Min(127, marioSpawnerComponent.maxCount + 1);
        }
        // ジャンプ
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            MarioAction("Jump");
        }

        // 移動
        var axisX = Input.GetAxis("Horizontal");
        var axisY = Input.GetAxis("Vertical");
        Vector3 moveVector = Camera.main.transform.right * axisX + Camera.main.transform.forward * axisY;
        if (moveVector.sqrMagnitude > 0)
        {
            // Bダッシュ
            if (Input.GetKey(KeyCode.B) || Input.GetKey(KeyCode.Joystick1Button1))
            {
                moveVector *= 2;
            }

            MarioAction("Move", new Vector2(moveVector.x, moveVector.z));
        }

        // 向き
        if (axisX < 0)
        {
            MarioAction("SetOrientation", Mario.Orientation.Left);
        }
        else if (axisX > 0)
        {
            MarioAction("SetOrientation", Mario.Orientation.Right);
        }
    }

    // マリオオブジェクトにメッセージを送信する
    public static void MarioAction(string name, object value = null)
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players)
        {
            player.SendMessage(name, value);
        }
    }
}
