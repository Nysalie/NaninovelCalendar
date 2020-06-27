using Naninovel;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

namespace NaninovelCalendar
{
    public static class AssetMenuItems
    {
        private class DoCopyAsset : EndNameEditAction
        {
            public override void Action (int instanceId, string targetPath, string sourcePath)
            {
                AssetDatabase.CopyAsset(sourcePath, targetPath);
                var newAsset = AssetDatabase.LoadAssetAtPath<GameObject>(targetPath);
                ProjectWindowUtil.ShowCreatedAsset(newAsset);
            }
        }

        private static void CreatePrefabCopy (string prefabPath, string copyName)
        {
            var assetPath = PathUtils.AbsoluteToAssetPath(PathUtils.Combine(PackagePath.PrefabsPath, $"{prefabPath}.prefab"));
            CreateAssetCopy(assetPath, copyName);
        }

        private static void CreateAssetCopy (string assetPath, string copyName)
        {
            var targetPath = $"{copyName}.prefab";
            var endAction = ScriptableObject.CreateInstance<DoCopyAsset>();
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, endAction, targetPath, null, assetPath);
        }

        [MenuItem("Assets/Create/Naninovel/Calendar/Calendar UI")]
        private static void CreateBacklogUI () => CreatePrefabCopy("CalendarUI", "CalendarUI");
    }
}
