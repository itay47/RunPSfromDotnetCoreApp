﻿using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Threading.Tasks;
using Microsoft.PowerShell;

namespace RunPSfromDotnetCoreApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var filename = "D:\\createfiles.ps1";

            Console.WriteLine($"calling {filename} - no params");
            var res = await RunScript(filename);
            WriteOutput(res);

            Console.WriteLine($"calling {filename} - with params");
            var res2 = await RunScript(
                filename, 
                new CommandParameterCollection() 
                    { 
                        new CommandParameter("MyName","Itay")
                    }
                );
            WriteOutput(res2);

            Exit();
        }

        private static void WriteOutput(ICollection<PSObject> Results)
        {
            foreach (var item in Results)
            {
                Console.WriteLine($"returned: {item}");
            }
        }

        private static void Exit()
        {
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        /// <summary>
        /// Runs a PowerShell script taking it's path and parameters.
        /// </summary>
        /// <param name="scriptFullPath">The full file path for the .ps1 file.</param>
        /// <param name="parameters">The parameters for the script, can be null.</param>
        /// <returns>The output from the PowerShell execution.</returns>
        public async static Task<ICollection<PSObject>> RunScript(string scriptFullPath, ICollection<CommandParameter> parameters = null)
        {
            // Requires to run external scripts otherwise we'll get ExecutionPolicy exception!
            InitialSessionState initialSessionState = InitialSessionState.CreateDefault();
            initialSessionState.ExecutionPolicy = ExecutionPolicy.Unrestricted;

            using (var runspace = RunspaceFactory.CreateRunspace(initialSessionState))
            {
                runspace.Open();
                using (var pipeline = runspace.CreatePipeline())
                {
                    var cmd = new Command(scriptFullPath);
                    if (parameters != null)
                    {
                        foreach (var p in parameters)
                        {
                            cmd.Parameters.Add(p);
                        }
                    }
                    pipeline.Commands.Add(cmd);


                    var results = pipeline.Invoke();
                    return await Task.FromResult(results);
                }
            }
        }
    }
}
