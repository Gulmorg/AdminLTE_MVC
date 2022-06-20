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
            Console.WriteLine("Generated row number: 1");
            var output = "<div class=\"row\">";

            int loopLimit = _cardGenerators.Count < FakeData.CARDS_PER_ROW ?    // If card amount is smaller than cards per row setting
                    _cardGenerators.Count :                                     // set loop limit to card count
                    FakeData.CARDS_PER_ROW;                                     // otherwise set it to cards per row

            for (int col = 0; col < loopLimit; col++)
            {
                Console.WriteLine("generated column number: " + (col + 1));
                output += GenerateColumns(col);
            }
            output += "</div>";

            return output;
        }

        private string GenerateColumns(int col)
        {
            // if card count is more than cards per row: generate another card(i+cards_per_row) in a specific column

            var output =    $"<section class=\"col-lg-{_columnWidth} connectedSortable ui-sortable\"> ";

            var mod = _cardGenerators.Count % FakeData.CARDS_PER_ROW != 0;
            int rowCount = (_cardGenerators.Count / FakeData.CARDS_PER_ROW + Convert.ToInt32(mod));

            for (int row = 0; row < rowCount; row++)
            {
                Console.WriteLine($"Generated Column: {col}, Row: {row}");
                output += _cardGenerators[col].GenerateCard();
                col += FakeData.CARDS_PER_ROW;
            }

            int secondRowIndex = col + FakeData.CARDS_PER_ROW;

            //if (_cardGenerators.Count > FakeData.CARDS_PER_ROW &&
            //    _cardGenerators.Count > secondRowIndex)
            //{
            //    output += _cardGenerators[secondRowIndex].GenerateCard();
            //}

            output += $"</section>";
            return output;
        }
    }
}


