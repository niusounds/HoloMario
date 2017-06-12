using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class GestureController : MonoBehaviour
{
    private GestureRecognizer gestureRecognizer;

    void Start()
    {
        var jumped = false;
        gestureRecognizer = new GestureRecognizer();
        gestureRecognizer.NavigationUpdatedEvent += (InteractionSourceKind source, Vector3 normalizedOffset, Ray headRay) =>
        {
            // 向き
            if (normalizedOffset.x > 0)
            {
                Controller.MarioAction("SetOrientation", Mario.Orientation.Right);
            }
            else if (normalizedOffset.x < 0)
            {
                Controller.MarioAction("SetOrientation", Mario.Orientation.Left);
            }

            // 移動
            Vector3 moveVector = Camera.main.transform.right * normalizedOffset.x + Camera.main.transform.forward * normalizedOffset.z;
            if (moveVector.sqrMagnitude > 0)
            {
                Controller.MarioAction("Move", new Vector2(moveVector.x, moveVector.z));
            }

            // ジャンプ
            if (!jumped)
            {
                if (normalizedOffset.y >= 1.0)
                {
                    Controller.MarioAction("Jump");
                    jumped = true;
                }
            }
        };
        gestureRecognizer.NavigationCanceledEvent += (InteractionSourceKind source, Vector3 normalizedOffset, Ray headRay) =>
        {
            jumped = false;
        };
        gestureRecognizer.NavigationCompletedEvent += (InteractionSourceKind source, Vector3 normalizedOffset, Ray headRay) =>
        {
            jumped = false;
        };
        gestureRecognizer.StartCapturingGestures();
    }
}
