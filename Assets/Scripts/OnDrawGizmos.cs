//using UnityEngine;
//using UnityEditor;

//[ExecuteInEditMode]
//public class SelectParentOnChildClick : MonoBehaviour
//{
//    void OnDrawGizmos()
//    {
//        // Check if a child is selected
//        if (Selection.activeGameObject != null && Selection.activeGameObject.transform != transform)
//        {
//            // Ensure the selected object is a child of this GameObject
//            if (Selection.activeGameObject.transform.IsChildOf(transform))
//            {
//                // Set the parent as the selected object
//                Selection.activeGameObject = gameObject;
//            }
//        }
//    }
//}
