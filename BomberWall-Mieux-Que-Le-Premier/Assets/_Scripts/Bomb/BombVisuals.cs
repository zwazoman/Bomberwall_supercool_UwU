using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombVisuals : MonoBehaviour
{
    [SerializeField] Color _color = new Color(0,0,0);

    MeshRenderer _renderer;
    MaterialPropertyBlock _propertyBlock;
    

    private void Awake()
    {
        //TryGetComponent(out _renderer);
        _renderer = GetComponent<MeshRenderer>();
        _propertyBlock = new MaterialPropertyBlock();
    }

    private void Update()
    {
        _propertyBlock.SetColor("_BaseColor",_color);
        _renderer.SetPropertyBlock(_propertyBlock);
    }
}
