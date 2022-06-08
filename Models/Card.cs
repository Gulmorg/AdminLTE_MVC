namespace AdminLTE_MVC.Models
{
    public class Card
    {
        // Code to generate:
        //
        //<!-- Card 1 -->
        //<div class="card card-info">
        //  <div class="card-header">
        //    <h3 class="card-title"><i class="fas fa-thermometer"></i> Kabin Sıcaklık</h3>

        //    <div class="card-tools">
        //      <button type = "button" class="btn btn-tool" data-card-widget="collapse">
        //        <i class="fas fa-minus"></i>
        //      </button>
        //    </div>
        //  </div>
        //  <div class="card-body gauge-parent">
        //    <div class="text-center">
        //        @SnmpManager.GetValue(Model[1]) °C
        //    </div>
        //    <canvas id = "gaugeOne" style="min-height: 100%; height: 100%; max-height: 100%; max-width: 100%;"></canvas>
        //  </div>

        //  <div class="card-footer text-center">
        //    More info<i class="fas fa-arrow-circle-right"></i>
        //  </div>
        //</div>

        private static byte CardCount;

        public Card(string type, string title)
        {
            Id = CardCount++;
            Position = Id;

            CardType = type;
            CardTitle = title;
        }

        ~Card()
        {
            CardCount--;
        }

        public byte Id { get; }
        public byte Position { get; set; }
        public string CardType { get; set; }
        public string CardTitle { get; set; }

        public string Generate()
        {
            var output = $"<!-- Card 1 -->\n" +
                         $"<div class=\"card card-info\">" +
                         $"  <div class=\"card-header\">" +
                         $"    <h3 class=\"card-title\"><i class=\"fas fa-thermometer\"></i> Kabin Sıcaklık</h3>" +
                         $"    <div class=\"card-tools\">" +
                         $"      <button type = \"button\" class=\"btn btn-tool\" data-card-widget=\"collapse\">" +
                         $"        <i class=\"fas fa-minus\"></i>" +
                         $"      </button>" +
                         $"    </div>" +
                         $"  </div>" +
                         $"  <div class=\"card-body gauge-parent\">" +
                         $"    <div class=\"text-center\">" +
                         $"        @SnmpManager.GetValue(Model[1]) °C" +
                         $"    </div>" +
                         $"    <canvas id = \"gaugeOne\" style=\"min-height: 100%; height: 100%; max-height: 100%; max-width: 100%;\"></canvas>" +
                         $"  </div>" +

                         $"  <div class=\"card-footer text-center\">" +
                         $"    More info<i class=\"fas fa-arrow-circle-right\"></i>" +
                         $"  </div>" +
                         $"</div>";

            return output;
        }
    }
}