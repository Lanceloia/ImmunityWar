using UnityEngine;
using UnityEngine.EventSystems;

public class CameraDragHandler : MonoBehaviour, IDragHandler
{
    public GameObject mainCamera;       // 需要控制的相机
    public float dragSpeed = 0.5f;  // 拖动速度，调整灵敏度

    private Vector3 lastMousePosition;  // 上一次鼠标的位置


    // 限制相机位置的边界
    public Vector2 minBounds;
    public Vector2 maxBounds;

    

    void Start()
    {
        
        
    }

    // 实现 OnDrag 方法来响应拖动事件
    public void OnDrag(PointerEventData eventData)
    {
        mainCamera.GetComponent<CameraController>().isFollowing = false;

        Vector3 delta = eventData.delta;
        Vector3 move = new Vector3(-delta.x * dragSpeed * Time.deltaTime, -delta.y * dragSpeed * Time.deltaTime, 0);

        // 更新相机位置
        mainCamera.transform.Translate(move, Space.World);

        // 限制相机在边界范围内
        float clampedX = Mathf.Clamp(mainCamera.transform.position.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(mainCamera.transform.position.y, minBounds.y, maxBounds.y);
        mainCamera.transform.position = new Vector3(clampedX, clampedY, mainCamera.transform.position.z);


    }
}

