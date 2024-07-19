internal class ClientsFileLogger : ILogger<FieldChangeInfo>
{
    private readonly UserChangesService _logService;
    public ClientsFileLogger(string filename) => _logService = new UserChangesService(new ClientChangesFileTable(filename));

    public void Log(FieldChangeInfo logInfo) => _logService.Add(logInfo);
}
