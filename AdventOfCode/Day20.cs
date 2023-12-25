using AoCHelper;

namespace AdventOfCode;

public class Day20 : BaseDay
{
    private readonly string[] _input;
    private readonly Dictionary<string, Module> _modules = [];
    
    public Day20()
    {
        _input = File.ReadAllText(InputFilePath).Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        foreach (var line in _input)
        {
            string[] moduleData = line.Split([" -> ", ", "], StringSplitOptions.RemoveEmptyEntries);
            var type = moduleData[0][0];
            if (type == '%')
            {
                var newModule = new Module();
                newModule.name = moduleData[0][1..];
                newModule.moduleType = "Flip-flop";
                newModule.destinationmodules.AddRange(moduleData.Skip(1));
                _modules[newModule.name] = newModule;
            }
            else if (type == '&')
            {
                var newModule = new Module();
                newModule.name = moduleData[0].Substring(1);
                newModule.moduleType = "Conjunction";
                newModule.destinationmodules.AddRange(moduleData.Skip(1));
                _modules[newModule.name] = newModule;
            }
            else
            {
                var newModule = new Module();
                newModule.name = "broadcaster";
                newModule.moduleType = "broadcaster";
                newModule.destinationmodules.AddRange(moduleData.Skip(1));
                _modules[newModule.name] = newModule;
            }
        }
    }

    public override ValueTask<string> Solve_1()
    {
        var stack = new Queue<string>();
        stack.Enqueue("broadcaster");
        while (stack.TryDequeue(out string moduleStr))
        {
            var currentModule = _modules[moduleStr];
            foreach (var destinationmoduleStr in currentModule.destinationmodules)
            {
                var destinationModule = _modules[destinationmoduleStr];
                switch (destinationModule.moduleType)
                {
                    case "Conjunction":
                        stack.Enqueue(destinationmoduleStr);
                        break;
                    case "Flip-flop":
                        stack.Enqueue(destinationmoduleStr);
                        break;
                    case "broadcaster":
                        stack.Enqueue(destinationmoduleStr);
                        break;
                    default:
                        throw new ArgumentException($"{destinationModule.moduleType} is not valid.");
                }
            }
        }
        return new ValueTask<string>();
    }

    public override ValueTask<string> Solve_2()
    {
        return new ValueTask<string>();
    }

    class Module
    {
        public string name;
        public string moduleType;
        public bool pulse = false;
        public List<string> destinationmodules = [];
    }
}
