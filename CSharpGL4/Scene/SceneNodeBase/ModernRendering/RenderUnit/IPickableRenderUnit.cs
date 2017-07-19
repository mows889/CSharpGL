﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpGL
{
    /// <summary>
    /// A smallest unit that can render somthing.
    /// </summary>
    public class IPickableRenderUnit
    {
        /// <summary>
        /// 
        /// </summary>
        public ShaderProgram Program { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public VertexArrayObject VertexArrayObject { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public VertexBuffer PositionBuffer { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public GLStateList StateList { get; private set; }

        /// <summary>
        /// A smallest unit that can render somthing.
        /// </summary>
        /// <param name="program"></param>
        /// <param name="vao"></param>
        /// <param name="positionBuffer"></param>
        /// <param name="states"></param>
        public IPickableRenderUnit(ShaderProgram program, VertexArrayObject vao, VertexBuffer positionBuffer, params GLState[] states)
        {
            this.Program = program;
            this.VertexArrayObject = vao;
            this.PositionBuffer = positionBuffer;
            this.StateList = new GLStateList();
            this.StateList.AddRange(states);
        }

        public void Render()
        {
            ShaderProgram program = this.Program;

            // 绑定shader
            program.Bind();
            program.PushUniforms();

            this.StateList.On();

            this.VertexArrayObject.Render(program);

            this.StateList.Off();

            // 解绑shader
            program.Unbind();
        }


        public void Dispose()
        {
            VertexArrayObject vao = this.VertexArrayObject;
            if (vao != null)
            {
                vao.Dispose();
            }
            ShaderProgram program = this.Program;
            if (program != null)
            {
                program.Dispose();
            }
        }

    }
}
