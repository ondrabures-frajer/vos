# userrhash

Jednoduchá WinForms aplikace v C# (.NET 8) demonstrující přihlašování uživatelů s hashovanými hesly.

## Funkce

- Přihlášení uživatele (hesla hashována pomocí SHA-256)
- Uložení uživatelů do XML souboru pomocí serializace
- Změna vlastního hesla po přihlášení
- Admin účet s rozšířenou správou uživatelů (přidání, smazání, reset hesla)
- Využití dědičnosti (`User` → `Admin`) a multiformulářů

## Výchozí účet

```
Uživatel: admin
Heslo:    admin
```

## Technologie

- C# / .NET 8
- Windows Forms
- XML serializace (`XmlSerializer`)
- SHA-256 hashování (`System.Security.Cryptography`)
