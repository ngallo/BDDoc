﻿using BDDoc.Core;
using System;

namespace BDDoc
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class ScenarioAttribute : BDDocAttribute, IScenarioAttrib
    {
        //Constructors

        public ScenarioAttribute(string text) : base(text, 5) { }
    }
}
