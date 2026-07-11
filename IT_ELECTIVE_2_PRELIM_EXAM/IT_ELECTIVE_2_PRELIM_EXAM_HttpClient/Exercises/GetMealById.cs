using System.Net;
using System.Text.Json;

namespace IT_ELECTIVE_2_PRELIM_EXAM_HttpClient.Exercises;

public static class GetMealById
{
    public static async Task Run(System.Net.Http.HttpClient client)
    {
        string url = "https://themealdb.com/api/json/v1/1/lookup.php?i=52771";

        var response = await client.GetAsync(url);

        if (response.StatusCode != HttpStatusCode.OK)
            throw new Exception("Status code should be 200 OK.");

        string body = await response.Content.ReadAsStringAsync();

        using JsonDocument document = JsonDocument.Parse(body);

        JsonElement meals = document.RootElement.GetProperty("meals");

        if (meals.ValueKind == JsonValueKind.Null || meals.GetArrayLength() == 0)
            throw new Exception("Meal not found.");

        string? mealName = meals[0].GetProperty("strMeal").GetString();

        // Accept both the old and new API names
        if (string.IsNullOrWhiteSpace(mealName) ||
            !mealName.Contains("Arrabiata", StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception($"Expected a meal containing 'Arrabiata' but got '{mealName}'.");
        }
    }
}