using System.Net;
using System.Text.Json;

namespace IT_ELECTIVE_2_PRELIM_EXAM_HttpClient.Exercises;

public static class DeserializeMeals
{
    public static async Task Run(System.Net.Http.HttpClient client)
    {
        string url = "https://themealdb.com/api/json/v1/1/search.php?f=a";

        var response = await client.GetAsync(url);

        if (response.StatusCode != HttpStatusCode.OK)
            throw new Exception("Status code should be 200 OK.");

        string body = await response.Content.ReadAsStringAsync();

        using JsonDocument document = JsonDocument.Parse(body);

        JsonElement meals = document.RootElement.GetProperty("meals");

        if (meals.ValueKind == JsonValueKind.Null)
            throw new Exception("Meals should not be null.");

        if (meals.GetArrayLength() <= 0)
            throw new Exception("Meals array should contain at least one item.");

        foreach (JsonElement meal in meals.EnumerateArray())
        {
            string? mealName = meal.GetProperty("strMeal").GetString();
            Console.WriteLine(mealName);
        }
    }
}