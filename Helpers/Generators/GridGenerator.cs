﻿using AdminLTE_MVC.Data.FakeDatabase;
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
            var output = "<div class=\"row\">";

            int loopLimit = _cardGenerators.Count < FakeData.CARDS_PER_ROW ?    // If card amount is smaller than cards per row setting
                    _cardGenerators.Count :                                     // set loop limit to card count
                    FakeData.CARDS_PER_ROW;                                     // otherwise set it to cards per row

            // Generate columns
            for (int cardIndex = 0; cardIndex < FakeData.CARDS_PER_ROW; cardIndex++)    // BUG: figure out a way to count how many counts per column
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("generated column index: " + cardIndex);

                output += $"<section class=\"col-lg-{_columnWidth} connectedSortable ui-sortable\"> ";
                if (loopLimit > 0) output += GenerateColumns(cardIndex);
                output += $"</section>";
            }
            output += "</div>";

            return output;
        }
        int colConsoleWriteLine = 0;
        private string GenerateColumns(int cardIndex)   // URGENT TODO: implement card count thing
        {
            // if card count is more than cards per row: generate another card(i+cards_per_row) in a specific column

            var output = string.Empty;

            int rowLimit = _cardGenerators.Count < FakeData.CARDS_PER_ROW ?    // If card amount is smaller than cards per row setting
                    _cardGenerators.Count :                                     // set loop limit to card count
                    FakeData.CARDS_PER_ROW;                                     // otherwise set it to cards per row
            Console.WriteLine(rowLimit);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Generated card ID: " + _cardGenerators[cardIndex].Id);

            int rowConsoleWriteLine = 0;

            // Generate Psuedo-Rows
            for (int row = 0; row < 2; row++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Col: {colConsoleWriteLine}, Row: {rowConsoleWriteLine}");
                output += _cardGenerators[cardIndex].GenerateCard();
                cardIndex += FakeData.CARDS_PER_ROW;
                rowConsoleWriteLine++;

            }
            colConsoleWriteLine++;
            return output;
        }
    }
}


