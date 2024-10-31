using UnityEngine;
using UnityEngine.EventSystems;

public class CameraDragHandler : MonoBehaviour, IDragHandler
{
    public GameObject mainCamera;       // ��Ҫ���Ƶ����
    public float dragSpeed = 0.5f;  // �϶��ٶȣ�����������

    private Vector3 lastMousePosition;  // ��һ������λ��


    // �������λ�õı߽�
    public Vector2 minBounds;
    public Vector2 maxBounds;

    

    void Start()
    {
        
        
    }

    // ʵ�� OnDrag ��������Ӧ�϶��¼�
    public void OnDrag(PointerEventData eventData)
    {
        mainCamera.GetComponent<CameraController>().isFollowing = false;

        Vector3 delta = eventData.delta;
        Vector3 move = new Vector3(-delta.x * dragSpeed * Time.deltaTime, -delta.y * dragSpeed * Time.deltaTime, 0);

        // �������λ��
        mainCamera.transform.Translate(move, Space.World);

        // ��������ڱ߽緶Χ��
        float clampedX = Mathf.Clamp(mainCamera.transform.position.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(mainCamera.transform.position.y, minBounds.y, maxBounds.y);
        mainCamera.transform.position = new Vector3(clampedX, clampedY, mainCamera.transform.position.z);


    }
}

