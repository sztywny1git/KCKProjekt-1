using System.Data.SQLite;
using Dapper;

public class Produkt
{
    public int Id { get; set; }
    public string Nazwa { get; set; }
    public decimal Cena { get; set; }
    public string Opis { get; set; }
    public int Ilosc { get; set; }

    // Konstruktor
    public Produkt() { }

    public Produkt(string nazwa, decimal cena, string opis, int ilosc = 1)
    {
        Nazwa = nazwa;
        Cena = cena;
        Opis = opis;
        Ilosc = ilosc;
    }
}

public class Uzytkownik
{
    public int Id { get; set; }
    public string Nazwa { get; set; }
    public string Haslo { get; set; }
    public bool IsAdmin { get; set; }

    public Uzytkownik() { }

    public Uzytkownik(string nazwa, string haslo, bool isAdmin = false)
    {
        Nazwa = nazwa;
        Haslo = haslo;
        IsAdmin = isAdmin;
    }
}

public class UzytkownikModel
{
    private readonly string _connectionString = "Data Source=sklep.db;Version=3;";
    public Uzytkownik ZalogowanyUzytkownik { get; private set; }

    public UzytkownikModel()
    {
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Execute(@"
                    CREATE TABLE IF NOT EXISTS uzytkownicy (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Nazwa TEXT NOT NULL,
                        Haslo TEXT NOT NULL,
                        IsAdmin INTEGER DEFAULT 0
                    )");
        }
    }

    public bool ZarejestrujUzytkownika(string nazwa, string haslo)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            // Haszowanie hasła
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(haslo);

            // Dodawanie użytkownika do bazy danych
            connection.Execute("INSERT INTO uzytkownicy (Nazwa, Haslo) VALUES (@Nazwa, @Haslo)",
                new { Nazwa = nazwa, Haslo = hashedPassword });

            return true;
        }
    }


    public bool ZalogujUzytkownika(string nazwa, string haslo)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            // Pobranie użytkownika z bazy
            var user = connection.QueryFirstOrDefault<Uzytkownik>(
                "SELECT * FROM uzytkownicy WHERE Nazwa = @Nazwa",
                new { Nazwa = nazwa });

            // Weryfikacja hasła
            if (user != null && BCrypt.Net.BCrypt.Verify(haslo, user.Haslo))
            {
                ZalogowanyUzytkownik = user;
                return true;
            }

            return false;
        }
    }


    public void Wyloguj()
    {
        ZalogowanyUzytkownik = null;
    }
}

public class ProduktModel
{
    private readonly string _connectionString = "Data Source=sklep.db;Version=3;";

    public ProduktModel()
    {
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Execute(@"
                    CREATE TABLE IF NOT EXISTS produkty (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Nazwa TEXT NOT NULL,
                        Cena REAL NOT NULL,
                        Opis TEXT,
                        Ilosc INTEGER DEFAULT 1
                    )");
        }
    }

    public List<Produkt> PobierzWszystkieProdukty()
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            return connection.Query<Produkt>("SELECT * FROM produkty").ToList();
        }
    }

    public void DodajProdukt(Produkt produkt)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Execute("INSERT INTO produkty (Nazwa, Cena, Opis, Ilosc) VALUES (@Nazwa, @Cena, @Opis, @Ilosc)",
                new { produkt.Nazwa, produkt.Cena, produkt.Opis, produkt.Ilosc });
        }
    }

    public void UsunProdukt(int id)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Execute("DELETE FROM produkty WHERE Id = @Id", new { Id = id });
        }
    }

    public void EdytujProdukt(Produkt produkt)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Execute(
                "UPDATE produkty SET Nazwa = @Nazwa, Cena = @Cena, Opis = @Opis, Ilosc = @Ilosc WHERE Id = @Id",
                new { produkt.Nazwa, produkt.Cena, produkt.Opis, produkt.Ilosc, produkt.Id });
        }
    }
}

public class Koszyk
{
    public List<Produkt> Produkty { get; private set; } = new List<Produkt>();

    public void DodajProdukt(Produkt produkt, int ilosc)
    {
        var istniejącyProdukt = Produkty.FirstOrDefault(p => p.Id == produkt.Id);
        if (istniejącyProdukt != null)
        {
            istniejącyProdukt.Ilosc += ilosc;
        }
        else
        {
            produkt.Ilosc = ilosc;
            Produkty.Add(produkt);
        }
    }

    public void UsunProdukt(Produkt produkt)
    {
        Produkty.Remove(produkt);
    }

    public void WyczyscKoszyk()
    {
        Produkty.Clear();
    }
}