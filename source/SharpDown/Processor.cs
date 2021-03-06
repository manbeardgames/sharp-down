using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using SharpDown.Models;
using SharpDown.Models.Tags;

namespace SharpDown
{
    /// <summary type="class">
    ///     Processes the XDocument imported by the <see cref="Importer"/>
    /// </summary>
    /// <example>
    ///     <code>
    ///         // Use the importer to import an XML Document
    ///         var document = Importer.Run("path/to/xml/document");
    ///
    ///         //  Pass the document to the Processor.Run method to get
    ///         //  the results
    ///         AssemblyModel result = Processor.Run(document);
    ///     </code>
    /// </example>
    public static class Processor
    {
        //  The assembly model that is cretaed from the processing
        private static AssemblyModel _assembly;

        /// <summary>
        ///     Tells the processor to begin
        /// </summary>
        public static AssemblyModel Run(XDocument doc)
        {
            _assembly = new AssemblyModel();
            Process(doc.Root);
            return _assembly;
        }

        /// <summary>
        ///     Processes the given <see cref="XElement"/>
        /// </summary>
        /// <param name="root">The <see cref="XElement"/> to process </param>
        private static void Process(XElement root)
        {
            // if (root.Name != "param" && s_context["lastNode"] == "param")
            // {
            //     // sw.WriteLine();
            // }

            switch (root.Name.ToString())
            {
                case "doc":
                    HandleDocElement(root);
                    break;
                case "members":
                    HandleMembersElement(root);
                    break;
                case "member":
                    HandleSingleMemberElement(root);
                    break;
            }
        }


        /// <summary>
        ///     Handles the processing of the "doc" element
        /// </summary>
        /// <param name="docElement">The "doc" <see cref="XElement"/></param>
        private static void HandleDocElement(XElement docElement)
        {
            foreach (var node in docElement.Nodes())
            {
                //  Typecast  the node to an XElement
                var element = (XElement)node;

                //  Check if the element is the "assembly" element
                if (element.Name == "assembly")
                {
                    //  Add the assembly context
                    _assembly.Name = element.Element("name").Value;
                }
                else if (element.Name == "members")
                {
                    //  Process the members
                    Process(element);
                }


            }
        }

        /// <summary>
        ///     Handles the processing of the "members" element
        /// </summary>
        /// <param name="membersElement">The "members" <see cref="XElement"/></param>
        private static void HandleMembersElement(XElement membersElement)
        {
            //  First sort all of the child member nodes so that they are
            //  grouped properly
            var members = new List<XElement>(membersElement.Elements("member"));
            members.Sort((a, b) =>
            a.Attribute(XName.Get("name")).Value.Substring(2).CompareTo(
                b.Attribute(XName.Get("name")).Value.Substring(2)));

            var t = members.Where(x => x.Attribute(XName.Get("name")).Value[0] == 'T');
            var f = members.Where(x => x.Attribute(XName.Get("name")).Value[0] == 'F');
            var p = members.Where(x => x.Attribute(XName.Get("name")).Value[0] == 'P');
            var m = members.Where(x => x.Attribute(XName.Get("name")).Value[0] == 'M');
            var e = members.Where(x => x.Attribute(XName.Get("name")).Value[0] == 'E');
            var bang = members.Where(x => x.Attribute(XName.Get("name")).Value[0] == '!');


            //  Process all T: Members
            foreach (var typeMember in t)
            {
                Process(typeMember);
            }

            // //  Process all F: Members
            // foreach(var fieldMembers in f)
            // {
            //     Process(fieldMembers);
            // }

            // //  Process all P: Members
            // foreach(var propertyMember in p)
            // {
            //     Process(propertyMember);
            // }

            // //  Process all M: Members
            // foreach(var methodMember in m)
            // {
            //     Process(methodMember);
            // }

            // //  Process all E: members
            // foreach(var eventMember in e)
            // {
            //     Process(eventMember);
            // }

            // //  Process all !: Members
            // foreach(var bangMember in bang)
            // {
            //     Process(bangMember);
            // }

        }

        /// <summary>
        ///     Handles the processing of a "member" element
        /// </summary>
        /// <param name="memberElement">The "member" <see cref="XElement"/></param>
        private static void HandleSingleMemberElement(XElement memberElement)
        {
            //  Create a new MemberModel object
            var member = new MemberModel();

            //  Set the member assembly
            member.Assembly = _assembly;

            //  Get the name identifier from the member
            member.NameID = memberElement.Attribute(XName.Get("name")).Value;

            //  The first character of the name is the type of member
            member.MemberType = member.NameID[0];



            //  Perform further processing depending on the type of member
            switch (member.MemberType)
            {
                //  type: class, interface, struct, enum, delegate
                case 'T':
                    HandleTypeMember(memberElement, member);
                    break;
                //  field
                case 'F':
                    HandleFieldMember(memberElement, member);
                    break;
                //  Property
                case 'P':
                    HandlePropertyMember(memberElement, member);
                    break;
                //  method (including such special methods as constructors operators, and so forth)
                case 'M':
                    HandleMethodMember(memberElement, member);
                    break;
                //  event
                case 'E':
                    HandleEventMember(memberElement, member);
                    break;
                //  error string
                case '!':
                    HandleErrorMember(memberElement, member);
                    break;
                default:
                    throw new Exception($"Member of type '{member.MemberType}' is not supported");
            }

        }

