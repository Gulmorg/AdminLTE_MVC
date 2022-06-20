using AdminLTE_MVC.Data.FakeDatabase;
using AdminLTE_MVC.Models.Dashboard;
using Microsoft.AspNetCore.Html;

namespace AdminLTE_MVC.Helpers.Generators
{
    public class GridGenerator
    {
        public GridGenerator(List<Target> targets)
        {
            _cardGenerators = new List<CardGenerator>();
            foreach (var target in targets)
            {
                var generator = new CardGenerator(target);
                CardIds.Add(generator.Id);
                _cardGenerators.Add(generator);
            }
        }

        public List<uint> CardIds { get; private set; } = new List<uint>();

        private readonly List<CardGenerator> _cardGenerators = new();

        public IHtmlContent Generate()
        {
            var output = string.Empty;
            var cardCount = _cardGenerators.Count;
            var mod = cardCount % FakeData.CARDS_PER_ROW != 0;
            byte rowCount = (byte)(cardCount / FakeData.CARDS_PER_ROW + Convert.ToByte(mod));

            for (int row = 0; row < rowCount; row++)
            {
                byte loopLimit = FakeData.CARDS_PER_ROW + (row * FakeData.CARDS_PER_ROW) > cardCount ?  // If current row total width is bigger than the card amount
                    (byte)cardCount :                                                                   // set loop limit to card count
                    (byte)((row + 1) * FakeData.CARDS_PER_ROW);                                         // otherwise set it to cards per row

                output += "<div class=\"row\">";
                for (int i = row * FakeData.CARDS_PER_ROW; i < loopLimit; i++)
                {
                    output += _cardGenerators[i].Generate();
                }
                output += "</div>";
            }

            return new HtmlString(output);
        }
    }
}


