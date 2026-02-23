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
using System.Xml;

using Html;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using OdtSharp.OpenDocument;
using static OdtSharp.OdtSharpUtil;

namespace OdtSharp
{
	//*-------------------------------------------------------------------------*
	//*	OdtDocumentCollection																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of OdtDocumentItem Items.
	/// </summary>
	public class OdtDocumentCollection : List<OdtDocumentItem>
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
	//*	OdtDocumentItem																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// An individual ODT document.
	/// </summary>
	public class OdtDocumentItem
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		///// <summary>
		///// The map of property names to XML attribute counterparts.
		///// </summary>
		//private static PropertyAttributeNameCollection mPropertyNames =
		//	GetPropertyNames();
		/// <summary>
		/// The active debug directory for ODT files.
		/// </summary>
		private static string mOdtDebugDirectory = "";
		/// <summary>
		/// Load the document definition from the local resource.
		/// </summary>
		private static OpenDocumentDefinition mDocumentDefinition =
			OpenDocumentDefinition.LoadFromResource();
		//	C:\Temp\Active
		/// <summary>
		/// Value indicating whether C:\Temp is the current directory.
		/// </summary>
		private static bool mTempIsCurrentDirectory = true;
		///// <summary>
		///// The map of text block types to their XML tag counterparts.
		///// </summary>
		//private static TextBlockTypeCollection mTextBlockTypes =
		//	GetTextBlockTypeDefinitions();

