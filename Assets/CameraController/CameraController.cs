using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;            // 当前相机跟随的目标
    public Vector2 minBounds;           // 摄像机移动的最小边界
    public Vector2 maxBounds;           // 摄像机移动的最大边界
    public float minZoom;      // 最小缩放值
    public float maxZoom;     // 最大缩放值
    public float zoomSpeed = 2f;  // 控制滚轮缩放的速度
    public float smoothSpeed = 0.125f;  // 跟随的平滑度
    public float dragSpeed = 2.0f;      // 鼠标拖动的速度

    //private Vector3 offset;             // 相机与目标的偏移量
    private Vector3 initialPosition;    // 相机的初始位置

    public bool isFollowing = true;    // 是否需要跟随

    void Start()
    {
        // 记录初始位置和偏移
        initialPosition = new Vector3(0,0,-2);
        minZoom = 1f;
        maxZoom = 5f; 
    }

    void Update()
    {
        // 拖动相机
       // HandleDrag();
        sizeControl();

        // 重置相机位置
        if (Input.GetKeyDown(KeyCode.R)) // 假设使用 R 键重置
        {
            //ResetCameraPosition();
            isFollowing = true;
        }
    }

    private void sizeControl()
    { 
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // 如果滚轮有输入
        if (scrollInput != 0)
        {
            // 修改相机的orthographicSize来进行缩放
            Camera.main.orthographicSize -= scrollInput * zoomSpeed;
            // 限制相机的缩放范围
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);
            //Debug.Log("Camera Size: " + Camera.main.orthographicSize);  // 打印当前缩放值
        }
    }

    void LateUpdate()
    {
        // 跟随目标对象
        if (target != null && isFollowing)
        {
            FollowTarget();
        }
    }

    // 跟随目标对象并限制在边界内
    void FollowTarget()
    {
        Vector3 desiredPosition = target.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // 限制相机在边界范围内
        float clampedX = Mathf.Clamp(smoothedPosition.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(smoothedPosition.y, minBounds.y, maxBounds.y);
        transform.position = new Vector3(clampedX, clampedY, smoothedPosition.z);
    }

    /* 鼠标拖动相机
    void HandleDrag()
    {
        if (Input.GetMouseButtonDown(0))  // 左键按下开始拖动
        {
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(0)) // 左键抬起停止拖动
        {
            isDragging = false;
        }

        if (isDragging)
        {
            float h = Input.GetAxis("Mouse X") * dragSpeed * Time.deltaTime;
            float v = Input.GetAxis("Mouse Y") * dragSpeed * Time.deltaTime;
            Vector3 dragDirection = new Vector3(-h, -v, 0);  // 相机移动的方向反转
            transform.Translate(dragDirection, Space.World);

            // 限制相机在边界范围内
            float clampedX = Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x);
            float clampedY = Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y);
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }*/

    // 重置相机位置到初始位置
    public void ResetCameraPosition()
    {
        isFollowing = false;
        transform.position = initialPosition;
    }

    // 更新相机跟随的目标
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;

    }
}
