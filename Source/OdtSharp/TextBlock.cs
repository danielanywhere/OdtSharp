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

namespace OdtSharp
{
	//*-------------------------------------------------------------------------*
	//*	TextBlockCollection																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of TextBlockItem Items.
	/// </summary>
	public class TextBlockCollection : List<TextBlockItem>
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
	//*	TextBlockItem																														*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Information about an individual ODT text block.
	/// </summary>
	public class TextBlockItem
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
		//*	Blocks																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="Blocks">Blocks</see>.
		/// </summary>
		private TextBlockCollection mBlocks = new TextBlockCollection();
		/// <summary>
		/// Get a reference to the collection of child blocks in this block.
		/// </summary>
		public TextBlockCollection Blocks
		{
			get { return mBlocks; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	BlockType																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="BlockType">BlockType</see>.
		/// </summary>
		private TextBlockTypeEnum mBlockType = TextBlockTypeEnum.None;
		/// <summary>
		/// Get/Set the recognized type of this block.
		/// </summary>
		public TextBlockTypeEnum BlockType
		{
			get { return mBlockType; }
			set { mBlockType = value; }
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

			if(mBlockType != TextBlockTypeEnum.None)
			{
				result = mBlockType.ToString();
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