		//*-----------------------------------------------------------------------*
		//* AddNodesAsBlocks																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Add HTML nodes to the provided blocks collection.
		/// </summary>
		/// <param name="nodes">
		/// </param>
		/// <param name="blocks">
		/// </param>
		private static void AddNodesAsBlocks(HtmlNodeCollection nodes,
			ElementCollection blocks)
		{
			ElementItem block = null;

			if(nodes?.Count > 0 && blocks != null)
			{
				foreach(HtmlNodeItem nodeItem in nodes)
				{
					if(nodeItem.NodeType == "text" || nodeItem.NodeType.Length == 0)
					{
						block = new ElementItem()
						{
							ElementType = OpenDocumentElementTypeEnum.Text
						};
						block.Properties.Add(new PropertyItem()
						{
							Name = "Text",
							Value = nodeItem.Text
						});
						blocks.Add(block);
					}
					else
					{
						block = new ElementItem()
						{
							ElementType = mDocumentDefinition.GetTypeFromTag(
								nodeItem.NodeType),
							NodeType = nodeItem.NodeType
						};
						if(block.ElementType == OpenDocumentElementTypeEnum.None)
						{
							block.OriginalContent = nodeItem.Html;
						}
						block.Properties.AddRange(GetMappedProperties(nodeItem));
						blocks.Add(block);
						AddNodesAsBlocks(nodeItem.Nodes, block.Elements);
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////* AddParagraph																													*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Add a paragraph block to the specified collection, using the provided
		///// node content.
		///// </summary>
		///// <param name="content">
		///// Reference to the content manager.
		///// </param>
		///// <param name="blocks">
		///// Reference to the collection of blocks to which the object will be
		///// added.
		///// </param>
		///// <param name="node">
		///// Reference to the node containing the information to add.
		///// </param>
		//public static void AddParagraph(OdtContentFileItem content,
		//	ElementCollection blocks, XmlNode node)
		//{
		//	ParagraphItem paragraph = null;

		//	if(blocks != null && node != null)
		//	{
		//		paragraph = new ParagraphItem();
		//		paragraph.Deserialize(content, node);
		//		blocks.Add(paragraph);
		//	}
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* DeserializeStyles																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Deserialize the style definitions.
		/// </summary>
		/// <param name="document">
		/// Reference to the document for which the styles will be processed.
		/// </param>
		private static void DeserializeStyles(OdtDocumentItem document)
		{
			ElementItem block = null;
			ElementCollection blocks = null;
			HtmlDocument html = null;
			HtmlNodeItem node = null;

			if(document?.mContentFile?.Nodes.Count > 0)
			{
				html = document.mContentFile;
				//	Font faces.
				blocks = document.mFontFaces;
				blocks.Clear();
				node = html.Nodes.FindMatch(x =>
					x.NodeType == "office:font-face-decls");
				if(node != null)
				{
					AddNodesAsBlocks(node.Nodes, blocks);
				}
				//	Automatic styles.
				blocks = document.mAutomaticStyles;
				blocks.Clear();
				node = html.Nodes.FindMatch(x =>
					x.NodeType == "office:automatic-styles");
				if(node != null)
				{
					AddNodesAsBlocks(node.Nodes, blocks);
				}
				//	Base styles.
				html = document.mStylesFile;
				blocks = document.mBaseStyles;
				blocks.Clear();
				node = html.Nodes.FindMatch(x =>
					x.NodeType == "office:styles");
				if(node != null)
				{
					AddNodesAsBlocks(node.Nodes, blocks);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* DeserializeTextContent																								*
		//*-----------------------------------------------------------------------*
		///// <summary>
		///// Deserialize the general text content of the presented node item.
		///// </summary>
		///// <param name="block">
		///// Reference to the parent block.
		///// </param>
		///// <param name="nodes">
		///// Reference to the colleciton of nodes to be parsed as child blocks
		///// to the current block.
		///// </param>
		//private static void DeserializeTextContent(ElementItem parentBlock,
		//	HtmlNodeCollection nodes)
		//{
		//	ElementItem block = null;
		//	ElementCollection blocks = null;
		//	int propertyCount = 0;

		//	if(parentBlock != null && nodes?.Count > 0)
		//	{
		//		blocks = parentBlock.Elements;
		//		foreach(HtmlNodeItem nodeItem in nodes)
		//		{
		//			if(nodeItem.NodeType == "text" || nodeItem.NodeType.Length == 0)
		//			{
		//				block = new ElementItem()
		//				{
		//					ElementType = OpenDocumentElementTypeEnum.Text
		//				};
		//				block.Properties.Add(new PropertyItem()
		//				{
		//					Name = "Text",
		//					Value = nodeItem.Text
		//				});
		//				blocks.Add(block);
		//			}
		//			else
		//			{
		//				block = new ElementItem()
		//				{
		//					ElementType = mDocumentDefinition.GetTypeFromTag(nodeItem.NodeType),
		//						NodeType = nodeItem.NodeType
		//				};
		//				if(block.ElementType == OpenDocumentElementTypeEnum.None)
		//				{
		//					block.OriginalContent = nodeItem.Html;
		//				}
		//				block.Properties.AddRange(GetMappedProperties(nodeItem));
		//				blocks.Add(block);
		//				DeserializeTextContent(block, nodeItem.Nodes);
		//			}
		//		}
		//	}
		//}
		////*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Deserialize the general text content of the document from the
		/// underlying data.
		/// </summary>
		/// <param name="document">
		/// Reference to the user document.
		/// </param>
		private static void DeserializeTextContent(OdtDocumentItem document)
		{
			ElementItem block = null;
			ElementCollection blocks = null;
			HtmlNodeItem body = null;
			HtmlDocument html = null;
			//XmlNodeList nodes = null;
			//ParagraphItem paragraph = null;

			if(document?.mContentFile?.Nodes.Count > 0)
			{
				html = document.mContentFile;
				blocks = document.mElements;
				blocks.Clear();
				body = html.Nodes.FindMatch(x => x.NodeType == "office:text");
				if(body != null)
				{
					document.mUseSoftPageBreaks =
						ToBool(body.Attributes["text:use-soft-page-breaks"]);
					document.mGlobalText =
						(ToBool(body.Attributes["text:global"]) ||
						document.mMimeType.ToLower() ==
						"application/vnd.oasis.opendocument.text-master");
					foreach(HtmlNodeItem nodeItem in body.Nodes)
					{
						if(nodeItem.NodeType == "text" || nodeItem.NodeType.Length == 0)
						{
							block = new ElementItem()
							{
								ElementType = OpenDocumentElementTypeEnum.Text
							};
							block.Properties.Add(new PropertyItem()
							{
								Name = "Text",
								Value = nodeItem.Text
							});
							blocks.Add(block);
						}
						else
						{
							block = new ElementItem()
							{
								ElementType = mDocumentDefinition.GetTypeFromTag(
									nodeItem.NodeType),
								NodeType = nodeItem.NodeType
							};
							if(block.ElementType == OpenDocumentElementTypeEnum.None)
							{
								block.OriginalContent = nodeItem.Html;
							}
							block.Properties.AddRange(GetMappedProperties(nodeItem));
							blocks.Add(block);
							//DeserializeTextContent(block, nodeItem.Nodes);
							AddNodesAsBlocks(nodeItem.Nodes, block.Elements);
						}
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////* GetContentDocument																										*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Return a reference to the content data file.
		///// </summary>
		///// <param name="document">
		///// Reference to the document for which the content item will be returned.
		///// </param>
		///// <returns>
		///// Reference to the raw ODT content document, if found. Otherwise,
		///// null.
		///// </returns>
		//private static HtmlDocument GetContentDocument(OdtDocumentItem document)
		//{
		//	string content = "";
		//	HtmlDocument result = null;

		//	if(document.mOdtProject?.Exists == true)
		//	{
		//		content = File.ReadAllText("content.xml");
		//		result = new HtmlDocument(content, true, true);
		//	}
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetMappedProperties																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a list of mapped properties for the current source node.
		/// </summary>
		/// <param name="node">
		/// Reference to the node from which to read the attributes.
		/// </param>
		/// <returns>
		/// List of properties, with mapped names resolved, found on the
		/// provided node, if found. Otherwise, an empty list. If a mapped name for
		/// a node attribute was not found, its natural name is returned in the
		/// list.
		/// </returns>
		private static List<PropertyItem> GetMappedProperties(HtmlNodeItem node)
		{
			PropertyItem property = null;
			OpenDocumentAttributeItem propertyAttribute = null;
			List<PropertyItem> result = new List<PropertyItem>();

			if(node != null)
			{
				foreach(HtmlAttributeItem attributeItem in node.Attributes)
				{
					propertyAttribute =
						mDocumentDefinition.Attributes.FirstOrDefault(x =>
							x.MappedName == attributeItem.Name);
					if(propertyAttribute != null)
					{
						property = new PropertyItem()
						{
							Name = propertyAttribute.DisplayName
						};
					}
					else
					{
						property = new PropertyItem()
						{
							Name = attributeItem.Name
						};
					}
					property.Value = attributeItem.Value;
					result.Add(property);
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////* GetPropertyNames																											*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Return a list of property names and their base XML attribute name
		///// counterparts.
		///// </summary>
		///// <returns>
		///// Reference to a collection of property names, associated with their
		///// XML attribute name counterparts.
		///// </returns>
		//private static PropertyAttributeNameCollection GetPropertyNames()
		//{
		//	PropertyAttributeNameItem item = null;
		//	string[] lines = ResourceMain.tsvAttributeNameMap.Split('\n');
		//	PropertyAttributeNameCollection result =
		//		new PropertyAttributeNameCollection();
		//	string[] values = null;

		//	foreach(string lineItem in lines)
		//	{
		//		item = null;
		//		values = lineItem.Trim().Split('\t');
		//		if(values.Length > 0)
		//		{
		//			item = new PropertyAttributeNameItem()
		//			{
		//				PropertyName = values[0].Trim()
		//			};
		//			result.Add(item);
		//		}
		//		if(item != null && values.Length > 1)
		//		{
		//			item.AttributeName = values[1].Trim();
		//		}
		//	}
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////* GetTextBlockTypeDefinitions																						*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Return a master collection of text block type definitions.
		///// </summary>
		///// <returns>
		///// Reference to the master list of text block type definitions.
		///// </returns>
		//private static TextBlockTypeCollection GetTextBlockTypeDefinitions()
		//{
		//	string[] lines = null;
		//	TextBlockTypeCollection result = new TextBlockTypeCollection();
		//	TextBlockTypeItem typeItem = null;
		//	TextBlockTypeEnum typeType = TextBlockTypeEnum.None;
		//	string[] values = null;

		//	lines = ResourceMain.tsvTextBlockTypeMap.Split('\n',
		//		StringSplitOptions.RemoveEmptyEntries);
		//	foreach(string lineItem in lines)
		//	{
		//		typeItem = null;
		//		values = lineItem.Trim().Split('\t');
		//		if(values.Length > 0)
		//		{
		//			//	Readable type name is present.
		//			if(Enum.TryParse<TextBlockTypeEnum>(values[0].Trim(),
		//				true, out typeType))
		//			{
		//				typeItem = new TextBlockTypeItem()
		//				{
		//					BlockType = typeType
		//				};
		//				result.Add(typeItem);
		//			}
		//			else
		//			{
		//				Trace.WriteLine("Unrecognized Text Block Type definition: " +
		//					$"{values[0]}");
		//			}
		//		}
		//		if(typeItem != null && values.Length > 1)
		//		{
		//			typeItem.NodeTag = values[1].Trim();
		//		}
		//	}
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* InitializeStyles																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Initialize the general document styles from the underlying data.
		/// </summary>
		/// <param name="document">
		/// Reference to the user document.
		/// </param>
		private static void InitializeStyles(OdtDocumentItem document)
		{

		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	OpenOdtProject																												*
		//*-----------------------------------------------------------------------*
		//	TODO: Remove auto-assigned temp folder when going to production.
		//	TODO: Delete temp folder when ending application.
		private DirectoryInfo mOdtProject = null;
		//private static DirectoryInfo mOdtProject =
		//	new DirectoryInfo(@"C:\Temp\Active\PageMockups_NEW");
		/// <summary>
		/// Get/Set a reference to the ODT project directory where all of
		/// the component files are currently open.
		/// </summary>
		/// <param name="document">
		/// Reference to the document to be opened.
		/// </param>
		private static void OpenOdtProject(OdtDocumentItem document)
		{
			byte[] buffer = new byte[4096];
			string directoryName = "";
			string entryName = "";
			FileStream fs = null;
			FileInfo sourceFile = null;
			ZipFile file = null;
			string targetFullName = "";
			Stream zipStream = null;

			if(document != null && document.mOdtProject == null)
			{
				//	If the project has not yet been opened, then open it now.
				//mOdtProject = new DirectoryInfo(GetTempPath());
				if(mTempIsCurrentDirectory && mOdtDebugDirectory?.Length > 0)
				{
					document.mOdtProject = new DirectoryInfo(mOdtDebugDirectory);
				}
				else
				{
					document.mOdtProject = new DirectoryInfo(GetTempPath());
					Trace.WriteLine($"Temporary folder: {document.mOdtProject.Name}");
				}
				if(document.mOdtProject.Exists)
				{
					//The folder already exists, delete it and all of its contents.
					Trace.WriteLine("Deleting pre-existing XML source...");
					document.mOdtProject.Delete(true);
				}
				Trace.WriteLine("Creating XML working folder...");
				document.mOdtProject.Create();
				sourceFile = new FileInfo(document.mSourceFilename);
				if(sourceFile.Exists)
				{
					//	Create a target copy of the file that has no locks.
					sourceFile.CopyTo(
						Path.Combine(document.mOdtProject.FullName, sourceFile.Name),
						true);
					sourceFile = new FileInfo(
						Path.Combine(document.mOdtProject.FullName, sourceFile.Name));
				}
				if(sourceFile != null && sourceFile.Exists)
				{
					//	Copied file is available.
					//	TODO: Reduce the size of image to its cropped and scaled size.
					try
					{
						fs = File.OpenRead(sourceFile.FullName);
						file = new ZipFile(fs);
						//	Unzip all of the member files.
						foreach(ZipEntry zipEntryItem in file)
						{
							if(zipEntryItem.IsFile)
							{
								//	Files only. Ignore directories.
								entryName = zipEntryItem.Name;
								zipStream = file.GetInputStream(zipEntryItem);
								targetFullName =
									Path.Combine(document.mOdtProject.FullName, entryName);
								directoryName = Path.GetDirectoryName(targetFullName);
								if(directoryName.Length > 0 &&
									!Directory.Exists(directoryName))
								{
									Directory.CreateDirectory(directoryName);
								}
								using(FileStream streamWriter = File.Create(targetFullName))
								{
									StreamUtils.Copy(zipStream, streamWriter, buffer);
								}
								//	Update the physical file's modified date.
								File.SetLastWriteTime(targetFullName, zipEntryItem.DateTime);
							}
						}
					}
					finally
					{
						if(file != null)
						{
							file.IsStreamOwner = true;  //	Close underlying streams.
							file.Close();
						}
					}
					sourceFile.Delete();
					sourceFile = null;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	AutomaticStyles																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="AutomaticStyles">AutomaticStyles</see>.
		/// </summary>
		private ElementCollection mAutomaticStyles = new ElementCollection();
		/// <summary>
		/// Get a reference to the collection of automatic styles on this document.
		/// </summary>
		public ElementCollection AutomaticStyles
		{
			get { return mAutomaticStyles; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	BaseStyles																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="BaseStyles">BaseStyles</see>.
		/// </summary>
		private ElementCollection mBaseStyles = new ElementCollection();
		/// <summary>
		/// Get a reference to the collection of base styles on this document.
		/// </summary>
		public ElementCollection BaseStyles
		{
			get { return mBaseStyles; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* CreateDocument																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create and initialize a new, blank document.
		/// </summary>
		/// <returns>
		/// Reference to a newly created and initialized ODT document.
		/// </returns>
		public static OdtDocumentItem CreateDocument()
		{
			OdtDocumentItem result = new OdtDocumentItem();
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* DumpElements																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Dump the text blocks in the document.
		/// </summary>
		/// <param name="document">
		/// Reference of the document containing the blocks to dump.
		/// </param>
		/// <param name="builder">
		/// The string builder to which the content will be sent.
		/// </param>
		public static void DumpElements(OdtDocumentItem document,
			StringBuilder builder)
		{
			if(document?.mElements.Count > 0)
			{
				builder.AppendLine("*** Elements ***");
				DumpElements(document.mElements, 0, builder);
				builder.AppendLine();
				builder.AppendLine("*** Font Faces ***");
				DumpElements(document.mFontFaces, 0, builder);
				builder.AppendLine();
				builder.AppendLine("*** Base Styles ***");
				DumpElements(document.mBaseStyles, 0, builder);
				builder.AppendLine();
				builder.AppendLine("*** Automatic Styles ***");
				DumpElements(document.mAutomaticStyles, 0, builder);
				builder.AppendLine();
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Dump the text blocks in the supplied collection.
		/// </summary>
		/// <param name="blocks">
		/// Reference to the collection of blocks to dump.
		/// </param>
		/// <param name="indent">
		/// The indent level at which to dump on this level.
		/// </param>
		/// <param name="builder">
		/// The string builder to which the content will be sent.
		/// </param>
		public static void DumpElements(ElementCollection blocks, int indent,
			StringBuilder builder)
		{
			if(blocks?.Count > 0 && indent > -1)
			{
				foreach(ElementItem blockItem in blocks)
				{
					if(indent > 0)
					{
						builder.Append(new string(' ', indent));
					}
					builder.AppendLine(blockItem.ToString());
					builder.Append(new string(' ', indent + 2));
					builder.AppendLine("Properties:");
					foreach(PropertyItem propertyItem in blockItem.Properties)
					{
						builder.Append(new string(' ', indent + 3));
						builder.AppendLine(propertyItem.ToString());
					}
					builder.AppendLine("");
					DumpElements(blockItem.Elements, indent + 1, builder);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Elements																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="Elements">Elements</see>.
		/// </summary>
		private ElementCollection mElements = new ElementCollection();
		/// <summary>
		/// Get a reference to the collection of content elements in this document.
		/// </summary>
		public ElementCollection Elements
		{
			get { return mElements; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	FontFaces																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="FontFaces">FontFaces</see>.
		/// </summary>
		private ElementCollection mFontFaces = new ElementCollection();
		/// <summary>
		/// Get a reference to the collection of font face declarations in this
		/// document.
		/// </summary>
		public ElementCollection FontFaces
		{
			get { return mFontFaces; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetBaseFilesFolder																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a reference to the folder at which the raw files for this
		/// document are stored.
		/// </summary>
		/// <param name="document">
		/// Reference to the document for which the unzipped directory folder will
		/// be retrieved.
		/// </param>
		/// <returns>
		/// Reference to the directory folder at which all of the raw files for
		/// this document are currently unzipped.
		/// </returns>
		public static DirectoryInfo GetBaseFilesFolder(OdtDocumentItem document)
		{
			DirectoryInfo result = null;

			if(document != null)
			{
				result = document.mOdtProject;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GlobalText																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="GlobalText">GlobalText</see>.
		/// </summary>
		private bool mGlobalText = false;
		/// <summary>
		/// Get/Set whether this document uses a global (Master Document) text
		/// strategy.
		/// </summary>
		public bool GlobalText
		{
			get { return mGlobalText; }
			set { mGlobalText = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	MasterStyles																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="MasterStyles">MasterStyles</see>.
		/// </summary>
		private StyleCollection mMasterStyles = new StyleCollection();
		/// <summary>
		/// Get a reference to the collection of master styles on this document.
		/// </summary>
		public StyleCollection MasterStyles
		{
			get { return mMasterStyles; }
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	Meta																																	*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Private member for <see cref="Meta">Meta</see>.
		///// </summary>
		//private MetaDataCollection mMeta = new MetaDataCollection();
		///// <summary>
		///// Get a reference to the collection of meta values on the document.
		///// </summary>
		//public MetaDataCollection Meta
		//{
		//	get { return mMeta; }
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	MimeType																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="MimeType">MimeType</see>.
		/// </summary>
		private string mMimeType = "";
		/// <summary>
		/// Get/Set the mime type of this document.
		/// </summary>
		public string MimeType
		{
			get { return mMimeType; }
			set { mMimeType = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Open																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Container for the content file.
		/// </summary>
		private HtmlDocument mContentFile = null;
		/// <summary>
		/// Container for the settings file.
		/// </summary>
		private HtmlDocument mSettingsFile = null;
		/// <summary>
		/// Container for the styles file.
		/// </summary>
		private HtmlDocument mStylesFile = null;
		/// <summary>
		/// Open the specified ODT document and return the object model.
		/// </summary>
		/// <param name="filename">
		/// The fully qualified path and filename of the document to open.
		/// </param>
		/// <returns>
		/// Reference to the newly parsed ODT document object, if successful.
		/// Otherwise, null.
		/// </returns>
		public static OdtDocumentItem Open(string filename)
		{
			string content = "";
			FileInfo file = null;
			OdtDocumentItem result = null;

			if(filename?.Length > 0)
			{
				file = new FileInfo(filename);
				if(file.Exists)
				{
					result = new OdtDocumentItem()
					{
						SourceFilename = file.FullName
					};
					OpenOdtProject(result);
					if(result.mOdtProject?.FullName.Length > 0 &&
						result.mOdtProject.Exists)
					{
						content = File.ReadAllText(
							Path.Combine(result.mOdtProject.FullName, "content.xml"));
						result.mContentFile = new HtmlDocument(content, true, true, true);
						content = File.ReadAllText(
							Path.Combine(result.mOdtProject.FullName, "settings.xml"));
						result.mSettingsFile = new HtmlDocument(content, true, true);
						content = File.ReadAllText(
							Path.Combine(result.mOdtProject.FullName, "styles.xml"));
						result.mStylesFile = new HtmlDocument(content, true, true);
						result.mMimeType = File.ReadAllText(
							Path.Combine(result.mOdtProject.FullName, "mimetype"));
						//DeserializeSettings(result);
						DeserializeStyles(result);
						DeserializeTextContent(result);
					}
				}
				else
				{
					Trace.WriteLine("Error opening document. " +
						$"File not found: {file.FullName}");
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Save																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Save the open document.
		/// </summary>
		/// <param name="filename">
		/// Optional filename under which to save the document. If not specified,
		/// the filename under which the document was opened is used. If the
		/// original document didn't have a name, the file is saved under a
		/// globally unique identity in the user's temp folder.
		/// </param>
		/// <returns>
		/// True if the save was successful. Otherwise, false.
		/// </returns>
		public bool Save(string filename = "")
		{
			bool result = false;
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Scripts																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="Scripts">Scripts</see>.
		/// </summary>
		private ScriptCollection mScripts = new ScriptCollection();
		/// <summary>
		/// Get a reference to the collection of scripts on this document.
		/// </summary>
		public ScriptCollection Scripts
		{
			get { return mScripts; }
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	Sections																															*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Private member for <see cref="Sections">Sections</see>.
		///// </summary>
		//private SectionCollection mSections = new SectionCollection();
		///// <summary>
		///// Get a reference to the collection of sections on this document.
		///// </summary>
		//public SectionCollection Sections
		//{
		//	get { return mSections; }
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Settings																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="Settings">Settings</see>.
		/// </summary>
		private SettingCollection mSettings = new SettingCollection();
		/// <summary>
		/// Get a reference to the collection of settings on this document.
		/// </summary>
		public SettingCollection Settings
		{
			get { return mSettings; }
			set { mSettings = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SourceFilename																												*
		//*-----------------------------------------------------------------------*
		private string mSourceFilename = "";
		/// <summary>
		/// Get/Set the full path and filename of the ODT source file.
		/// </summary>
		public string SourceFilename
		{
			get { return mSourceFilename; }
			set { mSourceFilename = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Styles																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for <see cref="Styles">Styles</see>.
		/// </summary>
		private StyleCollection mStyles = new StyleCollection();
		/// <summary>
		/// Get a reference to the collection of general styles on this document.
		/// </summary>
		public StyleCollection Styles
		{
			get { return mStyles; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	UseSoftPageBreaks																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Private member for
		/// <see cref="UseSoftPageBreaks">UseSoftPageBreaks</see>.
		/// </summary>
		private bool mUseSoftPageBreaks = true;
		/// <summary>
		/// Get/Set a value indicating whether to use soft page breaks.
		/// </summary>
		public bool UseSoftPageBreaks
		{
			get { return mUseSoftPageBreaks; }
			set { mUseSoftPageBreaks = value; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

}
