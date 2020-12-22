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
        public SetMacros restDayMacros = new SetMacros();
        public SetMacros lightDayMacros = new SetMacros();
        public SetMacros moderateDayMacros = new SetMacros();
        public SetMacros hardDayMacros = new SetMacros();

    }
}