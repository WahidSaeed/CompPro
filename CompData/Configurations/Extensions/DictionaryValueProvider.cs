using System.Collections.Generic;
using System.Globalization;

internal class DictionaryValueProvider<T>
{
    private Dictionary<string, object> backingStore;
    private CultureInfo currentCulture;

    public DictionaryValueProvider(Dictionary<string, object> backingStore, CultureInfo currentCulture)
    {
        this.backingStore = backingStore;
        this.currentCulture = currentCulture;
    }
}