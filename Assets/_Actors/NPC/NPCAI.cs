using UnityEngine;

namespace Game.Actors
{
    [RequireComponent(typeof(ActorAvatar))]
    public class NPCAI : MonoBehaviour
    {
        ActorAvatar avatar;
        [SerializeField] GameObject target;
        [SerializeField] float maxChaseDistance = 5f;
        [SerializeField] float destroyThreshold = 20f;
        [SerializeField] float stoppingDistance = 2f;
        bool isChasing = false;

        // Use this for initialization
        void Start()
        {
            avatar = GetComponent<ActorAvatar>();
        }

        // Update is called once per frame
        void Update()
        {
            if (target)
            {
                MoveToTarget();
                DestroyAfterThreshold();
            }
        }

        void MoveToTarget()
        {
            if (target && avatar.GetDistance(target) > stoppingDistance
                       && avatar.GetDistance(target) < maxChaseDistance)
            {
                if (!isChasing)
                {
                    isChasing = true;
                }
                avatar.MoveAvatar((target.transform.position - transform.position).normalized);
            }
            else if (isChasing)
            {
                isChasing = false;
                avatar.MoveAvatar(Vector2.zero);
            }
        }

        void DestroyAfterThreshold()
        {
            if (avatar.GetDistance(target) > destroyThreshold)
            {
                Destroy(gameObject);
            }
        }

        public void SetTarget(GameObject target)
        {
            this.target = target;
        }

        public void Untarget()
        {
            this.target = gameObject;
        }

    }
}
