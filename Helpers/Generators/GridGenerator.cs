using AdminLTE_MVC.Data.FakeDatabase;
using AdminLTE_MVC.Models.Dashboard;
using Microsoft.AspNetCore.Html;

namespace AdminLTE_MVC.Helpers.Generators
{
    public class GridGenerator
    {
        public GridGenerator(List<Target> targets)
        {
            foreach (var target in targets)
            {
                var generator = new CardGenerator(target);
                CardIds.Add(generator.Id);
                _cardGenerators.Add(generator);
            }
        }

        public List<uint> CardIds { get; private set; } = new();

        private readonly List<CardGenerator> _cardGenerators = new();
        private readonly string _columnWidth = (12 / FakeData.CARDS_PER_ROW).ToString();

        public IHtmlContent Generate()
        {
            var output = GenerateRow();
            return new HtmlString(output);
        }

        private string GenerateRow()
        {

            int columnLoopLimit = _cardGenerators.Count < FakeData.CARDS_PER_ROW ?  // If card amount is smaller than cards per row setting
                    _cardGenerators.Count :                                         // set loop limit to card count
                    FakeData.CARDS_PER_ROW;                                         // otherwise set it to cards per row
            Console.WriteLine(columnLoopLimit);
            // Generate columns
            var output = "<div class=\"row\">";
            for (int cardIndex = 0; cardIndex < FakeData.CARDS_PER_ROW; cardIndex++)    // BUG: figure out a way to count how many counts per column
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("generated column index: " + cardIndex);

                output += $"<section class=\"col-lg-{_columnWidth} connectedSortable ui-sortable\"> ";
                if (cardIndex < columnLoopLimit) output += GenerateColumns(cardIndex);  // Index overflow protection for when there are less cards then FakeData.CARDS_PER_ROW
                output += $"</section>";
            }
            output += "</div>";

            return output;
        }

        int _rowConsoleWriteLine = 0;
        int _colConsoleWriteLine = 0;
        private string GenerateColumns(int cardIndex)
        {

            var output = string.Empty;

            int rowLimit = 0;   // CURRENT: Add that thing with the modulo from history

            _rowConsoleWriteLine = 0;

            // Generate Psuedo-Rows
            for (int row = 0; row < rowLimit; row++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Col: {_colConsoleWriteLine}, Row: {_rowConsoleWriteLine}");

                var currentIteration = 7;           // CURRENT: calculate current iteration: if total card count is more than (cards per row * (current iteration row +1)): generate another card


                if (cardIndex < currentIteration)
                    output += _cardGenerators[cardIndex].GenerateCard();
                cardIndex += FakeData.CARDS_PER_ROW;

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Generated card ID: " + _cardGenerators[cardIndex].Id);

                _rowConsoleWriteLine++;
            }

            _colConsoleWriteLine++;

            return output;
        }
    }
}


