using System;


namespace MacroCalcHttp {

    public static class Calculations {

        public static double BMR (string Sex, int Age, int HeightInches, int Weight) {
            if (Sex.ToUpper() == "MALE") {
                double BMR = (10 * (Weight / 2.2)) + (6.25 * (HeightInches * 2.54)) -((5 * Age) + 5); //  Mifflin-St. Jeor formula
                return BMR;        
            } else if (Sex.ToUpper() == "FEMALE"){
                double BMR = (10 * (Weight / 2.2)) + (6.25 * (HeightInches * 2.54)) -((5 * Age) + 5) - 161; //  Mifflin-St. Jeor formula
                return BMR;
            } else {
                double BMR = 0.0;
                return BMR;
            }
        }

        public static double restDayCals (double BMR) {
            double calories = BMR * 1.2;
            return calories;
        }

        public static double lightDayCals (double BMR) {
            double calories = BMR * 1.375;
            return calories;
        }

        public static double modDayCals (double BMR) {
            double calories = BMR * 1.55;
            return calories;
        }

        public static double hardDayCals (double BMR) {
            double calories = BMR * 1.725;
            return calories;
        }

        public static Tuple<int, int, int, double> Macros(int calories, int weight, string type) {
            int protein = weight * 1;
            if (type == "rest") {
                int carbs = Convert.ToInt32(weight * 0.5);
                int fats = Convert.ToInt32((calories - ((carbs + protein) * 4)) / 9);
                if (fats < 55) {
                    fats = 55;
                }
                double TDEE = ((protein * 4) + (carbs * 4) + (fats * 9));
                return new Tuple<int,int,int,double>(protein, carbs, fats, TDEE);
            } else if (type == "light") {
                int carbs = Convert.ToInt32(weight * 1);
                int fats = Convert.ToInt32((calories - ((carbs + protein) * 4)) / 9);
                if (fats < 55) {
                    fats = 55;
                }
                double TDEE = ((protein * 4) + (carbs * 4) + (fats * 9));
                return new Tuple<int,int,int,double>(protein, carbs, fats, TDEE);
            } else if (type == "moderate") {
                int carbs = Convert.ToInt32(weight * 1.5);
                int fats = Convert.ToInt32((calories - ((carbs + protein) * 4)) / 9);
                if (fats < 55) {
                    fats = 55;
                }
                double TDEE = ((protein * 4) + (carbs * 4) + (fats * 9));
                return new Tuple<int,int,int,double>(protein, carbs, fats, TDEE);
            } else if (type == "hard") {
                int carbs = Convert.ToInt32(weight * 2);
                int fats = Convert.ToInt32((calories - ((carbs + protein) * 4)) / 9);
                if (fats < 55) {
                    fats = 55;
                }
                double TDEE = ((protein * 4) + (carbs * 4) + (fats * 9));
                return new Tuple<int,int,int,double>(protein, carbs, fats, TDEE);
            } else {
                int fats = 0;
                int carbs = 0;
                double TDEE = 0.0;
                return new Tuple<int,int,int,double>(protein, carbs, fats, TDEE);
            }
            
        }  
      
    }

}