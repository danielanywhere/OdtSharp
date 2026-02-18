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
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using ActionEngine;
using OdtSharp;
using StyleAgnosticCommandArgs;

namespace OdtSharpConsole
{
	//*-------------------------------------------------------------------------*
	//*	Program																																	*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Main application instance of the OdtSharpConsole application.
	/// </summary>
	public class Program
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
		//*	_Main																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Configure and run the application.
		/// </summary>
		public static async Task Main(string[] args)
		{
			bool bShowHelp = false; //	Flag - Explicit Show Help.
			CommandArgCollection commandArgs = null;
			string key = "";        //	Current Parameter Key.
			string lowerArg = "";   //	Current Lowercase Argument.
			StringBuilder message = new StringBuilder();
			Program prg = new Program();  //	Initialized instance.

			ConsoleTraceListener consoleListener = new ConsoleTraceListener();
			Trace.Listeners.Add(consoleListener);
			Console.WriteLine("OdtSharpConsole.exe");

			//OdtActionItem.RecognizedActions.AddRange(new string[]
			//{
			//	""
			//});

			prg.mActionItem = new OdtActionItem();

			commandArgs = new CommandArgCollection(args);
			foreach(CommandArgItem argItem in commandArgs)
			{
				switch(argItem.Name)
				{
					case "":
						switch(argItem.Value.ToLower())
						{
							case "?":
								bShowHelp = true;
								break;
							case "wait":
								prg.mWaitAfterEnd = true;
								break;
						}
						break;
					case "action":
						if(argItem.Value.Length > 0)
						{
							prg.mActionItem.Action = argItem.Value;
						}
						break;
					case "infile":
						//	Input file.
						if(argItem.Value.Length > 0)
						{
							prg.mActionItem.InputFilename = argItem.Value;
							prg.mActionItem.OutputName = argItem.Value;
						}
						break;
					case "workingpath":
						prg.mActionItem.WorkingPath = argItem.Value;
						break;
				}
			}
			if(!bShowHelp)
			{
				if(prg.mActionItem.Action == "None" || prg.mActionItem.Action == "")
				{
					message.AppendLine("Please specify an /action:{ActionName}");
					bShowHelp = true;
				}
				if(prg.mActionItem.InputFilename.Length == 0)
				{
					message.AppendLine(
						"Please specify a file to open with /infile:{Filename}");
					bShowHelp = true;
				}
			}
			if(bShowHelp)
			{
				//	Display Syntax.
				Console.WriteLine(message.ToString() + "\r\n" + ResourceMain.Syntax);
			}
			else
			{
				//	Run the configured application.
				await prg.Run();
			}
			if(prg.mWaitAfterEnd)
			{
				Console.WriteLine("Press [Enter] to exit...");
				Console.ReadLine();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ActionItem																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="ActionItem">ActionItem</see>.
		/// </summary>
		private OdtActionItem mActionItem = null;
		/// <summary>
		/// Get/Set a reference to the action item for this session.
		/// </summary>
		public OdtActionItem ActionItem
		{
			get { return mActionItem; }
			set { mActionItem = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Run																																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Run the configured application.
		/// </summary>
		public async Task Run()
		{
			await mActionItem.Run();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	WaitAfterEnd																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="WaitAfterEnd">WaitAfterEnd</see>.
		/// </summary>
		private bool mWaitAfterEnd = false;
		/// <summary>
		/// Get/Set a value indicating whether to wait for user keypress after
		/// processing has completed.
		/// </summary>
		public bool WaitAfterEnd
		{
			get { return mWaitAfterEnd; }
			set { mWaitAfterEnd = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
