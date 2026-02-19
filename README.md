# OdtSharp Library

<img src="Images/OdtSharpBanner01.jpg" width="100%" alt="14th century author working on his next masterpiece by a combination of candlelight and twilight" />

A .NET Standard 2.1 library that allows you to read, modify, and write OpenDocument ODT files, such as those used natively by LibreOffice Writer and Apache OpenOffice Writer, and supported by several others.

<p>&nbsp;</p>

## Strategy

The purpose of this project is to contribute to digital sovereignty by serving as a tiny, fast, consistent, cross-platform library, fully capable of producing standard, open-source documents equally well in automated environments and user-driven desktop editing applications alike.

This project directly implements the OpenDocument 1.2 format (ISO 26300), which can be freely downloaded from the International Standards Organization.

To realize maximum functionality in the least amount of time with a minimal application of initial effort, a tokenizing approach is taken here which reads every element from the file, assigning it a uniform token with a human-readable type enumeration and a set of human-readable properties that directly correspond to the original element's attributes. This will allow me to get the first functioning versions of the library working in a few days rather than several weeks, while still allowing for a strict system of dedicated objects to wrap those tokens at a later date, if ample evidence exists to suggest that a higher form of object-based representation warrants the extreme amount of additional effort and resources required to provide that burden of dependency.

Although the level of specific object depth I am purposely avoiding is the path most often taken with preceding libraries, such as the original RTF management libraries, XPS support for WPF, Aspose.Words, Syncfusion.DocIO and others, they are all heavy, slow, resource intensive, and don't levitate the productivity of the user very much beyond what a simple, well-named token tree can provide in just a few hours of development time, and for the tiny difference in actual end value, I am going to currently take the position that the vast difference in resources between the two approaches don't justify tens of thousands of dedicated objects in infrastructure. The practical result will be more clear as this library reaches production.

At the current token-only stage, the enumeration type name of the token, as well as the names of each property, are mapped to human-readable values as they are being deserialized, then mapped back to XML values as they are being reserialized. This allows for much more intuitive use of the document's object model during processing and modification activities.

Additionally, although the files are stored in an XML format, I will be processing them with a simple, reliable HTML library that allows me to vastly simplify reading and writing operations to the base content by discarding literally all of the complexity related to namespaces, xpath, and using any kind of crazy filters to find an attribute I already know is there. Just by perusing this project, you can see how much less work is needed when you treat XML directly as an HTML object model than as some mythical creature with which you have to have an obfuscated conversation just to retrieve a value.

<p>&nbsp;</p>

## Status

Preliminary development of this project is currently in progress. If you find a bug, please don't hesitate to create an issue. If you have an idea for its direction, you are invited to start or participate in a discussion. If you intend to level off some functionality, please create a fork and submit those changes as a pull request.
