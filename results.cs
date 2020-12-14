using System;

namespace MacroCalcHttp{
    public class SetMacros {
        public int Calories { get; set; }
        public int Protein { get; set; }
        public int Carbs { get; set; }
        public int Fats { get; set; }
    }

    public class Results {
        public DateTime CreatedDate = DateTime.Now;
        public int BasalMetabolicRate { get; set; }
        public SetMacros restDayMacros;
        public SetMacros lightDayMacros;
        public SetMacros moderateDayMacros;
        public SetMacros hardDayMacros;
    }
}