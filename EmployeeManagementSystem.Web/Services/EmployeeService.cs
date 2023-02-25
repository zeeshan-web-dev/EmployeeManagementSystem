using EmployeeManagementSystem.DTO.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace EmployeeManagementSystem.Web.Services
{
    public class EmployeeService
    {
        private readonly IConfiguration _configuration;
        private readonly string apiUrl;
        private readonly RestClient _restClient;
        public EmployeeService(IConfiguration configuration)
        {
            _configuration = configuration;
            apiUrl = _configuration.GetValue<string>("apiurl");
            _restClient = new RestClient(apiUrl);
        }
        public async Task<List<EmployeeDto>> GetEmployeesAsync()
        {
            
            var request = new RestRequest("/employee", Method.Get);

            request.AddHeader("Content-type", "application/json");
            request.AddHeader("Accept", "application/json");

            var response = await _restClient.ExecuteAsync(request);
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    // Deserialize the Object or throw
                    return JsonConvert.DeserializeObject<List<EmployeeDto>>(response.Content);

                default:
                    //TODO: Add custom exception
                    throw new Exception("Something wrong on server");
            }
        }
        public async Task<EmployeeDto> GetEmployeeAsync(int id)
        {
            var request = new RestRequest($"/employee/{id}", Method.Get);

            request.AddHeader("Content-type", "application/json");
            request.AddHeader("Accept", "application/json");

            var response = await _restClient.ExecuteAsync(request);
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    // Deserialize the Object or throw
                    return JsonConvert.DeserializeObject<EmployeeDto>(response.Content);

                default:
                    //TODO: Add custom exception
                    throw new Exception("Something wrong on server");
            }
        }
        public async Task AddOrUpdateEmployeeAsync(EmployeeDto employee)
        {
            string url = employee.Id > 0 ? "/employee/Update" : "/employee";
            var type = employee.Id > 0 ? Method.Put : Method.Post;
            var request = new RestRequest(url, type);

            request.AddHeader("Content-type", "application/json");
            request.AddHeader("Accept", "application/json");

            request.AddJsonBody(JsonConvert.SerializeObject(employee));

            var response = await _restClient.ExecuteAsync(request);

            if (response.StatusCode != System.Net.HttpStatusCode.Created && response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Something wrong on server");
        }
        public async Task DeleteEmployeeAsync(int id)
        {
            var request = new RestRequest($"/employee/{id}", Method.Delete);

            request.AddHeader("Content-type", "application/json");
            request.AddHeader("Accept", "application/json");

            var response = await _restClient.ExecuteAsync(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Something wrong on server");
        }
    }
}
