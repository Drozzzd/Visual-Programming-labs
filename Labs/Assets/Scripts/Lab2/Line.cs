using System;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public Block block1;
    public Block block2;
    private LineRenderer _lineRenderer;

    public static readonly List<Line> Lines = new();
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        Lines.Add(this);
        _lineRenderer = GetComponent<LineRenderer>();
    }

    [Obsolete("Obsolete")]
    private void Start()
    {
        _lineRenderer.SetWidth(0.2f, 0.2f);
        _lineRenderer.SetColors(Color.red, Color.green);
        _lineRenderer.SetPosition(0, Vector3.zero);
        _lineRenderer.SetPosition(1, Vector3.up);
        _lineRenderer.sortingOrder = 1;
        _lineRenderer.SetVertexCount(2);
        _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
    }

    private void Update()
    {
        // var pos1 = _camera.ScreenToWorldPoint(block1.transform.position);
        // _lineRenderer.SetPosition(0, pos1);
        // var pos2 = _camera.ScreenToWorldPoint(block2.transform.position);
        // _lineRenderer.SetPosition(1, pos2);
        _lineRenderer.SetPosition(0, block1.transform.position);
        _lineRenderer.SetPosition(1, block2.transform.position);
    }
}