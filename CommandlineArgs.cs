using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace ShutdownDialog
{
    public class CommandlineArgs 
    {   
        private Dictionary<string, string> args = new();
        public Dictionary<string, string> ReadArgs { 
            get { return args; } 
        }
        private Dictionary<string, List<string>> allowedArgs = new Dictionary<string, List<string>>(3);

        public CommandlineArgs() 
        {   
            allowedArgs.Add("--time", new List<string>() {"1", "180"});
            allowedArgs.Add("-t", new List<string>() {"1", "180"});
            allowedArgs.Add("--default", new List<string>() {"shutdown", "restart"});
            readAndParseCLArguments();
        }

        private void readAndParseCLArguments() {
            string first = "";
            
            foreach (string arg in Environment.GetCommandLineArgs()) 
            {
                if(arg.StartsWith('-') || arg.StartsWith("--")) 
                {
                    first = arg;
                }
                else 
                {
                    if(allowedArgs.ContainsKey(first))
                    {   
                        string error = "";
                        if(first == "--time" || first == "-t") 
                        {
                            int number;
                            List<int> range = allowedArgs[first].Select(x => int.Parse(x)).ToList();
                            if(int.TryParse(arg, out number) && number >= range[0] && number <= range[1])
                            {
                                ReadArgs.Add("time", number.ToString());
                                continue;
                            }
                            error = $"Valid range is {range[0]}..{range[1]}";
                        }
                        else 
                        {
                            if(allowedArgs[first].Contains(arg))
                            {
                                ReadArgs.Add("default", arg);
                                continue;
                            }
                            error = $"Allowed values are {String.Join(',', allowedArgs[first])}";
                        }
                        System.Console.WriteLine($"Invalid value for argument {first}. {error}");
                    }
                    else
                    {
                        System.Console.WriteLine($"Invalid argument {arg}");
                    }
                }
            }
        }
    }
}
