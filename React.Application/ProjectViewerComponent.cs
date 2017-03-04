﻿using React.DirectRenderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using React.Core;
using React.Box;

namespace React.Application
{
    public class ProjectViewerComponent : Component<ProjectViewerComponent>
    {
        public override IElement Render()
        {
            return new Line(new LineProps(LineDirection.Vertical),
                new TextElement(new TextElementProps("File1")),
                new TextElement(new TextElementProps("File2")));
        }
    }
}
