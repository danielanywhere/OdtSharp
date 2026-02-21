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

using Html;
using OdtSharp.OpenDocument;

namespace OdtSharp
{
	//*-------------------------------------------------------------------------*
	//*	ElementCollection																												*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of ElementItem Items.
	/// </summary>
	public class ElementCollection : List<ElementItem>
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

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	ElementItem																															*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Information about an individual ODT element block.
	/// </summary>
	public class ElementItem
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
		//*	Elements																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="Elements">Elements</see>.
		/// </summary>
		private ElementCollection mElements = new ElementCollection();
		/// <summary>
		/// Get a reference to the collection of child blocks in this block.
		/// </summary>
		public ElementCollection Elements
		{
			get { return mElements; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ElementType																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="ElementType">ElementType</see>.
		/// </summary>
		private OpenDocumentElementTypeEnum mElementType =
			OpenDocumentElementTypeEnum.None;
		/// <summary>
		/// Get/Set the recognized type of this block.
		/// </summary>
		public OpenDocumentElementTypeEnum ElementType
		{
			get { return mElementType; }
			set { mElementType = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NodeType																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="NodeType">NodeType</see>.
		/// </summary>
		private string mNodeType = "";
		/// <summary>
		/// Get/Set the type of node represented by this block.
		/// </summary>
		public string NodeType
		{
			get { return mNodeType; }
			set { mNodeType = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	OriginalContent																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="OriginalContent">OriginalContent</see>.
		/// </summary>
		private string mOriginalContent = "";
		/// <summary>
		/// Get/Set the original content of this item.
		/// </summary>
		internal string OriginalContent
		{
			get { return mOriginalContent; }
			set { mOriginalContent = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Properties																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="Properties">Properties</see>.
		/// </summary>
		private PropertyCollection mProperties = new PropertyCollection();
		/// <summary>
		/// Get a reference to the collection of properties on this item.
		/// </summary>
		public PropertyCollection Properties
		{
			get { return mProperties; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ToString																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the string representation of this item.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			string result = "";

			if(mElementType != OpenDocumentElementTypeEnum.None)
			{
				result = mElementType.ToString();
			}
			else
			{
				result = mNodeType;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
