# macroCalcHttp

<h1 align="center">Calorie & Macronutrient Calculator Azure Function</h1>
<div>
<h2>This Azure Function is being developed using the following:</h2>
  <ul>
    <li>C#
    <li>.Net Core 3.1
  </ul>
  </div>
<div>
<h2>Current form takes the following variables:</h2>
  <ul>
    <li>sex - This is biological sex and based on the two Mifflin-St. Jeor equations. 
    <li>age - Age in years.
    <li>heightInches - Height in inches.
    <li>weight - Weight in pounds.
  </ul>
</div>
<div>
<h2>The function will use the Mifflin-St. Jeor equations to determine Basal Metabolic Rate. Then output the following information:</h2>
 <ul>
  <li>Rest Day Macronutrient & Calorie Allotment - A starting point for calories to consume on days you do not exercise.
  <li>Light Day Macronutrient & Calorie Allotment - A starting point for calories to consume on days regular exercise. Example - a 1 hour CrossFit class.
  <li>Moderate Day Macronutrient & Calorie Allotment - A staring point for calories to consume on days where you may be training for a competion or doing high volume exercise. 
  <li>Hard Day Macronutrient & Calorie Allotment - A starting point for calories to consume if you may be an athlete devoting significant time to a sport. 
 </ul>
 <p>You can couple this function with a form to send information.
</div>
