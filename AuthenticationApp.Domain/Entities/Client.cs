namespace AuthenticationApp.Domain.Entities;

public class Client
{
    public Guid Id { get; private set; }
    public string ClientId { get; private set; } = default!;
    public string ClientSecret { get; private set; } = default!;
    public bool IsActive { get; private set; } 

    private readonly List<ClientScope> _scopes = [];
    public IReadOnlyCollection<ClientScope> Scopes => _scopes;

    protected Client() { }

    public Client(string clientId, string clientSecret)
    {
        Id = Guid.NewGuid();
        ClientId = clientId;
        ClientSecret = clientSecret;
        IsActive = true;
    }

    public void AddScope(string scope)
    {
        if (_scopes.Any(s => s.Scope == scope))
            return;

        _scopes.Add(new ClientScope(Id, scope));
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
