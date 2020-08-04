mono bin/Debug/netcoreapp3.1/EmailSync.dll $(jq -r ".profiles.EmailSync.commandLineArgs" Properties/launchSettings.json)
