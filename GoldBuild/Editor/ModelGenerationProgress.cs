using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
	public class ModelGenerationProgress : EditorWindow
	{
		private readonly List<Message> _messages = new List<Message>();
		private Vector2 _scrollPosMessage;

		public float ProgressValue = 0f;
		public string EndMessage = string.Empty;
		
		public void OnGUI()
		{
			EditorGUILayout.LabelField("");
			EditorGUILayout.LabelField("");

			EditorGUILayout.LabelField("Your model classes are being generated. This may take some time");
			EditorGUI.ProgressBar(new Rect(10, 70, position.width - 20, 20), this.ProgressValue, this.EndMessage);

			EditorGUILayout.LabelField("");
			EditorGUILayout.LabelField("");
			EditorGUILayout.LabelField("");

			EditorGUILayout.LabelField("Messages and errors:");
			EditorGUILayout.LabelField("");

			this._scrollPosMessage = EditorGUILayout.BeginScrollView(this._scrollPosMessage, GUILayout.Width(this.position.width - 6), GUILayout.Height(370));

			foreach (var message in this._messages)
			{
				GUILayout.Label($@"    {message.AddTime.ToLongTimeString()}: {message.TextMessage}", new GUIStyle { normal = { textColor = message.TextColor } });
			}

			EditorGUILayout.EndScrollView();

			if (GUI.Button(new Rect(600, 518, 50, 20), new GUIContent("Close")))
			{
				this.Close();
			}
			
		}

		public void AddError(string message)
		{
			this._messages.Add(new Message() { TextColor = Color.red, AddTime = DateTime.Now, TextMessage = message });
		}

		public void AddMessage(string message)
		{
			this._messages.Add(new Message() { TextColor = Color.black, AddTime = DateTime.Now, TextMessage = message });
		}

		private struct Message
		{
			public Color TextColor;
			public DateTime AddTime;
			public string TextMessage;
		}
	}
}