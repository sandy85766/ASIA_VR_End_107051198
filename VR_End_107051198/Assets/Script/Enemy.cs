using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("移動速度"), Range(0, 50)]
    public float speed = 3;
    [Header("停止距離"), Range(0, 50)]
    public float stopDistance = 2.5f;
    [Header("攻擊冷卻時間"), Range(0, 50)]
    public float cd = 2f;
    [Header("攻擊中心點")]
    public Transform atkPoint;
    [Header("攻擊長度"), Range(0f, 5f)]
    public float atkLength;

    private Transform player;
    private NavMeshAgent nav;
    private Animator ani;
    private float timer;  // 計時器

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        // 尋找其他遊戲物件("物件名稱").變形元件
        player = GameObject.Find("Player").transform;
        nav.speed = speed;
        nav.stoppingDistance = stopDistance;
    }

    private void Update()
    {
        Track();
        Attack();
    }


    
    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawRay(atkPoint.position, atkPoint.forward * atkLength);
    }

    // 射線擊中的物件
    private RaycastHit hit;

    // 攻擊
    private void Attack()
    {
        if (nav.remainingDistance < stopDistance)
        {
            // 時間 累加 (一幀的時間)
            timer += Time.deltaTime;

            // 取得玩家的座標
            Vector3 pos = player.position;
            // 將玩家座標Y軸 指定為 本物件的Y軸
            pos.y = transform.position.y;
            // 看向(玩家的座標)
            transform.LookAt(pos);

            if (timer >= cd)
            {
                ani.SetTrigger("攻擊觸發");
                timer = 0;
            }

        }
    }

    // 追蹤
    private void Track()
    {
        nav.SetDestination(player.position);
        ani.SetBool("跑步開關", nav.remainingDistance > stopDistance);
    }
}
