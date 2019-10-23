The SDK is installed in C:\Program Files(x86)\Microsoft Azure Sphere SDK.
%ProgramData%\Microsoft\Azure Sphere\TapDriverInstaller\TapDriverInstaller.ps1" Install



C:\Apps\AzureSphere\Tools>azsphere tenant create --name telara
Created a new Azure Sphere tenant:
 --> Tenant Name: telara
 --> Tenant ID:   54a0eb8d-9e58-4ead-be3a-e35c68719de6
Selected Azure Sphere tenant 'telara' as the default.
You may now wish to claim the attached device into this tenant using 'azsphere device claim'.
Command completed successfully in 00:00:38.1083946.

C:\Apps\AzureSphere\Tools>azsphere login
The selected Azure Sphere tenant 'telara' (54a0eb8d-9e58-4ead-be3a-e35c68719de6) will be retained.
Successfully logged in with the selected AAD user. This authentication will be used for subsequent commands.
Command completed successfully in 00:00:18.8170129.

C:\Apps\AzureSphere\Tools>azsphere device claim
Claiming device.
Successfully claimed device ID 'BA467E11A391F36BF95C2AB3CDFE95ABB05D48DB649F14FE6535FD87D8224A3BE4C4C2DE0F7A949DC97D8AAD98007A08A4DC23171F6FDA89359B74A63E4BBFDB' into tenant 'telara' with ID '54a0eb8d-9e58-4ead-be3a-e35c68719de6'.
Command completed successfully in 00:00:07.2884458.


C:\Apps\AzureSphere\Tools>azsphere device wifi show-status
SSID                :
Configuration state : unknown
Connection state    : disconnected
Security state      : unknown
Frequency           :
Mode                :
Key management      : UNKNOWN
WPA State           : DISCONNECTED
IP Address          :
MAC Address         : 00:02:b5:03:56:6c

Command completed successfully in 00:00:03.1804104.



`azsphere device wifi scan`



azsphere device wifi add --ssid SINGTEL-DAC1 --psk Ion54cx30

C:\Apps\AzureSphere\Tools>azsphere device wifi add --ssid SINGTEL-DAC1 --psk Ion54cx30
Add network succeeded:
ID                  : 0
SSID                : SINGTEL-DAC1
Configuration state : enabled
Connection state    : unknown
Security state      : psk
Targeted scan       : False

Command completed successfully in 00:00:02.5968303.


C:\Apps\AzureSphere\Tools>azsphere device wifi show-status
SSID                : SINGTEL-DAC1
Configuration state : enabled
Connection state    : connected
Security state      : psk
Frequency           : 2432
Mode                : station
Key management      : WPA2-PSK
WPA State           : COMPLETED
IP Address          : 192.168.1.141
MAC Address         : 00:02:b5:03:56:6c

Command completed successfully in 00:00:02.2474872.


azsphere device show-ota-status

C:\Apps\AzureSphere\Tools>azsphere device show-ota-status
Your device is running Azure Sphere OS version 19.05.
The Azure Sphere Security Service is targeting this device with Azure Sphere OS version 19.09.
warn: Your device is running an older Azure Sphere OS version (19.05). It has not yet started receiving the available update to version 19.09.
warn: Your device is connected to Wi-Fi. If the over-the-air update does not begin, reset your device and try again.
Go to aka.ms/AzureSphereUpgradeGuidance for further advice and support.
Command completed successfully in 00:00:06.2210006.

... a few minutes later

C:\Apps\AzureSphere\Tools>azsphere device show-ota-status
Your device is running Azure Sphere OS version 19.09.
The Azure Sphere Security Service is targeting this device with Azure Sphere OS version 19.09.
Your device has the expected version of the Azure Sphere OS: 19.09.
Command completed successfully in 00:00:07.6892913.

C:\Apps\AzureSphere\Tools>

To subscribe to Azure Updates through the RSS feed:
https://azurecomcdn.azureedge.net/en-us/updates/feed/?product=azure-sphere

