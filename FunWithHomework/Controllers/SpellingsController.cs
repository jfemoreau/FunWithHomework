using FunWithHomework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FunWithHomework.Controllers
{
    public class SpellingsController
    {
        private JsonStorageController _jsonStorageController;
        private SpellingCollection _spellings = new SpellingCollection();
        private List<Spelling> _sortedSpellings;

        public SpellingsController(JsonStorageController jsonStorageController)
        {
            _jsonStorageController = jsonStorageController;
            
            UpdateSortedSpellings();
        }

        public void Add(Spelling spelling)
        {
            if (spelling == null)
                throw new ArgumentNullException(nameof(spelling));

            _spellings.AddOrUpdate(spelling.GetHashCode(), spelling, (key, oldValue) => spelling);
            UpdateSortedSpellings();
        }

        public void Remove(Spelling spelling)
        {
            if (spelling == null)
                throw new ArgumentNullException(nameof(spelling));

            _ = _spellings.TryRemove(spelling.GetHashCode(), out _);
            UpdateSortedSpellings();
        }

        public void RemoveAll()
        {
            _spellings.Clear();
            UpdateSortedSpellings();
        }

        public async Task LoadData()
        {
            var spellingList = await _jsonStorageController.Retrieve(_sortedSpellings);
           LoadDataFromList(spellingList);
        }

        public void LoadDataFromJasonList(string jsonList)
        {
            var list = JsonSerializer.Deserialize<List<Spelling>>(jsonList);
            LoadDataFromList(list);
        }

        private void LoadDataFromList(List<Spelling> spellings)
        {
            if (_spellings == null)
                _spellings = new SpellingCollection();

            if (spellings != null)
            {
                foreach (var spelling in spellings)
                {
                    _spellings.AddOrUpdate(spelling.GetHashCode(), spelling, (key, oldValue) => spelling);
                }
            }

            UpdateSortedSpellings();
        }

        public async Task StoreData()
        {
            await _jsonStorageController.Store(_sortedSpellings);
        }

        public List<Spelling> GetSortedList()
        {
            return _sortedSpellings;
        }

        public string GetDownloadableFile()
        {
            return JsonSerializer.Serialize(GetSortedList());
        }

        private void UpdateSortedSpellings()
        {
            _sortedSpellings = _spellings.Values.ToList();
            _sortedSpellings.Sort((spelling1, spelling2) => DateTime.Compare(spelling2.DateCreated, spelling1.DateCreated));
        }
    }
}
