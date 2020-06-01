using UnityEngine;


public class CameraController : MonoBehaviour
{
    #region Variables

    [Header("Margins")]
    [SerializeField] [Range(0f, 10f)] private float xMargin = 0f;
    [SerializeField] [Range(0f, 10f)] private float yMargin = 0f;

    [Header("Smoothness")]
    [SerializeField] [Range(0f, 10f)] private float xSmooth = 4.5f;
    [SerializeField] [Range(0f, 10f)] private float ySmooth = 4.5f;

    private float boundsMinX;
    private float boundsMinY;
    private float boundsMaxX;
    private float boundsMaxY;

    private Transform player;
    new private Camera camera;

    #endregion Variables


    private void Start()
    {
        camera = GetComponent<Camera>();

        boundsMinX = -1f;
        boundsMinY = -1f;
        boundsMaxX = MapManager.Instance.width + 1f;
        boundsMaxY = MapManager.Instance.height + 1f;
    }


    private void LateUpdate()
    {
        player = PlayerLogicBehaviour.Instance.transform;

        if (player)
        {
            TrackPlayer();
        }
    }


    private bool CheckXMargin() => Mathf.Abs(transform.position.x - player.position.x) > xMargin;


    private bool CheckYMargin() => Mathf.Abs(transform.position.y - player.position.y) > yMargin;


    private void TrackPlayer()
    {
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        float camVertExtent = camera.orthographicSize;
        float camHorzExtent = camera.aspect * camVertExtent;

        float leftBound = boundsMinX + camHorzExtent;
        float rightBound = boundsMaxX - camHorzExtent;
        float bottomBound = boundsMinY + camVertExtent;
        float topBound = boundsMaxY - camVertExtent;

        if (CheckXMargin())
        {
            targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.deltaTime);
        }

        if (CheckYMargin())
        {
            targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.deltaTime);
        }

        targetX = Mathf.Clamp(targetX, leftBound, rightBound);
        targetY = Mathf.Clamp(targetY, bottomBound, topBound);

        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}
