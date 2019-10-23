# Azure Sphere CLI

## Login to Azure AD

CLI:

`azsphere login`

If you don't have a work or school account that you want to use with Azure Sphere, and you have no other account with Microsoft or Azure, you can create a new directory that has a new work/school account.

https://account.azure.com/organization


```
azsphere show-version
```


azsphere device show-attached

azsphere device show-ota-status
azsphere device recover --images <path to OS Recovery Images>

azsphere device wifi scan
azsphere device wifi add --ssid ?????? --key ???????
azsphere device wifi show-status
azsphere device wifi list 
azsphere device wifi enable 
azsphere device wifi disable 
azsphere device wifi delete

azsphere device sideload deploy --manualstart --imagepackage <imagepackagepath>

azsphere device sideload show-status
azsphere device sideload start --debug --componentid 4f1fd9fc-d412-41e9-b3e6-8a8570b9a750
azsphere device sideload stop --componentid 4f1fd9fc-d412-41e9-b3e6-8a8570b9a750
azsphere device sideload delete --componentid 4f1fd9fc-d412-41e9-b3e6-8a8570b9a750
azsphere device sideload delete

azsphere device sideload show-quota

## 1st build 

make
mkdir approot\bin
copy Blink.out approot\bin\app
copy app_manifest.json approot

azsphere image package-application --input "C:\src\github.com\ongzhixian\csi\Csi.AzureSphere\approot" --output manual.imagepackage --targetapiset "2" --verbose --hardwaredefinition "C:\src\github.com\azure\azure-sphere-samples\Hardware\mt3620_rdb\mt3620_rdb.json"


azsphere image package-application --input "C:\src\github.com\ongzhixian\csi\Csi.AzureSphere\approot" --output manual.imagepackage --targetapiset "2" --verbose --hardwaredefinition "C:\src\github.com\azure\azure-sphere-samples\Hardware\mt3620_rdb\sample_hardware.json"

azsphere device prep-debug
azsphere device sideload deploy --imagepackage manual.imagepackage

## Subsequent builds

make
copy Blink.out approot\bin\app

azsphere device sideload deploy --imagepackage manual.imagepackage
