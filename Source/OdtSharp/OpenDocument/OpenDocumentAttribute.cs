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

namespace OdtSharp.OpenDocument
{
	//*-------------------------------------------------------------------------*
	//*	OpenDocumentAttributeCollection																					*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of OpenDocumentAttributeItem Items.
	/// </summary>
	public class OpenDocumentAttributeCollection :
		List<OpenDocumentAttributeItem>
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
		//* Dump																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Dump the collection of attributes to text.
		/// </summary>
		/// <param name="attributes">
		/// Reference to the collection of attributes to display.
		/// </param>
		public static void Dump(List<OpenDocumentAttributeItem> attributes)
		{
			if(attributes?.Count > 0)
			{
				foreach(OpenDocumentAttributeItem attributeItem in attributes)
				{
					Console.WriteLine(
						$"{attributeItem.DisplayName} : {attributeItem.MappedName}");
					//foreach(string elementNameItem in attributeItem.Elements)
					//{
					//	Console.WriteLine($" {elementNameItem}");
					//}
				}
			}
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	OpenDocumentAttributeItem																								*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Information about an individual attribute definition.
	/// </summary>
	public class OpenDocumentAttributeItem :
		IComparable<OpenDocumentAttributeItem>
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
		//* CompareTo																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Compare by display name alphabetically.
		/// </summary>
		/// <param name="otherItem">
		/// Reference to the other item to which this item will be compared.
		/// </param>
		/// <returns>
		/// < 0 if this item appears first, 0 if both items are in the same
		/// position, and > 0 if this item appears last.
		/// </returns>
		public int CompareTo(OpenDocumentAttributeItem otherItem)
		{
			int result = 1;
			if(otherItem != null)
			{
				result = $"{mDisplayName}|{mMappedName}".CompareTo(
					$"{otherItem.mDisplayName}|{otherItem.mMappedName}");
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	DisplayName																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="DisplayName">DisplayName</see>.
		/// </summary>
		private string mDisplayName = "";
		/// <summary>
		/// Get/Set the user display name of this item.
		/// </summary>
		[JsonProperty(Order = 0)]
		public string DisplayName
		{
			get { return mDisplayName; }
			set { mDisplayName = value; }
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	Elements																															*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Private member for <see cref="Elements">Elements</see>.
		///// </summary>
		//private List<string> mElements = new List<string>();
		///// <summary>
		///// Get a reference to the list of elements to which this attribute is
		///// associated.
		///// </summary>
		//[JsonProperty(Order = 2)]
		//public List<string> Elements
		//{
		//	get { return mElements; }
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	MappedName																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="MappedName">MappedName</see>.
		/// </summary>
		private string mMappedName = "";
		/// <summary>
		/// Get/Set the type-mapped unique display name of this item.
		/// </summary>
		[JsonProperty(Order = 1)]
		public string MappedName
		{
			get { return mMappedName; }
			set { mMappedName = value; }
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	ShouldSerializeElements																								*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Return a value indicating whether the Elements property should be
		///// serialized.
		///// </summary>
		///// <returns>
		///// A value indicating whether or not to serialize the property.
		///// </returns>
		//public bool ShouldSerializeElements()
		//{
		//	return mElements.Count > 0;
		//}
		////*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
