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
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace OdtSharp
{
	//*-------------------------------------------------------------------------*
	//*	OdtSharpUtil																														*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Utility features and functionality for use with the OdtSharp library.
	/// </summary>
	public class OdtSharpUtil
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
		//* GetAttributeValue																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the value of the specified attribute.
		/// </summary>
		/// <param name="node">
		/// Reference to the node for which the attribute will be returned.
		/// </param>
		/// <param name="attributeName">
		/// Name of the attribute to retrieve.
		/// </param>
		/// <returns>
		/// Value of the specified attribute within the caller's node, if found.
		/// Otherwise, an empty string.
		/// </returns>
		public static string GetAttributeValue(XmlNode node, string attributeName)
		{
			XmlNode item = null;
			string result = "";

			if(node != null && node.Attributes.Count > 0 &&
				attributeName?.Length > 0)
			{
				item = node.Attributes.GetNamedItem(attributeName);
				if(item != null)
				{
					result = item.Value;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetAttributeValueBool																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the value of the specified attribute as a boolean value.
		/// </summary>
		/// <param name="node">
		/// Reference to the node for which the attribute will be returned.
		/// </param>
		/// <param name="attributeName">
		/// Name of the attribute to retrieve.
		/// </param>
		/// <returns>
		/// Value of the specified attribute within the caller's node, if found.
		/// Otherwise, false.
		/// </returns>
		public static bool GetAttributeValueBool(XmlNode node,
			string attributeName)
		{
			XmlNode item = null;
			bool result = false;

			if(node != null && node.Attributes.Count > 0 &&
				attributeName?.Length > 0)
			{
				item = node.Attributes.GetNamedItem(attributeName);
				if(item != null)
				{
					result = ToBool(item.Value);
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetAttributeValueInt																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the value of the specified attribute as an Int32 value.
		/// </summary>
		/// <param name="node">
		/// Reference to the node for which the attribute will be returned.
		/// </param>
		/// <param name="attributeName">
		/// Name of the attribute to retrieve.
		/// </param>
		/// <returns>
		/// Value of the specified attribute within the caller's node, if found.
		/// Otherwise, an empty string.
		/// </returns>
		public static int GetAttributeValueInt(XmlNode node, string attributeName)
		{
			XmlNode item = null;
			int number = 0;
			int result = 0;
			string value = "";

			if(node != null && node.Attributes.Count > 0 &&
				attributeName?.Length > 0)
			{
				item = node.Attributes.GetNamedItem(attributeName);
				if(item != null)
				{
					value = item.Value;
					if(value?.Length > 0)
					{
						if(int.TryParse(value, out number))
						{
							result = number;
						}
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetTempPath																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a new, unique temporary files path.
		/// </summary>
		/// <returns>
		/// New unique path that can be created and used for working purposes.
		/// </returns>
		public static string GetTempPath()
		{
			return
				Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("D"));
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ToBool																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Provide fail-safe conversion of string to boolean value.
		/// </summary>
		/// <param name="value">
		/// Value to convert.
		/// </param>
		/// <returns>
		/// Boolean value. False if not convertible.
		/// </returns>
		public static bool ToBool(object value)
		{
			bool result = false;
			if(value != null)
			{
				result = ToBool(value.ToString());
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Provide fail-safe conversion of string to boolean value.
		/// </summary>
		/// <param name="value">
		/// Value to convert.
		/// </param>
		/// <param name="defaultValue">
		/// The default value to return if the value was not present.
		/// </param>
		/// <returns>
		/// Boolean value. False if not convertible.
		/// </returns>
		public static bool ToBool(string value, bool defaultValue = false)
		{
			//	A try .. catch block was originally implemented here, but the
			//	following text was being sent to output on each unsuccessful
			//	match.
			//	Exception thrown: 'System.FormatException' in mscorlib.dll
			bool result = false;

			if(value?.Length > 0)
			{
				result = Regex.IsMatch(value, ResourceMain.rxBoolTrue);
			}
			else
			{
				result = defaultValue;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
