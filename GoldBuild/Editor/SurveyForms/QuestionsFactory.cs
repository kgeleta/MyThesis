using System;
using UnityEngine;

namespace Assets.Scripts.CoreFramework
{
	public class QuestionsFactory
	{
		private GameObject _parentObject;

		public QuestionsFactory(GameObject parentObject)
		{
			this._parentObject = parentObject;
		}

		public GameObject CreateQuestion(QuestionType questionType)
		{
			var name = questionType.ToString();
			try
			{
				var question = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(name));
				question.transform.SetParent(this._parentObject.transform, false);
				question.name = name;

				return question;
			}
			catch (ArgumentException)
			{
				throw new ArgumentException($@"Could not find prefab named {name} in Resources directory. Check your spelling or create this prefab.");
			}
		}
	}
}