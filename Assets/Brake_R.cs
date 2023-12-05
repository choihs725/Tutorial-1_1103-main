using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Brake_R : MonoBehaviour
{
    public InputActionReference brakeAction;
    public bool val = false;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        brakeAction.action.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        // brakeAction�� ���� ���� �о��
        float brakeValue = brakeAction.action.ReadValue<float>();

        // brakeValue�� 1�̸� (��ư�� ��������) val�� true�� ����
        val = brakeValue == 1;

        // ��ư�� ������ �� �α׸� ���
        if (val)
        {
            Debug.Log("Left Brake activated");
        }
    }
}
