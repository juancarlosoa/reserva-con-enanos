using System.Net.Http.Json;

namespace ReservaConEnanos.Frontend.Http;

public class BaseHttpService
{
    private readonly HttpClient _httpClient;

    public BaseHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T?> GetAsync<T>(string uri)
    {
        try
        {
            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en GET {uri}: {ex.Message}");
            return default;
        }
    }

    public async Task<T?> PostAsync<T>(string uri, object body)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(uri, body);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en POST {uri}: {ex.Message}");
            return default;
        }
    }

    public async Task<T?> PutAsync<T>(string uri, object body)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync(uri, body);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en PUT {uri}: {ex.Message}");
            return default;
        }
    }

    public async Task<bool> DeleteAsync(string uri)
    {
        try
        {
            var response = await _httpClient.DeleteAsync(uri);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en DELETE {uri}: {ex.Message}");
            return false;
        }
    }
}