using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggingObject : MonoBehaviour
{

    public float m_ClickHoldTime = 0.1f;
    public float m_timeHold = 0f;
    private Transform m_CameraTransform;

    private bool m_EditingContent = false;

    private float m_MovePlaneDistance;
    public SettingModelController settingModelController; 
    void Start()
    {
        m_CameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_EditingContent)
        {
            Vector3 projection = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_MovePlaneDistance));
            transform.position = projection;
        }
    }

    private void OnMouseDrag()
    {
        if(this.gameObject!=settingModelController.targetGO){
            return;
        }
        m_timeHold += Time.deltaTime;

        if (m_timeHold >= m_ClickHoldTime && !m_EditingContent)
        {
            m_MovePlaneDistance = Vector3.Dot(transform.position - m_CameraTransform.position, m_CameraTransform.forward) / m_CameraTransform.forward.sqrMagnitude;
            m_EditingContent = true;
        }
    }

    private void OnMouseUp()
    {
        m_timeHold = 0f;
        m_EditingContent = false;
    }
}
