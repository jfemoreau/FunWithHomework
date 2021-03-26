using Blazored.LocalStorage;
using System;
using System.Threading.Tasks;

namespace FunWithHomework.Controllers
{
    public class JsonStorageController
    {
        private ILocalStorageService _localStorage;
     
        public JsonStorageController(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task Store<T>(T instance)
        {
            await _localStorage.SetItemAsync<T>(instance.ToString(), instance);

        }

        public async Task<T> Retrieve<T>(T instance)
        {
            var key = instance.ToString();

            if (!await _localStorage.ContainKeyAsync(key))
                return await Task.FromResult<T>((T)(object)null);
            try
            {
                var toReturn = await _localStorage.GetItemAsync<T>(key);
                return toReturn;
            }
            catch (Exception exception)
            {
                Console.Out.WriteLine(exception.Message);
                return await Task.FromResult<T>((T)(object)null);
            }

            
        }
    }
}
