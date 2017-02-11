﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectReact
{
    public enum LineDirection
    {
        Horizontal,
        Vertical
    }

    public class Line : Element<LineState, Line>
    {
        public Line(LineDirection direction, params IElement[] children)
        {
            this.Children = children;
            this.Direction = direction;
        }

        public IElement[] Children { get; }
        public LineDirection Direction { get; }
        public Action<ClickEvent> OnMouseClick { get; set; }
    }

    public class LineState : IUpdatableElementState<Line>
    {
        private List<IElementState> nestedElementStates;
        private Action<ClickEvent> onMouseClick;

        public LineState(Line e, UpdateContext context)
        {
            var originalBounds = context.Bounds;
            nestedElementStates = new List<IElementState>();
            var b = originalBounds;
            foreach (var child in e.Children)
            {
                var newState = child.Update(null, new UpdateContext(b, context.Renderer, context.Context));
                nestedElementStates.Add(newState);
                b = Bounds.Remaining(e.Direction, b, newState.BoundingBox);
            }
            BoundingBox = Bounds.Sum(e.Direction, originalBounds, nestedElementStates.Select(p => p.BoundingBox));
            onMouseClick = e.OnMouseClick;
        }

        public void Update(Line other, UpdateContext context)
        {
            var originalBounds = context.Bounds;
            var b = context.Bounds;
            var newStates = new List<IElementState>();
            for (int i = 0; i < Math.Max(nestedElementStates.Count, other.Children.Length); ++i)
            {
                if (i >= other.Children.Length)
                {
                    nestedElementStates[i].Dispose();
                    continue;
                }
                IElementState existingState = i >= nestedElementStates.Count ? null : nestedElementStates[i];
                var newState = other.Children[i].Update(existingState, new UpdateContext(b, context.Renderer, context.Context));
                newStates.Add(newState);
                b = Bounds.Remaining(other.Direction, b, newState.BoundingBox);
            }
            nestedElementStates = newStates;
            BoundingBox = Bounds.Sum(other.Direction, originalBounds, nestedElementStates.Select(p => p.BoundingBox));
            onMouseClick = other.OnMouseClick;
        }

        public Bounds BoundingBox { get; private set; }

        public void Dispose()
        {
            foreach (var state in nestedElementStates)
                state.Dispose();
        }

        public void Render(IRenderer r)
        {
            foreach (var element in nestedElementStates)
            {
                element.Render(r);
            }
        }

        public void OnMouseClick(ClickEvent click)
        {
            foreach (var child in nestedElementStates)
            {
                if (Bounds.IsInBounds(child.BoundingBox, click))
                {
                    child.OnMouseClick(click);
                    break;
                }
            }
            onMouseClick?.Invoke(click);
        }
    }
}