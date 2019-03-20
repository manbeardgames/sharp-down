using System;
using System.Xml;
using System.Xml.Linq;

namespace SharpDown
{
    /// <summary>
    ///     Imports the XML Document to process
    /// </summary>
    public static class Importer
    {
        /// <summary>
        ///     Tells the importer to begin
        /// </summary>
        public static XDocument Run(string filePath)
        {
            //  Load the document
            XDocument doc = XDocument.Load(filePath);

            //  Ensure that the first node in the document is <doc>
            //  if it's not, then throw error
            if (doc.Root.Name != "doc")
            {
                throw new Exception("The xml document is missing the <doc> root node.  Please ensure the xml document is the one auto generated through visual studio");
            }

            return doc;
        }
    }
}