# Klip App2App SDK SDK for Unity3D
 [![DependencyBadge](https://img.shields.io/badge/dependency-newtonsoft.json%3A2.0.0-brightgreen)](https://github.com/JamesNK/Newtonsoft.Json)


모바일 네이티브(Android, iOS)만 지원하는 SDK를 Unity3D에서 사용하기위해 제작하였습니다. 
오픈소스로 작성된 GroundX의 [KlipA2ASDK]를 참고하여 최대한 Unity3D 자체에서 지원하는 기능으로만 개발하였습니다.

- Klip A2A SDK에서 지원하는 모든 기능들을 이용가능합니다.
- ✨단 한번의 클릭으로 클립 지갑과 연결하는 데모를 제공합니다.✨
## Unity SDK
- iOS(info.plist)나 Android(AndroidManifest,xml)에서 필요한 기본 세팅을 지원합니다.  
- Unity3D 자체 기능만 사용하다보니 카카오톡 설치여부나 클립 지원 여부는 데모 5번을 참고하셔서 개발하시면 됩니다.


## 설치
[KlipA2AUnity_0.0.1.unitypackage Download](https://github.com/lancekun/Klip-A2A-SDK-Unity/releases/download/0.0.1/KlipA2AUnity_0.0.1.unitypackage)
KlipA2AUnity_0.0.1.unitypackage 를 다운받아서 import하면 바로 사용가능합니다.
데모가 필요 없는 분들은 A2A-SDK폴더의 Demo만 제외하고 Import하시면 됩니다.


## Klip A2A 문서 참조
[KlipA2ASDK]

## 데모
- Prepare / Request / Result 를 순차적으로 실행해보면서 A2A SDK 과정을 이해해볼수있습니다.
- 한번의 클릭으로 클립 지갑과 연결하는 예제
- 플랫폼 서포트를 체크하는 예제
- ...클레이튼,토큰 전송이나 카드 전송 예제는 Klip Partners만 가능해서 제작은 안했으나, SDK를 이용해 충분히 구현이 가능합니다.
[Demo APK Download](https://github.com/lancekun/Klip-A2A-SDK-Unity/releases/download/0.0.1/KlipA2AUnityDemo_0.0.1.apk)
![klipa2ademo_sample](https://user-images.githubusercontent.com/10954717/147196051-844fe365-ecb3-4a09-a281-8b9e043d7865.png)

## 딥링크 설정 방법
**호출시 규격**

```c#
[SerializeField] private string AppCallbackSuccessUri = "klipdemo://request?success";
[SerializeField] private string AppCallbackFailUri    = "klipdemo://request?fail";
```
데모를 확인하시면 위와 같이 Uri가 설정되어있는것을 볼수있습니다. 
이는 Request과정에서 Klip결과를 받아와서 다시 앱으로 복귀하기위해 Klip에 전달하는 값입니다.

**안드로이드**
Assets/KlipSDK/A2A-SDK/Editor/KlipA2AAndroidManifestModifier.cs 파일에 아래 코드로 scheme 과 host를 추가해 줄수있습니다.

```c#
List<KlipSchemeData> schemeDatas = new List<KlipSchemeData>();
schemeDatas.Add(new KlipSchemeData("klipdemo", "request"));
        
androidManifest.SetDeepLinkScheme(schemeDatas);
```

iOS의 경우에는 프로젝트 세팅에서 설정이 가능합니다.
관련된 더 자세한 내용은 [유니티 공식 문서 Enabling deep linking](https://docs.unity3d.com/Manual/enabling-deep-linking.html)을 참고하세요.



## 관련 프로젝트

| Plugin |
| ------ | 
| [KlipA2ASDK] |
| [NewtonSoft Json](https://github.com/JamesNK/Newtonsoft.Json)|


## License
MIT

[//]: # 
   [KlipA2ASDK]:<https://docs.klipwallet.com/>
