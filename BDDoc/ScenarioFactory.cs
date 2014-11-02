﻿using System.Collections.Generic;
using BDDoc.Reflection;
using System;

namespace BDDoc
{
    internal sealed class ScenarioFactory
    {
        //Fields

        private readonly IReflectionHelper _reflectionHelper;

        //Constructors

        public ScenarioFactory(IReflectionHelper reflectionHelper)
        {
            if (reflectionHelper == null)
            {
                throw new ArgumentNullException();
            }
            _reflectionHelper = reflectionHelper;
        }

        //Methods

        public static ScenarioFactory CreateInstance()
        {
            var reflectionHelper = new ReflectionHelper();
            return new ScenarioFactory(reflectionHelper);
        }

        public PlainScenario CreateScenario(int skipFrames)
        {
            IList<IStoryAttrib> storyAttribs;
            IList<IScenarioAttrib> scenarioAttribs;
            var refHelper = new ReflectionHelper();
            refHelper.RetrieveCallingMethodAttributes(++skipFrames, out storyAttribs, out scenarioAttribs);
            return new PlainScenario(storyAttribs, scenarioAttribs);
        }
    }
}
