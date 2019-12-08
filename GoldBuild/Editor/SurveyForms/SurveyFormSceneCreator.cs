using Assets.Scripts.CoreFramework;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Editor
{
	internal class SurveyFormSceneCreator
	{
		private readonly Dictionary<PropertyInfo, QuestionType> _questions;

		private GameObject _canvas;
		private GameObject _panel;

		public SurveyFormSceneCreator(Dictionary<PropertyInfo, QuestionType> questions)
		{
			this._questions = questions;
		}

		public void Create()
		{
			var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);

			// Setup scene
			AddEventSystemToScene();
			AddCanvasToScene();
			AddPanelToCanvas();
			AddSubmitButtonToPanel();
			AddIntroTextToPanel();

			// add questions
			var questionFactory = new QuestionsFactory(this._panel);

			foreach (var question in _questions)
			{
				if (question.Value != QuestionType.Empty)
				{
					questionFactory.CreateQuestion(question.Value).name = question.Key.Name;
				}
			}

			// save the scene (open save file form)
			EditorSceneManager.SaveScene(scene);
		}

		private void AddEventSystemToScene()
		{
			var eventSystem = new GameObject("EventSystem");
			eventSystem.AddComponent<EventSystem>();
			eventSystem.AddComponent<StandaloneInputModule>();
		}

		private void AddCanvasToScene()
		{
			var canvasGameObject = new GameObject("Canvas");
			canvasGameObject.AddComponent<Canvas>();

			var canvas = canvasGameObject.GetComponent<Canvas>();
			canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			canvasGameObject.AddComponent<CanvasScaler>().scaleFactor = 2f;
			canvasGameObject.AddComponent<GraphicRaycaster>();

			this._canvas = canvasGameObject;
		}

		private void AddPanelToCanvas()
		{
			var panel = new GameObject("QuestionPanel");
			panel.AddComponent<CanvasRenderer>();
			Image image = panel.AddComponent<Image>();
			image.color = Color.gray;


			// Stretch panel
			var rectTransform = panel.GetComponent<RectTransform>();
			rectTransform.anchorMin = new Vector2(0, 0);
			rectTransform.anchorMax = new Vector2(1, 1);
			rectTransform.offsetMin = new Vector2(0, 0);
			rectTransform.offsetMax = new Vector2(0, 0);

			// Add panel to canvas
			panel.transform.SetParent(this._canvas.transform, false);
			this._panel = panel;
		}

		private void AddSubmitButtonToPanel()
		{
			var button = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("SubmitButton"));
			button.transform.SetParent(this._panel.transform, false);
			button.name = "SubmitButton";
		}

		private void AddIntroTextToPanel()
		{
			var text = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("IntroText"));
			text.transform.SetParent(this._panel.transform, false);
			text.name = "IntroText";
		}
	}
}