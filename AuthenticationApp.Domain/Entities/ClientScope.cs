
namespace AuthenticationApp.Domain.Entities;

public class ClientScope
{
    public Guid Id { get; private set; }
    public Guid ClientId { get; private set; } = default!;
    public string Scope { get; private set; } = default!;

    protected ClientScope() { }

    public ClientScope(Guid clientId, string scope)
    {
        Id = Guid.NewGuid();
        ClientId = clientId;
        Scope = scope;
    }
}
