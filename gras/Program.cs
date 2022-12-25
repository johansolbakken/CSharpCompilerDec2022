using gras.CodeAnalysis;

void prettyPrint(SyntaxNode node, string indent = "", bool isLast = true)
{
    var marker = isLast ? "+-- " : "|-- ";

    Console.Write(indent);
    Console.Write(marker);
    Console.Write(node.Kind);

    if (node is SyntaxToken t && t.Value != null)
    {
        Console.Write(" ");
        Console.Write(t.Value);
    }

    Console.WriteLine("");

    indent += isLast ? "    " : "|   ";

    var lastChild = node.GetChildren().LastOrDefault();

    foreach (var child in node.GetChildren())
    {
        prettyPrint(child, indent, child == lastChild);
    }
}

bool showTree = false;

while (true)
{
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write("> ");
    var line = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(line))
        break;

    if (line == "#showtree")
    {
        showTree = !showTree;
        if (showTree)
            Console.WriteLine("Showing parse trees");
        else
            Console.WriteLine("Hiding parse trees");
        continue;
    }

    if (line == "#cls")
    {
        Console.Clear();
        continue;
    }

    var syntaxTree = SyntaxTree.parse(line);


    if (showTree)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        prettyPrint(syntaxTree.Root);
    }

    if (syntaxTree.Diagnostics.Any())
    {
        Console.ForegroundColor = ConsoleColor.Red;
        foreach (var message in syntaxTree.Diagnostics)
        {
            Console.WriteLine(message);
        }
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.White;
        var e = new Evaluator(syntaxTree.Root);
        var result = e.Evaluate();
        Console.WriteLine(result);
    }
}