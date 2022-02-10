using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteAlways]
public class TileMapGridProperties : MonoBehaviour
{
    private Tilemap tilemap;

    [SerializeField] private SO_GridProperties gridProperties = null;
    [SerializeField] private GridBoolProperty gridBoolProperty = GridBoolProperty.diggable;

    private void OnEnable()
    {
        if(!Application.IsPlaying(gameObject))
        {
            tilemap = GetComponent<Tilemap>();
            if(gridProperties != null)
            {
                gridProperties.gridPropertyList.Clear();
            }
        }
    }

    private void OnDisable()
    {
        if(!Application.IsPlaying(gameObject))
        {
            UpdateGridProperites();
            if(gridProperties != null)
            {
                EditorUtility.SetDirty(gridProperties);
            }
        }
    }
}
