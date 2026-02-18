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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ActionEngine;
using OdtSharp;

namespace OdtSharpConsole
{
	//*-------------------------------------------------------------------------*
	//*	OdtWorkingDocumentCollection																						*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of OdtWorkingDocumentItem Items.
	/// </summary>
	public class OdtWorkingDocumentCollection : List<OdtWorkingDocumentItem>
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
	//*	OdtWorkingDocumentItem																									*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Implementation information about an individual ODT working document.
	/// </summary>
	public class OdtWorkingDocumentItem : ActionDocumentItem
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
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the OdtWorkingDocumentItem item.
		/// </summary>
		public OdtWorkingDocumentItem()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the OdtWorkingDocumentItem item.
		/// </summary>
		/// <param name="filename">
		/// Fully qualified path and filename of the file to open.
		/// </param>
		public OdtWorkingDocumentItem(string filename)
		{
			if(filename?.Length > 0)
			{
				InitializeDocument(filename);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Document																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="Document">Document</see>.
		/// </summary>
		private OdtDocumentItem mDocument = null;
		/// <summary>
		/// Get/Set a reference to the ODT document.
		/// </summary>
		public OdtDocumentItem Document
		{
			get { return mDocument; }
			set { mDocument = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	InitializeDocument																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Initialize the specialized document for general use.
		/// </summary>
		public void InitializeDocument(string filename)
		{
			FileInfo file = null;

			if(filename?.Length > 0)
			{
				Name = filename;
				file = new FileInfo(filename);
				if(file.Exists)
				{
					mDocument = OdtDocumentItem.Open(filename);
				}
			}
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

}
