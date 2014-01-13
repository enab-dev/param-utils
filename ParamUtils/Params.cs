using System;
using System.Collections.Generic;
using System.Configuration;

namespace ParamUtils
{
	/// <summary>
	/// A utility class for reading in configuration settings cast as their intended type. Extends thread-safe singleton
	/// implementation and throws an error if the specified configuration entry is missing or is of the wrong type. Handles
	/// string, bool, int32 and int64 types.
	/// </summary>
	public class Params : SingletonBase<Params>
	{
		private Params() { }

		#region Error Handling

		private enum BadParamType { Missing, WrongType };
		private readonly Dictionary<BadParamType, string> BadParamTemplates = new Dictionary<BadParamType, string> () {
			{ BadParamType.Missing, "No configuration entry found for key {0}! Please add an entry of type {1}." },
			{ BadParamType.WrongType, "Configuration setting under key {0} is not of the type {1}!" }
		};

		private T HandleBadParameter<T> (string key, BadParamType errorType)
		{
			throw new ConfigurationErrorsException(string.Format(BadParamTemplates[errorType], key, typeof(T).ToString ()));
		}

		#endregion

		/// <summary>
		/// Gets the parameter specified by settingKey and returns it as type T.
		/// </summary>
		/// <returns>Parameter value</returns>
		/// <param name="settingKey">Configuration key</param>
		/// <typeparam name="T">Parameter type</typeparam>
		public T GetParameter<T> (string settingKey)
		{
			var settingValue = ConfigurationManager.AppSettings[settingKey];

			if (string.IsNullOrEmpty(settingValue))
			{
				return HandleBadParameter<T> (settingKey, BadParamType.Missing);
			}

			var typeCode = Type.GetTypeCode(typeof(T));

			switch (typeCode)
			{
				case TypeCode.String:
					return CastStringToType<T> (settingValue);
				case TypeCode.Boolean:
					if (settingValue.Equals ("true", StringComparison.InvariantCultureIgnoreCase)
					|| settingValue.Equals("false", StringComparison.InvariantCultureIgnoreCase))
					{
						return CastStringToType<T> (settingValue.ToLower());
					}
					break;
				case TypeCode.Int32:
					int tempInt;
					if (Int32.TryParse(settingValue, out tempInt))
					{
						return CastStringToType<T> (settingValue);
					}
					break;
				case TypeCode.Int64:
					long tempLong;
					if (Int64.TryParse(settingValue, out tempLong))
					{
						return CastStringToType<T> (settingValue);
					}
					break;
			}

			return HandleBadParameter<T> (settingKey, BadParamType.WrongType);
		}

		private T CastStringToType<T> (string settingValue)
		{
			return (T)Convert.ChangeType(settingValue, typeof(T));
		}
	}
}

