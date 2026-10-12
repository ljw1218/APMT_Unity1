using Unity.VectorGraphics;
using UnityEngine;

public class SwarmEnemy : MonoBehaviour
{
    [SerializeField] private float maxLifeTime = 12f;
    public bool isRushing {  get; private set; }
    private Vector2 dir;
    private float speed;
    private bool enteredScreen;
    private float timer;
    private float margin = 0.15f;

    private Rigidbody2D rigid;
    Camera cam;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    public void StartRush(Vector2 dir,float rushSpeed)
    {
        this.dir = dir;
        speed = rushSpeed;
        enteredScreen = false;
        timer = 0f;
        isRushing = true;

        GetComponent<SpriteRenderer>().flipX = dir.x < 0;
    }

    private void FixedUpdate()
    {
        if (!isRushing)
            return;
        rigid.MovePosition(rigid.position + dir * speed * Time.fixedDeltaTime);
        rigid.linearVelocity = Vector2.zero;
    }

    private void Update()
    {
        if (!isRushing)
            return;

        timer += Time.deltaTime;
        Vector3 vp = cam.WorldToViewportPoint(transform.position);
        bool inScreen = vp.x > 0 && vp.x < 1 && vp.y > 0 && vp.y < 1;

        if(inScreen)
        {
            enteredScreen = true;
            return;
        }

        bool farOut = vp.x < -1 * margin || vp.x > 1 + margin || vp.y < -1 * margin || vp.y > 1 + margin;
        if ((enteredScreen && farOut) || timer > maxLifeTime)
            Despawn();
    }

    private void Despawn()
    {
        isRushing = false;
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        isRushing = false;
    }
}
