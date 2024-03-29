using AdventureOfZoldan.Core;
using AdventureOfZoldan.Inventories;
using UnityEngine;

namespace AdventureOfZoldan.Player
{
    [RequireComponent(typeof(Pickup))]
    public class RunoverPickup : MonoBehaviour
    {
        Pickup pickup;

        private void Awake()
        {
            pickup = GetComponent<Pickup>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {            
            if (collision.GetComponent<PlayerController>() != null)
            {
                if (pickup.CanBePickedUp())
                {
                    pickup.PickupItem();
                    AudioPlayer.Instance.PlayPickupClip();
                }
            }
        }
    }
}

