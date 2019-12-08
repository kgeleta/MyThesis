using Assets.Editor.SurveyForms;
using Assets.Scripts.CoreFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
	public class SurveyFormWizard : EditorWindow
	{
		private readonly List<Type> _contextTypes = ClassesInformation.GetAllContextClasses();

		private Vector2 _scrollPosContext;
		private Vector2 _scrollPosEntity;
		private Type _selectedContextType;
		private Type _selectedEntityType;
		private Dictionary<PropertyInfo, QuestionType> _propertyQuestions;
		private Window _currentWindow = Window.ContextClassSelection;

		[MenuItem("Feedback/Create survey form")]
		public static void Init()
		{
			// Get existing open window or if none, make a new one:
			SurveyFormWizard window = (SurveyFormWizard)EditorWindow.GetWindow(typeof(SurveyFormWizard), false, "Survey form creator");
			window.Show();
		}

		public void OnGUI()
		{
			switch (this._currentWindow)
			{
				case Window.ContextClassSelection:
					this.OnGUIContextClassSelection();
					break;
				case Window.PropertyMapping:
					this.OnGUIPropertiesMapping();
					break;
			}
		}

		private void OnGUIContextClassSelection()
		{
			GUILayout.FlexibleSpace();
			GUILayout.Label("Select context class: ");

			this._scrollPosContext = EditorGUILayout.BeginScrollView(this._scrollPosContext, GUILayout.Width(this.position.width - 20), GUILayout.Height(150));

			foreach (var contextType in _contextTypes)
			{
				if (GUILayout.Button(contextType.Name))
				{
					this._selectedContextType = contextType;
				}
			}

			EditorGUILayout.EndScrollView();

			if (this._selectedContextType != null)
			{
				GUILayout.Label("Select entity class: ");
			}
			this._scrollPosEntity = EditorGUILayout.BeginScrollView(this._scrollPosEntity, GUILayout.Width(this.position.width - 20), GUILayout.Height(150));

			if (this._selectedContextType != null)
			{
				foreach (var entityClass in ClassesInformation.GetEntitiesOfContext(this._selectedContextType))
				{
					if (GUILayout.Button(entityClass.PropertyType.GetGenericArguments()[0].Name))
					{
						this._selectedEntityType = entityClass.PropertyType.GetGenericArguments()[0];
						var properties = new List<PropertyInfo>(this._selectedEntityType.GetProperties());

						this._propertyQuestions = new Dictionary<PropertyInfo, QuestionType>();
						foreach (var publicProperty in properties)
						{
							this._propertyQuestions.Add(publicProperty, QuestionType.Empty);
						}

						this._currentWindow = Window.PropertyMapping;
					}
				}
			}

			EditorGUILayout.EndScrollView();

			GUILayout.FlexibleSpace();
		}

		private void OnGUIPropertiesMapping()
		{
			GUILayout.FlexibleSpace();
			GUILayout.Label("Select question type for each property: ");
			GUILayout.FlexibleSpace();

			foreach (var property in this._propertyQuestions.Keys.ToList())
			{
				EditorGUILayout.BeginHorizontal();

				string type = property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)
					? $@"{property.PropertyType.GetGenericArguments()[0].Name}?"
					: property.PropertyType.Name;

				GUILayout.Label($@"{type}", GUILayout.Width(90));
				GUILayout.Label($@"{property.Name}", GUILayout.Width(170));

				this._propertyQuestions[property] =
					(QuestionType)EditorGUILayout.EnumPopup(this._propertyQuestions[property]);

				EditorGUILayout.EndHorizontal();
			}

			GUILayout.FlexibleSpace();
			GUILayout.BeginHorizontal();

			if (GUILayout.Button("back"))
			{
				this._currentWindow = Window.ContextClassSelection;
			}
			if (GUILayout.Button("Generate survey form"))
			{
				var sceneGenerator = new SurveyFormSceneCreator(this._propertyQuestions);
				sceneGenerator.Create();

				var scriptGenerator = new ScriptGenerator(this._selectedContextType, this._selectedEntityType,
					this._propertyQuestions);

				var className = $"{this._selectedEntityType.Name}Script";
				string script = scriptGenerator.Generate(className);
				string path = $@"{Application.dataPath}\Model\{className}.cs";
				File.WriteAllText(path, script);
				AssetDatabase.Refresh();

				GameObject placeHolder = new GameObject("Scripts");
				this.Close();
				EditorUtility.DisplayDialog("Information", $"Add {className} script to Scripts GameObject", "Ok");

			}

			GUILayout.EndHorizontal();
			GUILayout.Label(string.Empty);
		}

		private void AddScriptAutomatically(string className)
		{
			AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
			Type generatedClass = ClassesInformation.GetGeneratedScriptClassByName(className);
			GameObject placeHolder = new GameObject("Scripts");
			placeHolder.AddComponent(generatedClass);
		}
	}

	enum Window
	{
		ContextClassSelection,
		PropertyMapping
	}
}