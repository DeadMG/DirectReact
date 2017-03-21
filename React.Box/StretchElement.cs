﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using React.Core;

namespace React.Box
{
    public class StretchElement : Element<StretchElementState, StretchElement>
    {
        public StretchElement(IElement child)
        {
            this.Child = child;
        }

        public IElement Child { get; }
    }

    public class StretchElementState : IElementState
    {
        private readonly IElementState nestedState;

        public StretchElementState(StretchElementState existing, StretchElement element, RenderContext context)
        {
            nestedState = element.Child.Update(existing?.nestedState, context);
            BoundingBox = context.Bounds;
        }

        public Bounds BoundingBox { get; set; }
                
        public void Render(IRenderer r)
        {
            nestedState.Render(r);
        }
    }
}
