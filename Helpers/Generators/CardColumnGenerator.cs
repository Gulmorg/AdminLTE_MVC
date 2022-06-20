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

        public IHtmlContent GenerateColumn()
        {
            var output = string.Empty;

            // Non fixed height
            //for (int i = _row * COLUMND_WIDTH; i < _cardGenerators.Count; i++)
            //{
            //    output += _cardGenerators[i].Generate();
            //}

            // Fixed height
            var mod = _cardGenerators.Count % COLUMND_WIDTH != 0;
            byte rowCount = (byte)(_cardGenerators.Count / COLUMND_WIDTH + Convert.ToByte(mod));
            Console.WriteLine($"generator count: {_cardGenerators.Count} / col width: {COLUMND_WIDTH} + mod: {mod}");
            Console.WriteLine("= row count:" + rowCount);
            for (int row = 0; row < rowCount; row++)
            {
                byte loopLimit = COLUMND_WIDTH + (row * COLUMND_WIDTH) > _cardGenerators.Count ?   // If current row total width is bigger than the card amount
                    (byte)_cardGenerators.Count :                                                   // set loop limit to card amount
                    (byte)(COLUMND_WIDTH + (row * COLUMND_WIDTH));                                 // otherwise set it to total width 

                for (int i = row * COLUMND_WIDTH; i < loopLimit; i++)
                {
                    output += _cardGenerators[i].Generate();
                }
            }

            return new HtmlString(output);
        }
    }
}


