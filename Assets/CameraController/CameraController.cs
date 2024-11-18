using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;            // ��ǰ��������Ŀ��
    public Vector2 minBounds;           // ������ƶ�����С�߽�
    public Vector2 maxBounds;           // ������ƶ������߽�
    public float minZoom;      // ��С����ֵ
    public float maxZoom;     // �������ֵ
    public float zoomSpeed = 2f;  // ���ƹ������ŵ��ٶ�
    public float smoothSpeed = 0.125f;  // �����ƽ����
    public float dragSpeed = 2.0f;      // ����϶����ٶ�

    //private Vector3 offset;             // �����Ŀ���ƫ����
    private Vector3 initialPosition;    // ����ĳ�ʼλ��

    public bool isFollowing = true;    // �Ƿ���Ҫ����

    void Start()
    {
        // ��¼��ʼλ�ú�ƫ��
        initialPosition = new Vector3(0,0,-2);
        minZoom = 1f;
        maxZoom = 5f; 
    }

    void Update()
    {
        // �϶����
       // HandleDrag();
        sizeControl();

        // �������λ��
        if (Input.GetKeyDown(KeyCode.R)) // ����ʹ�� R ������
        {
            //ResetCameraPosition();
            isFollowing = true;
        }
    }

    private void sizeControl()
    { 
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // �������������
        if (scrollInput != 0)
        {
            // �޸������orthographicSize����������
            Camera.main.orthographicSize -= scrollInput * zoomSpeed;
            // ������������ŷ�Χ
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);
            //Debug.Log("Camera Size: " + Camera.main.orthographicSize);  // ��ӡ��ǰ����ֵ
        }
    }

    void LateUpdate()
    {
        // ����Ŀ�����
        if (target != null && isFollowing)
        {
            FollowTarget();
        }
    }

    // ����Ŀ����������ڱ߽���
    void FollowTarget()
    {
        Vector3 desiredPosition = target.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // ��������ڱ߽緶Χ��
        float clampedX = Mathf.Clamp(smoothedPosition.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(smoothedPosition.y, minBounds.y, maxBounds.y);
        transform.position = new Vector3(clampedX, clampedY, smoothedPosition.z);
    }

    /* ����϶����
    void HandleDrag()
    {
        if (Input.GetMouseButtonDown(0))  // ������¿�ʼ�϶�
        {
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(0)) // ���̧��ֹͣ�϶�
        {
            isDragging = false;
        }

        if (isDragging)
        {
            float h = Input.GetAxis("Mouse X") * dragSpeed * Time.deltaTime;
            float v = Input.GetAxis("Mouse Y") * dragSpeed * Time.deltaTime;
            Vector3 dragDirection = new Vector3(-h, -v, 0);  // ����ƶ��ķ���ת
            transform.Translate(dragDirection, Space.World);

            // ��������ڱ߽緶Χ��
            float clampedX = Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x);
            float clampedY = Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y);
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }*/

    // �������λ�õ���ʼλ��
    public void ResetCameraPosition()
    {
        isFollowing = false;
        transform.position = initialPosition;
    }

    // ������������Ŀ��
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;

    }
}
