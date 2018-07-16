using System;
using Declarations.Exceptions;

namespace Common
{
    public static class TokenProvider
    {
        private const string GitUserTokenEnvVariableName = "GIT_USER_TOKEN";
        private static string _token;
        public static string Token
        {
            get
            {
                if (!string.IsNullOrEmpty(_token))
                    return _token;
                _token = Environment.GetEnvironmentVariable(GitUserTokenEnvVariableName, EnvironmentVariableTarget.Machine);

                if(string.IsNullOrEmpty(_token))
                    throw new NoTokenException($"Token not found in environment variables. " +
                                                   $"Add {GitUserTokenEnvVariableName} environment variable in console:{Environment.NewLine}" +
                                                   $"setx {GitUserTokenEnvVariableName} \"<token>\"");

                return _token;
            }
	        set
	        {
	            Environment.SetEnvironmentVariable(GitUserTokenEnvVariableName, value, EnvironmentVariableTarget.Machine);
                _token = value; 
	        }
        }
    }
}
