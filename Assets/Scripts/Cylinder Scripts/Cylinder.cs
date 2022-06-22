using System.Collections.Generic;
using UnityEngine;

namespace Cylinder_Scripts
{
    public class Cylinder : MonoBehaviour
    {
        [Header("Helixes Explosion Force Info")]
        [SerializeField]
        private float force;
        [SerializeField]
        private float upForce;

        [Header("Components Used Explosion Info")]
        private readonly List<Rigidbody> _rigidbodies = new();
        private readonly List<GameObject> _pointHelixes = new();

        private void Start()
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                if (!transform.GetChild(i).TryGetComponent<Rigidbody>(out var rb)) continue;
                _rigidbodies.Add(rb);
            }
            
            for (var i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).TryGetComponent<Rigidbody>(out var rb)) continue;
                _pointHelixes.Add(transform.GetChild(i).gameObject);
            }
        }

        public void DamageIfPointIsScored()
        {
            foreach (var rb in _rigidbodies)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
                rb.transform.parent = null;
                rb.velocity = Vector3.zero;

                rb.AddExplosionForce(force, transform.position, 100, 0, ForceMode.Impulse);
                rb.AddForce(new Vector3(0, upForce, 0), ForceMode.Impulse);

                Destroy(rb.gameObject, 1);
            }
            
            foreach (var go in _pointHelixes)
            {
                go.SetActive(false);
            }
        }
    }
}
