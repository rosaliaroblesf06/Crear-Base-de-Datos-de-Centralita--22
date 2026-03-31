using System;
using Microsoft.Data.Sqlite;

class Program
{
    static string connectionString = "Data Source=centralita.db";

    static void Main(string[] args)
    {
        CrearBaseDeDatos();

        Console.WriteLine("Ingrese los datos de la llamada");

        Console.Write("Numero de origen: ");
        string origen = Console.ReadLine();

        Console.Write("Numero de destino: ");
        string destino = Console.ReadLine();

        Console.Write("Duracion (minutos): ");
        int duracion = int.Parse(Console.ReadLine());

        Console.Write("Costo: ");
        double costo = double.Parse(Console.ReadLine());

        InsertarLlamada(origen, destino, duracion, costo);

        MostrarLlamadas();
    }

    static void CrearBaseDeDatos()
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
            CREATE TABLE IF NOT EXISTS Llamadas (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                NumeroOrigen TEXT,
                NumeroDestino TEXT,
                Duracion INTEGER,
                Costo REAL
            );
            ";

            command.ExecuteNonQuery();
        }
    }

    static void InsertarLlamada(string origen, string destino, int duracion, double costo)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
            INSERT INTO Llamadas (NumeroOrigen, NumeroDestino, Duracion, Costo)
            VALUES ($origen, $destino, $duracion, $costo);
            ";

            command.Parameters.AddWithValue("$origen", origen);
            command.Parameters.AddWithValue("$destino", destino);
            command.Parameters.AddWithValue("$duracion", duracion);
            command.Parameters.AddWithValue("$costo", costo);

            command.ExecuteNonQuery();
        }

        Console.WriteLine("Llamada guardada");
    }

    static void MostrarLlamadas()
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Llamadas";

            using (var reader = command.ExecuteReader())
            {
                Console.WriteLine("\nLista de llamadas:\n");

                while (reader.Read())
                {
                    Console.WriteLine(
                        "ID: " + reader.GetInt32(0) +
                        ", Origen: " + reader.GetString(1) +
                        ", Destino: " + reader.GetString(2) +
                        ", Duracion: " + reader.GetInt32(3) +
                        ", Costo: $" + reader.GetDouble(4)
                    );
                }
            }
        }
    }
}
