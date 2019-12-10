using System;
using System.Data.SqlClient;
using Assets.Editor;
using UnityEditor;
using UnityEngine;
using UnityFeedback;

public class ConnectionTest
{
	[MenuItem("Feedback/Test connection")]
	public static void TestConnection()
	{
		try
		{
			using (SqlConnection connection = new SqlConnection(FeedbackAPI.Configuration.ConnectionString(false)))
			{
				connection.Open(); // throws if invalid
				EditorUtility.DisplayDialog("Connection test", $"Successfully connected to database", "Ok");
			}
		}
		catch (Exception e)
		{
			EditorUtility.DisplayDialog("Connection test", $@"Error: {e.Message}", "Ok");
		}
	}

}