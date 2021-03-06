﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.Core
{
    public class ImageElementProps
    {
        public ImageElementProps(Image image)
        {
            this.Image = image;
        }

        public Image Image { get; }
    }

    public class ImageElement : IElement<IElementState>, IKnownSizeElement<IKnownSizeElementState>
    {
        public ImageElement(ImageElementProps props)
        {
            this.Props = props;
        }

        public ImageElementProps Props { get; }

        public IElementState Update(IElementState existing, RenderContext context, Bounds bounds)
        {
            return context.Renderer.UpdateImageElementState(existing, this, context, bounds);
        }

        public IKnownSizeElementState Update(IKnownSizeElementState existing, RenderContext context)
        {
            return context.Renderer.UpdateImageElementState(existing, this, context);
        }
    }
}
