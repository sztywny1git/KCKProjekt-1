using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;



[Route("api/[controller]")]
[ApiController]
public class UzytkownikController : ControllerBase
{
    private readonly UzytkownikModel _uzytkownikModel;
    private readonly ProduktModel _produktModel;
    private readonly Koszyk _koszyk;

    public UzytkownikController(UzytkownikModel uzytkownikModel, ProduktModel produktModel, Koszyk koszyk)
    {
        _uzytkownikModel = uzytkownikModel;
        _produktModel = produktModel;
        _koszyk = koszyk;
    }

    [HttpGet("produkty")]
    public IActionResult PobierzProdukty()
    {
        var produkty = _produktModel.PobierzWszystkieProdukty();
        return Ok(produkty);
    }

    [HttpPost("koszyk/dodaj")]
    public IActionResult DodajDoKoszyka([FromBody] DodajDoKoszykaRequest request)
    {
        var produkt = _produktModel.PobierzWszystkieProdukty().FirstOrDefault(p => p.Id == request.ProduktId);

        if (produkt == null)
        {
            return NotFound(new { message = "Nie znaleziono produktu o podanym ID." });
        }

        _koszyk.DodajProdukt(produkt, request.Ilosc);
        return Ok(new { message = "Produkt dodany do koszyka." });
    }

    [HttpDelete("koszyk/usun")]
    public IActionResult UsunZKoszyka([FromBody] UsunZKoszykaRequest request)
    {
        var produkt = _koszyk.Produkty.FirstOrDefault(p => p.Id == request.ProduktId);

        if (produkt == null)
        {
            return NotFound(new { message = "Produkt nie został znaleziony w koszyku." });
        }

        if (request.Ilosc >= produkt.Ilosc)
        {
            _koszyk.UsunProdukt(produkt);
        }
        else
        {
            produkt.Ilosc -= request.Ilosc;
        }

        return Ok(new { message = "Produkt zaktualizowany w koszyku." });
    }

    [HttpPost("uzytkownik/logowanie")]
    public IActionResult Zaloguj([FromBody] LogowanieRequest request)
    {
        if (_uzytkownikModel.ZalogujUzytkownika(request.Nazwa, request.Haslo))
        {
            return Ok(new { message = "Zalogowano pomyślnie.", isAdmin = _uzytkownikModel.ZalogowanyUzytkownik.IsAdmin });
        }

        return Unauthorized(new { message = "Błędna nazwa użytkownika lub hasło." });
    }

    [HttpPost("uzytkownik/rejestracja")]
    public IActionResult Zarejestruj([FromBody] RejestracjaRequest request)
    {
        if (_uzytkownikModel.ZarejestrujUzytkownika(request.Nazwa, request.Haslo))
        {
            return Ok(new { message = "Zarejestrowano pomyślnie." });
        }

        return Conflict(new { message = "Użytkownik o tej nazwie już istnieje." });
    }

    [HttpGet("koszyk")]
    public IActionResult PobierzKoszyk()
    {
        var produkty = _koszyk.Produkty;
        return Ok(produkty);
    }

    [HttpDelete("koszyk/wyczysc")]
    public IActionResult WyczyscKoszyk()
    {
        _koszyk.WyczyscKoszyk();
        return Ok(new { message = "Koszyk został wyczyszczony." });
    }

    [HttpPost("produkty/dodaj")]
    public IActionResult DodajProdukt([FromBody] DodajProduktRequest request)
    {
        var produkt = new Produkt { Nazwa = request.Nazwa, Cena = request.Cena, Opis = request.Opis };
        _produktModel.DodajProdukt(produkt);
        return Ok(new { message = "Produkt dodany pomyślnie." });
    }

    [HttpPut("produkty/edytuj")]
    public IActionResult EdytujProdukt([FromBody] EdytujProduktRequest request)
    {
        var produkt = new Produkt { Id = request.Id, Nazwa = request.Nazwa, Cena = request.Cena, Opis = request.Opis };
        _produktModel.EdytujProdukt(produkt);
        return Ok(new { message = "Produkt edytowany pomyślnie." });
    }

    [HttpDelete("produkty/usun/{id}")]
    public IActionResult UsunProdukt(int id)
    {
        _produktModel.UsunProdukt(id);
        return Ok(new { message = "Produkt usunięty pomyślnie." });
    }
}

// Request DTOs
public class DodajDoKoszykaRequest
{
    public int ProduktId { get; set; }
    public int Ilosc { get; set; }
}

public class UsunZKoszykaRequest
{
    public int ProduktId { get; set; }
    public int Ilosc { get; set; }
}

public class LogowanieRequest
{
    public string Nazwa { get; set; }
    public string Haslo { get; set; }
}

public class RejestracjaRequest
{
    public string Nazwa { get; set; }
    public string Haslo { get; set; }
}

public class DodajProduktRequest
{
    public string Nazwa { get; set; }
    public decimal Cena { get; set; }
    public string Opis { get; set; }
}

public class EdytujProduktRequest
{
    public int Id { get; set; }
    public string Nazwa { get; set; }
    public decimal Cena { get; set; }
    public string Opis { get; set; }
}