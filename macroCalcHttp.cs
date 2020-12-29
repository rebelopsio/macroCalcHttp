using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MacroCalcHttp
{
    public static class macroCalcHttp
    {
        [FunctionName("macroCalcHttp")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string sex = req.Query["sex"]; // stores the biologic sex of the requester. [male/female]
            int age = Int32.Parse(req.Query["age"]); // stores the age of the requester in years.
            int heightInches = Int32.Parse(req.Query["heightInches"]); // stores the height of the requester in inches.
            int weight = Int32.Parse(req.Query["weight"]); // stores the weight of the requester in lbs

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            sex = sex ?? data?.sex;
            
            int bMR = Convert.ToInt32(Calculations.BMR(sex, age, heightInches, weight));
            int rDayCals = Convert.ToInt32(Calculations.Cals(bMR, "rest"));
            int lDayCals = Convert.ToInt32(Calculations.Cals(bMR, "light"));
            int mDayCals = Convert.ToInt32(Calculations.Cals(bMR, "moderate"));
            int hDayCals = Convert.ToInt32(Calculations.Cals(bMR,"hard"));
            
            var objRDayMacros = Calculations.Macros(rDayCals, weight, "rest");
            var objLDayMacros = Calculations.Macros(lDayCals, weight, "light");
            var objMDayMacros = Calculations.Macros(mDayCals, weight, "moderate");
            var objHDayMacros = Calculations.Macros(hDayCals, weight, "hard");
            
            // Instantiate object
            Results macros = new Results();
            macros.restDayMacros.Protein = objRDayMacros.Item1;
            macros.restDayMacros.Carbs = objRDayMacros.Item2;
            macros.restDayMacros.Fats = objRDayMacros.Item3;
            macros.restDayMacros.Calories = Convert.ToInt32(objRDayMacros.Item4);
            macros.lightDayMacros.Protein = objLDayMacros.Item1;
            macros.lightDayMacros.Carbs = objLDayMacros.Item2;
            macros.lightDayMacros.Fats = objLDayMacros.Item3;
            macros.lightDayMacros.Calories = Convert.ToInt32(objLDayMacros.Item4);
            macros.moderateDayMacros.Protein = objMDayMacros.Item1;
            macros.moderateDayMacros.Carbs = objMDayMacros.Item2;
            macros.moderateDayMacros.Fats = objMDayMacros.Item3;
            macros.moderateDayMacros.Calories = Convert.ToInt32(objMDayMacros.Item4);
            macros.hardDayMacros.Protein = objHDayMacros.Item1;
            macros.hardDayMacros.Carbs = objHDayMacros.Item2;
            macros.hardDayMacros.Fats = objHDayMacros.Item3;
            macros.hardDayMacros.Calories = Convert.ToInt32(objHDayMacros.Item4);
            macros.BasalMetabolicRate = bMR;
            
           var json = JsonConvert.SerializeObject(macros, Formatting.Indented);

           return new OkObjectResult(json);
        }
    }
}
