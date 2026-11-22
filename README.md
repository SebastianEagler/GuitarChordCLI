# GuitarChordCLI

Konsolowa aplikacja dla Windows, która wyświetla gryf sześciostrunowej gitary (24 progi, strojenie standardowe E A D G B E) i zaznacza na nim nuty akordu oraz, opcjonalnie, skali.

## Funkcje
- Trójdźwięki: Major i Minor.
- Akordy septymowe: Major 7, Minor 7 i Dominant 7.
- Podświetlanie skali (dur lub moll naturalny) odpowiadającej wybranemu akordowi.
- Trzy motywy kolorów: Dark, Light i Contrast.
- Pod gryfem: do 4 kształtów (chwytów) wybranego akordu z numerami nut akordu.

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
  - `Fretboard` – budowanie tabeli gryfu i tabelek z kształtami akordów.
  - `Rendering` – rysowanie kilku tabelek obok siebie.
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

## Jak czytać gryf
**Uwaga: główny gryf pokazuje wszystkie dostępne nuty akordu na całym gryfie.** Program zaznacza każdą pozycję, w której występuje dźwięk należący do akordu, a nie konkretny chwyt. Dlatego na jednej strunie może być zaznaczonych kilka progów, a w chwycie, który faktycznie gramy, wybieramy z nich tylko niektóre.

Przykład: dla G Minor 7 (G, A#, D, F) zaznaczone jest G na progu 3 pierwszej struny od góry (E). Dla G Major 7 (G, B, D, F#) na tej samej strunie zaznaczone byłoby F# na progu 2, a nie F.

Gryf jest rysowany jak w tabulaturze: od góry struna e (wysoka), B, G, D, A, a na dole E (niska).

## Kształty akordów (Chord shapes)
Pod gryfem program rysuje do 4 kształtów akordu w różnych miejscach gryfu, obok siebie. Każdy kształt to osobna tabelka z oknem 4 progów (numery progów są w nagłówku):

- liczba w komórce to numer nuty w akordzie: **1** = prima (dźwięk podstawowy), **2** = tercja, **3** = kwinta, **4** = septyma (tylko w akordach 7),
- **x** w pierwszej kolumnie oznacza strunę, której nie gramy,
- litera struny po lewej (e, B, G, D, A, E) jest **pokolorowana**, jeśli pusta struna (próg zerowy) jest nutą akordu, np. litera D w akordzie D-dur. Kolor dotyczy też strun, które w danym kształcie są grane na progu albo wyciszone,
- liczba w pierwszej kolumnie oznacza strunę pustą, która jest w tym kształcie faktycznie grana (np. pusta struna D w akordzie D-dur to **1**),
- **.** oznacza, że na tym progu nic nie jest grane.

Kształty są dobierane algorytmem: najniższy grany dźwięk to prima, grane są co najmniej 4 sąsiednie struny, występują wszystkie nuty akordu, całość mieści się w 4 progach i wymaga najwyżej 4 palców (struny na tym samym progu liczone jako bareta). To sugestie chwytów, a nie gotowa lista wszystkich możliwych układów.

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
