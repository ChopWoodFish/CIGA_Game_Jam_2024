using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// namespace ShootingEditor2D
// {
    /// <summary>
    /// 通用2D Trigger
    /// </summary>
    public class Trigger2DCheck : MonoBehaviour
    {
        private Collider2D coll;
        private float colliderParam1;
        private float colliderParam2;
        private Vector2 colliderParam3;
        private Action OnCheck;
        
        private LayerMask TargetLayers;  // 一个mask是对多个layer的指示
        // public int EnterCount;
        // public bool Triggered
        // {
        //     get { return EnterCount > 0; }
        // }
        private RaycastHit2D[] hits;
        public bool HasHit => hits.Length > 0;

        private void Start()
        {
            gameObject.layer = LayerMask.NameToLayer("Checker");
            
            coll = GetComponent<Collider2D>();
            TargetLayers = coll.includeLayers;
            if (coll is CircleCollider2D circleCollider)
            {
                colliderParam1 = circleCollider.radius;
                OnCheck = CheckCircle;
            }
            else if (coll is BoxCollider2D boxCollider)
            {
                colliderParam1 = boxCollider.size.x;
                colliderParam2 = boxCollider.size.y;
                colliderParam3 = boxCollider.offset;
                OnCheck = CheckBox;
            }
        }

        public void Check()
        {
            OnCheck.Invoke();
        }

        /// <summary>
        /// 筛选挂载了指定脚本的物体等
        /// </summary>
        /// <param name="filterMethod"></param>
        public void Check(Func<GameObject, bool> filterMethod)
        {
            Check();
            List<RaycastHit2D> filteredHits = new List<RaycastHit2D>();
            foreach (var hit in hits)
            {
                if (filterMethod.Invoke(hit.transform.gameObject))
                {
                    filteredHits.Add(hit);
                }
            }

            hits = filteredHits.ToArray();
        }

        private void CheckCircle()
        {
            // bool hasHit = Physics2D.OverlapCircle(transform.position, colliderParam1, TargetLayers);
            hits = Physics2D.CircleCastAll(transform.position, colliderParam1,
                Vector2.zero, 0, TargetLayers);
            // Debug.Log($"hit ground {hits.Length}");
        }

        private void CheckBox()
        {
            hits = Physics2D.BoxCastAll(transform.position + (Vector3) colliderParam3,
                new Vector2(colliderParam1, colliderParam2), 0,
                Vector2.zero, 0, TargetLayers);

            // var listHit = hits.ToList();
            // foreach (var hit in listHit)
            // {
            //     if (hit.transform == transform)
            //     {
            //         listHit.Remove(hit);
            //     }
            // }
        }

        bool IsInLayerMask(GameObject obj, LayerMask mask)
        {
            var objLayerMask = 1 << obj.layer;
            return (mask & objLayerMask) > 0;
        }

        public GameObject GetFirstHitThing(bool excludeSelf = true)
        {
            if (hits.Length == 0)
            {
                return null;
            }
            
            return hits[0].transform.gameObject;

            // if (excludeSelf)
            // {
            //     foreach (var hit in hits)
            //     {
            //         if (hit.transform == transform)
            //         {
            //             continue;
            //         }
            //         return hit.transform.gameObject;
            //     }   
            // }
            // else
            // {
            //     return hits[0].transform.gameObject;
            // }
            //
            // return null;
        }

        public List<GameObject> GetAllHitThings()
        {
            List<GameObject> ret = new List<GameObject>();
            foreach (var hit in hits)
            {
                ret.Add(hit.transform.gameObject);
            }

            return ret;
        }
    }
// }