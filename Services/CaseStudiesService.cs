using Creida.Data;

namespace Creida.Services;

/// <summary>
/// Vaka çalışmalarının JSON tabanlı CRUD'ı. Tam record alanları korunarak
/// JSON'a serileştiriliyor. İlk açılışta Data/CaseStudies.cs içindeki
/// statik veriden seed edilir.
/// </summary>
public class CaseStudiesService
{
    private const string Key = "cases";
    private readonly ContentStore _store;

    public CaseStudiesService(ContentStore store)
    {
        _store = store;
        if (!_store.Exists(Key))
            _store.Write(Key, CaseStudies.All.ToList());
    }

    public List<CaseStudy> All() => _store.ReadOr(Key, () => CaseStudies.All.ToList());

    public CaseStudy? BySlug(string slug) =>
        All().FirstOrDefault(c => string.Equals(c.Slug, slug, StringComparison.OrdinalIgnoreCase));

    public void Save(List<CaseStudy> list) => _store.Write(Key, list);

    public CaseStudy Upsert(CaseStudy c)
    {
        var list = All();
        var idx = list.FindIndex(x => x.Slug == c.Slug);
        if (idx >= 0) list[idx] = c;
        else list.Add(c);
        Save(list);
        return c;
    }

    public bool Delete(string slug)
    {
        var list = All();
        var idx = list.FindIndex(x => x.Slug == slug);
        if (idx < 0) return false;
        list.RemoveAt(idx);
        Save(list);
        return true;
    }

    public void Reorder(IList<string> slugs)
    {
        var list = All();
        var bySlug = list.ToDictionary(x => x.Slug);
        var ordered = new List<CaseStudy>();
        foreach (var s in slugs)
            if (bySlug.TryGetValue(s, out var c)) ordered.Add(c);
        foreach (var c in list)
            if (!slugs.Contains(c.Slug)) ordered.Add(c);
        Save(ordered);
    }
}
