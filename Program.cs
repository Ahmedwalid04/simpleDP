using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;

record Test
{
    public int Id { get; init; }
    public string Name { get; init; }
}

class Program
{
    static IDbConnection db = new SqliteConnection("Data Source=test.db");

    static void Main(string[] args)
    {
        DisplayAllRecords();

        Console.WriteLine("Insert Data");
        Console.Write("Name: ");
        string name = Console.ReadLine();
        Console.Write("Id: ");
        int id = int.Parse(Console.ReadLine());

        InsertData(name, id);

        Console.WriteLine("Update Data");
        Console.Write("ID: ");
        int idToUpdate = int.Parse(Console.ReadLine());
        Console.Write("New Name: ");
        string newName = Console.ReadLine();
        UpdateData(idToUpdate, newName);

        Console.WriteLine("Delete Data");
        Console.Write("ID: ");
        int idToDelete = int.Parse(Console.ReadLine());
        DeleteData(idToDelete);
    }

    static void InsertData(string name, int id)
    {
        string sql = "INSERT INTO test (name, id) VALUES (@Name, @Id)";
        db.Execute(sql, new { Name = name, Id = id });
    }

    static void UpdateData(int id, string name)
    {
        string sql = "UPDATE test SET name = @Name WHERE id = @Id";
        db.Execute(sql, new { Id = id, Name = name });
    }

    static void DeleteData(int id)
    {
        string sql = "DELETE FROM test WHERE id = @Id";
        db.Execute(sql, new { Id = id });
    }

    static void DisplayAllRecords()
    {
        string sql = "SELECT * FROM test";
        var result = db.Query<Test>(sql);
        foreach (var record in result)
        {
            Console.WriteLine($"ID: {record.Id}, Name: {record.Name}");
        }
    }
}
