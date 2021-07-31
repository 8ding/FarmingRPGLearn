using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SwitchConfineBoundingShape : MonoBehaviour
{
    private void OnEnable()
    {
        EventHandler.AfterSceneLoadEvent += SwitchBoundingShape;
    }

    private void OnDisable()
    {
        EventHandler.AfterSceneLoadEvent -= SwitchBoundingShape;
    }
    
    /// <summary>
    /// 切换cinemachine所引用的界定镜头边界的碰撞体
    /// </summary>
    private void SwitchBoundingShape()
    {
        PolygonCollider2D polygonCollider2D = GameObject.FindGameObjectWithTag(Tags.BoundsConfiner).GetComponent<PolygonCollider2D>();
        CinemachineConfiner confiner = GetComponent<CinemachineConfiner>();
        confiner.m_BoundingShape2D = polygonCollider2D;

    }
}