        /// <summary>
        ///     Handles the processing of a T: "member" element
        /// </summary>
        /// <param name="typeElement">The "member" <see cref="XElement"/> with a name value beginning with T:</param>
        /// <param name="member">The <see cref="TypeMemberModel"/> reference</param>
        private static void HandleTypeMember(XElement typeElement, MemberModel member)
        {
            //  Get the name without the T: type string
            //  Example -- 'T:SharpDown.Processor' becomes 'SharpDown.Processor'
            var name = member.NameID.Replace("T:", "");

            //  Split the name by the periods
            //   Example -- 'SharpDown.Processor' becomes ['SharpDown', 'Processor']
            var nameSpaces = name.Split('.');

            //  Rejoing the split name with periods, excluding the last element to form
            //  the namespace
            //  Example -- ['SharpDown', 'Processor'] becomes 'SharpDown'
            member.NameSpace = string.Join('.', nameSpaces, 0, nameSpaces.Length - 1);

            //  Get the name of the member.  It's the last element in the namespaces list
            //  Example -- ['SharpDown', 'Processor'] becomes 'Processor'
            member.Name = nameSpaces[nameSpaces.Length - 1];


            //  Handle the child elements
            HandleMemberChildren(typeElement, member);

            // Create a new TypeMemberModel based on this member
            TypeMemberModel model = new TypeMemberModel(member);

            //  Add this to the assemblies Type collection
            _assembly.Types.Add(model);
        }

        private static void HandleMemberChildren(XElement root, MemberModel member)
        {
            //  Create a new list of tags
            member.Tags = new List<Tag>();

            if (root.HasElements)
            {
                foreach (var element in root.Elements())
                {
                    member.Tags.Add(ProcessTag(element));
                }
            }
        }

        private static Tag ProcessTag(XElement root)
        {
            Tag tag = new Tag();
            tag.Name = root.Name.ToString();
            tag.Value = root.Value;
            if (root.HasElements)
            {
                foreach (var element in root.Elements())
                {
                    tag.Children.Add(ProcessTag(element));
                }
            }

            return tag;
        }
















        /// <summary>
        ///     Handles the processing of a F: "member" element
        /// </summary>
        /// <param name="fieldElement">The "member" <see cref="XElement"/> with a name value beginning with F:</param>
        /// <param name="member">The <see cref="MemberModel"/> reference</param>
        private static void HandleFieldMember(XElement fieldElement, MemberModel member)
        {
            //  Ge thte name without the F:
            var name = member.NameID.Replace("F:", "");

            //  Split the name by the periods
            var namespaces = name.Split('.');

            //  Rejoing the split name with periods, excluding the last element, to
            //  form the namespace
            member.NameSpace = string.Join('.', namespaces, 0, namespaces.Length - 1);

            FieldMemberModel model = new FieldMemberModel(member);

            // var match = _assembly.Types.FirstOrDefault

        }

        /// <summary>
        ///     Handles the processing of a P: "member" element
        /// </summary>
        /// <param name="propertyElement">The "member" <see cref="XElement"/> with a name value beginning with P:</param>
        /// <param name="member">The <see cref="MemberModel"/> reference</param>
        private static void HandlePropertyMember(XElement propertyElement, MemberModel member)
        {
            //  Get the name without the P:
            var name = member.NameID.Replace("P:", "");

            //  Split the name by periods
            var namespaces = name.Split(',');

            //  Rejoin the split name with periods, excluding the last element, to
            //  form the namespace
            member.NameSpace = string.Join('.', namespaces, 0, namespaces.Length - 1);
        }

        /// <summary>
        ///     Handles the processing of a M: "member" element
        /// </summary>
        /// <param name="methodElement">The "member" <see cref="XElement"/> with a name value beginning with M:</param>
        /// <param name="member">The <see cref="MemberModel"/> reference</param>
        private static void HandleMethodMember(XElement methodElement, MemberModel member)
        {
            //  Get the name without the M:
            var name = member.NameID.Replace("M:", "");

            // TODO handle #ctor and () methods

        }


        /// <summary>
        ///     Handles the processing of a E: "member" element
        /// </summary>
        /// <param name="eventElement">The "member" <see cref="XElement"/> with a name value beginning with E:</param>
        /// <param name="member">The <see cref="MemberModel"/> reference</param>        
        private static void HandleEventMember(XElement eventElement, MemberModel member) { }

        /// <summary>
        ///     Handles the processing of a !: "member" element
        /// </summary>
        /// <param name="errorElement">The "member" <see cref="XElement"/> with a name value beginning with !:</param>
        /// <param name="member">The <see cref="MemberModel"/> reference</param>         
        private static void HandleErrorMember(XElement errorElement, MemberModel member) { }

    }
}