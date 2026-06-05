# Keepass2Android (fork)

Fork of [Keepass2Android](https://github.com/PhilippC/keepass2android) with a small UX addition for sharing field values to other Android apps.

Upstream docs: [docs/README.md](docs/README.md)

## Changes in this fork

### Share field text (system share sheet)

When viewing a password entry, each field popup menu (⋮) now includes **Share** / **Поделиться** next to **Copy to clipboard**.

- Sends the **current field value only** as plain text (`ACTION_SEND`, `text/plain`)
- Uses the standard Android share sheet — any app can receive the text (messengers, notes, Bluetooth HID keyboard apps, etc.)
- No custom intents and no hardcoded target package

Implementation:

- `src/keepass2android-app/EntryActivityClasses/ShareTextPopupMenuItem.cs`
- wired in `EntryActivity.RegisterTextPopup()`

### Build fixes for local Windows / `adb install`

- `EmbedAssembliesIntoApk=true` and `AndroidFastDeploymentType=None` in `keepass2android-app.csproj`  
  (Debug builds without this crash on startup with *Fast Deployment* when installed via `adb`)
- Gradle: androidx lifecycle `resolutionStrategy` for removed Maven artifacts
- Aligned AGP / Gradle wrapper / NDK version for local toolchain

## Build (Windows, device install)

Requirements: .NET 9 SDK + Android workload, Android SDK/NDK, JDK 17+, Git submodules.

```powershell
git submodule update --init --recursive

$env:ANDROID_SDK_ROOT = "C:\android\sdk"
$env:ANDROID_HOME     = "C:\android\sdk"
$env:ANDROID_NDK_ROOT = "C:\android\sdk\ndk\27.0.12077973"

# native + java deps (see Makefile or build-scripts/)
# then publish Release APK:

dotnet publish src\keepass2android-app\keepass2android-app.csproj `
  -p:AndroidSdkDirectory="$env:ANDROID_SDK_ROOT" `
  -t:SignAndroidPackage `
  -p:Flavor=Net `
  -p:Configuration=Release `
  -p:Platform=AnyCPU

# APK:
# src\keepass2android-app\bin\Release\net9.0-android\publish\keepass2android.keepass2android-Signed.apk

adb install -r src\keepass2android-app\bin\Release\net9.0-android\publish\keepass2android.keepass2android-Signed.apk
```

Use **Release** (or Debug with embedded assemblies) for `adb install`. Do not use Visual Studio fast-deploy-only Debug packages on a phone without VS attached.

## License

Same as upstream: GPLv3. See upstream repository and `docs/README.md`.
