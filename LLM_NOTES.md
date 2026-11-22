# Notatka dla LLM: GuitarChordCLI

## Cel
Konsolowa aplikacja .NET 9 (Windows) rysująca gryf 6-strunowej gitary (24 progi) w terminalu. Koloruje nuty akordu (czerwony) i opcjonalnie skali (zielony). Interakcja klawiszami; README po polsku, kod i komentarze po angielsku.

## Struktura
- `GuitarChordCLI.csproj`: net9.0-windows, PublishAot, Nullable, ImplicitUsings, InvariantGlobalization. Wyłącza kompilację `GuitarChordCLI.Tests\**` (podfolder z własnym csproj). Referuje `..\CLITextTable\src\CLITextTable\CLITextTable.csproj`.
- **CLITextTable to osobne repo** (`A:\NET\source\repos\CLITextTable`, github.com/SebastianEagler/CLITextTable, własne testy i CI); musi leżeć obok tego repo. Namespace `CLITextTable.*`: generyczna tabela tekstowa:
  - `CliTable`: `AddStringRow`, `AddRow`, `SetColor`, `ColorCells(List<string> notes, CellColorOptions)`, `Print()` (konsola) oraz `Print(ICliOutput)` (dowolne wyjście, np. SVG; GuitarChordCLI używa tylko `Print()`).
  - `Configuration/` (`CliConfigureOptions`, `CellColorOptions`, `RowOptions`), `Data/CliCell`, `UI/` (`ICliOutput` + `ConsoleCliOutput`, `CliLineFormater`, `CliLineSeparators`).
- `MusicalRepository/` (namespace `GuitarChordCLI.MusicalRepository`, typy publiczne dla testów):
  - `NotesData`: enum `MusicNotes` (C..B, sharpy: `C_sharp`), lista nut `"C","C#",...`, mapowanie string<->enum.
  - `ChordBuilder`: jedna prywatna `Build(root, name, intervals, inversion)`; inwersja n = obrót listy nut w lewo o n. Metody: `GetChord` (triady dur/mol), `GetSeventhChord` (maj7 `0,4,7,11`; m7 `0,3,7,10`; dominant `0,4,7,10`), `GetExtendedChord` (dim, aug, sus4). Wszystkie zwracają `ChordNote` (Name + Notes). Nazwa ma sufiks `(inversion n)` tylko gdy n > 0.
  - `ChordEnums`: `ChordTonal`, `ChordSeventhTonal`, `ChordInversion`, `ChordSeventhInversion`, `ChordExtendedType`.
  - `FretboardData`: `Notes` (struna -> 25 nut, próg 0-24) generowane ze strojenia E B G D A E; `Count = 24`.
  - `ScaleData.cs`: `ScalesDefinition.GetScale(MusicNotes, ScaleType)` generuje skalę dur / moll naturalny z interwałów; `enum ScaleType`.
- `Fretboard/FretboardBuilder.cs` (namespace `GuitarChordCLI.Fretboard`): buduje `CliTable` z nutami strun (nagłówek 0..24, E_low na górze).
- `Menu/MainMenu.cs`: pętla `ShowMenu(MenuAction)`; delegat `MenuAction(ChordSelection)`, rekord `ChordSelection(Tonal, SeventhTonal, SeventhMode, Root, ShowScale)`. Klawisze: ←/→ root, M quality (dur/mol lub maj7/m7/7), T tryb triada/akord 7, S skala, C motyw kolorów, Esc wyjście. Pod tabelą wypisywana jest legenda kolorów.
- `Themes/ColorTheme.cs`: motywy kolorów `Dark`, `Light`, `Contrast` (tło, tekst, nagłówek menu, linie, numery progów, nuty gryfu, `Chord`/`Scale` jako `CellColorOptions`); `Apply()` ustawia kolory konsoli, `Next()` przełącza motyw (klawisz C). Motyw przechodzi przez `ChordSelection.Theme` do `FretboardBuilder(theme)` i `Program.cs`. `CliTable` używa `CliConfigureOptions.DefaultBackgroundColor` do całego wydruku (potrzebne dla jasnego tła); po wyjściu `MainMenu` robi `ResetColor` + `Clear`.
- `ConsoleSetup.cs`: rozmiar okna 155x35, czcionka Consolas 24 (P/Invoke `SetCurrentConsoleFontEx`), UTF-8.
- `Program.cs`: łączy całość; w callbacku buduje gryf, koloruje skalę (zielony), potem nuty akordu (czerwony, nadpisuje), wypisuje nazwę i gryf.
- `GuitarChordCLI.Tests/`: xUnit (akordy, inwersje, skale, gryf). Uruchomienie: `dotnet test GuitarChordCLI.Tests`.

## Przepływ
`Main` -> `ConsoleSetup` -> `MainMenu.ShowMenu` -> każda iteracja: `Console.Clear`, tabela menu, legenda, callback (`ChordBuilder` + `FretboardBuilder` + `ColorCells` + `Print`), `ReadKey`.

## Znane ograniczenia
- Notacja tylko z krzyżykami (`A#`, nie `Bb`), zgodna z gryfem; ColorCells porównuje stringi nut.
- Brak oktaw: kolorowana jest każda pozycja z daną nazwą nuty.
- Akordy dim/aug/sus i inwersje istnieją w `ChordBuilder`, ale menu ich nie udostępnia (tylko Root); brak half-diminished, akordów 9 itd.
- Zależność od Windows (P/Invoke `kernel32`); `PublishAot` wymaga uwagi przy `DllImport` i refleksji.
- `CliTable.Dispose()` jest pusty (tylko dla składni `using`).
- Skale tylko dur i moll naturalny.

## Podpowiedzi dla LLM przy zmianach
- Nowy typ akordu: dodać interwały w `ChordBuilder` (enum w `ChordEnums.cs`), rozszerzyć `ChordSelection`/`MainMenu`, wywołanie w `Program.cs`, test w `GuitarChordCLI.Tests`.
- Nowa skala: dodać wartość `ScaleType` i tablicę interwałów w `ScalesDefinition`.
- Kolorowanie: `CliTable.ColorCells(List<string>, CellColorOptions)`; kolejność wywołań decyduje o priorytecie koloru.
- Nowy plik `.cs` w podfolderze z własnym csproj trzeba wykluczyć w `GuitarChordCLI.csproj` (`Compile Remove`).
