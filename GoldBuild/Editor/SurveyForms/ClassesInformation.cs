using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Assets.Editor
{
	public class ClassesInformation
	{
		/// <summary>
		/// Finds all derived classes of <see cref="DbContext"/> in all assemblies in solution.
		/// </summary>
		/// <returns>List of derived classes of type <see cref="DbContext"/>.</returns>
		public static List<Type> GetAllContextClasses()
		{
			List<Type> contextTypes = FindAllDerivedTypes<DbContext>();
			return contextTypes;
		}

		public static Type GetGeneratedScriptClassByName(string name)
		{
			Debug.Log("inside find all derived types");
			foreach (var type in FindAllDerivedTypes<MonoBehaviour>())
			{
				Debug.Log(type.FullName);
				if (type.FullName == name)
				{
					return type;
				}
			}

			Debug.Log("find all derived return null");
			return null;
		}

		/// <summary>
		/// Finds all public properties of generic type <see cref="DbSet{TEntity}"/> of given class.
		/// </summary>
		/// <param name="contextType">Class with entity properties.</param>
		/// <returns>List of public properties of generic type <see cref="DbSet{TEntity}"/>.</returns>
		public static List<PropertyInfo> GetEntitiesOfContext(Type contextType)
		{
			var properties = new List<PropertyInfo>(contextType.GetProperties());
			return properties.FindAll(property => property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));
		}

		private static List<Type> FindAllDerivedTypes<T>()
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

			List<Type> result = new List<Type>();
			foreach (var assembly in assemblies)
			{
				result.AddRange(FindAllDerivedTypesInAssembly<T>(assembly));
			}

			return result;
		}

		private static List<Type> FindAllDerivedTypesInAssembly<T>(Assembly assembly)
		{
			var derivedType = typeof(T);
			return assembly
				.GetTypes()
				.Where(t =>
					t != derivedType &&
					derivedType.IsAssignableFrom(t)
				).ToList();
		}


	}
}