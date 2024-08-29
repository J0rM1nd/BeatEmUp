using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Unit Unit;
    private bl_Joystick Joystick;
    private Button hitButton;
    public void Init(bl_Joystick joystick, Button button)
    {
        Joystick = joystick;
        hitButton = button;
    }

    private void Start()
    {
        Unit = GetComponent<Unit>();
        hitButton.onClick.AddListener(Unit.Punch);
    }
    private void Update()
    {
        //Vector3 direction = Vector3.zero;
        //if (Input.GetKey(KeyCode.W))
        //    direction += Vector3.right;
        //if (Input.GetKey(KeyCode.A))
        //    direction += Vector3.forward;
        //if (Input.GetKey(KeyCode.S))
        //    direction += Vector3.left;
        //if (Input.GetKey(KeyCode.D))
        //    direction += Vector3.back;
        Unit.direction = new Vector3(0.2f*Joystick.Vertical, 0f, -0.2f*Joystick.Horizontal);

        //if (Input.GetMouseButtonDown(0))
        //    Unit.Punch();
    }
}