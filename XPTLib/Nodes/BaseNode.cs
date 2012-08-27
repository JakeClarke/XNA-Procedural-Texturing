using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace XPTLib.Nodes
{
    public abstract class BaseNode : IDisposable
    {

        public delegate Color[] Render(int hight,int width);

        Dictionary<string, Render> inputs = new Dictionary<string, Render>(), outputs = new Dictionary<string, Render>();

        public Graph Graph { get; private set; }

        public BaseNode(Graph graph)
        {
            this.Graph = graph;
            this.Graph.AddNode(this);
        }

        protected void registerInput(string name)
        {
            inputs.Add(name, null);
        }

        protected Render getInput(string name)
        {
            return this.inputs[name];
        }

        public void SetInput(string name, Render s)
        {
            Debug.Assert(this.inputs.ContainsKey(name));
            this.inputs[name] = s;
        }

        public int GetInputCount()
        {
            return this.inputs.Count();
        }

        public string GetInputName(int i)
        {
            return this.inputs.ElementAt(i).Key;
        }

        protected void registerOutput(string name, Render s)
        {
            this.outputs.Add(name, s);
        }

        public Render GetOutput(string name)
        {
            return this.outputs[name];
        }

        public int GetOutputCount()
        {
            return this.outputs.Count;
        }

        public string GetOutputName(int i)
        {
            return this.outputs.ElementAt(i).Key;
        }

        public void Dispose()
        {
            this.Graph.RemoveNode(this);
        }
    }
}
