using English_word_notebook_WinUI3.Core.Models;

namespace English_word_notebook_WinUI3.Core.Contracts.Services;

// Remove this class once your pages/features are using your data.
public interface ISampleDataService
{
    Task<IEnumerable<SampleOrder>> GetListDetailsDataAsync();
}
