using Creida.Data;

namespace Creida.Services;

/// <summary>
/// Düzenlenebilir ekip listesi. Backing store: Data/Content/team.json
/// JSON dosyası yoksa Data/TeamMembers.cs içindeki statik veriden seed eder.
/// Bu sayede mevcut site ilk açılışta hiç boş ekip listesi göstermez.
/// </summary>
public class TeamService
{
    private const string Key = "team";
    private readonly ContentStore _store;

    public TeamService(ContentStore store)
    {
        _store = store;
        // İlk açılışta seed
        if (!_store.Exists(Key))
        {
            _store.Write(Key, TeamMembers.All.ToList());
        }
    }

    public List<TeamMember> All()
    {
        return _store.ReadOr(Key, () => TeamMembers.All.ToList());
    }

    public TeamMember? BySlug(string slug)
    {
        return All().FirstOrDefault(t =>
            string.Equals(t.Slug, slug, StringComparison.OrdinalIgnoreCase));
    }

    public void Save(List<TeamMember> list)
    {
        _store.Write(Key, list);
    }

    public TeamMember Upsert(TeamMember m)
    {
        var list = All();
        var idx = list.FindIndex(x => x.Slug == m.Slug);
        if (idx >= 0) list[idx] = m;
        else list.Add(m);
        Save(list);
        return m;
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
        var reordered = new List<TeamMember>();
        foreach (var s in slugs)
            if (bySlug.TryGetValue(s, out var m)) reordered.Add(m);
        // Listede kalan ama yeni sırada olmayanları sona ekle (defansif)
        foreach (var m in list)
            if (!slugs.Contains(m.Slug)) reordered.Add(m);
        Save(reordered);
    }
}
