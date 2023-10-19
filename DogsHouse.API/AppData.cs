namespace DogsHouse.Application
{
    public static class AppData
    {
        /// <summary>
        /// Gets a value indicating whether the application is running in a Docker container.
        /// </summary>
        public static bool InDocker => Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
    }
}
