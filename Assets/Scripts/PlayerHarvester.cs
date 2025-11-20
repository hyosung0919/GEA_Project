using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHarvester : MonoBehaviour
{
    public float rayDistance = 5f;
    public LayerMask hitMask = ~0;
    public int toolDamage = 1;
    public float hitCooldown = 0.15f;
    private float _nexHitTime;
    private Camera _cam;
    public Inventory inventory;
    InventoryUI invenUI;

    void Awake()
    {
        _cam = GetComponentInChildren<Camera>();
        if (inventory == null) inventory = gameObject.AddComponent<Inventory>();
        invenUI = FindObjectOfType<InventoryUI>();
    }

    void Update()
    {
        if (invenUI.selectedIndex < 0)
        {
            if (Input.GetMouseButton(0) && Time.time >= _nexHitTime)
            {
                _nexHitTime = Time.time + hitCooldown;

                Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                if (Physics.Raycast(ray, out var hit, rayDistance, hitMask, QueryTriggerInteraction.Ignore))
                {
                    var block = hit.collider.GetComponent<Block>();
                    if (block != null)
                    {
                        {
                            block.Hit(toolDamage, inventory);
                        }
                    }
                }
            }

        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                if (Physics.Raycast(ray, out var hit, rayDistance, hitMask, QueryTriggerInteraction.Ignore))
                {
                    Vector3Int placePos = AdjacentCellOnHitFace(hit);

                    BlockType seleted = invenUI.GetInventorySlot();
                    if (inventory.Consume(seleted, 1))
                    {
                        FindObjectOfType<NoiseVoxelMap>().PlaceTile(placePos, seleted);
                    }
                }
            }
        }

        static Vector3Int AdjacentCellOnHitFace(in RaycastHit hit)
        {
            Vector3 basecenter = hit.collider.transform.position;
            Vector3 adjCenter = basecenter + hit.normal;
            return Vector3Int.RoundToInt(adjCenter);
        }
    }
}