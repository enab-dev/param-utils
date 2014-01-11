using System;
using System.Collections.Generic;
using System.Configuration;

namespace ParamUtils
{
	public static class Params
	{
		#region bad parameter handling

		private enum BadParamType { Missing, WrongType };
		private static readonly Dictionary<BadParamType, string> BadParamTemplates = new Dictionary<BadParamType, string> () {
			{ BadParamType.Missing, "No configuration entry found for key {0}! Please add an entry of type {1}." },
			{ BadParamType.WrongType, "Configuration setting under key {0} is not of the type {1}!" }
		};

		private static T HandleBadParameter<T> (string key, BadParamType errorType)
		{
			throw new ConfigurationErrorsException(string.Format(BadParamTemplates[errorType], key, typeof(T).ToString ()));
		}

		#endregion

		public static T GetParameter<T> (string settingKey)
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

		private static T CastStringToType<T> (string settingValue)
		{
			return (T)Convert.ChangeType(settingValue, typeof(T));
		}
	}
}

