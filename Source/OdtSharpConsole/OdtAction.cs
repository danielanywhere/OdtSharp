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
	//*	OdtActionCollection																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of OdtActionItem Items.
	/// </summary>
	public class OdtActionCollection :
		ActionCollectionBase<OdtActionItem, OdtActionCollection>
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
	//*	OdtActionItem																														*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Information about an individual ODT console action.
	/// </summary>
	public class OdtActionItem :
		ActionItemBase<OdtActionItem, OdtActionCollection>
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* ExportJson																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Export the input file as JSON.
		/// </summary>
		/// <param name="action">
		/// Reference to the action containing information about the files to
		/// read and export.
		/// </param>
		private static void ExportJson(OdtActionItem action)
		{
			ActionDocumentItem actionDocument = null;
			string content = "";

			if(action != null)
			{
				if(CheckElements(action,
					ActionElementEnum.InputFilename | ActionElementEnum.OutputFilename))
				{
					actionDocument = GetWorkingDocument(action);
					if(actionDocument is OdtWorkingDocumentItem odtDocument)
					{
						content = OdtDocumentItem.ToJson(odtDocument.Document);
						File.WriteAllText(action.OutputFile.FullName, content);
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* OpenWorkingDocument																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Open the working file to allow multiple operations to be completed
		/// in the same session.
		/// </summary>
		protected override void OpenWorkingDocument()
		{
			StringBuilder builder = null;
			string content = "";
			ActionDocumentItem doc = null;
			int docIndex = 0;
			ActionOptionItem option = null;

			if(CheckElements(this,
				ActionElementEnum.InputFilename))
			{
				// Load the document if a filename was specified.
				docIndex = WorkingDocumentIndex;
				if(docIndex > -1 && docIndex < InputFiles.Count)
				{
					WorkingDocument =
						new OdtWorkingDocumentItem(InputFiles[docIndex].FullName);
					Trace.WriteLine(
						$" Working document: {this.InputFiles[docIndex].Name}",
						$"{MessageImportanceEnum.Info}");
					option = Options.FirstOrDefault(x => x.Name.ToLower() == "dump");
					if(option?.Value.Length > 0 &&
						WorkingDocument is OdtWorkingDocumentItem document)
					{
						builder = new StringBuilder();
						OdtDocumentItem.DumpElements(document.Document, builder);
						if(option.Value.ToLower() == "console")
						{
							Trace.WriteLine(builder.ToString());
						}
						else
						{
							File.WriteAllText(
								ActionEngineUtil.AbsolutePath(this.WorkingPath, option.Value),
									builder.ToString());
							Trace.WriteLine(
								$"Working document dump written to: {option.Value}");
						}
					}
				}
				else
				{
					Trace.WriteLine(
						$" Working document index out of range at: {docIndex}",
						$"{MessageImportanceEnum.Warn}");
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* RunCustomAction																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Run a custom action.
		/// </summary>
		protected override void RunCustomAction()
		{
			base.RunCustomAction();
			switch(this.Action.ToLower())
			{
				case "exportjson":
					ExportJson(this);
					break;
			}
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************

	}
	//*-------------------------------------------------------------------------*

}
