namespace AllIn1SpringsToolkit
{
	public static class SpringsToolkitConstants
	{
		public const string ADD_COMPONENT_PATH = "AllIn1SpringsToolkit/";


		public const string CUSTOM_EDITOR_HEADER = "AllIn1SpringsToolkitCustomEditorHeader";

		public static string GetComponentPath(string componentName)
		{
			string res = $"{ADD_COMPONENT_PATH}/{componentName}";
			return res;
		}
	}
}