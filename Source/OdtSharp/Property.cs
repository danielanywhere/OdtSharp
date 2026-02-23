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
using System.Linq;
using System.Text;

using static OdtSharp.OdtSharpUtil;

namespace OdtSharp
{
	//*-------------------------------------------------------------------------*
	//*	PropertyCollection																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of PropertyItem Items.
	/// </summary>
	public class PropertyCollection : List<PropertyItem>
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
		//* SetPropertyValue																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set the value of the specified property on the provided target
		/// collection, creating a new value if it did not yet exist.
		/// </summary>
		/// <param name="properties">
		/// Reference to the collection of properties to receive the value.
		/// </param>
		/// <param name="propertyName">
		/// Name of the property to set.
		/// </param>
		/// <param name="propertyValue">
		/// Value to place on the property.
		/// </param>
		public static void SetPropertyValue(PropertyCollection properties,
			string propertyName, string propertyValue)
		{
			PropertyItem property = null;

			if(properties != null && propertyName?.Length > 0)
			{
				property = properties.FirstOrDefault(x =>
					StringComparer.OrdinalIgnoreCase.Equals(x.Name, propertyName));
				if(property == null)
				{
					property = new PropertyItem()
					{
						Name = propertyName
					};
					properties.Add(property);
				}
				if(propertyValue?.Length > 0)
				{
					property.Value = propertyValue;
				}
				else
				{
					property.Value = "";
				}
			}
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	PropertyItem																														*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Name and value of this property.
	/// </summary>
	public class PropertyItem
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
		//*	Name																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="Name">Name</see>.
		/// </summary>
		private string mName = "";
		/// <summary>
		/// Get/Set the name of this property.
		/// </summary>
		public string Name
		{
			get { return mName; }
			set { mName = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ToString																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the string representation of this item.
		/// </summary>
		/// <returns>
		/// The string representation of this item.
		/// </returns>
		public override string ToString()
		{
			string result = $"{SeparateWords(mName)} = {mValue}";

			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Value																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="Value">Value</see>.
		/// </summary>
		private string mValue = "";
		/// <summary>
		/// Get/Set the value of this property.
		/// </summary>
		public string Value
		{
			get { return mValue; }
			set { mValue = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
