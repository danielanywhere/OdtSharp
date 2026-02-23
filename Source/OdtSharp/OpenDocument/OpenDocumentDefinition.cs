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
using System.Reflection;
using System.Text;

using Newtonsoft.Json;

namespace OdtSharp.OpenDocument
{
	//*-------------------------------------------------------------------------*
	//*	OpenDocumentDefinition																									*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Definition data enumerating the traits of the OpenDocument specification.
	/// </summary>
	public class OpenDocumentDefinition
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
		private OpenDocumentAttributeCollection mAttributes =
			new OpenDocumentAttributeCollection();
		/// <summary>
		/// Get a reference to the collection of defined attributes.
		/// </summary>
		[JsonProperty(Order = 1)]
		public OpenDocumentAttributeCollection Attributes
		{
			get { return mAttributes; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Elements																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="Elements">Elements</see>.
		/// </summary>
		private OpenDocumentElementCollection mElements =
			new OpenDocumentElementCollection();
		/// <summary>
		/// Get a reference to the collection of defined elements.
		/// </summary>
		[JsonProperty(Order = 0)]
		public OpenDocumentElementCollection Elements
		{
			get { return mElements; }
		}
		//*-----------------------------------------------------------------------*

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
		public OpenDocumentElementTypeEnum GetTypeFromTag(string tagName)
		{
			OpenDocumentElementItem item = null;
			OpenDocumentElementTypeEnum localResult =
				OpenDocumentElementTypeEnum.None;
			OpenDocumentElementTypeEnum result = OpenDocumentElementTypeEnum.None;

			if(tagName?.Length > 0 && tagName != "text")
			{
				item = mElements.FirstOrDefault(x => x.MappedName == tagName);
				if(item != null)
				{
					if(Enum.TryParse<OpenDocumentElementTypeEnum>(item.DisplayName,
						true, out localResult))
					{
						result = localResult;
					}
				}
			}
			else
			{
				result = OpenDocumentElementTypeEnum.Text;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* LoadFromResource																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Load an OpenDocument definition from the local resource.
		/// </summary>
		/// <returns>
		/// Reference to the locally available built-in OpenDocument definition.
		/// </returns>
		public static OpenDocumentDefinition LoadFromResource()
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			string content = "";
			string resourceName = "OdtSharp.Resources.OpenDocumentDefinition.json";
			//string[] resourceNames = assembly.GetManifestResourceNames();
			OpenDocumentDefinition result = null;

			//foreach(string resourceNameItem in resourceNames)
			//{
			//	Trace.WriteLine(resourceNameItem);
			//}

			using(Stream stream = assembly.GetManifestResourceStream(resourceName))
			{
				if(stream != null)
				{
					using(StreamReader reader = new StreamReader(stream, Encoding.UTF8))
					{
						content = reader.ReadToEnd();
						result = JsonConvert.DeserializeObject<OpenDocumentDefinition>(
							content);
					}
				}
				else
				{
					Trace.WriteLine("Error reading document definition resource.");
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
