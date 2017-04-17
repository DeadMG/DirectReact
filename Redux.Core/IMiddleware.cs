﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redux.Core
{
    public interface IMiddleware<TAction>
    {
        void Dispatch(Action<TAction> next, TAction action);
    }
}
