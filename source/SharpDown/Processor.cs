using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using SharpDown.Models;

namespace SharpDown
{
    public static class Processor
    {
        private static Dictionary<string, string> s_context;
        private static AssemblyModel _assembly;

        public static void Run(XDocument doc)
        {
            _assembly = new AssemblyModel();
            Process(doc.Root);
        }

        private static void Process(XElement root)
        {
            if (root.Name != "param" && s_context["lastNode"] == "param")
            {
                // sw.WriteLine();
            }

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

        private static void HandleDocElement(XElement root)
        {
            foreach (var node in root.Nodes())
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

        private static void HandleMembersElement(XElement root)
        {
            //  First sort all of the child member nodes so that they are
            //  grouped properly
            var members = new List<XElement>(root.Elements("member"));
            members.Sort((a, b) =>
            a.Attribute(XName.Get("name")).Value.Substring(2).CompareTo(
                b.Attribute(XName.Get("name")).Value.Substring(2)));

            // Process each of the members
            foreach (var member in members)
            {
                Process(member);
            }

        }

        private static void HandleSingleMemberElement(XElement root)
        {
            //  Create a new MemberModel object
            var member = new MemberModel();

            //  Set the member assembly
            member.Assembly = _assembly.Name;

            //  Get the name identifier from the member
            member.Name = root.Attribute(XName.Get("name")).Value;

            //  The first character of the name is the type of member
            member.MemberType = member.Name[0];



            //  Perform further processing depending on the type of member
            switch (member.MemberType)
            {
                //  type: class, interface, struct, enum, delegate
                case 'T':
                    HandleTypeMember(root, member);
                    break;
                //  field
                case 'F':
                    HandleFieldMember(root, member);
                    break;
                //  Property
                case 'P':
                    HandlePropertyMember(root, member);
                    break;
                //  method (including such special methods as constructors operators, and so forth)
                case 'M':
                    HandleMethodMember(root, member);
                    break;
                //  event
                case 'E':
                    HandleEventMember(root, member);
                    break;
                //  error string
                case '!':
                    HandleErrorMember(root, member);
                    break;
                default:
                    throw new Exception($"Member of type '{memberType}' is not supported");
            }

        }

        private static void HandleTypeMember(XElement root, MemberModel member)
        {
            //  Get the name without the T: type string
            var name = member.Name.Replace("T:", "");

            //  Split the name by the periods
            var nameSpaces = name.Split('.');

            //  Rejoing the split name with periods, excluding the last element to form
            //  the namespace
            member.NameSpace = string.Join('.', nameSpaces, 0, nameSpaces.Length - 1);
        }



        private static void HandleFieldMember(XElement root, MemberModel member)
        {
            //  Ge thte name without the F:
            var name = member.Name.Replace("F:", "");

            //  Split the name by the periods
            var namespaces = name.Split('.');

            //  Rejoing the split name with periods, excluding the last element, to
            //  form the namespace
            member.NameSpace = string.Join('.', namespaces, 0, namespaces.Length - 1);
        }


        private static void HandlePropertyMember(XElement root, MemberModel member)
        {
            //  Get the name without the P:
            var name = member.Name.Replace("P:", "");

            //  Split the name by periods
            var namespaces = name.Split(',');

            //  Rejoin the split name with periods, excluding the last element, to
            //  form the namespace
            member.NameSpace = string.Join('.', namespaces, 0, namespaces.Length - 1);
        }


        private static void HandleMethodMember(XElement root, MemberModel member) 
        { 
            //  Get the name without the M:
            var name = member.Name.Replace("M:", "");

            // TODO handle #ctor and () methods
        }
        private static void HandleEventMember(XElement root, MemberModel member) { }
        private static void HandleErrorMember(XElement root, MemberModel member) { }

    }
}