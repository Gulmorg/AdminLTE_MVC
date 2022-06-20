using AdminLTE_MVC.Models.Dashboard;
using Microsoft.AspNetCore.Html;

namespace AdminLTE_MVC.Helpers.Generators
{
    public class CardColumnGenerator
    {
        const byte COLUMND_WIDTH = 4;   // this could be made adjustable via user preferences but css classes in CardGenerator.cs will need to be coupled as well

        public CardColumnGenerator(List<Target> targets)
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
        private byte _row;

        public IHtmlContent GenerateColumn()
        {
            var output = string.Empty;

            // UNUSED: Generating every card on the same row works better for now, 
            // keep this just in case we convert to a fixed height grid
            //byte loopLimit = COLUMND_WIDTH + (_row * COLUMND_WIDTH) > _cardGenerators.Count ?     // If current row total width is bigger than the card amount
            //     (byte)_cardGenerators.Count :                                                    // set loop limit to card amount
            //     (byte)(COLUMND_WIDTH + (_row * COLUMND_WIDTH));                                  // otherwise set it to total width

            //for (int i = _row * COLUMND_WIDTH; i < loopLimit; i++)
            //{
            //    output += _cardGenerators[i].Generate();
            //}
            //_row++;

            // Comment this out if fixed height grid
            for (int i = _row * COLUMND_WIDTH; i < _cardGenerators.Count; i++)
            {
                output += _cardGenerators[i].Generate();
            }

            return new HtmlString(output);
        }
    }
}
