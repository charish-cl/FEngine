using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FEngine.NodeEditor.Runtime
{
    public class NodeDataBase:ScriptableObject
    {
        public List<Node> Nodes;
        
        public List<Connection> connections;    //连接列表
        
    }
}