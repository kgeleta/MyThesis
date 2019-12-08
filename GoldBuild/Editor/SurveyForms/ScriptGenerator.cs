using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Assets.Scripts.CoreFramework;

namespace Assets.Editor.SurveyForms
{
	public class ScriptGenerator
	{
		private readonly Type _contextType;
		private readonly Type _entityType;
		private readonly Dictionary<PropertyInfo, QuestionType> _mappedProperties;

		public ScriptGenerator(Type contextType, Type entityType, Dictionary<PropertyInfo, QuestionType> mappedProperties)
		{
			_contextType = contextType;
			_entityType = entityType;
			_mappedProperties = mappedProperties;
		}

		public string Generate(string className)
		{

			return 
$@"using System;
using Feedback.Model;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class {className} : MonoBehaviour
{{
	// field declaration
{this.GenerateFieldsDeclarations()}

	void Start()
	{{
		// get references from scene
{this.GenerateFiledInstantiation()}

		// Setup submit button
		GameObject.Find(""SubmitButton"").GetComponent<Button>().onClick.AddListener(this.Submit);
	}}

	 public void Submit()
    {{
		try
		{{
			using (var context = new {this._contextType.FullName}())
			{{
				var entity = new {this._entityType.FullName}()
				{{
					#warning Assign values to commented properties:
{this.GeneratePropertiesAssignment()}
				}};
			context.{this._entityType.Name}.Add(entity);
			context.SaveChanges();
			}}
		}}
		catch (Exception e)
		{{
			Debug.Log(e);
		}}
		finally
		{{
			SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
		}}
	}}

	private int getIntFromDescription(string description)
	 {{
		 if (description == ""not at all"")
		 {{
			 return 0;
		 }}

		 if (description == ""slightly"")
		 {{
			 return 1;
		 }}

		 if (description == ""moderately"")
		 {{
			 return 2;
		 }}

		 if (description == ""fairly"")
		 {{
			 return 3;
		 }}

		 return 4;
	 }}
}}";
		}

		private string GenerateFieldsDeclarations()
		{
			StringBuilder stringBuilder = new StringBuilder();

			foreach (var mappedProperty in this._mappedProperties)
			{
				stringBuilder.AppendLine(this.GenerateSingleFieldDeclaration(mappedProperty.Value,
					mappedProperty.Key.Name));
			}

			return stringBuilder.ToString();
		}

		private string GenerateSingleFieldDeclaration(QuestionType questionType, string propertyName)
		{
			switch (questionType)
			{
				case QuestionType.InputField:
					return $"\tprivate Text _{propertyName.ToLower()};";
				case QuestionType.Dropdown:
					return $"\tprivate Dropdown _{propertyName.ToLower()};";
				case QuestionType.RadioButtons:
					return $"\tprivate ToggleGroup _{propertyName.ToLower()};";
				case QuestionType.Slider:
					return $"\tprivate Slider _{propertyName.ToLower()};";
			}

			return string.Empty;
		}

		private string GenerateFiledInstantiation()
		{
			StringBuilder stringBuilder = new StringBuilder();

			foreach (var mappedProperty in this._mappedProperties)
			{
				stringBuilder.AppendLine(this.GenerateSingleFieldInstantiation(mappedProperty.Value,
					mappedProperty.Key.Name));
			}

			return stringBuilder.ToString();
		}

		private string GenerateSingleFieldInstantiation(QuestionType questionType, string propertyName)
		{
			switch (questionType)
			{
				case QuestionType.InputField:
					return $"\t\tthis._{propertyName.ToLower()} = GameObject.Find(\"{propertyName}\").transform.Find(\"Answer\").transform.Find(\"Text\").GetComponent<Text>();";
				case QuestionType.Dropdown:
					return $"\t\tthis._{propertyName.ToLower()} = GameObject.Find(\"{propertyName}\").transform.Find(\"Answer\").GetComponent<Dropdown>();";
				case QuestionType.RadioButtons:
					return $"\t\tthis._{propertyName.ToLower()} = GameObject.Find(\"{propertyName}\").transform.Find(\"Answer\").GetComponent<ToggleGroup>();";
				case QuestionType.Slider:
					return $"\t\tthis._{propertyName.ToLower()} = GameObject.Find(\"{propertyName}\").transform.Find(\"Answer\").GetComponent<Slider>();";
			}

			return string.Empty;
		}

		private string GeneratePropertiesAssignment()
		{
			StringBuilder stringBuilder = new StringBuilder();

			foreach (var mappedProperty in this._mappedProperties)
			{
				stringBuilder.AppendLine(this.GenerateSinglePropertyAssignment(mappedProperty.Value,mappedProperty.Key.Name));
			}

			return stringBuilder.ToString();
		}

		private string GenerateSinglePropertyAssignment(QuestionType questionType, string propertyName)
		{
			switch (questionType)
			{
				case QuestionType.InputField:
					return $"\t\t\t\t\t{propertyName} = this._{propertyName.ToLower()}.text,";
				case QuestionType.Dropdown:
					return $"\t\t\t\t\t{propertyName} = this.getIntFromDescription(this._{propertyName.ToLower()}.options[this._{propertyName.ToLower()}.value].text),";
				case QuestionType.RadioButtons:
					return $"\t\t\t\t\t{propertyName} = this.getIntFromDescription(this._{propertyName.ToLower()}.ActiveToggles().FirstOrDefault()?.GetComponentInChildren<Text>().text),";
				case QuestionType.Slider:
					return $"\t\t\t\t\t{propertyName} = (int)this._{propertyName.ToLower()}.value,";
			}

			return $"//\t\t\t\t\t{propertyName} = null,";
		}
	}
}