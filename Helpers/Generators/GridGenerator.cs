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
        private readonly string _columnWidth = (12 / FakeData.CARDS_PER_ROW).ToString();    // 12 is how many divisions bootstrap makes

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

            // Generate columns
            var output = "<div class=\"row\">";
            for (int cardIndex = 0; cardIndex < FakeData.CARDS_PER_ROW; cardIndex++)    // BUG: figure out a way to count how many counts per column
            {
                output += $"<section class=\"col-lg-{_columnWidth} connectedSortable ui-sortable\"> ";
                if (cardIndex < columnLoopLimit) output += GenerateColumns(cardIndex);  // Index overflow protection for when there are less cards then FakeData.CARDS_PER_ROW
                output += $"</section>";
            }
            output += "</div>";

            return output;
        }

        private string GenerateColumns(int cardIndex)
        {
            var mod = _cardGenerators.Count % FakeData.CARDS_PER_ROW != 0;
            var rowLimit = (_cardGenerators.Count / FakeData.CARDS_PER_ROW) + Convert.ToInt32(mod);

            var output = string.Empty;

            // Generate Psuedo-Rows
            for (int row = 0; row < rowLimit; row++)
            {
                var maxCardCount = _cardGenerators.Count;

                if (cardIndex < maxCardCount) output += _cardGenerators[cardIndex].GenerateCard();

                cardIndex += FakeData.CARDS_PER_ROW;
            }
            return output;
        }
    }
}


