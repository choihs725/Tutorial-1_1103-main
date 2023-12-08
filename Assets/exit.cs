using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class exit : MonoBehaviour
{
    public InputActionReference exitAction;
    public bool val = false;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        exitAction.action.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        // brakeAction�� ���� ���� �о��
        float exitValue = exitAction.action.ReadValue<float>();

        // brakeValue�� 1�̸� (��ư�� ��������) val�� true�� ����
        val = exitValue == 1;

        // ��ư�� ������ �� �α׸� ���
        if (val)
        {
            Debug.Log("exit activated");
        }
    }
}
