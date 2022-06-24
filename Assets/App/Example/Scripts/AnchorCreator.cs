using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace FrameworkDesign.Example
{
    [RequireComponent(typeof(ARAnchorManager))]
    [RequireComponent(typeof(ARRaycastManager))]
    public class AnchorCreator : MonoBehaviour
    {
        [SerializeField]
        GameObject m_Prefab;//要放置的预制件
        public GameObject prefab
        {
            get => m_Prefab;
            set => m_Prefab = value;
        }

        static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
        List<ARAnchor> m_Anchors = new List<ARAnchor>();
        ARRaycastManager m_RaycastManager;
        ARAnchorManager m_AnchorManager;
        void Awake()
        {
            m_RaycastManager = GetComponent<ARRaycastManager>();
            m_AnchorManager = GetComponent<ARAnchorManager>();
        }
        void SetAnchorText(ARAnchor anchor, string text)
        {
            var canvasTextManager = anchor.GetComponent<CanvasTextManager>();
            if (canvasTextManager)
            {
                canvasTextManager.text = text;
            }
        }
        ARAnchor CreateAnchor(in ARRaycastHit hit)
        {
            ARAnchor anchor = null;
            // If we hit a plane, try to "attach" the anchor to the plane
            if (hit.trackable is ARPlane plane)
            {
                var planeManager = GetComponent<ARPlaneManager>();
                if (planeManager)
                {
                    Logger.Log("Creating anchor attachment.");
                    var oldPrefab = m_AnchorManager.anchorPrefab;                  
                    m_AnchorManager.anchorPrefab = prefab;//替换AnchorManager原有锚点预制件
                    anchor = m_AnchorManager.AttachAnchor(plane, hit.pose);//将锚点锚定到平面，当前hit.pose创建锚点
                    m_AnchorManager.anchorPrefab = oldPrefab;//还原AnchorManager原有锚点预制件
                    SetAnchorText(anchor, $"Attached to plane {plane.trackableId}");                    
                    return anchor;
                }
            }
            // Otherwise, just create a regular anchor at the hit pose
            Logger.Log("Creating regular anchor.");
            // Note: the anchor can be anywhere in the scene hierarchy
            var gameObject = Instantiate(prefab, hit.pose.position, hit.pose.rotation);//使用预制件和当前pose创建对象
            // Make sure the new GameObject has an ARAnchor component
            anchor = gameObject.GetComponent<ARAnchor>();
            if (anchor == null)
            {
                anchor = gameObject.AddComponent<ARAnchor>();//为当前对象添加锚点
            }
            SetAnchorText(anchor, $"Anchor (from {hit.hitType})");
            return anchor;
        }
        public void RemoveAllAnchors()
        {
            Logger.Log($"Removing all anchors ({m_Anchors.Count})");
            foreach (var anchor in m_Anchors)
            {
                Destroy(anchor.gameObject);//m_AnchorManager.RemoveAnchor(anchor);已弃用
            }
            m_Anchors.Clear();
        }
        void Update()
        {
            if (Input.touchCount == 0)
                return;
            var touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Began)
                return;
            // Raycast against planes and feature points射线的检测类型包括平面和
            const TrackableType trackableTypes =
                TrackableType.FeaturePoint |
                TrackableType.PlaneWithinPolygon;
            // Perform the raycast
            if (m_RaycastManager.Raycast(touch.position, s_Hits, trackableTypes))
            {
                // Raycast hits are sorted by distance, so the first one will be the closest hit.
                var hit = s_Hits[0];
                // Create a new anchor
                var anchor = CreateAnchor(hit);
                if (anchor)
                {
                    // Remember the anchor so we can remove it later.
                    m_Anchors.Add(anchor);//锚定以Component的形式对应于GameObject
                }
                else
                {
                    Logger.Log("Error creating anchor");
                }
            }
        }        
    }
}
