﻿namespace CSharpGL
{
    /// <summary>
    /// Renders a model provided by CSharpGL.
    /// </summary>
    public partial class SimpleRenderer : Renderer
    {
        /// <summary>
        /// create an Axis' renderer.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SimpleRenderer Create(Axis model)
        {
            return Create(model, model.Lengths);
        }

        /// <summary>
        /// create an Cube' renderer.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SimpleRenderer Create(Cube model)
        {
            return Create(model, model.Lengths);
        }

        /// <summary>
        /// create an Tetrahedron' renderer.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SimpleRenderer Create(Tetrahedron model)
        {
            return Create(model, model.Lengths);
        }

        /// <summary>
        /// create an Sphere' renderer.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SimpleRenderer Create(Sphere model)
        {
            return Create(model, model.Lengths);
        }

        /// <summary>
        /// create an Teapot' renderer.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SimpleRenderer Create(Teapot model)
        {
            return Create(model, model.Lengths);
        }

        internal static SimpleRenderer Create(IBufferable model, vec3 lengths)
        {
            ShaderCode[] shaderCodes = new ShaderCode[2];
            shaderCodes[0] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"Resources\Simple.vert"), ShaderType.VertexShader);
            shaderCodes[1] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"Resources\Simple.frag"), ShaderType.FragmentShader);
            var map = new PropertyNameMap();
            map.Add("in_Position", "position");
            map.Add("in_Color", "color");
            var renderer = new SimpleRenderer(model, shaderCodes, map);
            renderer.Lengths = lengths;
            return renderer;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="model"></param>
        /// <param name="shaderCodes"></param>
        /// <param name="propertyNameMap"></param>
        /// <param name="switches"></param>
        private SimpleRenderer(IBufferable model, ShaderCode[] shaderCodes,
            PropertyNameMap propertyNameMap, params GLSwitch[] switches)
            : base(model, shaderCodes, propertyNameMap, switches)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="arg"></param>
        protected override void DoRender(RenderEventArgs arg)
        {
            if (this.modelMatrixRecord.IsMarked())
            {
                this.SetUniform("modelMatrix", this.GetModelMatrix());
                this.modelMatrixRecord.CancelMark();
            }
            mat4 projection = arg.Camera.GetProjectionMatrix();
            mat4 view = arg.Camera.GetViewMatrix();
            this.SetUniform("projectionMatrix", projection);
            this.SetUniform("viewMatrix", view);

            base.DoRender(arg);
        }
    }
}