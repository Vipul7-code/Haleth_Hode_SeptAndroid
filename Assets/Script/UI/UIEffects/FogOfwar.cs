using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfwar : MonoBehaviour
{
    public GameObject m_fogOfWarPlane;
    public Transform m_player;
    public LayerMask m_fogLayer;
    public float m_radius = 1f;
    private float m_radiusSqr { get { return m_radius * m_radius; } }

    private Mesh m_mesh;
    private Vector3[] m_vertices;
    private Color[] m_colors;
    public bool mStart = false;

    // Use this for initialization
    void Start()
    {
        Debug.Log("objective scene::" + Globals.objectiveScene);
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (!mStart)
            return;
        Ray r = new Ray(transform.position, m_player.position - transform.position);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit, 1000, m_fogLayer, QueryTriggerInteraction.Collide))
        {
            for (int i = 0; i < m_vertices.Length; i++)
            {
                Vector3 v = m_fogOfWarPlane.transform.TransformPoint(m_vertices[i]);
                float dist = Vector3.SqrMagnitude(v - hit.point);
                if (dist < m_radiusSqr)
                {
                     float alpha = Mathf.Min(m_colors[i].a, dist / m_radiusSqr);
                    m_colors[i].a = alpha;
                }
            }
            UpdateColor();
        }
    }

    void Initialize()
    {
        m_fogOfWarPlane.GetComponent<MeshRenderer>().sortingOrder = 4;
        m_mesh = m_fogOfWarPlane.GetComponent<MeshFilter>().mesh;
        m_vertices = m_mesh.vertices;
        m_colors = new Color[m_vertices.Length];
        for (int i = 0; i < m_colors.Length; i++)
        {
            m_colors[i] = Color.grey;
        }
        UpdateColor();
    }

    void UpdateColor()
    {
        m_mesh.colors = m_colors;
    }
}
