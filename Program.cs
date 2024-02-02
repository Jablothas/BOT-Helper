using Spectre.Console;

namespace Project_Running_Bunny;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        // Create a table
        var table = new Table();

        // Add some columns
        table.AddColumn("Foo");
        table.AddColumn(new TableColumn("Bar").Centered());

        // Add some rows
        table.AddRow("Baz", "[green]Qux[/]");
        table.AddRow(new Markup("[blue]Corgi[/]"), new Panel("Waldo"));

        // Render the table to the console
        AnsiConsole.Write(table);
    }

    
}
