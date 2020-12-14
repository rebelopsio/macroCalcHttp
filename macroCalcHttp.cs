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
            int rDayCals = Convert.ToInt32(Calculations.restDayCals(bMR));
            int lDayCals = Convert.ToInt32(Calculations.lightDayCals(bMR));
            int mDayCals = Convert.ToInt32(Calculations.modDayCals(bMR));
            int hDayCals = Convert.ToInt32(Calculations.hardDayCals(bMR));
            
            // Instantiate objects 
            var objRDayMacros = Calculations.Macros(rDayCals, weight, "rest");
            var objLDayMacros = Calculations.Macros(lDayCals, weight, "light");
            var objMDayMacros = Calculations.Macros(mDayCals, weight, "moderate");
            var objHDayMacros = Calculations.Macros(hDayCals, weight, "hard");
            SetMacros restDayMacros = new SetMacros();
            restDayMacros.Protein = objRDayMacros.Item1;
            restDayMacros.Carbs = objRDayMacros.Item2;
            restDayMacros.Fats = objRDayMacros.Item3;
            restDayMacros.Calories = Convert.ToInt32(objRDayMacros.Item4);
            SetMacros lightDayMacros = new SetMacros();
            lightDayMacros.Protein = objLDayMacros.Item1;
            lightDayMacros.Carbs = objLDayMacros.Item2;
            lightDayMacros.Fats = objLDayMacros.Item3;
            lightDayMacros.Calories = Convert.ToInt32(objLDayMacros.Item4);
            var moderateDayMacros = new SetMacros();
            moderateDayMacros.Protein = objMDayMacros.Item1;
            moderateDayMacros.Carbs = objMDayMacros.Item2;
            moderateDayMacros.Fats = objMDayMacros.Item3;
            moderateDayMacros.Calories = Convert.ToInt32(objMDayMacros.Item4);
            SetMacros hardDayMacros = new SetMacros();
            hardDayMacros.Protein = objHDayMacros.Item1;
            hardDayMacros.Carbs = objHDayMacros.Item2;
            hardDayMacros.Fats = objHDayMacros.Item3;
            hardDayMacros.Calories = Convert.ToInt32(objHDayMacros.Item4);
            Results macros = new Results();
            macros.BasalMetabolicRate = bMR;
            macros.restDayMacros = restDayMacros;
            macros.lightDayMacros = lightDayMacros;
            macros.moderateDayMacros = moderateDayMacros;
            macros.hardDayMacros = hardDayMacros;

            string json = JsonConvert.SerializeObject(macros, Formatting.Indented);

            return new OkObjectResult(json);

        }
    }
}
