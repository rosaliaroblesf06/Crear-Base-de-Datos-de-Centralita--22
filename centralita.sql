CREATE TABLE IF NOT EXISTS Llamadas (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    NumeroOrigen TEXT NOT NULL,
    NumeroDestino TEXT NOT NULL,
    Duracion INTEGER NOT NULL,
    Costo REAL NOT NULL
);

INSERT INTO Llamadas (NumeroOrigen, NumeroDestino, Duracion, Costo)
VALUES ('8091234567', '8297654321', 10, 5.50);
