using UnityEngine;

// 扩展方法类
namespace Script.Scripts_HotUpdate
{
    public static class TransformExtensions
    {
        // 泛型扩展方法，返回第一个名字匹配的子孙物体上的指定组件
        public static T FindByName<T>(this Transform transform, string name) where T : Component
        {
            return GetComponentInChildrenWithNameRecursive<T>(transform, name);
        }
        
        public static void SetParent(this Transform transform,Transform parent, Vector3 localPos ) 
        {
            transform.SetParent(parent);
            transform.localPosition = localPos;
        }

        // 递归辅助方法来找到第一个匹配的组件
        private static T GetComponentInChildrenWithNameRecursive<T>(Transform transform, string name) where T : Component
        {
            foreach (Transform child in transform)
            {
                if (child.name == name)
                {
                    T component = child.GetComponent<T>();
                    if (component != null)
                    {
                        return component;
                    }
                }
                T foundComponent = GetComponentInChildrenWithNameRecursive<T>(child, name);
                if (foundComponent != null)
                {
                    return foundComponent;
                }
            }
            return null;
        }
    }
}