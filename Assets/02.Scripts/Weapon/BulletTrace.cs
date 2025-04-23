using UnityEngine;

public class BulletTrace : MonoBehaviour
{
    public float LifeTime;
    public Color TraceColor;
    public float TraceWidth;

    private LineRenderer _lineRenderer;
    private float _lifeTimer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        if (_lineRenderer.material == null)
        {
            _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        }
        _lineRenderer.startColor = TraceColor;
        _lineRenderer.endColor = TraceColor;
        _lineRenderer.startWidth = TraceWidth;
        _lineRenderer.endWidth = TraceWidth;
        _lifeTimer = 0f;
    }

    private void Update()
    {
        if (_lifeTimer > LifeTime)
        {
            gameObject.SetActive(false);
            _lifeTimer = 0f;
        }
        else
        {
            _lifeTimer += Time.deltaTime;
        }
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    public void Effect(Vector3 start, Vector3 end)
    {
        _lineRenderer.SetPosition(0, start);
        _lineRenderer.SetPosition(1, end);

        //gameObject.SetActive(true);
    }
}
