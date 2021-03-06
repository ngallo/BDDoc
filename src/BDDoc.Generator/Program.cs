﻿using BDDoc.Core;
using BDDoc.Core.Arguments;
using System;
using System.Threading;

namespace BDDoc
{
    class Program
    {
        //Fields

        private static int _isInitialized;

        //Methods

        internal static void Initialize()
        {
            if (Interlocked.Exchange(ref _isInitialized, 1) == 1)
            {
                return;
            }
            IoC.Map<ILogger, ConsoleLogger>();
            IoC.Map<IHtmlDocGenerator, HtmlDocGenerator>();
            IoC.Map<IArgumentsParser, ArgumentsParser>();
        }

        static void Main(string[] args)
        {
            Initialize();

            var logger = IoC.Resolve<ILogger>();

            try
            {
                IArgumentsParser argumentsParser;
                if (!ArgumentsParser.TryParse(args, out argumentsParser))
                {
                    logger.Error(argumentsParser.ErrorMessage);
                    Environment.Exit(1);
                }

                logger.Info("BDDoc HTML documentation generation started.");

                var docGenerator = IoC.Resolve<IHtmlDocGenerator>(new[] { argumentsParser });
                
                docGenerator.Generate();
                
                logger.Info("BDDoc HTML documentation generation completed.");
            }
            catch (Exception ex)
            {
                logger.Error("An error has occurred during the BDDoc HTML documentation generation.");
                var errorMessage = string.Format("ErrorMessage: {0}.", ex.Message);
                logger.Error(errorMessage);
                Environment.Exit(1);
            }

            Environment.Exit(0);
        }
    }
}
