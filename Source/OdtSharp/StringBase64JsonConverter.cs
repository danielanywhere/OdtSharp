/*
 * Copyright (c). 2026 Daniel Patterson, MCSD (danielanywhere).
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace OdtSharp
{
	//*-------------------------------------------------------------------------*
	//*	StringBase64JsonConverter																								*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// String to base64 serializer and base64 to string deserializer.
	/// </summary>
	public class StringBase64JsonConverter : JsonConverter<string>
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* ReadJson																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Deserialize the JSON content to target object value.
		/// </summary>
		/// <param name="reader">
		/// Reference to the active reader.
		/// </param>
		/// <param name="objectType">
		/// Reference to the represented data type.
		/// </param>
		/// <param name="existingValue">
		/// The existing value of the target object.
		/// </param>
		/// <param name="hasExistingValue">
		/// Value indicating whether the target object already has a value.
		/// </param>
		/// <param name="serializer">
		/// Reference to the active serializer.
		/// </param>
		/// <returns>
		/// The deserialized representation of the JSON value.
		/// </returns>
		public override string ReadJson(JsonReader reader, Type objectType,
			string existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			string base64 = "";
			byte[] buffer = null;
			string result = "";

			if(reader.TokenType != JsonToken.Null)
			{
				base64 = (string)reader.Value;
				if(!string.IsNullOrEmpty(base64))
				{
					buffer = Convert.FromBase64String(base64);
					result = Encoding.UTF8.GetString(buffer);
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* WriteJson																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Serialize the source object value to a JSON value.
		/// </summary>
		/// <param name="writer">
		/// Reference to the active writer.
		/// </param>
		/// <param name="value">
		/// The source value to serialize.
		/// </param>
		/// <param name="serializer">
		/// Reference to the active serializer.
		/// </param>
		public override void WriteJson(JsonWriter writer, string value,
			JsonSerializer serializer)
		{
			string base64 = "";
			byte[] buffer = null;

			if(value != null)
			{
				buffer = Encoding.UTF8.GetBytes(value);
				base64 = Convert.ToBase64String(buffer);
				writer.WriteValue(base64);
			}
			else
			{
				writer.WriteNull();
			}
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
