﻿using BDDoc.Core;
using BDDoc.Core.Arguments;
using System;

namespace BDDoc
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = IoC.Resolve<ILogger>();

            try
            {
                ArgumentsParser argumentsParser;
                if (!ArgumentsParser.TryParse(args, out argumentsParser))
                {
                    logger.Error(argumentsParser.ErrorMessage);
                    Environment.Exit(1);
                }

                logger.Info("BDDoc HTML documentation generation started.");

                var inputDir = argumentsParser[ArgumentsParser.CInputDir];
                var outputDir = argumentsParser[ArgumentsParser.COutputDir];
                var docGenerator = new HtmlDocGenerator((string)inputDir, (string)outputDir);
                
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
