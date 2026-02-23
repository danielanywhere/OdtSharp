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

using Newtonsoft.Json;

namespace OdtSharp.OpenDocument
{
	//*-------------------------------------------------------------------------*
	//*	OpenDocumentElementCollection																						*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of OpenDocumentElementItem Items.
	/// </summary>
	public class OpenDocumentElementCollection : List<OpenDocumentElementItem>
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
		/// <param name="elements">
		/// Reference to the collection of elements to display.
		/// </param>
		public static void Dump(List<OpenDocumentElementItem> elements)
		{
			if(elements?.Count > 0)
			{
				foreach(OpenDocumentElementItem elementItem in elements)
				{
					Console.WriteLine(
						$"{elementItem.DisplayName} : {elementItem.MappedName}");
					Console.WriteLine("Attributes:");
					foreach(string attributeItem in elementItem.Attributes)
					{
						Console.WriteLine($" {attributeItem}");
					}
					Console.WriteLine("Elements:");
					foreach(string childElementItem in elementItem.ChildElements)
					{
						Console.WriteLine($" {childElementItem}");
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	OpenDocumentElementItem																									*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Information about an OpenDocument element.
	/// </summary>
	public class OpenDocumentElementItem : IComparable<OpenDocumentElementItem>
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
		//*	Attributes																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="Attributes">Attributes</see>.
		/// </summary>
		private List<string> mAttributes = new List<string>();
		/// <summary>
		/// Get a reference to the collection of attributes available to this
		/// element.
		/// </summary>
		[JsonProperty(Order = 2)]
		public List<string> Attributes
		{
			get { return mAttributes; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ChildElements																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="ChildElements">ChildElements</see>.
		/// </summary>
		private List<string> mChildElements = new List<string>();
		/// <summary>
		/// Get a reference to the list of elements that can appear under this
		/// one.
		/// </summary>
		[JsonProperty(Order = 3)]
		public List<string> ChildElements
		{
			get { return mChildElements; }
		}
		//*-----------------------------------------------------------------------*

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
		public int CompareTo(OpenDocumentElementItem otherItem)
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

		//*-----------------------------------------------------------------------*
		//*	ShouldSerializeAttributes																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the Attributes property should be
		/// serialized.
		/// </summary>
		/// <returns>
		/// A value indicating whether or not to serialize the property.
		/// </returns>
		public bool ShouldSerializeAttributes()
		{
			return mAttributes.Count > 0;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ShouldSerializeChildElements																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the ChildElements property should be
		/// serialized.
		/// </summary>
		/// <returns>
		/// A value indicating whether or not to serialize the property.
		/// </returns>
		public bool ShouldSerializeChildElements()
		{
			return mChildElements.Count > 0;
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
