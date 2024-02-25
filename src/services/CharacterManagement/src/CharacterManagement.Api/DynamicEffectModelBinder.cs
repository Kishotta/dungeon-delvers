using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;

namespace CharacterManagement.Api;

public class DynamicEffectModelBinder : IModelBinder
{
    public async Task BindModelAsync (ModelBindingContext bindingContext)
    {
        using var reader = new StreamReader(bindingContext.HttpContext.Request.Body);
        var requestBody = await reader.ReadToEndAsync();

        if (requestBody.IsNullOrEmpty ())
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return;
        }

        using var jsonDoc = JsonDocument.Parse (requestBody);
        var root = jsonDoc.RootElement;
        var typeDiscriminator = root.GetProperty ("type").GetString();

        if (typeDiscriminator.IsNullOrEmpty ())
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return;
        }

        var assembly = Assembly.GetExecutingAssembly();
        var targetType = assembly.GetTypes()
                           .FirstOrDefault(t => t.Name.Equals (typeDiscriminator, StringComparison.OrdinalIgnoreCase));

        if (targetType is null)
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return;
        }

        try
        {
            var deserizalizedObject = JsonSerializer.Deserialize (requestBody, targetType, new JsonSerializerOptions
                                                                                           {
                                                                                               PropertyNameCaseInsensitive = true
                                                                                           });
            bindingContext.Result = ModelBindingResult.Success (deserizalizedObject);
        }
        catch (JsonException)
        {
            bindingContext.Result = ModelBindingResult.Failed();
        }
    }
}
