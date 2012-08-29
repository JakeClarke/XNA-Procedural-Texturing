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

        public Render GetInput(string name)
        {
            return this.inputs[name];
        }

        public Render GetInput(int i)
        {
            return this.inputs.ElementAt(i).Value;
        }

        public void SetInput(string name, Render s)
        {
            if (this.inputs.ContainsKey(name))
            {
                this.inputs[name] = s;
            }
            else
            {
                throw new Exception("Use of a unregistered input. Inputs must to registered by the node before it can be used.");
            }
        }

        public int GetInputCount()
        {
            return this.inputs.Count();
        }

        public string GetInputName(int i)
        {
            return this.inputs.ElementAt(i).Key;
        }

        /// <summary>
        /// Get all input names.
        /// </summary>
        /// <returns>An array of input names.</returns>
        public string[] GetInputNames()
        {
            string[] names = new string[this.GetInputCount()];
            for (int i = 0; i < names.Length; i++)
            {
                names[i] = this.GetInputName(i);
            }

            return names;
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

        /// <summary>
        /// Get all output names.
        /// </summary>
        /// <returns>An array of the output names.</returns>
        public string[] GetOuputNames()
        {
            string[] names = new string[this.GetOutputCount()];
            for (int i = 0; i < names.Length; i++)
            {
                names[i] = this.GetOutputName(i);
            }

            return names;
        }

        public void Dispose()
        {
            this.Graph.RemoveNode(this);
        }
    }
}
