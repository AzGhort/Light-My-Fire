using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;

// Expand and collapse all components in Inspector window
// By explicitly choosing option from gizmo of each component

namespace LightMyFire
{
	public static class ExpandFoldAllInspector
	{
		[MenuItem("CONTEXT/Component/Collapse All")]
		private static void collapseAll(MenuCommand command) {
			setAllInspectorsExpanded((command.context as Component).gameObject, false);
		}

		[MenuItem("CONTEXT/Component/Expand All")]
		private static void expandAll(MenuCommand command) {
			setAllInspectorsExpanded((command.context as Component).gameObject, true);
		}

		private static void setAllInspectorsExpanded(GameObject go, bool expanded) {
			Component[] components = go.GetComponents<Component>();
			foreach (Component component in components) {
				if (component is Renderer) {
					var mats = ((Renderer)component).sharedMaterials;
					for (int i = 0; i < mats.Length; ++i) {
						InternalEditorUtility.SetIsInspectorExpanded(mats[i], expanded);
					}
				}
				InternalEditorUtility.SetIsInspectorExpanded(component, expanded);
			}
			ActiveEditorTracker.sharedTracker.ForceRebuild();
		}
	}
#endif
}