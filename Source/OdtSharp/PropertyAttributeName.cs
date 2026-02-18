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

namespace OdtSharp
{
	//*-------------------------------------------------------------------------*
	//*	PropertyAttributeNameCollection																					*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of PropertyAttributeNameItem Items.
	/// </summary>
	public class PropertyAttributeNameCollection : List<PropertyAttributeNameItem>
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
	//*	PropertyAttributeNameItem																								*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Information about a relationship between a property name and its
	/// associated attribute name.
	/// </summary>
	public class PropertyAttributeNameItem
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
		//*	AttributeName																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="AttributeName">AttributeName</see>.
		/// </summary>
		private string mAttributeName = "";
		/// <summary>
		/// Get/Set the attribute name.
		/// </summary>
		public string AttributeName
		{
			get { return mAttributeName; }
			set { mAttributeName = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	PropertyName																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="PropertyName">PropertyName</see>.
		/// </summary>
		private string mPropertyName = "";
		/// <summary>
		/// Get/Set the property name.
		/// </summary>
		public string PropertyName
		{
			get { return mPropertyName; }
			set { mPropertyName = value; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

}
