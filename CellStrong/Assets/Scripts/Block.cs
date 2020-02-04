using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    Default = 0
}

public class Block : MonoBehaviour
{
    public BlockType blockType;
    public int layer;
    public SpriteRenderer sprite;

    public void UpdateLayer(int value)
    {
        if (layer == value)
            return;
        layer = value;
        if (layer > 10)
            layer = 10;
        sprite.sprite = GridManager.Instance.blockSpries[layer - 1];
    }

    public void ShiftLayer(int offset)
    {
        layer += offset;
        if(layer > 10)
            layer = 10;
        sprite.sprite = GridManager.Instance.blockSpries[layer - 1];
    }

    public void Die()
    {
        GameObject vfx = Instantiate(GridManager.Instance.destroyVFX, transform.position, Quaternion.identity);
        UpdateParticleColor(vfx, GridManager.Instance.blockColors[layer - 1]);
    }

    public void Land()
    {
        GameObject vfx = Instantiate(GridManager.Instance.landingVFX, transform.position - 0.5f * transform.up, Quaternion.identity);
        UpdateParticleColor(vfx, GridManager.Instance.blockColors[layer - 1]);
    }

    public void Land(Vector3 direction)
    {
        GameObject vfx = Instantiate(GridManager.Instance.landingVFX, transform.position +  0.5f * direction, Quaternion.identity);
        UpdateParticleColor(vfx, GridManager.Instance.blockColors[layer - 1]);
    }

    private void UpdateParticleColor(GameObject effectObj, Color color)
    {
        ParticleSystem.MainModule main = effectObj.GetComponent<ParticleSystem>().main;
        main.startColor = color;

        foreach (Transform child in effectObj.transform)
        {
            ParticleSystem ps = child.GetComponent<ParticleSystem>();
            if (ps == null)
                continue;

            main = ps.main;
            main.startColor = color;
        }
    }
}
