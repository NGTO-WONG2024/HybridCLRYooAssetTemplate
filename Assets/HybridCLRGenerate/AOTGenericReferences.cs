using System.Collections.Generic;
public class AOTGenericReferences : UnityEngine.MonoBehaviour
{

	// {{ AOT assemblies
	public static readonly IReadOnlyList<string> PatchedAOTAssemblyList = new List<string>
	{
		"System.Core.dll",
		"Unity.Postprocessing.Runtime.dll",
		"Unity.RenderPipelines.Core.Runtime.dll",
		"UnityEngine.CoreModule.dll",
		"UnityEngine.JSONSerializeModule.dll",
		"UnityEngine.UIElementsModule.dll",
		"mscorlib.dll",
	};
	// }}

	// {{ constraint implement type
	// }} 

	// {{ AOT generic types
	// System.Action<MoreMountains.Feel.StrikePin>
	// System.Action<MoreMountains.Tools.MMAnimatorMirror.MMAnimatorMirrorBind>
	// System.Action<MoreMountains.Tools.MMDebug.DebugLogItem>
	// System.Action<MoreMountains.Tools.MMGeometry.MMEdge>
	// System.Action<MoreMountains.Tools.MMPersistent.ComponentData>
	// System.Action<MoreMountains.Tools.MMSoundManagerSound>
	// System.Action<MoreMountains.Tools.MMTilemapGeneratorLayer.MMTilemapGeneratorLayerSafeSpot>
	// System.Action<UnityEngine.Color>
	// System.Action<UnityEngine.Quaternion>
	// System.Action<UnityEngine.SceneManagement.Scene>
	// System.Action<UnityEngine.UIElements.RuleMatcher>
	// System.Action<UnityEngine.UIElements.StyleSelectorPart>
	// System.Action<UnityEngine.Vector2>
	// System.Action<UnityEngine.Vector3>
	// System.Action<UnityEngine.Vector3Int>
	// System.Action<UnityEngine.Vector4>
	// System.Action<byte>
	// System.Action<float,float>
	// System.Action<float>
	// System.Action<int,object>
	// System.Action<int>
	// System.Action<object,object>
	// System.Action<object>
	// System.Collections.Generic.ArraySortHelper<MoreMountains.Feel.StrikePin>
	// System.Collections.Generic.ArraySortHelper<MoreMountains.Tools.MMAnimatorMirror.MMAnimatorMirrorBind>
	// System.Collections.Generic.ArraySortHelper<MoreMountains.Tools.MMDebug.DebugLogItem>
	// System.Collections.Generic.ArraySortHelper<MoreMountains.Tools.MMGeometry.MMEdge>
	// System.Collections.Generic.ArraySortHelper<MoreMountains.Tools.MMPersistent.ComponentData>
	// System.Collections.Generic.ArraySortHelper<MoreMountains.Tools.MMSoundManagerSound>
	// System.Collections.Generic.ArraySortHelper<MoreMountains.Tools.MMTilemapGeneratorLayer.MMTilemapGeneratorLayerSafeSpot>
	// System.Collections.Generic.ArraySortHelper<UnityEngine.SceneManagement.Scene>
	// System.Collections.Generic.ArraySortHelper<UnityEngine.UIElements.RuleMatcher>
	// System.Collections.Generic.ArraySortHelper<UnityEngine.UIElements.StyleSelectorPart>
	// System.Collections.Generic.ArraySortHelper<UnityEngine.Vector3>
	// System.Collections.Generic.ArraySortHelper<UnityEngine.Vector3Int>
	// System.Collections.Generic.ArraySortHelper<byte>
	// System.Collections.Generic.ArraySortHelper<float>
	// System.Collections.Generic.ArraySortHelper<int>
	// System.Collections.Generic.ArraySortHelper<object>
	// System.Collections.Generic.Comparer<MoreMountains.Feel.StrikePin>
	// System.Collections.Generic.Comparer<MoreMountains.Tools.MMAnimatorMirror.MMAnimatorMirrorBind>
	// System.Collections.Generic.Comparer<MoreMountains.Tools.MMDebug.DebugLogItem>
	// System.Collections.Generic.Comparer<MoreMountains.Tools.MMGeometry.MMEdge>
	// System.Collections.Generic.Comparer<MoreMountains.Tools.MMPersistent.ComponentData>
	// System.Collections.Generic.Comparer<MoreMountains.Tools.MMSoundManagerSound>
	// System.Collections.Generic.Comparer<MoreMountains.Tools.MMTilemapGeneratorLayer.MMTilemapGeneratorLayerSafeSpot>
	// System.Collections.Generic.Comparer<UnityEngine.SceneManagement.Scene>
	// System.Collections.Generic.Comparer<UnityEngine.UIElements.RuleMatcher>
	// System.Collections.Generic.Comparer<UnityEngine.UIElements.StyleSelectorPart>
	// System.Collections.Generic.Comparer<UnityEngine.Vector3>
	// System.Collections.Generic.Comparer<UnityEngine.Vector3Int>
	// System.Collections.Generic.Comparer<byte>
	// System.Collections.Generic.Comparer<float>
	// System.Collections.Generic.Comparer<int>
	// System.Collections.Generic.Comparer<object>
	// System.Collections.Generic.Dictionary.Enumerator<int,UnityEngine.Color>
	// System.Collections.Generic.Dictionary.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.Enumerator<object,MoreMountains.Tools.MMSpeedTestItem>
	// System.Collections.Generic.Dictionary.Enumerator<object,int>
	// System.Collections.Generic.Dictionary.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,UnityEngine.Color>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,MoreMountains.Tools.MMSpeedTestItem>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,int>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection<int,UnityEngine.Color>
	// System.Collections.Generic.Dictionary.KeyCollection<int,object>
	// System.Collections.Generic.Dictionary.KeyCollection<object,MoreMountains.Tools.MMSpeedTestItem>
	// System.Collections.Generic.Dictionary.KeyCollection<object,int>
	// System.Collections.Generic.Dictionary.KeyCollection<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,UnityEngine.Color>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,MoreMountains.Tools.MMSpeedTestItem>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,int>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection<int,UnityEngine.Color>
	// System.Collections.Generic.Dictionary.ValueCollection<int,object>
	// System.Collections.Generic.Dictionary.ValueCollection<object,MoreMountains.Tools.MMSpeedTestItem>
	// System.Collections.Generic.Dictionary.ValueCollection<object,int>
	// System.Collections.Generic.Dictionary.ValueCollection<object,object>
	// System.Collections.Generic.Dictionary<int,UnityEngine.Color>
	// System.Collections.Generic.Dictionary<int,object>
	// System.Collections.Generic.Dictionary<object,MoreMountains.Tools.MMSpeedTestItem>
	// System.Collections.Generic.Dictionary<object,int>
	// System.Collections.Generic.Dictionary<object,object>
	// System.Collections.Generic.EqualityComparer<MoreMountains.Tools.MMSpeedTestItem>
	// System.Collections.Generic.EqualityComparer<UnityEngine.Color>
	// System.Collections.Generic.EqualityComparer<UnityEngine.Vector2>
	// System.Collections.Generic.EqualityComparer<byte>
	// System.Collections.Generic.EqualityComparer<float>
	// System.Collections.Generic.EqualityComparer<int>
	// System.Collections.Generic.EqualityComparer<object>
	// System.Collections.Generic.HashSet.Enumerator<int>
	// System.Collections.Generic.HashSet.Enumerator<object>
	// System.Collections.Generic.HashSet<int>
	// System.Collections.Generic.HashSet<object>
	// System.Collections.Generic.HashSetEqualityComparer<int>
	// System.Collections.Generic.HashSetEqualityComparer<object>
	// System.Collections.Generic.ICollection<MoreMountains.Feel.StrikePin>
	// System.Collections.Generic.ICollection<MoreMountains.Tools.MMAnimatorMirror.MMAnimatorMirrorBind>
	// System.Collections.Generic.ICollection<MoreMountains.Tools.MMDebug.DebugLogItem>
	// System.Collections.Generic.ICollection<MoreMountains.Tools.MMGeometry.MMEdge>
	// System.Collections.Generic.ICollection<MoreMountains.Tools.MMPersistent.ComponentData>
	// System.Collections.Generic.ICollection<MoreMountains.Tools.MMSoundManagerSound>
	// System.Collections.Generic.ICollection<MoreMountains.Tools.MMTilemapGeneratorLayer.MMTilemapGeneratorLayerSafeSpot>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,UnityEngine.Color>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,MoreMountains.Tools.MMSpeedTestItem>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,int>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.ICollection<UnityEngine.SceneManagement.Scene>
	// System.Collections.Generic.ICollection<UnityEngine.UIElements.RuleMatcher>
	// System.Collections.Generic.ICollection<UnityEngine.UIElements.StyleSelectorPart>
	// System.Collections.Generic.ICollection<UnityEngine.Vector3>
	// System.Collections.Generic.ICollection<UnityEngine.Vector3Int>
	// System.Collections.Generic.ICollection<byte>
	// System.Collections.Generic.ICollection<float>
	// System.Collections.Generic.ICollection<int>
	// System.Collections.Generic.ICollection<object>
	// System.Collections.Generic.IComparer<MoreMountains.Feel.StrikePin>
	// System.Collections.Generic.IComparer<MoreMountains.Tools.MMAnimatorMirror.MMAnimatorMirrorBind>
	// System.Collections.Generic.IComparer<MoreMountains.Tools.MMDebug.DebugLogItem>
	// System.Collections.Generic.IComparer<MoreMountains.Tools.MMGeometry.MMEdge>
	// System.Collections.Generic.IComparer<MoreMountains.Tools.MMPersistent.ComponentData>
	// System.Collections.Generic.IComparer<MoreMountains.Tools.MMSoundManagerSound>
	// System.Collections.Generic.IComparer<MoreMountains.Tools.MMTilemapGeneratorLayer.MMTilemapGeneratorLayerSafeSpot>
	// System.Collections.Generic.IComparer<UnityEngine.SceneManagement.Scene>
	// System.Collections.Generic.IComparer<UnityEngine.UIElements.RuleMatcher>
	// System.Collections.Generic.IComparer<UnityEngine.UIElements.StyleSelectorPart>
	// System.Collections.Generic.IComparer<UnityEngine.Vector3>
	// System.Collections.Generic.IComparer<UnityEngine.Vector3Int>
	// System.Collections.Generic.IComparer<byte>
	// System.Collections.Generic.IComparer<float>
	// System.Collections.Generic.IComparer<int>
	// System.Collections.Generic.IComparer<object>
	// System.Collections.Generic.IEnumerable<MoreMountains.Feel.StrikePin>
	// System.Collections.Generic.IEnumerable<MoreMountains.Tools.MMAnimatorMirror.MMAnimatorMirrorBind>
	// System.Collections.Generic.IEnumerable<MoreMountains.Tools.MMDebug.DebugLogItem>
	// System.Collections.Generic.IEnumerable<MoreMountains.Tools.MMGeometry.MMEdge>
	// System.Collections.Generic.IEnumerable<MoreMountains.Tools.MMPersistent.ComponentData>
	// System.Collections.Generic.IEnumerable<MoreMountains.Tools.MMSoundManagerSound>
	// System.Collections.Generic.IEnumerable<MoreMountains.Tools.MMTilemapGeneratorLayer.MMTilemapGeneratorLayerSafeSpot>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,UnityEngine.Color>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,MoreMountains.Tools.MMSpeedTestItem>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,int>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IEnumerable<UnityEngine.SceneManagement.Scene>
	// System.Collections.Generic.IEnumerable<UnityEngine.UIElements.RuleMatcher>
	// System.Collections.Generic.IEnumerable<UnityEngine.UIElements.StyleSelectorPart>
	// System.Collections.Generic.IEnumerable<UnityEngine.Vector3>
	// System.Collections.Generic.IEnumerable<UnityEngine.Vector3Int>
	// System.Collections.Generic.IEnumerable<byte>
	// System.Collections.Generic.IEnumerable<float>
	// System.Collections.Generic.IEnumerable<int>
	// System.Collections.Generic.IEnumerable<object>
	// System.Collections.Generic.IEnumerator<MoreMountains.Feel.StrikePin>
	// System.Collections.Generic.IEnumerator<MoreMountains.Tools.MMAnimatorMirror.MMAnimatorMirrorBind>
	// System.Collections.Generic.IEnumerator<MoreMountains.Tools.MMDebug.DebugLogItem>
	// System.Collections.Generic.IEnumerator<MoreMountains.Tools.MMGeometry.MMEdge>
	// System.Collections.Generic.IEnumerator<MoreMountains.Tools.MMPersistent.ComponentData>
	// System.Collections.Generic.IEnumerator<MoreMountains.Tools.MMSoundManagerSound>
	// System.Collections.Generic.IEnumerator<MoreMountains.Tools.MMTilemapGeneratorLayer.MMTilemapGeneratorLayerSafeSpot>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,UnityEngine.Color>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,MoreMountains.Tools.MMSpeedTestItem>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,int>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IEnumerator<UnityEngine.SceneManagement.Scene>
	// System.Collections.Generic.IEnumerator<UnityEngine.UIElements.RuleMatcher>
	// System.Collections.Generic.IEnumerator<UnityEngine.UIElements.StyleSelectorPart>
	// System.Collections.Generic.IEnumerator<UnityEngine.Vector3>
	// System.Collections.Generic.IEnumerator<UnityEngine.Vector3Int>
	// System.Collections.Generic.IEnumerator<byte>
	// System.Collections.Generic.IEnumerator<float>
	// System.Collections.Generic.IEnumerator<int>
	// System.Collections.Generic.IEnumerator<object>
	// System.Collections.Generic.IEqualityComparer<int>
	// System.Collections.Generic.IEqualityComparer<object>
	// System.Collections.Generic.IList<MoreMountains.Feel.StrikePin>
	// System.Collections.Generic.IList<MoreMountains.Tools.MMAnimatorMirror.MMAnimatorMirrorBind>
	// System.Collections.Generic.IList<MoreMountains.Tools.MMDebug.DebugLogItem>
	// System.Collections.Generic.IList<MoreMountains.Tools.MMGeometry.MMEdge>
	// System.Collections.Generic.IList<MoreMountains.Tools.MMPersistent.ComponentData>
	// System.Collections.Generic.IList<MoreMountains.Tools.MMSoundManagerSound>
	// System.Collections.Generic.IList<MoreMountains.Tools.MMTilemapGeneratorLayer.MMTilemapGeneratorLayerSafeSpot>
	// System.Collections.Generic.IList<UnityEngine.SceneManagement.Scene>
	// System.Collections.Generic.IList<UnityEngine.UIElements.RuleMatcher>
	// System.Collections.Generic.IList<UnityEngine.UIElements.StyleSelectorPart>
	// System.Collections.Generic.IList<UnityEngine.Vector3>
	// System.Collections.Generic.IList<UnityEngine.Vector3Int>
	// System.Collections.Generic.IList<byte>
	// System.Collections.Generic.IList<float>
	// System.Collections.Generic.IList<int>
	// System.Collections.Generic.IList<object>
	// System.Collections.Generic.KeyValuePair<int,UnityEngine.Color>
	// System.Collections.Generic.KeyValuePair<int,object>
	// System.Collections.Generic.KeyValuePair<object,MoreMountains.Tools.MMSpeedTestItem>
	// System.Collections.Generic.KeyValuePair<object,int>
	// System.Collections.Generic.KeyValuePair<object,object>
	// System.Collections.Generic.List.Enumerator<MoreMountains.Feel.StrikePin>
	// System.Collections.Generic.List.Enumerator<MoreMountains.Tools.MMAnimatorMirror.MMAnimatorMirrorBind>
	// System.Collections.Generic.List.Enumerator<MoreMountains.Tools.MMDebug.DebugLogItem>
	// System.Collections.Generic.List.Enumerator<MoreMountains.Tools.MMGeometry.MMEdge>
	// System.Collections.Generic.List.Enumerator<MoreMountains.Tools.MMPersistent.ComponentData>
	// System.Collections.Generic.List.Enumerator<MoreMountains.Tools.MMSoundManagerSound>
	// System.Collections.Generic.List.Enumerator<MoreMountains.Tools.MMTilemapGeneratorLayer.MMTilemapGeneratorLayerSafeSpot>
	// System.Collections.Generic.List.Enumerator<UnityEngine.SceneManagement.Scene>
	// System.Collections.Generic.List.Enumerator<UnityEngine.UIElements.RuleMatcher>
	// System.Collections.Generic.List.Enumerator<UnityEngine.UIElements.StyleSelectorPart>
	// System.Collections.Generic.List.Enumerator<UnityEngine.Vector3>
	// System.Collections.Generic.List.Enumerator<UnityEngine.Vector3Int>
	// System.Collections.Generic.List.Enumerator<byte>
	// System.Collections.Generic.List.Enumerator<float>
	// System.Collections.Generic.List.Enumerator<int>
	// System.Collections.Generic.List.Enumerator<object>
	// System.Collections.Generic.List<MoreMountains.Feel.StrikePin>
	// System.Collections.Generic.List<MoreMountains.Tools.MMAnimatorMirror.MMAnimatorMirrorBind>
	// System.Collections.Generic.List<MoreMountains.Tools.MMDebug.DebugLogItem>
	// System.Collections.Generic.List<MoreMountains.Tools.MMGeometry.MMEdge>
	// System.Collections.Generic.List<MoreMountains.Tools.MMPersistent.ComponentData>
	// System.Collections.Generic.List<MoreMountains.Tools.MMSoundManagerSound>
	// System.Collections.Generic.List<MoreMountains.Tools.MMTilemapGeneratorLayer.MMTilemapGeneratorLayerSafeSpot>
	// System.Collections.Generic.List<UnityEngine.SceneManagement.Scene>
	// System.Collections.Generic.List<UnityEngine.UIElements.RuleMatcher>
	// System.Collections.Generic.List<UnityEngine.UIElements.StyleSelectorPart>
	// System.Collections.Generic.List<UnityEngine.Vector3>
	// System.Collections.Generic.List<UnityEngine.Vector3Int>
	// System.Collections.Generic.List<byte>
	// System.Collections.Generic.List<float>
	// System.Collections.Generic.List<int>
	// System.Collections.Generic.List<object>
	// System.Collections.Generic.ObjectComparer<MoreMountains.Feel.StrikePin>
	// System.Collections.Generic.ObjectComparer<MoreMountains.Tools.MMAnimatorMirror.MMAnimatorMirrorBind>
	// System.Collections.Generic.ObjectComparer<MoreMountains.Tools.MMDebug.DebugLogItem>
	// System.Collections.Generic.ObjectComparer<MoreMountains.Tools.MMGeometry.MMEdge>
	// System.Collections.Generic.ObjectComparer<MoreMountains.Tools.MMPersistent.ComponentData>
	// System.Collections.Generic.ObjectComparer<MoreMountains.Tools.MMSoundManagerSound>
	// System.Collections.Generic.ObjectComparer<MoreMountains.Tools.MMTilemapGeneratorLayer.MMTilemapGeneratorLayerSafeSpot>
	// System.Collections.Generic.ObjectComparer<UnityEngine.SceneManagement.Scene>
	// System.Collections.Generic.ObjectComparer<UnityEngine.UIElements.RuleMatcher>
	// System.Collections.Generic.ObjectComparer<UnityEngine.UIElements.StyleSelectorPart>
	// System.Collections.Generic.ObjectComparer<UnityEngine.Vector3>
	// System.Collections.Generic.ObjectComparer<UnityEngine.Vector3Int>
	// System.Collections.Generic.ObjectComparer<byte>
	// System.Collections.Generic.ObjectComparer<float>
	// System.Collections.Generic.ObjectComparer<int>
	// System.Collections.Generic.ObjectComparer<object>
	// System.Collections.Generic.ObjectEqualityComparer<MoreMountains.Tools.MMSpeedTestItem>
	// System.Collections.Generic.ObjectEqualityComparer<UnityEngine.Color>
	// System.Collections.Generic.ObjectEqualityComparer<UnityEngine.Vector2>
	// System.Collections.Generic.ObjectEqualityComparer<byte>
	// System.Collections.Generic.ObjectEqualityComparer<float>
	// System.Collections.Generic.ObjectEqualityComparer<int>
	// System.Collections.Generic.ObjectEqualityComparer<object>
	// System.Collections.Generic.Queue.Enumerator<object>
	// System.Collections.Generic.Queue<object>
	// System.Collections.Generic.Stack.Enumerator<MoreMountains.Feedbacks.TimeScaleProperties>
	// System.Collections.Generic.Stack.Enumerator<object>
	// System.Collections.Generic.Stack<MoreMountains.Feedbacks.TimeScaleProperties>
	// System.Collections.Generic.Stack<object>
	// System.Collections.ObjectModel.ReadOnlyCollection<MoreMountains.Feel.StrikePin>
	// System.Collections.ObjectModel.ReadOnlyCollection<MoreMountains.Tools.MMAnimatorMirror.MMAnimatorMirrorBind>
	// System.Collections.ObjectModel.ReadOnlyCollection<MoreMountains.Tools.MMDebug.DebugLogItem>
	// System.Collections.ObjectModel.ReadOnlyCollection<MoreMountains.Tools.MMGeometry.MMEdge>
	// System.Collections.ObjectModel.ReadOnlyCollection<MoreMountains.Tools.MMPersistent.ComponentData>
	// System.Collections.ObjectModel.ReadOnlyCollection<MoreMountains.Tools.MMSoundManagerSound>
	// System.Collections.ObjectModel.ReadOnlyCollection<MoreMountains.Tools.MMTilemapGeneratorLayer.MMTilemapGeneratorLayerSafeSpot>
	// System.Collections.ObjectModel.ReadOnlyCollection<UnityEngine.SceneManagement.Scene>
	// System.Collections.ObjectModel.ReadOnlyCollection<UnityEngine.UIElements.RuleMatcher>
	// System.Collections.ObjectModel.ReadOnlyCollection<UnityEngine.UIElements.StyleSelectorPart>
	// System.Collections.ObjectModel.ReadOnlyCollection<UnityEngine.Vector3>
	// System.Collections.ObjectModel.ReadOnlyCollection<UnityEngine.Vector3Int>
	// System.Collections.ObjectModel.ReadOnlyCollection<byte>
	// System.Collections.ObjectModel.ReadOnlyCollection<float>
	// System.Collections.ObjectModel.ReadOnlyCollection<int>
	// System.Collections.ObjectModel.ReadOnlyCollection<object>
	// System.Comparison<MoreMountains.Feel.StrikePin>
	// System.Comparison<MoreMountains.Tools.MMAnimatorMirror.MMAnimatorMirrorBind>
	// System.Comparison<MoreMountains.Tools.MMDebug.DebugLogItem>
	// System.Comparison<MoreMountains.Tools.MMGeometry.MMEdge>
	// System.Comparison<MoreMountains.Tools.MMPersistent.ComponentData>
	// System.Comparison<MoreMountains.Tools.MMSoundManagerSound>
	// System.Comparison<MoreMountains.Tools.MMTilemapGeneratorLayer.MMTilemapGeneratorLayerSafeSpot>
	// System.Comparison<UnityEngine.SceneManagement.Scene>
	// System.Comparison<UnityEngine.UIElements.RuleMatcher>
	// System.Comparison<UnityEngine.UIElements.StyleSelectorPart>
	// System.Comparison<UnityEngine.Vector3>
	// System.Comparison<UnityEngine.Vector3Int>
	// System.Comparison<byte>
	// System.Comparison<float>
	// System.Comparison<int>
	// System.Comparison<object>
	// System.Func<System.Threading.Tasks.VoidTaskResult>
	// System.Func<UnityEngine.Color>
	// System.Func<UnityEngine.Quaternion>
	// System.Func<UnityEngine.Vector2>
	// System.Func<UnityEngine.Vector3>
	// System.Func<UnityEngine.Vector4>
	// System.Func<byte>
	// System.Func<float>
	// System.Func<int>
	// System.Func<object,System.Threading.Tasks.VoidTaskResult>
	// System.Func<object,byte>
	// System.Func<object,int>
	// System.Func<object,object>
	// System.Func<object>
	// System.Linq.Buffer<object>
	// System.Linq.Enumerable.<OfTypeIterator>d__97<object>
	// System.Linq.Enumerable.<SelectManyIterator>d__17<object,object>
	// System.Linq.Enumerable.Iterator<object>
	// System.Linq.Enumerable.WhereArrayIterator<object>
	// System.Linq.Enumerable.WhereEnumerableIterator<object>
	// System.Linq.Enumerable.WhereListIterator<object>
	// System.Linq.EnumerableSorter<object,int>
	// System.Linq.EnumerableSorter<object,object>
	// System.Linq.EnumerableSorter<object>
	// System.Linq.OrderedEnumerable.<GetEnumerator>d__1<object>
	// System.Linq.OrderedEnumerable<object,int>
	// System.Linq.OrderedEnumerable<object,object>
	// System.Linq.OrderedEnumerable<object>
	// System.Predicate<MoreMountains.Feel.StrikePin>
	// System.Predicate<MoreMountains.Tools.MMAnimatorMirror.MMAnimatorMirrorBind>
	// System.Predicate<MoreMountains.Tools.MMDebug.DebugLogItem>
	// System.Predicate<MoreMountains.Tools.MMGeometry.MMEdge>
	// System.Predicate<MoreMountains.Tools.MMPersistent.ComponentData>
	// System.Predicate<MoreMountains.Tools.MMSoundManagerSound>
	// System.Predicate<MoreMountains.Tools.MMTilemapGeneratorLayer.MMTilemapGeneratorLayerSafeSpot>
	// System.Predicate<UnityEngine.SceneManagement.Scene>
	// System.Predicate<UnityEngine.UIElements.RuleMatcher>
	// System.Predicate<UnityEngine.UIElements.StyleSelectorPart>
	// System.Predicate<UnityEngine.Vector3>
	// System.Predicate<UnityEngine.Vector3Int>
	// System.Predicate<byte>
	// System.Predicate<float>
	// System.Predicate<int>
	// System.Predicate<object>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Threading.Tasks.VoidTaskResult>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.Threading.Tasks.VoidTaskResult>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.Threading.Tasks.VoidTaskResult>
	// System.Runtime.CompilerServices.TaskAwaiter<System.Threading.Tasks.VoidTaskResult>
	// System.Threading.Tasks.Task<System.Threading.Tasks.VoidTaskResult>
	// System.Threading.Tasks.TaskFactory<System.Threading.Tasks.VoidTaskResult>
	// System.ValueTuple<object,byte>
	// UnityEngine.Events.InvokableCall<MoreMountains.Tools.MMSwipeEvent>
	// UnityEngine.Events.InvokableCall<UnityEngine.Vector2>
	// UnityEngine.Events.InvokableCall<byte>
	// UnityEngine.Events.InvokableCall<float>
	// UnityEngine.Events.InvokableCall<object>
	// UnityEngine.Events.UnityAction<MoreMountains.Tools.MMSwipeEvent>
	// UnityEngine.Events.UnityAction<UnityEngine.SceneManagement.Scene,UnityEngine.SceneManagement.Scene>
	// UnityEngine.Events.UnityAction<UnityEngine.SceneManagement.Scene,int>
	// UnityEngine.Events.UnityAction<UnityEngine.Vector2>
	// UnityEngine.Events.UnityAction<byte>
	// UnityEngine.Events.UnityAction<float>
	// UnityEngine.Events.UnityAction<object>
	// UnityEngine.Events.UnityEvent<MoreMountains.Tools.MMSwipeEvent>
	// UnityEngine.Events.UnityEvent<UnityEngine.Vector2>
	// UnityEngine.Events.UnityEvent<byte>
	// UnityEngine.Events.UnityEvent<float>
	// UnityEngine.Events.UnityEvent<object>
	// UnityEngine.Rendering.PostProcessing.ParameterOverride<UnityEngine.Color>
	// UnityEngine.Rendering.PostProcessing.ParameterOverride<UnityEngine.Vector2>
	// UnityEngine.Rendering.PostProcessing.ParameterOverride<float>
	// UnityEngine.Rendering.VolumeParameter<UnityEngine.Color>
	// UnityEngine.Rendering.VolumeParameter<UnityEngine.Vector2>
	// UnityEngine.Rendering.VolumeParameter<float>
	// UnityEngine.UIElements.EventBase.<>c<object>
	// UnityEngine.UIElements.EventBase<object>
	// UnityEngine.UIElements.EventCallback<object>
	// UnityEngine.UIElements.EventCallbackFunctor<object>
	// UnityEngine.UIElements.ObjectPool.<>c<object>
	// UnityEngine.UIElements.ObjectPool<object>
	// UnityEngine.UIElements.UQuery.PredicateWrapper<object>
	// UnityEngine.UIElements.UQueryBuilder.<>c<object>
	// UnityEngine.UIElements.UQueryBuilder<object>
	// UnityEngine.UIElements.UQueryState.ActionQueryMatcher<object>
	// UnityEngine.UIElements.UQueryState.Enumerator<object>
	// UnityEngine.UIElements.UQueryState.ListQueryMatcher<object,object>
	// UnityEngine.UIElements.UQueryState<object>
	// }}

