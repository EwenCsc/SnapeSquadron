namespace Editor
{
	using UnityEditor;
	using UnityEditor.UIElements;
	using UnityEngine.UIElements;

	public static class CustomEditorHelper
	{

		/// <summary>
		/// Method which overrides DrawDefaultInspector() with UIElements
		/// </summary>
		/// <param name="container">Where the inspector have to be display</param>
		/// <param name="serializedObject">Representing the objects or the objects being inspected (Correspond with the Editor's field : serializedObject)</param>
		/// <param name="hideScript">To hide or not the script "field"</param>
		public static void FillDefaultInspector(VisualElement container, SerializedObject serializedObject, bool hideScript)
		{
			SerializedProperty property = serializedObject.GetIterator();
			if (property.NextVisible(true)) // Expand first child.
			{
				do
				{
					if (property.propertyPath == "m_Script" && hideScript)
					{
						continue;
					}
					var field = new PropertyField(property);
					field.name = "PropertyField:" + property.propertyPath;


					if (property.propertyPath == "m_Script" && serializedObject.targetObject != null)
					{
						field.SetEnabled(false);
					}

					container.Add(field);
				}
				while (property.NextVisible(false));
			}
		}
	}
}
