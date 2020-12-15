using System;


namespace MacroCalcHttp
{

    public static class Calculations
    {

        public static double BMR(string Sex, int Age, int HeightInches, int Weight)
        {
            double BMR = 0.0;
            switch (Sex.ToUpper())
            {
                case "MALE":
                    BMR = (10 * (Weight / 2.2)) + (6.25 * (HeightInches * 2.54)) -
                          ((5 * Age) + 5); //  Mifflin-St. Jeor formula
                    break;
                case "FEMALE":
                    BMR = (10 * (Weight / 2.2)) + (6.25 * (HeightInches * 2.54)) - ((5 * Age) + 5) -
                          161; //  Mifflin-St. Jeor formula
                    break;
            }
            return BMR;
        }
        
        public static double Cals(double BMR, string type)
        {
            var calories = BMR;
            switch (type)
            {
                case "rest":
                    calories = BMR * 1.2;
                    break;
                case "light":
                    calories = BMR * 1.375;
                    break;
                case "moderate":
                    calories = BMR * 1.55;
                    break;
                case "hard":
                    calories = BMR * 1.725;
                    break;
            }
            return calories;
        }

        public static Tuple<int, int, int, double> Macros(int calories, int weight, string type)
        {
            int protein = weight * 1;
            int carbs = 0;
            if (type == "rest")
            {
                carbs = Convert.ToInt32(weight * 0.5);
            }

            if (type == "light")
            {
                carbs = Convert.ToInt32(weight * 1);
            }

            if (type == "moderate")
            {
                carbs = Convert.ToInt32(weight * 1.5);
            }

            if (type == "hard")
            {
                carbs = Convert.ToInt32(weight * 2);
            }

            int fats = Convert.ToInt32((calories - ((carbs + protein) * 4)) / 9);
            if (fats < 55)
            {
                fats = 55;
            }

            double TDEE = ((protein * 4) + (carbs * 4) + (fats * 9));
            return new Tuple<int, int, int, double>(protein, carbs, fats, TDEE);
        }
    }
}