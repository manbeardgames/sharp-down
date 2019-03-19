using System.Collections.Generic;

namespace SharpDown.Interfaces
{
    public interface INode
    {
        List<INode> Children {get; set;}
         string Content {get; set;}
    }
}