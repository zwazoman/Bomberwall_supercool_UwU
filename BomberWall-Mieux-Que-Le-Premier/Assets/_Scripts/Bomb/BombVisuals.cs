using UnityEngine;

public class BombVisuals : MonoBehaviour
{
    [SerializeField] Bomb _bomb;
    [SerializeField] Color _color;

    MeshRenderer _renderer;
    MaterialPropertyBlock _propertyBlock;
    
    private void Awake()
    {
        //TryGetComponent(out _renderer);
        _renderer = GetComponent<MeshRenderer>();
        _propertyBlock = new MaterialPropertyBlock();
    }

    private void Start()
    {
        _bomb.OnBombExplode += BombExplosion;
    }

    private void Update()
    {
        _propertyBlock.SetColor("_BaseColor",_color);
        _renderer.SetPropertyBlock(_propertyBlock);
    }

    void BombExplosion()
    {
        //connard
    }
}
