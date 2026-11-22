# GuitarChordCLI

Konsolowa aplikacja dla Windows, która wyświetla gryf sześciostrunowej gitary (24 progi, strojenie standardowe E A D G B E) i zaznacza na nim nuty akordu oraz, opcjonalnie, skali.

## Funkcje
- Trójdźwięki: Major i Minor.
- Akordy septymowe: Major 7, Minor 7 i Dominant 7.
- Podświetlanie skali (dur lub moll naturalny) odpowiadającej wybranemu akordowi.
- Trzy motywy kolorów: Dark, Light i Contrast.

## Wymagania
- Windows (aplikacja zmienia czcionkę i rozmiar okna konsoli przez `kernel32`).
- .NET 9 SDK.
- Repozytorium biblioteki [CLITextTable](https://github.com/SebastianEagler/CLITextTable) sklonowane **obok** tego repozytorium (`..\CLITextTable`), bo projekt odwołuje się do niego przez `ProjectReference`:
  ```
  repos\
    GuitarChordCLI\
    CLITextTable\
  ```

## Skład projektu
- **GuitarChordCLI** – aplikacja konsolowa.
  - `MusicalRepository` – nuty, akordy (`ChordBuilder`), skale i dane gryfu.
  - `Fretboard` – budowanie tabeli gryfu.
  - `Menu` – menu i obsługa klawiszy.
  - `Themes` – motywy kolorów.
- **CLITextTable** – biblioteka do rysowania kolorowych tabel w konsoli, w osobnym repozytorium (`..\CLITextTable`).
- **GuitarChordCLI.Tests** – testy jednostkowe (xUnit) akordów, skal i gryfu.

## Uruchomienie
Oba repozytoria muszą leżeć w tym samym katalogu:
```
git clone https://github.com/SebastianEagler/CLITextTable.git
git clone https://github.com/SebastianEagler/GuitarChordCLI.git
cd GuitarChordCLI
```
Następnie:
```
dotnet build
dotnet run --project GuitarChordCLI.csproj
```
Można też otworzyć `GuitarChordCLI.sln` w Visual Studio 2022 i uruchomić projekt `GuitarChordCLI`.

## Testy
```
dotnet test GuitarChordCLI.Tests
```

## Sterowanie
| Klawisz | Działanie |
|---|---|
| ← / → | Wybór dźwięku podstawowego (Root) |
| M | Rodzaj akordu: Major/Minor, a w trybie 7: Major 7 → Minor 7 → Dominant 7 |
| T | Przełączanie między trójdźwiękami a akordami septymowymi |
| S | Pokazywanie lub ukrywanie skali na gryfie |
| C | Zmiana motywu kolorów: Dark → Light → Contrast |
| Esc | Wyjście |

## Kolory
Legenda jest wyświetlana pod menu.

| Motyw | Wygląd |
|---|---|
| Dark | czarne tło, akord czerwony, skala zielona |
| Light | białe tło, akord ciemnoczerwony, skala ciemnozielona |
| Contrast | czarne tło, akord czarny na żółtym, skala cyjan |

Przy włączonej skali nuty akordu mają pierwszeństwo przed nutami skali.

## Uwagi
- Nuty są zapisywane z krzyżykami (np. `A#` zamiast `Bb`), tak jak na gryfie.
- Kolorowane są wszystkie pozycje o danej nazwie nuty (bez rozróżniania oktaw).

## ToDo
- Akordy 9 i inne rozszerzone.
- Udostępnienie w menu akordów zmniejszonych, zwiększonych i sus oraz inwersji (są już w `ChordBuilder`).
