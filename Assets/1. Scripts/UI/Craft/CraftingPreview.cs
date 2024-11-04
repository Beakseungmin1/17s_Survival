using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingPreview : MonoBehaviour
{
    private List<Collider> _colliders = new List<Collider>();

    [SerializeField] private int _layerGround;
    [SerializeField] private int _IGNORE_RAYCAST_LAYER = 2;

    [SerializeField] private Material _green;
    [SerializeField] private Material _red;
    public bool isBuilded;


    private void Update()
    {
        ChangeColor();
    }

    private void ChangeColor()
    {
        if (isBuilded == true)
        {
            return;
        }

        if (_colliders.Count > 0)
        {
            SetMaterial(_red);
        }
        else
        {
            SetMaterial(_green);
        }
    }

    public void SetMaterial(Material material)
    {
        foreach (Transform child in this.transform)
        {
            var newMaterials = new Material[child.GetComponent<Renderer>().materials.Length];

            for (int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = material;
            }

            child.GetComponent<Renderer>().materials = newMaterials;
        }

    }

    public bool isBuildable()
    {
        return _colliders.Count == 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isBuilded) return;

        if (other.gameObject.layer != _layerGround || other.gameObject.layer != _IGNORE_RAYCAST_LAYER)
        {
            _colliders.Add(other);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isBuilded) return;

        if (other.gameObject.layer != _layerGround || other.gameObject.layer != _IGNORE_RAYCAST_LAYER)
        {
            _colliders.Remove(other);

        }
    }

}
