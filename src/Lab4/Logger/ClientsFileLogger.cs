internal class ClientsFileLogger : ILogger<FieldChangeInfo>
{
    private readonly ChangesService _logService;
    public ClientsFileLogger(string filename) => _logService = new ChangesService(new ClientChangesFileTable(filename));

    public void Log(FieldChangeInfo logInfo) => _logService.Add(logInfo);
}