	public void RefMethods()
	{
		// object[] System.Array.Empty<object>()
		// System.Void System.Array.Resize<UnityEngine.Vector3>(UnityEngine.Vector3[]&,int)
		// System.Void System.Array.Resize<int>(int[]&,int)
		// System.Void System.Array.Sort<object>(object[],System.Comparison<object>)
		// int System.Linq.Enumerable.Count<object>(System.Collections.Generic.IEnumerable<object>)
		// int System.Linq.Enumerable.Count<object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,bool>)
		// object System.Linq.Enumerable.FirstOrDefault<object>(System.Collections.Generic.IEnumerable<object>)
		// object System.Linq.Enumerable.Last<object>(System.Collections.Generic.IEnumerable<object>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.OfType<object>(System.Collections.IEnumerable)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.OfTypeIterator<object>(System.Collections.IEnumerable)
		// System.Linq.IOrderedEnumerable<object> System.Linq.Enumerable.OrderBy<object,object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,object>)
		// System.Linq.IOrderedEnumerable<object> System.Linq.Enumerable.OrderByDescending<object,int>(System.Collections.Generic.IEnumerable<object>,System.Func<object,int>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.SelectMany<object,object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,System.Collections.Generic.IEnumerable<object>>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.SelectManyIterator<object,object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,System.Collections.Generic.IEnumerable<object>>)
		// object[] System.Linq.Enumerable.ToArray<object>(System.Collections.Generic.IEnumerable<object>)
		// System.Collections.Generic.List<object> System.Linq.Enumerable.ToList<object>(System.Collections.Generic.IEnumerable<object>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.Where<object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,bool>)
		// object System.Reflection.CustomAttributeExtensions.GetCustomAttribute<object>(System.Reflection.MemberInfo)
		// System.Collections.Generic.IEnumerable<object> System.Reflection.CustomAttributeExtensions.GetCustomAttributes<object>(System.Reflection.MemberInfo,bool)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter,MoreMountains.Feedbacks.MMFeedbacks.<PlayFeedbacksTask>d__80>(System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter&,MoreMountains.Feedbacks.MMFeedbacks.<PlayFeedbacksTask>d__80&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter,MoreMountains.Feedbacks.MMFeedbacks.<PlayFeedbacksTask>d__81>(System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter&,MoreMountains.Feedbacks.MMFeedbacks.<PlayFeedbacksTask>d__81&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Threading.Tasks.VoidTaskResult>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter,MoreMountains.Feedbacks.MMFeedbacks.<PlayFeedbacksTask>d__80>(System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter&,MoreMountains.Feedbacks.MMFeedbacks.<PlayFeedbacksTask>d__80&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Threading.Tasks.VoidTaskResult>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter,MoreMountains.Feedbacks.MMFeedbacks.<PlayFeedbacksTask>d__81>(System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter&,MoreMountains.Feedbacks.MMFeedbacks.<PlayFeedbacksTask>d__81&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start<MoreMountains.Feedbacks.MMFeedbacks.<PlayFeedbacksTask>d__80>(MoreMountains.Feedbacks.MMFeedbacks.<PlayFeedbacksTask>d__80&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start<MoreMountains.Feedbacks.MMFeedbacks.<PlayFeedbacksTask>d__81>(MoreMountains.Feedbacks.MMFeedbacks.<PlayFeedbacksTask>d__81&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,MoreMountains.Feedbacks.MMF_MMSoundManagerSound.<TestPlaySound>d__82>(System.Runtime.CompilerServices.TaskAwaiter&,MoreMountains.Feedbacks.MMF_MMSoundManagerSound.<TestPlaySound>d__82&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,MoreMountains.Feedbacks.MMF_Sound.<TestPlaySound>d__52>(System.Runtime.CompilerServices.TaskAwaiter&,MoreMountains.Feedbacks.MMF_Sound.<TestPlaySound>d__52&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,MoreMountains.Tools.MMTilemapGenerator.<DelayedCopy>d__15>(System.Runtime.CompilerServices.TaskAwaiter&,MoreMountains.Tools.MMTilemapGenerator.<DelayedCopy>d__15&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<MoreMountains.Feedbacks.MMF_MMSoundManagerSound.<TestPlaySound>d__82>(MoreMountains.Feedbacks.MMF_MMSoundManagerSound.<TestPlaySound>d__82&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<MoreMountains.Feedbacks.MMF_Sound.<TestPlaySound>d__52>(MoreMountains.Feedbacks.MMF_Sound.<TestPlaySound>d__52&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<MoreMountains.Tools.MMTilemapGenerator.<DelayedCopy>d__15>(MoreMountains.Tools.MMTilemapGenerator.<DelayedCopy>d__15&)
		// object& System.Runtime.CompilerServices.Unsafe.As<object,object>(object&)
		// System.Void* System.Runtime.CompilerServices.Unsafe.AsPointer<object>(object&)
		// object UnityEngine.Component.GetComponent<object>()
		// object UnityEngine.Component.GetComponentInChildren<object>()
		// object UnityEngine.Component.GetComponentInParent<object>()
		// object[] UnityEngine.Component.GetComponentsInChildren<object>()
		// object[] UnityEngine.Component.GetComponentsInChildren<object>(bool)
		// object UnityEngine.GameObject.AddComponent<object>()
		// object UnityEngine.GameObject.GetComponent<object>()
		// object UnityEngine.GameObject.GetComponentInChildren<object>()
		// object UnityEngine.GameObject.GetComponentInChildren<object>(bool)
		// object UnityEngine.GameObject.GetComponentInParent<object>()
		// object UnityEngine.GameObject.GetComponentInParent<object>(bool)
		// object[] UnityEngine.GameObject.GetComponents<object>()
		// object[] UnityEngine.GameObject.GetComponentsInChildren<object>()
		// object[] UnityEngine.GameObject.GetComponentsInChildren<object>(bool)
		// MoreMountains.Tools.MMPersistent.Data UnityEngine.JsonUtility.FromJson<MoreMountains.Tools.MMPersistent.Data>(string)
		// object UnityEngine.JsonUtility.FromJson<object>(string)
		// object UnityEngine.Object.FindObjectOfType<object>()
		// object[] UnityEngine.Object.FindObjectsOfType<object>()
		// object[] UnityEngine.Object.FindObjectsOfType<object>(bool)
		// object UnityEngine.Object.Instantiate<object>(object)
		// object UnityEngine.Object.Instantiate<object>(object,UnityEngine.Transform)
		// object UnityEngine.Object.Instantiate<object>(object,UnityEngine.Transform,bool)
		// object UnityEngine.Object.Instantiate<object>(object,UnityEngine.Vector3,UnityEngine.Quaternion)
		// object UnityEngine.Object.Instantiate<object>(object,UnityEngine.Vector3,UnityEngine.Quaternion,UnityEngine.Transform)
		// bool UnityEngine.Rendering.PostProcessing.PostProcessProfile.TryGetSettings<object>(object&)
		// bool UnityEngine.Rendering.VolumeProfile.TryGet<object>(System.Type,object&)
		// bool UnityEngine.Rendering.VolumeProfile.TryGet<object>(object&)
		// object[] UnityEngine.Resources.ConvertObjects<object>(UnityEngine.Object[])
		// object[] UnityEngine.Resources.FindObjectsOfTypeAll<object>()
		// object UnityEngine.Resources.Load<object>(string)
		// System.Void UnityEngine.UIElements.CallbackEventHandler.AddEventCategories<object>(UnityEngine.UIElements.TrickleDown)
		// System.Void UnityEngine.UIElements.CallbackEventHandler.RegisterCallback<object>(UnityEngine.UIElements.EventCallback<object>,UnityEngine.UIElements.TrickleDown)
		// System.Void UnityEngine.UIElements.EventCallbackRegistry.RegisterCallback<object>(UnityEngine.UIElements.EventCallback<object>,UnityEngine.UIElements.TrickleDown,UnityEngine.UIElements.InvokePolicy)
		// object UnityEngine.UIElements.UQueryExtensions.Q<object>(UnityEngine.UIElements.VisualElement,string,string)
	}
}