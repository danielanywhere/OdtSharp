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
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace OdtSharp
{
	//*-------------------------------------------------------------------------*
	//*	TextBlockTypeCollection																									*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of TextBlockTypeItem Items.
	/// </summary>
	public class TextBlockTypeCollection : List<TextBlockTypeItem>
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
		//* GetTypeFromTag																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the text block type enumeration value, given the XML tag.
		/// </summary>
		/// <param name="tagName">
		/// Name of the XML tag for which to return the known enumeration value.
		/// </param>
		/// <returns>
		/// The text block type enumeration value associated with the provided
		/// tag name, if found. Otherwise, TextBlockTypeEnum.None.
		/// </returns>
		public TextBlockTypeEnum GetTypeFromTag(string tagName)
		{
			TextBlockTypeItem item = null;
			TextBlockTypeEnum result = TextBlockTypeEnum.None;

			if(tagName?.Length > 0)
			{
				item = this.FirstOrDefault(x => x.NodeTag == tagName);
				if(item != null)
				{
					result = item.BlockType;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	TextBlockTypeItem																												*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Information about the text block type and its XML mapping.
	/// </summary>
	public class TextBlockTypeItem
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
		//*	BlockType																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="BlockType">BlockType</see>.
		/// </summary>
		private TextBlockTypeEnum mBlockType = TextBlockTypeEnum.None;
		/// <summary>
		/// Get/Set the type of this text block type.
		/// </summary>
		public TextBlockTypeEnum BlockType
		{
			get { return mBlockType; }
			set { mBlockType = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NodeTag																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="NodeTag">NodeTag</see>.
		/// </summary>
		private string mNodeTag = "";
		/// <summary>
		/// Get/Set the XML node tag of this item.
		/// </summary>
		public string NodeTag
		{
			get { return mNodeTag; }
			set { mNodeTag = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
