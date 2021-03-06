﻿using React.Box;
using React.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.Redux
{
    public abstract class ReduxComponent<S, A, P, C> : StatelessComponent<P, C>
        where C : ReduxComponent<S, A, P, C>
    {
        public override IElement<IElementState> Render(P props, IComponentContext context)
        {
            var reduxContext = context as IReduxComponentContext<S, A>;
            if (reduxContext == null) throw new InvalidOperationException();
            return Render(props, reduxContext);
        }

        public abstract IElement<IElementState> Render(P props, IReduxComponentContext<S, A> redux);
    }

    public abstract class ReduxComponent<S, A, C> : ReduxComponent<S, A, EmptyProps, C>
        where C : ReduxComponent<S, A, C>
    {
    }
}
